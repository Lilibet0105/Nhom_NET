using _.Net____PCCV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmMainDashboard : Form
    {
        public frmMainDashboard()
        {
            InitializeComponent();
            OpenChildForm(new frmKanbanBoard());
        }
        // Biến dùng để lưu Form con đang hiển thị hiện tại
        private Form activeForm = null;

        private void OpenChildForm(Form childForm)
        {
            if (activeForm != null && activeForm.GetType() == childForm.GetType())
            {
                childForm.Dispose();
                return;
            }

            pnlContent.SuspendLayout();

            // Nếu đang có Form con mở sẵn thì đóng nó lại để giải phóng bộ nhớ
            if (activeForm != null)
                activeForm.Close();

            // Gán Form mới vào biến activeForm
            activeForm = childForm;

            // Cấu hình để Form con có thể nhét vừa vào Panel
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            // Thêm Form con vào pnlContent và hiển thị
            pnlContent.Controls.Clear(); // Xóa sạch giao diện cũ trong panel
            pnlContent.Controls.Add(childForm);
            pnlContent.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            pnlContent.ResumeLayout();
        }

        private void btnKanban_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmKanbanBoard());
        }

        private void btnTeam_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmTeamManager());
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            OpenChildForm(new _.Net____PCCV.frmCalendarView());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            OpenChildForm(new _.Net____PCCV.frmPerformanceDashboard());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMainDashboard_Load(object sender, EventArgs e)
        {
            if (UserSession.Role == "Staff")
            {
                // Nếu là Nhân viên thông thường (Staff): Ẩn nút Quản lý nhóm theo yêu cầu đồ án
                btnTeam.Visible = false;

                // (Tùy chọn thêm): Nếu muốn khóa thay vì ẩn, anh có thể dùng: btnTeam.Enabled = false;

                MessageBox.Show($"Đăng nhập với tư cách Nhân viên: Nút 'Quản lý nhóm' đã được ẩn bảo mật.", "Phân quyền hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (UserSession.Role == "Manager" || UserSession.Role == "Admin")
            {
                // Nếu là Quản trị viên/Trưởng nhóm: Hiển thị đầy đủ tính năng quản trị
                btnTeam.Visible = true;
            }
        }
    }
}
