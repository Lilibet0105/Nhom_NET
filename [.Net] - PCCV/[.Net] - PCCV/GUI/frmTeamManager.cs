using _Net____PCCV.BUS; // Sử dụng lớp xử lý nghiệp vụ
using _Net____PCCV.DTO; // Sử dụng đối tượng truyền dữ liệu
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GUI
{
    public partial class frmTeamManager : Form
    {
        // Khởi tạo đối tượng BUS xử lý thành viên
        private ThanhVienBUS tvBUS = new ThanhVienBUS();
        private bool isSuaMode = false; // Cờ đánh dấu chế độ Sửa

        public frmTeamManager()
        {
            InitializeComponent();
        }

        // ==================== SỰ KIỆN LOAD FORM ====================
        private void frmTeamManager_Load(object sender, EventArgs e)
        {
            LoadDanhSachThanhVien();
            CaiDatDataGridView();
            CaiDatTrangThaiMacDinh();
        }

        // ==================== CÀI ĐẶT HIỂN THỊ DATAGRIDVIEW ====================
        private void CaiDatDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            dataGridView1.Columns.Clear();

            // Thêm các cột tương ứng cấu trúc dữ liệu hồ sơ nhân sự
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Mã TV", DataPropertyName = "MaTV", Width = 70, Name = "MaTV" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Họ Tên", DataPropertyName = "HoTen", Width = 150, Name = "HoTen" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Chức Vụ", DataPropertyName = "ChucVu", Width = 120, Name = "ChucVu" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Email", DataPropertyName = "Email", Width = 160, Name = "Email" });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { HeaderText = "Số Điện Thoại", DataPropertyName = "SoDienThoai", Width = 110, Name = "SoDienThoai" });
        }

        // ==================== CÀI ĐẶT TRẠNG THÁI MẶC ĐỊNH ====================
        private void CaiDatTrangThaiMacDinh()
        {
            textBox1.ReadOnly = true; // Mã thành viên tự tăng nên để ReadOnly
            textBox1.BackColor = SystemColors.Control;
            button3.Enabled = false; // Nút Cập nhật ẩn khi chưa bấm Sửa
            isSuaMode = false;
            button2.Text = "Sửa Thành Viên";
        }

        // ==================== TẢI DỮ LIỆU LÊN GRIDVIEW ====================
        private void LoadDanhSachThanhVien()
        {
            try
            {
                DataTable dt = tvBUS.LayDanhSachThanhVien();
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hồ sơ nhân sự: " + ex.Message, "Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== XÓA TRẮNG CÁC Ô NHẬP LIỆU ====================
        private void XoaTrangNhapLieu()
        {
            textBox1.Clear(); // MaTV
            textBox2.Clear(); // HoTen
            textBox3.Clear(); // ChucVu
            textBox4.Clear(); // Email
            textBox5.Clear(); // SoDienThoai
            isSuaMode = false;
            button3.Enabled = false;
            button2.Text = "Sửa Thành Viên";
        }

        // ==================== CHUYỂN ĐỔI DỮ LIỆU SANG DTO ====================
        private ThanhVienDTO LayDuLieuTuForm()
        {
            ThanhVienDTO tv = new ThanhVienDTO();
            if (!string.IsNullOrEmpty(textBox1.Text))
                tv.MaTV = int.Parse(textBox1.Text.Trim());
            tv.HoTen = textBox2.Text.Trim();
            tv.ChucVu = textBox3.Text.Trim();
            tv.Email = textBox4.Text.Trim();
            tv.SoDienThoai = textBox5.Text.Trim();
            return tv;
        }

        // ==================== CHỨC NĂNG THÊM THÀNH VIÊN ====================
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Vui lòng nhập họ tên thành viên!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }

                ThanhVienDTO tv = LayDuLieuTuForm();
                bool result = tvBUS.ThemThanhVien(tv);

                if (result)
                {
                    MessageBox.Show("Thêm hồ sơ nhân sự thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachThanhVien();
                    XoaTrangNhapLieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== CHẾ ĐỘ SỬA HỒ SƠ ====================
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một nhân sự để chỉnh sửa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!isSuaMode)
            {
                DataGridRow row = dataGridView1.CurrentRow;
                textBox1.Text = row.Cells["MaTV"].Value?.ToString() ?? "";
                textBox2.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
                textBox3.Text = row.Cells["ChucVu"].Value?.ToString() ?? "";
                textBox4.Text = row.Cells["Email"].Value?.ToString() ?? "";
                textBox5.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? "";

                isSuaMode = true;
                button3.Enabled = true;
                button2.Text = "Hủy Sửa";
            }
            else
            {
                XoaTrangNhapLieu();
            }
        }

        // ==================== CHỨC NĂNG CẬP NHẬT (LƯU SỬA) ====================
        private void button3_Click(object sender, EventArgs e)
        {
            if (!isSuaMode) return;

            try
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Họ tên thành viên không được để trống!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ThanhVienDTO tv = LayDuLieuTuForm();
                bool result = tvBUS.SuaThanhVien(tv);

                if (result)
                {
                    MessageBox.Show("Cập nhật hồ sơ nhân sự thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachThanhVien();
                    XoaTrangNhapLieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== CHỨC NĂNG XÓA THÀNH VIÊN ====================
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn thành viên cần xóa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridView1.CurrentRow;
            string maTV = row.Cells["MaTV"].Value?.ToString() ?? "";
            string hoTen = row.Cells["HoTen"].Value?.ToString() ?? "";

            DialogResult dlg = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa thành viên '{hoTen}' (Mã: {maTV}) khỏi hệ thống?",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question
            );

            if (dlg == DialogResult.Yes)
            {
                try
                {
                    bool result = tvBUS.XoaThanhVien(int.Parse(maTV));
                    if (result)
                    {
                        MessageBox.Show("Xóa thành viên thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        // ==================== CHỨC NĂNG TÌM KIẾM THÀNH VIÊN ====================
        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                string tuKhoa = txtTimKiem.Text.Trim();
                DataTable dt = tvBUS.TimKiemThanhVien(tuKhoa);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["MaTV"].Value?.ToString() ?? "";
                textBox2.Text = row.Cells["HoTen"].Value?.ToString() ?? "";
                textBox3.Text = row.Cells["ChucVu"].Value?.ToString() ?? "";
                textBox4.Text = row.Cells["Email"].Value?.ToString() ?? "";
                textBox5.Text = row.Cells["SoDienThoai"].Value?.ToString() ?? "";
            }
        }
    }
}