using QuanLyCongViec.BUS;
using QuanLyCongViec.DTO;
using QuanLyCongViec.GUI;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    public partial class frmTeamManager : Form
    {
        private readonly ThanhVienBUS tvBUS = new ThanhVienBUS();
        private bool isSuaMode;

        public frmTeamManager()
        {
            InitializeComponent();
        }

        private void frmTeamManager_Load(object sender, EventArgs e)
        {
            CaiDatDataGridView();
            CaiDatTrangThaiMacDinh();
            NapDanhSachChucVu();
            LoadDanhSachThanhVien();
            txtSearchTeam.Text = "Nhập mã hoặc tên nhân viên...";
            txtSearchTeam.ForeColor = Color.Gray;
        }

        private void CaiDatDataGridView()
        {
            dgvTeam.AutoGenerateColumns = false;
            dgvTeam.Columns.Clear();
            dgvTeam.Columns.Add(UiHelper.TextColumn("MaTVHienThi", "MaTVHienThi", "Mã TV", 96, 16));
            dgvTeam.Columns.Add(UiHelper.TextColumn("MaTV", "MaTV", "ID", 60));
            dgvTeam.Columns["MaTV"].Visible = false;
            dgvTeam.Columns.Add(UiHelper.TextColumn("HoTen", "HoTen", "Họ tên", 170, 28));
            dgvTeam.Columns.Add(UiHelper.TextColumn("SoDienThoai", "SoDienThoai", "Số điện thoại", 125, 20));
            dgvTeam.Columns.Add(UiHelper.TextColumn("Email", "Email", "Email", 200, 30));
            dgvTeam.Columns.Add(UiHelper.TextColumn("ChucVu", "ChucVu", "Chức vụ", 130, 22));
            UiHelper.StyleGrid(dgvTeam);
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
                CapNhatMaThanhVienTuDong(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatMaThanhVienTuDong(DataTable dt)
        {
            int maxId = 0;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["MaTV"] != DBNull.Value && int.TryParse(row["MaTV"].ToString(), out int id) && id > maxId)
                    {
                        maxId = id;
                    }
                }
            }

            if (!isSuaMode)
            {
                txtMemberID.Text = UiHelper.FormatCode("TV", maxId + 1);
            }
        }

        private void XoaTrangNhapLieu()
        {
            txtMemberID.Clear();
            txtFullname.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            if (cboRole.Items.Count > 0)
            {
                cboRole.SelectedIndex = 0;
            }

            CaiDatTrangThaiMacDinh();
            CapNhatMaThanhVienTuDong(dgvTeam.DataSource as DataTable);
        }

        private ThanhVienDTO LayDuLieuTuForm()
        {
            return new ThanhVienDTO
            {
                MaTV = UiHelper.ParseCode(txtMemberID.Text).ToString(),
                HoTen = txtFullname.Text.Trim(),
                SoDienThoai = txtPhone.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                ChucVu = cboRole.Text.Trim()
            };
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtFullname.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên thành viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtFullname.Focus();
                    return;
                }

                if (tvBUS.ThemThanhVien(LayDuLieuTuForm()))
                {
                    MessageBox.Show("Thêm thành viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (dgvTeam.CurrentRow == null)
            {
                return;
            }

            if (!isSuaMode)
            {
                isSuaMode = true;
                btnSua.Text = "Lưu";
                txtMemberID.ReadOnly = true;
                txtMemberID.BackColor = SystemColors.Control;
            }
            else
            {
                try
                {
                    if (tvBUS.SuaThanhVien(LayDuLieuTuForm()))
                    {
                        MessageBox.Show("Cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
            if (dgvTeam.CurrentRow == null)
            {
                return;
            }

            string maTV = dgvTeam.CurrentRow.Cells["MaTV"].Value.ToString();
            string maHienThi = dgvTeam.CurrentRow.Cells["MaTVHienThi"].Value?.ToString() ?? UiHelper.FormatCode("TV", maTV);
            if (MessageBox.Show("Bạn có chắc muốn xóa thành viên " + maHienThi + " không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (tvBUS.XoaThanhVien(maTV))
                {
                    MessageBox.Show("Xóa thành công.", "Thông báo");
                    LoadDanhSachThanhVien();
                    XoaTrangNhapLieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi xóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvTeam_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            DataGridViewRow row = dgvTeam.Rows[e.RowIndex];
            txtMemberID.Text = row.Cells["MaTVHienThi"].Value?.ToString() ?? UiHelper.FormatCode("TV", row.Cells["MaTV"].Value);
            txtFullname.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
            txtPhone.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? "";
            txtEmail.Text = row.Cells["Email"].Value?.ToString() ?? "";
            cboRole.Text = row.Cells["ChucVu"].Value?.ToString() ?? "";
            isSuaMode = false;
            btnSua.Text = "Sửa";
        }

        private void btnSearchTeam_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtSearchTeam.Text.Trim();
            if (tuKhoa == "Nhập mã hoặc tên nhân viên...")
            {
                tuKhoa = "";
            }

            dgvTeam.DataSource = tvBUS.TimKiemThanhVien(tuKhoa);
            CapNhatMaThanhVienTuDong(dgvTeam.DataSource as DataTable);
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
            cboRole.Items.Clear();
            cboRole.Items.Add("Quản trị viên");
            cboRole.Items.Add("Developer");
            cboRole.Items.Add("Tester");
            cboRole.Items.Add("Project Manager");
            cboRole.DropDownStyle = ComboBoxStyle.DropDownList;
            if (cboRole.Items.Count > 0)
            {
                cboRole.SelectedIndex = 0;
            }
        }

        private void txtSearchTeam_Enter(object sender, EventArgs e)
        {
            if (txtSearchTeam.Text == "Nhập mã hoặc tên nhân viên...")
            {
                txtSearchTeam.Text = "";
                txtSearchTeam.ForeColor = Color.Black;
            }
        }

        private void txtSearchTeam_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchTeam.Text))
            {
                txtSearchTeam.Text = "Nhập mã hoặc tên nhân viên...";
                txtSearchTeam.ForeColor = Color.Gray;
            }
        }
    }
}
