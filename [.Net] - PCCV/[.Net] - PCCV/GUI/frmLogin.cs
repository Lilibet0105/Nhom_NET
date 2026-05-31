using System;
using System.Windows.Forms;
using BUS;

namespace GUI
{
    public partial class frmLogin : Form
    {
        private NguoiDungBUS _userBUS = new NguoiDungBUS();

        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string error;
            if (_userBUS.Login(txtUsername.Text.Trim(), txtPassword.Text, out error))
            {
                this.Hide();
                using (frmMainDashboard mainForm = new frmMainDashboard())
                {
                    mainForm.ShowDialog(this);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show(error, "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void btnExit_Click(object sender, EventArgs e) { this.Close(); }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();

            using (frmRegister registerForm = new frmRegister())
            {
                registerForm.ShowDialog();
            }
            this.Show();
        }
    }
}