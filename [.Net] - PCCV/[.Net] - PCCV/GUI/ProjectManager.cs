using System;
using System.Data;
using System.Windows.Forms;
using _Net____PCCV.BUS;
using _Net____PCCV.DTO;
using GUI;

namespace _.Net____PCCV
{
    public partial class ProjectManager : Form
    {
        private readonly ProjectManagerBUS duAnBUS = new ProjectManagerBUS();
        private bool isSuaMode;

        public ProjectManager()
        {
            InitializeComponent();
        }

        private void ProjectManager_Load(object sender, EventArgs e)
        {
            CaiDatDataGridView();
            CaiDatTrangThaiMacDinh();
            LoadDanhSachDuAn();
        }

        private void CaiDatDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add(UiHelper.TextColumn("MaDAHienThi", "MaDAHienThi", "MĂ£ dá»± Ă¡n", 100, 16));
            dataGridView1.Columns.Add(UiHelper.TextColumn("MaDA", "MaDA", "ID", 60));
            dataGridView1.Columns["MaDA"].Visible = false;
            dataGridView1.Columns.Add(UiHelper.TextColumn("TenDA", "TenDA", "TĂªn dá»± Ă¡n", 220, 28));
            dataGridView1.Columns.Add(UiHelper.TextColumn("MoTa", "MoTa", "MĂ´ táº£", 260, 32));

            DataGridViewTextBoxColumn colBatDau = UiHelper.TextColumn("NgayBatDau", "NgayBatDau", "Ngày bắt đầu", 140, 18);
            colBatDau.DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns.Add(colBatDau);

            DataGridViewTextBoxColumn colKetThuc = UiHelper.TextColumn("NgayKetThuc", "NgayKetThuc", "Ngày kết thúc", 140, 18);
            colKetThuc.DefaultCellStyle.Format = "dd/MM/yyyy";
            dataGridView1.Columns.Add(colKetThuc);

            dataGridView1.Columns.Add(UiHelper.TextColumn("TrangThai", "TrangThai", "Trạng thái", 140, 18));
            UiHelper.StyleGrid(dataGridView1);
        }

        private void CaiDatTrangThaiMacDinh()
        {
            textBox1.ReadOnly = true;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now.AddMonths(1);

            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(new object[] { "Má»›i khá»Ÿi táº¡o", "Äang cháº¡y", "HoĂ n thĂ nh", "Táº¡m dá»«ng" });
            comboBox1.SelectedIndex = 0;

            button3.Enabled = false;
            isSuaMode = false;
        }

        private void LoadDanhSachDuAn()
        {
            try
            {
                DataTable dt = duAnBUS.LayDanhSachDuAn();
                dataGridView1.DataSource = dt;
                CapNhatMaDuAnTuDong(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lá»—i khi táº£i danh sĂ¡ch dá»± Ă¡n: " + ex.Message, "Há»‡ thá»‘ng", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CapNhatMaDuAnTuDong(DataTable dt)
        {
            int maxId = 0;
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    if (row["MaDA"] != DBNull.Value && int.TryParse(row["MaDA"].ToString(), out int id) && id > maxId)
                    {
                        maxId = id;
                    }
                }
            }

            if (!isSuaMode)
            {
                textBox1.Text = UiHelper.FormatCode("DA", maxId + 1);
            }
        }

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
            button2.Text = "Sá»­a Dá»± Ăn";
            CapNhatMaDuAnTuDong(dataGridView1.DataSource as DataTable);
        }

        private ProjectManagerDTO LayDuLieuTuForm()
        {
            return new ProjectManagerDTO
            {
                MaDA = UiHelper.ParseCode(textBox1.Text),
                TenDA = textBox2.Text.Trim(),
                MoTa = textBox3.Text.Trim(),
                NgayBatDau = dateTimePicker1.Value.Date,
                NgayKetThuc = dateTimePicker2.Value.Date,
                TrangThai = comboBox1.SelectedItem?.ToString() ?? "Má»›i khá»Ÿi táº¡o"
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBox2.Text))
                {
                    MessageBox.Show("Vui lĂ²ng nháº­p tĂªn dá»± Ă¡n.", "ThĂ´ng bĂ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.Focus();
                    return;
                }

                if (dateTimePicker2.Value.Date < dateTimePicker1.Value.Date)
                {
                    MessageBox.Show("NgĂ y káº¿t thĂºc pháº£i lá»›n hÆ¡n hoáº·c báº±ng ngĂ y báº¯t Ä‘áº§u.", "ThĂ´ng bĂ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (duAnBUS.ThemDuAn(LayDuLieuTuForm()))
                {
                    MessageBox.Show("ThĂªm dá»± Ă¡n thĂ nh cĂ´ng.", "ThĂ´ng bĂ¡o", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachDuAn();
                    XoaTrangNhapLieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lĂ²ng chá»n má»™t dá»± Ă¡n trong báº£ng Ä‘á»ƒ sá»­a.", "ThĂ´ng bĂ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!isSuaMode)
            {
                DoDuLieuDongDuocChon(dataGridView1.CurrentRow);
                isSuaMode = true;
                button3.Enabled = true;
                button2.Text = "Há»§y Sá»­a";
            }
            else
            {
                XoaTrangNhapLieu();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!isSuaMode)
            {
                MessageBox.Show("Vui lĂ²ng nháº¥n nĂºt Sá»­a Dá»± Ăn trÆ°á»›c khi cáº­p nháº­t.", "ThĂ´ng bĂ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                if (duAnBUS.SuaDuAn(LayDuLieuTuForm()))
                {
                    MessageBox.Show("Cáº­p nháº­t dá»± Ă¡n thĂ nh cĂ´ng.", "ThĂ´ng bĂ¡o", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachDuAn();
                    XoaTrangNhapLieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("Vui lĂ²ng chá»n má»™t dá»± Ă¡n trong báº£ng Ä‘á»ƒ xĂ³a.", "ThĂ´ng bĂ¡o", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maDA = dataGridView1.CurrentRow.Cells["MaDA"].Value?.ToString() ?? "";
            string tenDA = dataGridView1.CurrentRow.Cells["TenDA"].Value?.ToString() ?? "";
            DialogResult dlg = MessageBox.Show("Báº¡n cĂ³ cháº¯c cháº¯n muá»‘n xĂ³a dá»± Ă¡n '" + tenDA + "' (" + UiHelper.FormatCode("DA", maDA) + ")?", "XĂ¡c nháº­n xĂ³a", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dlg != DialogResult.Yes)
            {
                return;
            }

            try
            {
                if (duAnBUS.XoaDuAn(int.Parse(maDA)))
                {
                    MessageBox.Show("XĂ³a dá»± Ă¡n thĂ nh cĂ´ng.", "ThĂ´ng bĂ¡o", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDanhSachDuAn();
                    XoaTrangNhapLieu();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lá»—i", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==================== CLICK VÀO DÒNG DATAGRIDVIEW ====================
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DoDuLieuDongDuocChon(dataGridView1.Rows[e.RowIndex]);
            }
        }

        private void DoDuLieuDongDuocChon(DataGridViewRow row)
        {
            textBox1.Text = row.Cells["MaDAHienThi"].Value?.ToString() ?? UiHelper.FormatCode("DA", row.Cells["MaDA"].Value);
            textBox2.Text = row.Cells["TenDA"].Value?.ToString() ?? "";
            textBox3.Text = row.Cells["MoTa"].Value?.ToString() ?? "";

            if (row.Cells["NgayBatDau"].Value != null && row.Cells["NgayBatDau"].Value != DBNull.Value)
            {
                dateTimePicker1.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);
            }

            if (row.Cells["NgayKetThuc"].Value != null && row.Cells["NgayKetThuc"].Value != DBNull.Value)
            {
                dateTimePicker2.Value = Convert.ToDateTime(row.Cells["NgayKetThuc"].Value);
            }

            string trangThai = row.Cells["TrangThai"].Value?.ToString() ?? "Má»›i khá»Ÿi táº¡o";
            int index = comboBox1.Items.IndexOf(trangThai);
            comboBox1.SelectedIndex = index >= 0 ? index : 0;
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
