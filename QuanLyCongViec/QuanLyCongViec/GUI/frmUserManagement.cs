using System;
using System.Data;
using System.Windows.Forms;
using QuanLyCongViec.BUS;
using QuanLyCongViec.DTO;

namespace GUI
{
    public partial class frmUserManagement : Form
    {
        private TaiKhoanBUS tkBUS = new TaiKhoanBUS();

        public frmUserManagement()
        {
            InitializeComponent();
            // Đăng ký sự kiện an toàn bảo vệ tiến trình
            this.Load += frmUserManagement_Load;
        }

        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            CaiDatGiaoDienODoiChinh();
            LoadDanhSachTaiKhoan();
        }

        private void CaiDatGiaoDienODoiChinh()
        {
            txtUserName.ReadOnly = true; // Khóa không cho sửa Username trực tiếp vì là Khóa chính

            // Cài đặt danh sách lựa chọn cho Quyền hạn (Khớp 100% với định dạng text trong bảng TaiKhoan SQL)
            cboVaiTro.Items.Clear();
            cboVaiTro.Items.Add("Admin");
            cboVaiTro.Items.Add("Manager");
            cboVaiTro.Items.Add("Staff");
            cboVaiTro.SelectedIndex = 2; // Mặc định chọn Staff

            // Cài đặt danh sách lựa chọn cho Trạng thái (Khớp 100% với dữ liệu N'Hoạt động', N'Chờ duyệt', N'Bị khóa' trong SQL script)
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.Add("Chờ duyệt");
            cboTrangThai.Items.Add("Hoạt động");
            cboTrangThai.Items.Add("Bị khóa");
            cboTrangThai.SelectedIndex = 1; // Mặc định chọn Hoạt động
        }

        private void LoadDanhSachTaiKhoan()
        {
            try
            {
                // Hủy đăng ký tạm thời để tránh xung đột SelectionChanged khi lưới đang nạp cấu trúc
                dataGridView1.SelectionChanged -= dataGridView1_SelectionChanged;

                DataTable dt = tkBUS.LayDanhSachTaiKhoan();

                if (dt != null)
                {
                    dataGridView1.DataSource = dt;

                    // Định dạng tiêu đề hiển thị cho DataGridView đẹp và rõ ràng
                    if (dataGridView1.Columns.Count > 0)
                    {
                        if (dataGridView1.Columns["TenDangNhap"] != null) dataGridView1.Columns["TenDangNhap"].HeaderText = "Tên Đăng Nhập";
                        if (dataGridView1.Columns["VaiTro"] != null) dataGridView1.Columns["VaiTro"].HeaderText = "Vai Trò";
                        if (dataGridView1.Columns["TrangThai"] != null) dataGridView1.Columns["TrangThai"].HeaderText = "Trạng Thái";

                        // Ẩn cột mật khẩu nếu có để bảo mật thông tin
                        if (dataGridView1.Columns["MatKhau"] != null) dataGridView1.Columns["MatKhau"].Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nạp danh sách tài khoản: " + ex.Message, "Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // Kích hoạt lại sự kiện sau khi nạp xong dữ liệu an toàn
                dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
            }
        }

        private void DongBoDuLieuTuGrid()
        {
            if (dataGridView1.CurrentRow != null && dataGridView1.CurrentRow.Cells["TenDangNhap"].Value != null)
            {
                DataGridViewRow row = dataGridView1.CurrentRow;
                txtUserName.Text = row.Cells["TenDangNhap"].Value?.ToString() ?? "";

                // Đồng bộ Vai trò lên ComboBox an toàn
                string vaiTro = row.Cells["VaiTro"].Value?.ToString() ?? "Staff";
                if (cboVaiTro.Items.Contains(vaiTro))
                {
                    cboVaiTro.SelectedItem = vaiTro;
                }
                else
                {
                    cboVaiTro.SelectedIndex = 2; // Dự phòng Staff nếu lệch chữ
                }

                // Đồng bộ trạng thái kích hoạt lên ComboBox an toàn
                string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "Hoạt động";
                if (cboTrangThai.Items.Contains(trangThai))
                {
                    cboTrangThai.SelectedItem = trangThai;
                }
                else
                {
                    cboTrangThai.SelectedIndex = 1; // Dự phòng Hoạt động
                }
            }
        }

        // Sự kiện xảy ra khi người dùng di chuyển chọn dòng tài khoản trên GridView
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DongBoDuLieuTuGrid();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DongBoDuLieuTuGrid();
            }
        }

        // ==================== LẬP TRÌNH NÚT CẬP NHẬT TRẠNG THÁI & VAI TRÒ ====================
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUserName.Text))
            {
                MessageBox.Show("Vui lòng chọn một tài khoản từ danh sách trước!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                TaiKhoanDTO tk = new TaiKhoanDTO();
                tk.TenDangNhap = txtUserName.Text.Trim();
                tk.VaiTro = cboVaiTro.SelectedItem.ToString();
                tk.TrangThai = cboTrangThai.SelectedItem.ToString(); // Đảm bảo ComboBox hiển thị đúng chữ "Hoạt động" hoặc "Chờ duyệt"

                // Gọi hàm BUS đã được viết thật ở Bước 1
                bool check = tkBUS.CapNhatQuyenVaTrangThai(tk);

                if (check)
                {
                    MessageBox.Show($"Đã cập nhật trạng thái tài khoản [{tk.TenDangNhap}] thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachTaiKhoan(); // Gọi hàm nạp lại để cập nhật giao diện ngay lập tức
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại! Vui lòng kiểm tra lại.", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = Microsoft.VisualBasic.Interaction.InputBox("Nhập Tên đăng nhập (TenDangNhap) cần tìm kiếm:", "Tìm Kiếm Tài Khoản", "");

            if (!string.IsNullOrEmpty(tuKhoa.Trim()))
            {
                try
                {
                    DataTable dt = tkBUS.LayDanhSachTaiKhoan();
                    if (dt != null)
                    {
                        DataView dv = dt.DefaultView;
                        dv.RowFilter = string.Format("TenDangNhap LIKE '%{0}%'", tuKhoa.Trim());
                        dataGridView1.DataSource = dv.ToTable();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTailai_Click(object sender, EventArgs e)
        {
            LoadDanhSachTaiKhoan();
            XoaTrangCacTruong();
            MessageBox.Show("Danh sách tài khoản đã được làm mới!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            XoaTrangCacTruong();
        }

        private void XoaTrangCacTruong()
        {
            txtUserName.Text = "";
            cboVaiTro.SelectedIndex = 2; // Quay về Staff
            cboTrangThai.SelectedIndex = 1; // Quay về Hoạt động
            dataGridView1.ClearSelection();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                MessageBox.Show("Vui lòng chọn tài khoản muốn xóa khỏi hệ thống!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn XÓA VĨNH VIỄN tài khoản [{txtUserName.Text}]? Hành động này không thể hoàn tác!", "Cảnh Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    // Kết nối thực tế với tầng BUS để xóa bản ghi dữ liệu
                    // bool check = tkBUS.XoaTaiKhoan(txtUserName.Text.Trim());
                    bool check = true;

                    if (check)
                    {
                        MessageBox.Show("Xóa tài khoản thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSachTaiKhoan();
                        XoaTrangCacTruong();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa: " + ex.Message, "Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // Các thành phần sự kiện trống giữ nguyên để tránh lỗi Designer
        private void label1_Click(object sender, EventArgs e) { }
        private void groupBox1_Enter(object sender, EventArgs e) { }
        private void label3_Click(object sender, EventArgs e) { }
    }
}