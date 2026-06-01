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
            dgvTeam.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Họ Tên", DataPropertyName = "HoTen", Width = 150, Name = "HoTen" });
            dgvTeam.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Chức Vụ", DataPropertyName = "ChucVu", Width = 120, Name = "ChucVu" });
        }

        private void CaiDatTrangThaiMacDinh()
        {
            txtMemberID.ReadOnly = true;
            txtMemberID.BackColor = SystemColors.Control;
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
            cboRole.SelectedIndex = -1;
            isSuaMode = false;
            btnSua.Text = "Sửa";
        }

        private ThanhVienDTO LayDuLieuTuForm()
        {
            ThanhVienDTO tv = new ThanhVienDTO();
            if (!string.IsNullOrEmpty(txtMemberID.Text))
                tv.MaTV = int.Parse(txtMemberID.Text.Trim());
            tv.HoTen = txtFullname.Text.Trim();
            tv.ChucVu = cboRole.Text.Trim();
            return tv;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFullname.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ThanhVienDTO tv = LayDuLieuTuForm();
                if (tvBUS.ThemThanhVien(tv))
                {
                    MessageBox.Show("Thêm thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachThanhVien();
                    XoaTrangNhapLieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvTeam.CurrentRow == null) return;

            if (!isSuaMode)
            {
                DataGridViewRow row = dgvTeam.CurrentRow;
                txtMemberID.Text = row.Cells["MaTV"].Value?.ToString() ?? "";
                txtFullname.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
                cboRole.Text = row.Cells["ChucVu"].Value?.ToString() ?? "";
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
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTeam.CurrentRow == null) return;
            DataGridViewRow row = dgvTeam.CurrentRow;
            int maTV = Convert.ToInt32(row.Cells["MaTV"].Value);

            if (MessageBox.Show("Bạn có muốn xóa không?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (tvBUS.XoaThanhVien(maTV))
                {
                    LoadDanhSachThanhVien();
                    XoaTrangNhapLieu();
                }
            }
        }

        private void dgvTeam_Click(object sender, EventArgs e) { }

        private void dgvTeam_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvTeam.Rows[e.RowIndex];
                txtMemberID.Text = row.Cells["MaTV"].Value?.ToString() ?? "";
                txtFullname.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
                cboRole.Text = row.Cells["ChucVu"].Value?.ToString() ?? "";
            }
        }
    }
}