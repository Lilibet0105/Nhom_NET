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

            if (cboRole.Items.Count > 0)
            {
                cboRole.SelectedIndex = 0;
            }
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            NguoiDungDTO newUser = new NguoiDungDTO()
            {
                TenDangNhap = txtUsername.Text.Trim(),
                MatKhau = txtPassword.Text,
                HoTen = txtFullName.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                ChucVu = cboRole.SelectedItem != null ? cboRole.SelectedItem.ToString() : "Nhân viên"
            };

            string error;

            if (_userBUS.Register(newUser, out error))
            {
                MessageBox.Show("Đăng ký tài khoản thành công!\nVui lòng chờ Quản trị viên phê duyệt.",
                                "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.Close();
            }
            else
            {
                MessageBox.Show(error, "Lỗi Nhập Liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}