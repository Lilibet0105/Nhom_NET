using System;
using System.Windows.Forms;
using BUS;
using DTO;

namespace GUI
{
    public partial class frmRegister : Form
    {
        private NguoiDungBUS _userBUS = new NguoiDungBUS();

        public frmRegister()
        {
            InitializeComponent();

            txtEmail.PasswordChar = '\0';

            if (cboRole.Items.Count > 0)
            {
                cboRole.SelectedIndex = 0;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text) ||
        string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtFullName.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tất cả các trường thông tin bắt buộc!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            NguoiDungDTO newUser = new NguoiDungDTO()
            {
                TenDangNhap = txtUsername.Text.Trim(),
                MatKhau = txtPassword.Text,
                HoTen = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                ChucVu = "Staff", // Đồng bộ cứng chức vụ theo bảng TaiKhoan SQL
            };

            string error;

            if (_userBUS.Register(newUser, out error))
            {
                MessageBox.Show("Đăng ký tài khoản thành công!\nVui lòng chờ Quản trị viên phê duyệt tại giao diện quản lý.",
                                "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(error, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}