using System;
using System.Data;
using System.Windows.Forms;
using QuanLyCongViec.BUS;
using QuanLyCongViec.DTO;

namespace QuanLyCongViec.GUI
{
    public partial class frmCalendarView : Form
    {
        private readonly CalendarViewBUS calendarBUS = new CalendarViewBUS();

        public frmCalendarView()
        {
            InitializeComponent();
        }

        private void CalendarView_Load(object sender, EventArgs e)
        {
            CaiDatDataGridView();
            CaiDatTrangThaiMacDinh();
            LoadDanhSachLich();
        }

        private void CaiDatDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(UiHelper.TextColumn("MaLichHienThi", "MaLichHienThi", "Mã lịch", 96, 18));
            dataGridView1.Columns.Add(UiHelper.TextColumn("TieuDe", "TieuDe", "Tiêu đề", 180, 28));
            dataGridView1.Columns.Add(UiHelper.TextColumn("DiaDiem", "DiaDiem", "Địa điểm", 130, 20));
            dataGridView1.Columns.Add(UiHelper.TextColumn("MoTa", "MoTa", "Mô tả", 220, 32));

            DataGridViewTextBoxColumn colBatDau = UiHelper.TextColumn("ThoiGianBatDau", "ThoiGianBatDau", "Bắt đầu", 140, 22);
            colBatDau.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dataGridView1.Columns.Add(colBatDau);

            DataGridViewTextBoxColumn colKetThuc = UiHelper.TextColumn("ThoiGianKetThuc", "ThoiGianKetThuc", "Kết thúc", 140, 22);
            colKetThuc.DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            dataGridView1.Columns.Add(colKetThuc);

            dataGridView1.Columns.Add(UiHelper.TextColumn("TrangThai", "TrangThai", "Trạng thái", 120, 18));
            dataGridView1.Columns.Add(UiHelper.TextColumn("MaLich", "MaLich", "ID", 60));
            dataGridView1.Columns["MaLich"].Visible = false;
            UiHelper.StyleGrid(dataGridView1);
        }

        private void LoadDanhSachLich()
        {
            try
            {
                DataTable dt = calendarBUS.LayDanhSachLich();
                dataGridView1.DataSource = dt;
                CapNhatMaLichTuDong(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không thể tải danh sách lịch: " + ex.Message, "Lỗi hệ thống", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatMaLichTuDong(DataTable dt)
        {
            int maxId = 0;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["MaLich"] != DBNull.Value && int.TryParse(row["MaLich"].ToString(), out int id) && id > maxId)
                    {
                        maxId = id;
                    }
                }
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text) || UiHelper.ParseCode(textBox1.Text) == 0)
            {
                textBox1.Text = UiHelper.FormatCode("LICH", maxId + 1);
            }
        }

        private void CaiDatTrangThaiMacDinh()
        {
            textBox1.ReadOnly = true;
            XoaTrangCacTruong();

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new object[] { "To Do", "In Progress", "Done" });
            comboBox1.SelectedIndex = 0;

            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void XoaTrangCacTruong()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddHours(1);
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
        }

        private CalendarViewDTO LayDuLieuTuForm()
        {
            return new CalendarViewDTO
            {
                MaLich = UiHelper.ParseCode(textBox1.Text),
                TieuDe = textBox2.Text.Trim(),
                DiaDiem = textBox3.Text.Trim(),
                MoTa = textBox4.Text.Trim(),
                ThoiGianBatDau = dateTimePicker1.Value,
                ThoiGianKetThuc = dateTimePicker2.Value,
                TrangThai = comboBox1.SelectedItem?.ToString() ?? "To Do"
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Tiêu đề lịch làm việc không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }

                if (calendarBUS.ThemLich(LayDuLieuTuForm()))
                {
                    MessageBox.Show("Thêm lịch làm việc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CaiDatTrangThaiMacDinh();
                    LoadDanhSachLich();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (UiHelper.ParseCode(textBox1.Text) <= 0)
                {
                    MessageBox.Show("Vui lòng chọn một lịch làm việc trước khi sửa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Tiêu đề lịch không được để trống.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }

                if (calendarBUS.SuaLich(LayDuLieuTuForm()))
                {
                    MessageBox.Show("Cập nhật lịch thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CaiDatTrangThaiMacDinh();
                    LoadDanhSachLich();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi nghiệp vụ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int maLich = UiHelper.ParseCode(textBox1.Text);
            if (maLich <= 0)
            {
                return;
            }

            DialogResult confirm = MessageBox.Show("Bạn có chắc chắn muốn xóa lịch làm việc này không?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirm != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (calendarBUS.XoaLich(maLich))
                {
                    MessageBox.Show("Xóa lịch làm việc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CaiDatTrangThaiMacDinh();
                    LoadDanhSachLich();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CaiDatTrangThaiMacDinh();
            LoadDanhSachLich();
        }

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
            if (rowIndex < 0 || rowIndex >= dataGridView1.Rows.Count)
            {
                return;
            }

            DataGridViewRow row = dataGridView1.Rows[rowIndex];
            if (row.Cells["MaLich"].Value == null || row.Cells["MaLich"].Value == DBNull.Value)
            {
                return;
            }

            textBox1.Text = row.Cells["MaLichHienThi"].Value?.ToString() ?? UiHelper.FormatCode("LICH", row.Cells["MaLich"].Value);
            textBox2.Text = row.Cells["TieuDe"].Value?.ToString() ?? "";
            textBox3.Text = row.Cells["DiaDiem"].Value?.ToString() ?? "";
            textBox4.Text = row.Cells["MoTa"].Value?.ToString() ?? "";

            if (row.Cells["ThoiGianBatDau"].Value != null && row.Cells["ThoiGianBatDau"].Value != DBNull.Value)
            {
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["ThoiGianBatDau"].Value);
            }

            if (row.Cells["ThoiGianKetThuc"].Value != null && row.Cells["ThoiGianKetThuc"].Value != DBNull.Value)
            {
                dateTimePicker2.Value = Convert.ToDateTime(row.Cells["ThoiGianKetThuc"].Value);
            }

            string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "To Do";
            int index = comboBox1.FindStringExact(trangThai);
            comboBox1.SelectedIndex = index >= 0 ? index : 0;

            button2.Enabled = true;
            button3.Enabled = true;
        }
    }
}
