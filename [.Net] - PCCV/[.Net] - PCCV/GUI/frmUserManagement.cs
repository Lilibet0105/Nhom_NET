using System;
using System.Data;
using System.Windows.Forms;
using _Net____PCCV.BUS;
using _Net____PCCV.DTO;

namespace GUI
{
    public partial class frmUserManagement : Form
    {
        private TaiKhoanBUS tkBUS = new TaiKhoanBUS();

        public frmUserManagement()
        {
            InitializeComponent();
        }

        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            LoadDanhSachTaiKhoan();
            CaiDatGiaoDienODoiChinh();
        }

        private void CaiDatGiaoDienODoiChinh()
        {
            txtUsername.ReadOnly = true; // Không cho sửa Username (Khóa chính)

            // Cài đặt danh sách lựa chọn cho Quyền hạn
            cboVaiTro.Items.Clear();
            cboVaiTro.Items.Add("Admin");
            cboVaiTro.Items.Add("Staff");
            cboVaiTro.SelectedIndex = 1; // Mặc định chọn Staff

            // Cài đặt danh sách lựa chọn cho Trạng thái kích hoạt tài khoản
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Kích hoạt");
            cboTrangThai.Items.Add("Tạm khóa");
            cboTrangThai.SelectedIndex = 0; // Mặc định chọn Kích hoạt
        }

        private void LoadDanhSachTaiKhoan()
        {
            try
            {
                DataTable dt = tkBUS.LayDanhSachTaiKhoan();
                dgvUsers.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách tài khoản: " + ex.Message, "Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sự kiện xảy ra khi người dùng chọn một dòng tài khoản trên GridView
        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow != null)
            {
                DataGridViewRow row = dgvUsers.CurrentRow;
                txtUsername.Text = row.Cells["Username"].Value?.ToString() ?? "";

                // Đồng bộ Vai trò lên ComboBox
                string vaiTro = row.Cells["VaiTro"].Value?.ToString() ?? "Staff";
                cboVaiTro.SelectedItem = vaiTro;

                // Đồng bộ trạng thái kích hoạt lên ComboBox
                string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "Kích hoạt";
                cboTrangThai.SelectedItem = trangThai;
            }
        }

        // ==================== LẬP TRÌNH NÚT CẬP NHẬT TRẠNG THÁI & VAI TRÒ ====================
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản cần thay đổi thông tin cấu hình!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Đóng gói dữ liệu thay đổi vào DTO
                TaiKhoanDTO tk = new TaiKhoanDTO
                {
                    Username = txtUsername.Text.Trim(),
                    VaiTro = cboVaiTro.SelectedItem.ToString(),
                    TrangThai = cboTrangThai.SelectedItem.ToString()
                };

                // Đẩy xuống tầng BUS để xử lý cập nhật cơ sở dữ liệu
                bool check = tkBUS.CapNhatQuyenVaTrangThai(tk);

                if (check)
                {
                    MessageBox.Show($"Cập nhật phân quyền tài khoản [{tk.Username}] thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachTaiKhoan(); // Refresh lại bảng hiển thị dữ liệu mới
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại, vui lòng kiểm tra lại kết nối!", "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Xảy ra lỗi: " + ex.Message, "Thông Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
} 