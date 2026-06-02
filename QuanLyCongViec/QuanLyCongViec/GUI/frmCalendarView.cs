using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuanLyCongViec.BUS;
using QuanLyCongViec.DTO;

namespace QuanLyCongViec.GUI
{
    public partial class frmCalendarView : Form
    {
        private CalendarViewBUS calendarBUS = new CalendarViewBUS();
        private bool isSuaMode = false; // Cờ đánh dấu đang ở chế độ Sửa

        public frmCalendarView()
        {
            InitializeComponent();
        }

        // ==================== SỰ KIỆN LOAD FORM ====================\
        private void CalendarView_Load(object sender, EventArgs e)
        {
            LoadDanhSachLich();
            CaiDatDataGridView();
            CaiDatTrangThaiMacDinh();
        }

        // ==================== CÀI ĐẶT HIỂN THỊ DATAGRIDVIEW ====================\
        private void CaiDatDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

            // Xóa cột cũ nếu có để tránh trùng lặp khi load lại
            dataGridView1.Columns.Clear();

            // Cấu hình các cột hiển thị đẹp mắt và khớp 100% với SQL dữ liệu
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MaLich", Name = "MaLich", HeaderText = "Mã Lịch", Width = 70 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TieuDe", Name = "TieuDe", HeaderText = "Tiêu Đề", Width = 150 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "DiaDiem", Name = "DiaDiem", HeaderText = "Địa Điểm", Width = 130 });
            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "MoTa", Name = "MoTa", HeaderText = "Mô Tả", Width = 180 });

            var colBD = new DataGridViewTextBoxColumn { DataPropertyName = "ThoiGianBatDau", Name = "ThoiGianBatDau", HeaderText = "Bắt Đầu", Width = 120 };
            colBD.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dataGridView1.Columns.Add(colBD);

            var colKT = new DataGridViewTextBoxColumn { DataPropertyName = "ThoiGianKetThuc", Name = "ThoiGianKetThuc", HeaderText = "Kết Thúc", Width = 120 };
            colKT.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dataGridView1.Columns.Add(colKT);

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "TrangThai", Name = "TrangThai", HeaderText = "Trạng Thái", Width = 120 });
        }

        // ==================== TẢI DỮ LIỆU TỪ SQL LÊN BẢNG ====================\
        private void LoadDanhSachLich()
        {
            try
            {
                DataTable dt = calendarBUS.LayDanhSachLich();
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách lịch: " + ex.Message, "Lỗi Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CaiDatTrangThaiMacDinh()
        {
            textBox1.ReadOnly = true; // Mã lịch tự tăng không cho sửa trực tiếp
            XoaTrangCacTruong();

            comboBox1.Items.Clear();
            comboBox1.Items.Add("Chưa Hoàn Thành");
            comboBox1.Items.Add("Đang Tiến Hành");
            comboBox1.Items.Add("Đã Hoàn Thành");
            comboBox1.SelectedIndex = 0;

            button1.Enabled = true;  // Nút Thêm
            button2.Enabled = false; // Nút Sửa tắt đi khi chưa chọn dòng
            button3.Enabled = false; // Nút Xóa tắt đi khi chưa chọn dòng
            isSuaMode = false;
        }

        private void XoaTrangCacTruong()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddHours(1);
            if (comboBox1.Items.Count > 0) comboBox1.SelectedIndex = 0;
        }

        // ==================== CHỨC NĂNG THÊM MỚI LỊCH ====================\
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Tiêu đề lịch làm việc không được để trống!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }

                CalendarViewDTO lich = new CalendarViewDTO
                {
                    TieuDe = textBox2.Text.Trim(),
                    DiaDiem = textBox3.Text.Trim(),
                    MoTa = textBox4.Text.Trim(),
                    ThoiGianBatDau = dateTimePicker1.Value,
                    ThoiGianKetThuc = dateTimePicker2.Value,
                    TrangThai = comboBox1.SelectedItem?.ToString() ?? "Chưa Hoàn Thành"
                };

                if (calendarBUS.ThemLich(lich))
                {
                    MessageBox.Show("Thêm lịch làm việc mới thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachLich();
                    CaiDatTrangThaiMacDinh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Nghiệp Vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== CHỨC NĂNG CẬP NHẬT (SỬA) ====================\
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox1.Text))
                {
                    MessageBox.Show("Vui lòng chọn một lịch làm việc từ danh sách bên dưới trước khi sửa!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Tiêu đề lịch không được bỏ trống!", "Cảnh Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }

                CalendarViewDTO lich = new CalendarViewDTO
                {
                    MaLich = int.Parse(textBox1.Text),
                    TieuDe = textBox2.Text.Trim(),
                    DiaDiem = textBox3.Text.Trim(),
                    MoTa = textBox4.Text.Trim(),
                    ThoiGianBatDau = dateTimePicker1.Value,
                    ThoiGianKetThuc = dateTimePicker2.Value,
                    TrangThai = comboBox1.SelectedItem?.ToString() ?? "Chưa Hoàn Thành"
                };

                if (calendarBUS.SuaLich(lich))
                {
                    MessageBox.Show("Cập nhật thông tin lịch thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachLich();
                    CaiDatTrangThaiMacDinh();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi Nghiệp Vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== CHỨC NĂNG XÓA LỊCH ====================\
        private void button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text)) return;

            DialogResult confirm = MessageBox.Show($"Bạn có chắc chắn muốn xóa lịch làm việc này không?", "Xác Nhận Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm == DialogResult.Yes)
            {
                try
                {
                    int maLich = int.Parse(textBox1.Text);
                    if (calendarBUS.XoaLich(maLich))
                    {
                        MessageBox.Show("Xóa lịch làm việc thành công!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSachLich();
                        CaiDatTrangThaiMacDinh();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ==================== CHỨC NĂNG LÀM MỚI FORM ====================\
        private void button4_Click(object sender, EventArgs e)
        {
            CaiDatTrangThaiMacDinh();
            LoadDanhSachLich();
        }

        // ==================== CLICK CHỌN DÒNG TRÊN BẢNG (AN TOÀN TUYỆT ĐỐI) ====================\
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DuLieuDongDuocChon(e.RowIndex);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DuLieuDongDuocChon(e.RowIndex);
        }

        private void DuLieuDongDuocChon(int rowIndex)
        {
            if (rowIndex >= 0 && rowIndex < dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[rowIndex];
                if (row.Cells["MaLich"].Value == null || row.Cells["MaLich"].Value == DBNull.Value) return;

                textBox1.Text = row.Cells["MaLich"].Value?.ToString() ?? "";
                textBox2.Text = row.Cells["TieuDe"].Value?.ToString() ?? "";
                textBox3.Text = row.Cells["DiaDiem"].Value?.ToString() ?? "";
                textBox4.Text = row.Cells["MoTa"].Value?.ToString() ?? "";

                if (row.Cells["ThoiGianBatDau"].Value != null && row.Cells["ThoiGianBatDau"].Value != DBNull.Value)
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["ThoiGianBatDau"].Value);
                if (row.Cells["ThoiGianKetThuc"].Value != null && row.Cells["ThoiGianKetThuc"].Value != DBNull.Value)
                    dateTimePicker2.Value = Convert.ToDateTime(row.Cells["ThoiGianKetThuc"].Value);

                string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "Chưa Hoàn Thành";
                int index = comboBox1.FindStringExact(trangThai);
                if (index >= 0) comboBox1.SelectedIndex = index;
                else comboBox1.SelectedIndex = 0;

                button2.Enabled = true; // Bật nút Sửa
                button3.Enabled = true; // Bật nút Xóa
            }
        }
    }
}