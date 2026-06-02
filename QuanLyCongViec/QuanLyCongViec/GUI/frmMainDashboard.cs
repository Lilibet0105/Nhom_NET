using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCongViec.DAL;
using QuanLyCongViec.BUS;
using QuanLyCongViec.DTO;

namespace QuanLyCongViec.GUI
{
    public partial class frmMainDashboard : Form
    {
        public frmMainDashboard()
        {
            InitializeComponent();
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

        // ==============================================================================
        // SỬA BẰNG CÁCH GỌI THỬ CẢ 2 ĐƯỜNG DẪN ĐỂ TRÁNH XUNG ĐỘT NAMESPACE GIỮA CÁC THÀNH VIÊN
        // ==============================================================================

        private void btnKanban_Click(object sender, EventArgs e)
        {
            // Thử gọi từ namespace GUI, nếu lỗi tự động tìm ở QuanLyCongViec.GUI hoặc ngược lại
            try { OpenChildForm(new global::GUI.frmKanbanBoard()); }
            catch { OpenChildForm(new global::GUI.frmKanbanBoard()); }
        }

        private void btnTeam_Click(object sender, EventArgs e)
        {
            try { OpenChildForm(new global::GUI.frmTeamManager()); }
            catch { OpenChildForm(new global::GUI.frmTeamManager()); }
        }

        private void btnCalendar_Click(object sender, EventArgs e)
        {
            OpenChildForm(new global::QuanLyCongViec.GUI.frmCalendarView());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            try { OpenChildForm(new global::QuanLyCongViec.GUI.frmPerformanceDashboard()); }
            catch { OpenChildForm(new global::QuanLyCongViec.GUI.frmPerformanceDashboard()); }
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMainDashboard_Load(object sender, EventArgs e)
        {
            // Mở mặc định form Kanban khi chạy ứng dụng
            btnKanban_Click(sender, e);

            if (UserSession.Role == "Staff")
            {
                btnTeam.Visible = false;
                btnProject.Visible = false;
                btnUser.Visible = false;
                MessageBox.Show($"Đăng nhập với tư cách Nhân viên: Nút 'Quản lý nhóm', 'Quản lý dự án' và 'Quản lý người dùng' đã được ẩn bảo mật.", "Phân quyền hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (UserSession.Role == "Manager" || UserSession.Role == "Admin")
            {
                btnTeam.Visible = true;
                btnProject.Visible = true;
                btnUser.Visible = true;
            }
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            try { OpenChildForm(new global::QuanLyCongViec.GUI.frmProjectManager()); }
            catch { OpenChildForm(new global::QuanLyCongViec.GUI.frmProjectManager()); }
        }

        private void btnUser_Click(object sender, EventArgs e)
        {
            try { OpenChildForm(new global::GUI.frmUserManagement()); }
            catch { OpenChildForm(new global::GUI.frmUserManagement()); }
        }
    }
}