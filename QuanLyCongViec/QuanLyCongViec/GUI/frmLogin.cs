using _.Net____PCCV;
using QuanLyCongViec.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // 1. Kiểm tra người dùng không được bỏ trống thông tin nhập vào
            if (string.IsNullOrEmpty(txtUsername.Text.Trim()) || string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ Tài khoản và Mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Khởi tạo lớp BUS để mượn hàm mã hóa SHA256 đồng bộ với lúc Đăng ký
                NguoiDungBUS userBUS = new NguoiDungBUS();
                string hashedPass = userBUS.ComputeSHA256(txtPassword.Text.Trim());

                // 2. Tạo câu lệnh truy vấn so khớp chuỗi đã mã hóa trong Database
                string query = "SELECT VaiTro, TrangThai FROM TaiKhoan WHERE TenDangNhap = @User AND MatKhau = @Pass";

                // 3. Gán giá trị vào tham số Parameter để truyền xuống SQL Server
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@User", txtUsername.Text.Trim()),
            new SqlParameter("@Pass", hashedPass) // ĐÃ SỬA: Truyền mật khẩu ĐÃ MÃ HÓA SHA256 thay vì chuỗi thô
                };

                // 4. Thực thi câu lệnh thông qua lớp kết nối DAL
                DataTable resultTable = DataConnection.ExecuteQuery(query, parameters);

                // 5. Kiểm tra kết quả trả về từ SQL Server
                if (resultTable != null && resultTable.Rows.Count > 0)
                {
                    string vaiTro = resultTable.Rows[0]["VaiTro"].ToString();
                    string trangThai = resultTable.Rows[0]["TrangThai"].ToString();

                    // Kiểm tra điều kiện phê duyệt tài khoản của hệ thống
                    if (trangThai != "Hoạt động")
                    {
                        MessageBox.Show($"Tài khoản của bạn đang ở trạng thái '{trangThai}' và chưa được phép đăng nhập vào hệ thống!",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return; // Chặn lại không cho vào MainDashboard
                    }

                    // Nếu tài khoản hợp lệ và đã hoạt động:
                    UserSession.Username = txtUsername.Text.Trim();
                    UserSession.Role = vaiTro;

                    // Hiển thị thông báo chào mừng kèm theo quyền hạn
                    MessageBox.Show($"Đăng nhập thành công! Quyền hạn: {vaiTro}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 6. Chuyển tiếp sang giao diện frmMainDashboard
                    this.Hide();
                    using (frmMainDashboard mainForm = new frmMainDashboard())
                    {
                        mainForm.ShowDialog(this);
                    }
                    this.Close();
                }
                else
                {
                    // Nếu không tìm thấy dòng nào trùng khớp trong bảng TaiKhoan
                    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Bắt lỗi nếu chuỗi kết nối Database Connection hoặc câu lệnh SQL có vấn đề
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            // 1. Ẩn form Đăng nhập hiện tại đi (hoặc dùng .Close() nếu form Login không phải form chính)
            this.Hide();

            // 2. Khởi tạo Form Đăng ký (Thay 'frmRegister' bằng tên chuẩn Form đăng ký trong dự án của bạn)
            frmRegister fRegister = new frmRegister();

            // 3. Hiển thị Form đăng ký lên màn hình
            fRegister.ShowDialog();

            // 4. Sau khi người dùng tắt Form đăng ký hoặc đăng ký xong quay lại, hiển thị lại Form đăng nhập
            this.Show();
        }
    }
}
