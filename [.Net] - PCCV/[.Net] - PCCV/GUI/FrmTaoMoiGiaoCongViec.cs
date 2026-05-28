using System;
using System.Data;
using System.Windows.Forms;
using _Net____PCCV.BUS;
using _Net____PCCV.DTO;

namespace GUI
{
    public partial class FrmTaoMoiGiaoCongViec : Form
    {
        private readonly TaskBUS taskBUS = new TaskBUS();
        private readonly int maTask;
        private bool isLoading;

        public FrmTaoMoiGiaoCongViec()
        {
            InitializeComponent();
            maTask = 0;
            Load += FrmTaoMoiGiaoCongViec_Load;
        }

        public FrmTaoMoiGiaoCongViec(int maTask)
        {
            InitializeComponent();
            this.maTask = maTask;
            Load += FrmTaoMoiGiaoCongViec_Load;
        }

        private void FrmTaoMoiGiaoCongViec_Load(object sender, EventArgs e)
        {
            isLoading = true;
            CaiDatMacDinh();
            LoadDuAn();
            LoadMucDoVaTrangThai();
            isLoading = false;
            LoadThanhVienTheoDuAn();

            if (maTask > 0)
            {
                LoadCongViecCanSua();
            }
        }

        private void CaiDatMacDinh()
        {
            Text = maTask > 0 ? "Cập nhật công việc" : "Tạo mới công việc";
            lblTieuDeForm.Text = maTask > 0 ? "CẬP NHẬT CÔNG VIỆC" : "TẠO MỚI CÔNG VIỆC";
            txtMaCongViec.ReadOnly = true;
            txtMaCongViec.Text = maTask > 0 ? maTask.ToString() : "Tự động";
            dtpNgayBatDau.Value = DateTime.Now;
            dtpHanXuLy.Value = DateTime.Now.Date.AddDays(1);
        }

        private void LoadDuAn()
        {
            DataTable duAn = taskBUS.LayDanhSachDuAn();
            cboDuAn.DisplayMember = "TenDA";
            cboDuAn.ValueMember = "MaDA";
            cboDuAn.DataSource = duAn;

            if (cboDuAn.Items.Count > 0)
            {
                cboDuAn.SelectedIndex = 0;
            }
        }

        private void LoadThanhVienTheoDuAn()
        {
            if (cboDuAn.SelectedValue == null || !int.TryParse(cboDuAn.SelectedValue.ToString(), out int maDA))
            {
                cboNguoiNhan.DataSource = null;
                return;
            }

            DataTable thanhVien = taskBUS.LayThanhVienTheoDuAn(maDA);
            cboNguoiNhan.DisplayMember = "HoTen";
            cboNguoiNhan.ValueMember = "MaNV";
            cboNguoiNhan.DataSource = thanhVien;
        }

        private void LoadMucDoVaTrangThai()
        {
            cboMucUuTien.Items.Clear();
            cboMucUuTien.Items.Add("Thấp");
            cboMucUuTien.Items.Add("Trung bình");
            cboMucUuTien.Items.Add("Cao");
            cboMucUuTien.SelectedItem = "Trung bình";

            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("To Do");
            cboTrangThai.Items.Add("In Progress");
            cboTrangThai.Items.Add("Done");
            cboTrangThai.SelectedItem = "To Do";
        }

        private void LoadCongViecCanSua()
        {
            TaskDTO task = taskBUS.LayCongViecTheoMa(maTask);
            if (task == null)
            {
                MessageBox.Show("Không tìm thấy công việc cần cập nhật!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult = DialogResult.Cancel;
                Close();
                return;
            }

            txtMaCongViec.Text = task.MaTask.ToString();
            txtTieuDe.Text = task.TenTask;
            txtMoTa.Text = task.MoTa;
            dtpNgayBatDau.Value = task.NgayTao;
            dtpHanXuLy.Value = task.Deadline;
            cboMucUuTien.SelectedItem = task.MucDoUuTien;
            cboTrangThai.SelectedItem = task.TrangThai;
            cboDuAn.SelectedValue = task.MaDA;
            LoadThanhVienTheoDuAn();
            cboNguoiNhan.SelectedValue = task.MaNguoiNhan;
        }

        private TaskDTO LayDuLieuTuForm()
        {
            return new TaskDTO
            {
                MaTask = maTask,
                TenTask = txtTieuDe.Text.Trim(),
                MoTa = txtMoTa.Text.Trim(),
                MucDoUuTien = cboMucUuTien.SelectedItem?.ToString(),
                TrangThai = cboTrangThai.SelectedItem?.ToString(),
                NgayTao = dtpNgayBatDau.Value,
                Deadline = dtpHanXuLy.Value,
                MaDA = cboDuAn.SelectedValue == null ? 0 : Convert.ToInt32(cboDuAn.SelectedValue),
                MaNguoiNhan = cboNguoiNhan.SelectedValue == null ? 0 : Convert.ToInt32(cboNguoiNhan.SelectedValue)
            };
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                TaskDTO task = LayDuLieuTuForm();
                if (taskBUS.LuuCongViec(task))
                {
                    MessageBox.Show(maTask > 0 ? "Cập nhật công việc thành công!" : "Thêm công việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cboDuAn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                LoadThanhVienTheoDuAn();
            }
        }
    }
}
