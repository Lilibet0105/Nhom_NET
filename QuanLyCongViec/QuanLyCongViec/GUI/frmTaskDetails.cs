using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using QuanLyCongViec.BUS;
using QuanLyCongViec.DTO;

namespace GUI
{
    public partial class frmTaskDetails : Form
    {
        private readonly TaskBUS taskBUS = new TaskBUS();
        private readonly int maTask;
        private TaskDTO currentTask;

        public frmTaskDetails(int maTask)
        {
            InitializeComponent();
            this.maTask = maTask;
            Load += frmTaskDetails_Load;
        }

        private void frmTaskDetails_Load(object sender, EventArgs e)
        {
            LoadNhanVien();
            LoadThongTinCongViec();
            LoadBinhLuan();
        }

        private void LoadNhanVien()
        {
            DataTable nhanVien = taskBUS.LayDanhSachNhanVien();
            cboNguoiBinhLuan.DisplayMember = "HoTen";
            cboNguoiBinhLuan.ValueMember = "MaNV";
            cboNguoiBinhLuan.DataSource = nhanVien;
        }

        private void LoadThongTinCongViec()
        {
            currentTask = taskBUS.LayCongViecTheoMa(maTask);
            if (currentTask == null)
            {
                MessageBox.Show("Không tìm thấy công việc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Close();
                return;
            }

            lblTieuDe.Text = currentTask.TenTask;
            lblMaTask.Text = $"Mã công việc: {currentTask.MaTask}";
            lblDuAn.Text = $"Dự án: {currentTask.TenDA}";
            lblNguoiThucHien.Text = $"Người thực hiện: {currentTask.TenNguoiNhan}";
            lblTrangThai.Text = $"Trạng thái: {currentTask.TrangThai}";
            lblUuTien.Text = $"Ưu tiên: {currentTask.MucDoUuTien}";
            lblNgayTao.Text = $"Ngày bắt đầu: {currentTask.NgayTao:dd/MM/yyyy HH:mm}";
            lblDeadline.Text = $"Hạn hoàn thành: {currentTask.Deadline:dd/MM/yyyy HH:mm}";
            lblTinhTrang.Text = $"Tình trạng: {currentTask.TinhTrang}";
            txtMoTa.Text = currentTask.MoTa;
            progressTienDo.Value = currentTask.TienDo;
            lblTienDo.Text = $"Tiến độ: {currentTask.TienDo}%";
        }

        private void LoadBinhLuan()
        {
            dgvBinhLuan.AutoGenerateColumns = false;
            dgvBinhLuan.DataSource = taskBUS.LayBinhLuanTheoCongViec(maTask);
        }

        private void btnThemBinhLuan_Click(object sender, EventArgs e)
        {
            try
            {
                TaskCommentDTO comment = new TaskCommentDTO
                {
                    MaTask = maTask,
                    MaNV = cboNguoiBinhLuan.SelectedValue == null ? 0 : Convert.ToInt32(cboNguoiBinhLuan.SelectedValue),
                    NoiDung = txtBinhLuan.Text.Trim()
                };

                if (taskBUS.ThemBinhLuan(comment))
                {
                    txtBinhLuan.Clear();
                    LoadBinhLuan();
                    MessageBox.Show("Thêm bình luận thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi bình luận", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSuaCongViec_Click(object sender, EventArgs e)
        {
            using (frmTaskActions frmSua = new frmTaskActions(maTask))
            {
                if (frmSua.ShowDialog(this) == DialogResult.OK)
                {
                    if (taskBUS.LayCongViecTheoMa(maTask) == null)
                    {
                        DialogResult = DialogResult.OK;
                        Close();
                        return;
                    }

                    LoadThongTinCongViec();
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
