using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using _Net____PCCV.BUS;
using _Net____PCCV.DTO;

namespace GUI
{
    public partial class frmKanbanBoard : Form
    {
        private readonly TaskBUS taskBUS = new TaskBUS();
        private bool suppressFilterReload;

        public frmKanbanBoard()
        {
            InitializeComponent();
            LoadBoLoc();
            LoadDanhSachCongViec();
        }

        private void btnTaoMoi_Click(object sender, EventArgs e)
        {
            using (frmTaskActions frmTaoMoi = new frmTaskActions(0))
            {
                if (frmTaoMoi.ShowDialog(this) == DialogResult.OK)
                {
                    LoadDanhSachCongViec();
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadDanhSachCongViec();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDanhSachCongViec();
        }

        private void btnXoaBoLoc_Click(object sender, EventArgs e)
        {
            ResetBoLoc();
            LoadDanhSachCongViec();
        }

        private void BoLoc_Changed(object sender, EventArgs e)
        {
            if (!suppressFilterReload)
            {
                LoadDanhSachCongViec();
            }
        }

        private void LoadBoLoc()
        {
            suppressFilterReload = true;

            cboLocUuTien.Items.Clear();
            cboLocUuTien.Items.Add("Tất cả");
            cboLocUuTien.Items.Add("Thấp");
            cboLocUuTien.Items.Add("Trung bình");
            cboLocUuTien.Items.Add("Cao");
            cboLocUuTien.SelectedIndex = 0;

            DataTable nhanVien = taskBUS.LayDanhSachNhanVien();
            DataRow allRow = nhanVien.NewRow();
            allRow["MaNV"] = 0;
            allRow["HoTen"] = "Tất cả";
            nhanVien.Rows.InsertAt(allRow, 0);

            cboLocNhanVien.DisplayMember = "HoTen";
            cboLocNhanVien.ValueMember = "MaNV";
            cboLocNhanVien.DataSource = nhanVien;
            cboLocNhanVien.SelectedIndex = 0;

            suppressFilterReload = false;
        }

        private void ResetBoLoc()
        {
            suppressFilterReload = true;
            txtTimKiem.Clear();

            if (cboLocUuTien.Items.Count > 0)
            {
                cboLocUuTien.SelectedIndex = 0;
            }

            if (cboLocNhanVien.Items.Count > 0)
            {
                cboLocNhanVien.SelectedIndex = 0;
            }

            suppressFilterReload = false;
        }

        private void LoadDanhSachCongViec()
        {
            ClearColumns();

            string mucDo = cboLocUuTien.SelectedIndex > 0 ? cboLocUuTien.SelectedItem.ToString() : null;
            int? maNV = null;
            if (cboLocNhanVien.SelectedValue != null && int.TryParse(cboLocNhanVien.SelectedValue.ToString(), out int selectedMaNV) && selectedMaNV > 0)
            {
                maNV = selectedMaNV;
            }

            DataTable dt = taskBUS.LayDanhSachCongViec(txtTimKiem.Text.Trim(), mucDo, maNV);
            foreach (DataRow row in dt.Rows)
            {
                TaskDTO task = taskBUS.MapTask(row);
                AddCardToColumn(task);
            }

            UpdateColumnTitles();
        }

        private void ClearColumns()
        {
            flpToDo.Controls.Clear();
            flpInProgress.Controls.Clear();
            flpDone.Controls.Clear();
        }

        private void AddCardToColumn(TaskDTO task)
        {
            FlowLayoutPanel target = GetColumnByStatus(task.TrangThai);
            target.Controls.Add(CreateTaskCard(task));
        }

        private Panel CreateTaskCard(TaskDTO task)
        {
            Panel card = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Cursor = Cursors.Hand,
                Margin = new Padding(8),
                Padding = new Padding(10),
                Size = new Size(310, 168),
                Tag = task
            };

            Label lblTenTask = new Label
            {
                AutoEllipsis = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Location = new Point(10, 8),
                Size = new Size(286, 42),
                Text = task.TenTask
            };

            Label lblMeta = new Label
            {
                ForeColor = Color.FromArgb(70, 70, 70),
                Location = new Point(10, 54),
                Size = new Size(286, 58),
                Text = $"Ưu tiên: {task.MucDoUuTien}\nNgười thực hiện: {task.TenNguoiNhan}\nHạn: {task.Deadline:dd/MM/yyyy}"
            };

            ProgressBar progressBar = new ProgressBar
            {
                Location = new Point(10, 114),
                Size = new Size(286, 12),
                Value = task.TienDo
            };

            Button btnChiTiet = new Button
            {
                Location = new Point(10, 132),
                Size = new Size(86, 26),
                Text = "Chi tiết",
                Tag = task.MaTask
            };
            btnChiTiet.Click += btnChiTiet_Click;

            Button btnSua = new Button
            {
                Location = new Point(106, 132),
                Size = new Size(72, 26),
                Text = "Sửa",
                Tag = task.MaTask
            };
            btnSua.Click += btnSua_Click;

            ComboBox cboTrangThai = new ComboBox
            {
                DropDownStyle = ComboBoxStyle.DropDownList,
                Location = new Point(188, 133),
                Size = new Size(108, 23),
                Tag = task.MaTask
            };
            cboTrangThai.Items.AddRange(new object[] { "To Do", "In Progress", "Done" });
            cboTrangThai.SelectedItem = task.TrangThai;
            cboTrangThai.SelectedIndexChanged += cboTrangThai_SelectedIndexChanged;

            card.Controls.Add(lblTenTask);
            card.Controls.Add(lblMeta);
            card.Controls.Add(progressBar);
            card.Controls.Add(btnChiTiet);
            card.Controls.Add(btnSua);
            card.Controls.Add(cboTrangThai);

            card.MouseDown += TaskCard_MouseDown;
            foreach (Control control in card.Controls)
            {
                if (!(control is Button) && !(control is ComboBox))
                {
                    control.MouseDown += TaskCard_MouseDown;
                }
            }

            return card;
        }

        private void TaskCard_MouseDown(object sender, MouseEventArgs e)
        {
            Control control = sender as Control;
            Panel card = control as Panel ?? control?.Parent as Panel;
            if (card?.Tag is TaskDTO && e.Button == MouseButtons.Left)
            {
                card.DoDragDrop(card, DragDropEffects.Move);
            }
        }

        private void KanbanColumn_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(typeof(Panel)) ? DragDropEffects.Move : DragDropEffects.None;
        }

        private void KanbanColumn_DragDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Panel)))
            {
                return;
            }

            Panel card = e.Data.GetData(typeof(Panel)) as Panel;
            FlowLayoutPanel targetColumn = sender as FlowLayoutPanel;
            if (!(card?.Tag is TaskDTO task) || targetColumn == null)
            {
                return;
            }

            string newStatus = targetColumn.Tag.ToString();
            if (task.TrangThai == newStatus)
            {
                return;
            }

            try
            {
                if (taskBUS.CapNhatTrangThai(task.MaTask, newStatus))
                {
                    LoadDanhSachCongViec();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi cập nhật trạng thái", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            if (comboBox?.Tag == null || comboBox.SelectedItem == null)
            {
                return;
            }

            int maTask = Convert.ToInt32(comboBox.Tag);
            string newStatus = comboBox.SelectedItem.ToString();
            TaskDTO task = taskBUS.LayCongViecTheoMa(maTask);
            if (task == null || task.TrangThai == newStatus)
            {
                return;
            }

            try
            {
                if (taskBUS.CapNhatTrangThai(maTask, newStatus))
                {
                    LoadDanhSachCongViec();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi cập nhật trạng thái", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            int maTask = Convert.ToInt32(((Button)sender).Tag);
            using (frmTaskActions frmSua = new frmTaskActions(maTask))
            {
                if (frmSua.ShowDialog(this) == DialogResult.OK)
                {
                    LoadDanhSachCongViec();
                }
            }
        }

        private void btnChiTiet_Click(object sender, EventArgs e)
        {
            int maTask = Convert.ToInt32(((Button)sender).Tag);
            using (frmTaskDetails frmDetails = new frmTaskDetails(maTask))
            {
                if (frmDetails.ShowDialog(this) == DialogResult.OK)
                {
                    LoadDanhSachCongViec();
                }
            }
        }

        private FlowLayoutPanel GetColumnByStatus(string status)
        {
            if (status == "Done")
            {
                return flpDone;
            }

            if (status == "In Progress")
            {
                return flpInProgress;
            }

            return flpToDo;
        }

        private void UpdateColumnTitles()
        {
            lblToDo.Text = $"CHƯA THỰC HIỆN ({flpToDo.Controls.Count})";
            lblInProgress.Text = $"ĐANG THỰC HIỆN ({flpInProgress.Controls.Count})";
            lblDone.Text = $"HOÀN THÀNH ({flpDone.Controls.Count})";
        }
    }
}
