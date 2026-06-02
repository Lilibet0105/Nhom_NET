using QuanLyCongViec.BUS;
using QuanLyCongViec.DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmTeamManager : Form
    {
        private ThanhVienBUS tvBUS = new ThanhVienBUS();
        private bool isSuaMode = false;

        public frmTeamManager()
        {
            InitializeComponent();
        }

        private void frmTeamManager_Load(object sender, EventArgs e)
        {
            LoadDanhSachThanhVien();
            CaiDatDataGridView();
            CaiDatTrangThaiMacDinh();
            NapDanhSachChucVu();
        }

        private void CaiDatDataGridView()
        {
            dgvTeam.AutoGenerateColumns = false;
            dgvTeam.AllowUserToAddRows = false;
            dgvTeam.AllowUserToDeleteRows = false;
            dgvTeam.ReadOnly = true;
            dgvTeam.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTeam.MultiSelect = false;

            dgvTeam.Columns.Clear();
            dgvTeam.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã TV", DataPropertyName = "MaTV", Width = 70, Name = "MaTV" });
            dgvTeam.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Họ Tên", DataPropertyName = "HoTen", Width = 140, Name = "HoTen" });
            dgvTeam.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Số Điện Thoại", DataPropertyName = "SoDienThoai", Width = 110, Name = "SoDienThoai" });
            dgvTeam.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email", Width = 150, Name = "Email" });
            dgvTeam.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Chức Vụ", DataPropertyName = "ChucVu", Width = 100, Name = "ChucVu" });
        }

        private void CaiDatTrangThaiMacDinh()
        {
            txtMemberID.ReadOnly = true;
            txtMemberID.BackColor = SystemColors.Window;
            isSuaMode = false;
            btnSua.Text = "Sửa";
        }

        private void LoadDanhSachThanhVien()
        {
            try
            {
                DataTable dt = tvBUS.LayDanhSachThanhVien();
                dgvTeam.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void XoaTrangNhapLieu()
        {
            txtMemberID.Clear();
            txtFullname.Clear();
            txtPhone.Clear(); 
            txtEmail.Clear(); 
            cboRole.SelectedIndex = -1;
            CaiDatTrangThaiMacDinh();
        }

        private ThanhVienDTO LayDuLieuTuForm()
        {
            ThanhVienDTO tv = new ThanhVienDTO();
            tv.MaTV = txtMemberID.Text.Trim();
            tv.HoTen = txtFullname.Text.Trim();
            tv.SoDienThoai = txtPhone.Text.Trim(); 
            tv.Email = txtEmail.Text.Trim();       
            tv.ChucVu = cboRole.Text.Trim();
            return tv;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFullname.Text))
                {
                    MessageBox.Show("Vui lòng nhập Họ tên thành viên!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ThanhVienDTO tv = LayDuLieuTuForm();
                if (tvBUS.ThemThanhVien(tv))
                {
                    MessageBox.Show("Thêm thành viên thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachThanhVien();
                    XoaTrangNhapLieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvTeam.CurrentRow == null) return;

            if (!isSuaMode)
            {
                txtMemberID.ReadOnly = true;
                txtMemberID.BackColor = SystemColors.Control;
                isSuaMode = true;
                btnSua.Text = "Lưu";
            }
            else
            {
                try
                {
                    ThanhVienDTO tv = LayDuLieuTuForm();
                    if (tvBUS.SuaThanhVien(tv))
                    {
                        MessageBox.Show("Cập nhật thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSachThanhVien();
                        XoaTrangNhapLieu();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi sửa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTeam.CurrentRow == null) return;
            DataGridViewRow row = dgvTeam.CurrentRow;
            string maTV = row.Cells["MaTV"].Value.ToString();

            if (MessageBox.Show($"Bạn có chắc muốn xóa thành viên {maTV} không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    if (tvBUS.XoaThanhVien(maTV))
                    {
                        MessageBox.Show("Xóa thành công!", "Thông báo");
                        LoadDanhSachThanhVien();
                        XoaTrangNhapLieu();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvTeam_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTeam.Rows[e.RowIndex];
                txtMemberID.Text = row.Cells["MaTV"].Value?.ToString() ?? "";
                txtFullname.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
                txtPhone.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? ""; // Đổ ngược SĐT lên ô nhập
                txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";       // Đổ ngược Email lên ô nhập
                cboRole.Text = row.Cells["ChucVu"].Value?.ToString() ?? "";

                isSuaMode = false;
                btnSua.Text = "Sửa";
                txtMemberID.ReadOnly = true;
                txtMemberID.BackColor = SystemColors.Control;
            }
        }

        private void btnSearchTeam_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtSearchTeam.Text.Trim();
            if (tuKhoa == "Nhập mã hoặc tên nhân viên...") tuKhoa = "";

            dgvTeam.DataSource = tvBUS.TimKiemThanhVien(tuKhoa);
        }

        private void btnRefreshTeam_Click(object sender, EventArgs e)
        {
            txtSearchTeam.Text = "Nhập mã hoặc tên nhân viên...";
            txtSearchTeam.ForeColor = Color.Gray;

            LoadDanhSachThanhVien();

            XoaTrangNhapLieu();
        }
        private void NapDanhSachChucVu()
        {
            // Xóa sạch các mục cũ nếu có để tránh bị lặp dữ liệu
            cboRole.Items.Clear();

            cboRole.Items.Add("Quản trị viên");
            cboRole.Items.Add("Developer");
            cboRole.Items.Add("Tester");
            cboRole.Items.Add("Project Manager");

            // Ép kiểu hiển thị thành dạng danh sách thả xuống
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;

            // Chọn sẵn dòng đầu tiên mặc định khi mở form
            if (cboRole.Items.Count > 0)
            {
                cboRole.SelectedIndex = 0;
            }
        }
    }
}