using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _Net____PCCV.BUS;
using _Net____PCCV.DTO;

namespace _.Net____PCCV
{
    public partial class LichLamViec : Form
    {
        private LichLamViecBUS lichBUS = new LichLamViecBUS();
        private bool isSuaMode = false; // Cờ đánh dấu đang ở chế độ Sửa

        public LichLamViec()
        {
            InitializeComponent();
        }

        // ==================== SỰ KIỆN LOAD FORM ====================
        private void LichLamViec_Load(object sender, EventArgs e)
        {
            LoadDanhSachLich();
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

            // Xóa cột cũ nếu có
            dataGridView1.Columns.Clear();

            // Thêm các cột hiển thị
            DataGridViewTextBoxColumn colMaLich = new DataGridViewTextBoxColumn();
            colMaLich.HeaderText = "Mã Lịch";
            colMaLich.DataPropertyName = "MaLich";
            colMaLich.Width = 70;
            dataGridView1.Columns.Add(colMaLich);

            DataGridViewTextBoxColumn colTieuDe = new DataGridViewTextBoxColumn();
            colTieuDe.HeaderText = "Tiêu Đề";
            colTieuDe.DataPropertyName = "TieuDe";
            colTieuDe.Width = 180;
            dataGridView1.Columns.Add(colTieuDe);

            DataGridViewTextBoxColumn colDiaDiem = new DataGridViewTextBoxColumn();
            colDiaDiem.HeaderText = "Địa Điểm";
            colDiaDiem.DataPropertyName = "DiaDiem";
            colDiaDiem.Width = 150;
            dataGridView1.Columns.Add(colDiaDiem);

            DataGridViewTextBoxColumn colMoTa = new DataGridViewTextBoxColumn();
            colMoTa.HeaderText = "Mô Tả";
            colMoTa.DataPropertyName = "MoTa";
            colMoTa.Width = 200;
            dataGridView1.Columns.Add(colMoTa);

            DataGridViewTextBoxColumn colBatDau = new DataGridViewTextBoxColumn();
            colBatDau.HeaderText = "Bắt Đầu";
            colBatDau.DataPropertyName = "ThoiGianBatDau";
            colBatDau.Width = 140;
            colBatDau.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dataGridView1.Columns.Add(colBatDau);

            DataGridViewTextBoxColumn colKetThuc = new DataGridViewTextBoxColumn();
            colKetThuc.HeaderText = "Kết Thúc";
            colKetThuc.DataPropertyName = "ThoiGianKetThuc";
            colKetThuc.Width = 140;
            colKetThuc.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dataGridView1.Columns.Add(colKetThuc);

            DataGridViewTextBoxColumn colTrangThai = new DataGridViewTextBoxColumn();
            colTrangThai.HeaderText = "Trạng Thái";
            colTrangThai.DataPropertyName = "TrangThai";
            colTrangThai.Width = 130;
            dataGridView1.Columns.Add(colTrangThai);
        }

        // ==================== CÀI ĐẶT TRẠNG THÁI MẶC ĐỊNH ====================
        private void CaiDatTrangThaiMacDinh()
        {
            textBox1.ReadOnly = true; // Mã lịch chỉ đọc
            textBox1.BackColor = SystemColors.Control; // Màu xám cho ô chỉ đọc
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddHours(1);
            comboBox1.SelectedIndex = 0; // Mặc định "Chưa Hoàn Thành"
            button3.Enabled = false; // Nút Cập Nhật ban đầu tắt
            isSuaMode = false;
        }

        // ==================== NẠP DỮ LIỆU LÊN DATAGRIDVIEW ====================
        private void LoadDanhSachLich()
        {
            try
            {
                DataTable dt = lichBUS.LayDanhSachLich();
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách lịch làm việc: " + ex.Message, "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== XÓA TRẮNG CÁC Ô NHẬP LIỆU ====================
        private void XoaTrangNhapLieu()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddHours(1);
            comboBox1.SelectedIndex = 0;
            isSuaMode = false;
            button3.Enabled = false;
            button2.Text = "Sửa Lịch";
        }

        // ==================== CHUYỂN DỮ LIỆU TỪ FORM SANG DTO ====================
        private LichLamViecDTO LayDuLieuTuForm()
        {
            LichLamViecDTO lich = new LichLamViecDTO();
            if (!string.IsNullOrEmpty(textBox1.Text))
                lich.MaLich = int.Parse(textBox1.Text.Trim());
            lich.TieuDe = textBox2.Text.Trim();
            lich.DiaDiem = textBox3.Text.Trim();
            lich.MoTa = textBox4.Text.Trim();
            lich.ThoiGianBatDau = dateTimePicker1.Value;
            lich.ThoiGianKetThuc = dateTimePicker2.Value;
            lich.TrangThai = comboBox1.SelectedItem?.ToString() ?? "Chưa Hoàn Thành";
            return lich;
        }

        // ==================== NÚT THÊM LỊCH ====================
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Validate dữ liệu nhập
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tiêu Đề lịch làm việc!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }
                if (dateTimePicker2.Value < dateTimePicker1.Value)
                {
                    MessageBox.Show("Thời gian kết thúc phải lớn hơn hoặc bằng thời gian bắt đầu!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LichLamViecDTO lich = LayDuLieuTuForm();
                bool result = lichBUS.ThemLich(lich);

                if (result)
                {
                    MessageBox.Show("Thêm lịch làm việc thành công!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachLich();
                    XoaTrangNhapLieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== NÚT SỬA LỊCH (CHUYỂN SANG CHẾ ĐỘ SỬA) ====================
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một lịch làm việc trong bảng để sửa!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!isSuaMode)
            {
                // Chuyển sang chế độ Sửa - nạp dữ liệu từ dòng đang chọn lên form
                DataGridViewRow row = dataGridView1.CurrentRow;
                textBox1.Text = row.Cells["MaLich"].Value?.ToString() ?? "";
                textBox2.Text = row.Cells["TieuDe"].Value?.ToString() ?? "";
                textBox3.Text = row.Cells["DiaDiem"].Value?.ToString() ?? "";
                textBox4.Text = row.Cells["MoTa"].Value?.ToString() ?? "";

                if (row.Cells["ThoiGianBatDau"].Value != null && row.Cells["ThoiGianBatDau"].Value != DBNull.Value)
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["ThoiGianBatDau"].Value);
                if (row.Cells["ThoiGianKetThuc"].Value != null && row.Cells["ThoiGianKetThuc"].Value != DBNull.Value)
                    dateTimePicker2.Value = Convert.ToDateTime(row.Cells["ThoiGianKetThuc"].Value);

                string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "Chưa Hoàn Thành";
                int index = comboBox1.Items.IndexOf(trangThai);
                comboBox1.SelectedIndex = index >= 0 ? index : 0;

                isSuaMode = true;
                button3.Enabled = true;
                button2.Text = "Hủy Sửa";
            }
            else
            {
                // Hủy chế độ sửa - xóa trắng form
                XoaTrangNhapLieu();
            }
        }

        // ==================== NÚT CẬP NHẬT LỊCH ====================
        private void button3_Click(object sender, EventArgs e)
        {
            if (!isSuaMode)
            {
                MessageBox.Show("Vui lòng nhấn nút 'Sửa Lịch' trước khi cập nhật!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tiêu Đề lịch làm việc!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }
                if (dateTimePicker2.Value < dateTimePicker1.Value)
                {
                    MessageBox.Show("Thời gian kết thúc phải lớn hơn hoặc bằng thời gian bắt đầu!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                LichLamViecDTO lich = LayDuLieuTuForm();
                bool result = lichBUS.SuaLich(lich);

                if (result)
                {
                    MessageBox.Show("Cập nhật lịch làm việc thành công!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachLich();
                    XoaTrangNhapLieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== NÚT XÓA LỊCH ====================
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một lịch làm việc trong bảng để xóa!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridView1.CurrentRow;
            string maLich = row.Cells["MaLich"].Value?.ToString() ?? "";
            string tieuDe = row.Cells["TieuDe"].Value?.ToString() ?? "";

            DialogResult dlg = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa lịch làm việc '" + tieuDe + "' (Mã: " + maLich + ")?",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (dlg == DialogResult.Yes)
            {
                try
                {
                    bool result = lichBUS.XoaLich(int.Parse(maLich));
                    if (result)
                    {
                        MessageBox.Show("Xóa lịch làm việc thành công!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSachLich();
                        XoaTrangNhapLieu();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ==================== CLICK VÀO DÒNG DATAGRIDVIEW ====================
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Xử lý khi người dùng click vào dòng trong DataGridView
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["MaLich"].Value?.ToString() ?? "";
                textBox2.Text = row.Cells["TieuDe"].Value?.ToString() ?? "";
                textBox3.Text = row.Cells["DiaDiem"].Value?.ToString() ?? "";
                textBox4.Text = row.Cells["MoTa"].Value?.ToString() ?? "";

                if (row.Cells["ThoiGianBatDau"].Value != null && row.Cells["ThoiGianBatDau"].Value != DBNull.Value)
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["ThoiGianBatDau"].Value);
                if (row.Cells["ThoiGianKetThuc"].Value != null && row.Cells["ThoiGianKetThuc"].Value != DBNull.Value)
                    dateTimePicker2.Value = Convert.ToDateTime(row.Cells["ThoiGianKetThuc"].Value);

                string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "Chưa Hoàn Thành";
                int index = comboBox1.Items.IndexOf(trangThai);
                comboBox1.SelectedIndex = index >= 0 ? index : 0;
            }
        }
    }
}
