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

namespace _.Net____PCCV
{
    public partial class frmProjectManager : Form
    {
        private ProjectManagerBUS duAnBUS = new ProjectManagerBUS();
        private bool isSuaMode = false; // Cờ đánh dấu đang ở chế độ Sửa

        public frmProjectManager()
        {
            InitializeComponent();
        }

        // ==================== SỰ KIỆN LOAD FORM ====================
        private void ProjectManager_Load(object sender, EventArgs e)
        {
            LoadDanhSachDuAn();
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
            DataGridViewTextBoxColumn colMaDA = new DataGridViewTextBoxColumn();
            colMaDA.HeaderText = "Mã Dự Án";
            colMaDA.DataPropertyName = "MaDA";
            colMaDA.Width = 80;
            dataGridView1.Columns.Add(colMaDA);

            DataGridViewTextBoxColumn colTenDA = new DataGridViewTextBoxColumn();
            colTenDA.HeaderText = "Tên Dự Án";
            colTenDA.DataPropertyName = "TenDA";
            colTenDA.Width = 200;
            dataGridView1.Columns.Add(colTenDA);

            DataGridViewTextBoxColumn colMoTa = new DataGridViewTextBoxColumn();
            colMoTa.HeaderText = "Mô Tả";
            colMoTa.DataPropertyName = "MoTa";
            colMoTa.Width = 250;
            dataGridView1.Columns.Add(colMoTa);

            DataGridViewTextBoxColumn colBatDau = new DataGridViewTextBoxColumn();
            colBatDau.HeaderText = "Ngày Bắt Đầu";
            colBatDau.DataPropertyName = "NgayBatDau";
            colBatDau.Width = 130;
            colBatDau.DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns.Add(colBatDau);

            DataGridViewTextBoxColumn colKetThuc = new DataGridViewTextBoxColumn();
            colKetThuc.HeaderText = "Ngày Kết Thúc";
            colKetThuc.DataPropertyName = "NgayKetThuc";
            colKetThuc.Width = 130;
            colKetThuc.DefaultCellStyle.Format = "dd/MM/yyyy";
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
            textBox1.ReadOnly = true; // Mã dự án chỉ đọc
            textBox1.BackColor = SystemColors.Control;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddMonths(1);
            comboBox1.SelectedIndex = 0; // Mặc định "Chưa Hoàn Thành"
            button3.Enabled = false; // Nút Cập Nhật ban đầu tắt
            isSuaMode = false;
        }

        // ==================== NẠP DỮ LIỆU LÊN DATAGRIDVIEW ====================
        private void LoadDanhSachDuAn()
        {
            try
            {
                DataTable dt = duAnBUS.LayDanhSachDuAn();
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách dự án: " + ex.Message, "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== XÓA TRẮNG CÁC Ô NHẬP LIỆU ====================
        private void XoaTrangNhapLieu()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddMonths(1);
            comboBox1.SelectedIndex = 0;
            isSuaMode = false;
            button3.Enabled = false;
            button2.Text = "Sửa Dự Án";
        }

        // ==================== CHUYỂN DỮ LIỆU TỪ FORM SANG DTO ====================
        private ProjectManagerDTO LayDuLieuTuForm()
        {
            ProjectManagerDTO duAn = new ProjectManagerDTO();
            if (!string.IsNullOrEmpty(textBox1.Text))
                duAn.MaDA = int.Parse(textBox1.Text.Trim());
            duAn.TenDA = textBox2.Text.Trim();
            duAn.MoTa = textBox3.Text.Trim();
            duAn.NgayBatDau = dateTimePicker1.Value.Date;
            duAn.NgayKetThuc = dateTimePicker2.Value.Date;
            duAn.TrangThai = comboBox1.SelectedItem?.ToString() ?? "Mới khởi tạo";
            return duAn;
        }

        // ==================== NÚT THÊM DỰ ÁN ====================
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tên Dự Án!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }
                if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
                {
                    MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ProjectManagerDTO duAn = LayDuLieuTuForm();
                bool result = duAnBUS.ThemDuAn(duAn);

                if (result)
                {
                    MessageBox.Show("Thêm dự án thành công!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachDuAn();
                    XoaTrangNhapLieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== NÚT SỬA DỰ ÁN (CHUYỂN SANG CHẾ ĐỘ SỬA) ====================
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dự án trong bảng để sửa!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!isSuaMode)
            {
                // Chuyển sang chế độ Sửa - nạp dữ liệu từ dòng đang chọn lên form
                DataGridViewRow row = dataGridView1.CurrentRow;
                textBox1.Text = row.Cells["MaDA"].Value?.ToString() ?? "";
                textBox2.Text = row.Cells["TenDA"].Value?.ToString() ?? "";
                textBox3.Text = row.Cells["MoTa"].Value?.ToString() ?? "";

                if (row.Cells["NgayBatDau"].Value != null && row.Cells["NgayBatDau"].Value != DBNull.Value)
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);
                if (row.Cells["NgayKetThuc"].Value != null && row.Cells["NgayKetThuc"].Value != DBNull.Value)
                    dateTimePicker2.Value = Convert.ToDateTime(row.Cells["NgayKetThuc"].Value);

                string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "Mới khởi tạo";
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

        // ==================== NÚT CẬP NHẬT DỰ ÁN ====================
        private void button3_Click(object sender, EventArgs e)
        {
            if (!isSuaMode)
            {
                MessageBox.Show("Vui lòng nhấn nút 'Sửa Dự Án' trước khi cập nhật!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Vui lòng nhập Tên Dự Án!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }
                if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
                {
                    MessageBox.Show("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                ProjectManagerDTO duAn = LayDuLieuTuForm();
                bool result = duAnBUS.SuaDuAn(duAn);

                if (result)
                {
                    MessageBox.Show("Cập nhật dự án thành công!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachDuAn();
                    XoaTrangNhapLieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== NÚT XÓA DỰ ÁN ====================
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn một dự án trong bảng để xóa!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = dataGridView1.CurrentRow;
            string maDA = row.Cells["MaDA"].Value?.ToString() ?? "";
            string tenDA = row.Cells["TenDA"].Value?.ToString() ?? "";

            DialogResult dlg = MessageBox.Show(
                "Bạn có chắc chắn muốn xóa dự án '" + tenDA + "' (Mã: " + maDA + ")?\nLưu ý: Tất cả công việc và thành viên thuộc dự án này cũng sẽ bị xóa!",
                "Xác nhận xóa",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (dlg == DialogResult.Yes)
            {
                try
                {
                    bool result = duAnBUS.XoaDuAn(int.Parse(maDA));
                    if (result)
                    {
                        MessageBox.Show("Xóa dự án thành công!", "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDanhSachDuAn();
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
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = row.Cells["MaDA"].Value?.ToString() ?? "";
                textBox2.Text = row.Cells["TenDA"].Value?.ToString() ?? "";
                textBox3.Text = row.Cells["MoTa"].Value?.ToString() ?? "";

                if (row.Cells["NgayBatDau"].Value != null && row.Cells["NgayBatDau"].Value != DBNull.Value)
                    dateTimePicker1.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);
                if (row.Cells["NgayKetThuc"].Value != null && row.Cells["NgayKetThuc"].Value != DBNull.Value)
                    dateTimePicker2.Value = Convert.ToDateTime(row.Cells["NgayKetThuc"].Value);

                string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "Mới khởi tạo";
                int index = comboBox1.Items.IndexOf(trangThai);
                comboBox1.SelectedIndex = index >= 0 ? index : 0;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}