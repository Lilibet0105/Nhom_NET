using _.Net____PCCV;
using _Net____PCCV.DAL;
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
                // 2. Tạo câu lệnh truy vấn so khớp chuỗi thô trực tiếp từ Database của anh
                string query = "SELECT VaiTro, TrangThai FROM TaiKhoan WHERE TenDangNhap = @User AND MatKhau = @Pass";

                // 3. Gán giá trị từ 2 ô TextBox vào tham số Parameter để truyền xuống SQL Server
                SqlParameter[] parameters = new SqlParameter[]
                {
            new SqlParameter("@User", txtUsername.Text.Trim()),
            new SqlParameter("@Pass", txtPassword.Text.Trim()) // Đọc chuỗi thô (ví dụ: 123456)
                };

                // 4. Thực thi câu lệnh thông qua lớp kết nối DAL của nhóm anh
                DataTable resultTable = DataConnection.ExecuteQuery(query, parameters);

                // 5. Kiểm tra kết quả trả về từ SQL Server
                if (resultTable != null && resultTable.Rows.Count > 0)
                {
                    UserSession.Username = txtUsername.Text.Trim();
                    UserSession.Role = resultTable.Rows[0]["VaiTro"].ToString();
                    string vaiTro = resultTable.Rows[0]["VaiTro"].ToString();
                    string trangThai = resultTable.Rows[0]["TrangThai"].ToString();

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
                // Bắt lỗi nếu chuỗi kết nối Database Connection có vấn đề
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
    }
}
