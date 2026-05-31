using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    partial class frmKanbanBoard
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private TableLayoutPanel tlpHeader;
        private Label lblTieuDe;
        private FlowLayoutPanel flpActions;
        private Button btnTaoMoi;
        private Button btnLamMoi;
        private FlowLayoutPanel flpFilter;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Label lblLocUuTien;
        private ComboBox cboLocUuTien;
        private Label lblLocNhanVien;
        private ComboBox cboLocNhanVien;
        private Button btnTimKiem;
        private Button btnXoaBoLoc;
        private TableLayoutPanel tlpBangCongViec;
        private Panel pnlToDo;
        private Panel pnlInProgress;
        private Panel pnlDone;
        private Label lblToDo;
        private Label lblInProgress;
        private Label lblDone;
        private FlowLayoutPanel flpToDo;
        private FlowLayoutPanel flpInProgress;
        private FlowLayoutPanel flpDone;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            pnlHeader = new Panel();
            tlpHeader = new TableLayoutPanel();
            lblTieuDe = new Label();
            flpActions = new FlowLayoutPanel();
            btnTaoMoi = new Button();
            btnLamMoi = new Button();
            flpFilter = new FlowLayoutPanel();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            lblLocUuTien = new Label();
            cboLocUuTien = new ComboBox();
            lblLocNhanVien = new Label();
            cboLocNhanVien = new ComboBox();
            btnTimKiem = new Button();
            btnXoaBoLoc = new Button();
            tlpBangCongViec = new TableLayoutPanel();
            pnlToDo = new Panel();
            lblToDo = new Label();
            flpToDo = new FlowLayoutPanel();
            pnlInProgress = new Panel();
            lblInProgress = new Label();
            flpInProgress = new FlowLayoutPanel();
            pnlDone = new Panel();
            lblDone = new Label();
            flpDone = new FlowLayoutPanel();
            pnlHeader.SuspendLayout();
            tlpHeader.SuspendLayout();
            flpActions.SuspendLayout();
            flpFilter.SuspendLayout();
            tlpBangCongViec.SuspendLayout();
            pnlToDo.SuspendLayout();
            pnlInProgress.SuspendLayout();
            pnlDone.SuspendLayout();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(tlpHeader);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(16, 12, 16, 10);
            pnlHeader.Size = new Size(1184, 142);
            pnlHeader.TabIndex = 0;
            // 
            // tlpHeader
            // 
            tlpHeader.ColumnCount = 2;
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpHeader.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 260F));
            tlpHeader.Controls.Add(lblTieuDe, 0, 0);
            tlpHeader.Controls.Add(flpActions, 1, 0);
            tlpHeader.Controls.Add(flpFilter, 0, 1);
            tlpHeader.Dock = DockStyle.Fill;
            tlpHeader.Location = new Point(16, 12);
            tlpHeader.Name = "tlpHeader";
            tlpHeader.RowCount = 2;
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
            tlpHeader.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpHeader.Size = new Size(1152, 120);
            tlpHeader.TabIndex = 0;
            tlpHeader.SetColumnSpan(flpFilter, 2);
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoEllipsis = true;
            lblTieuDe.Dock = DockStyle.Fill;
            lblTieuDe.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            lblTieuDe.Location = new Point(3, 0);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(886, 48);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "BẢNG CÔNG VIỆC";
            lblTieuDe.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // flpActions
            // 
            flpActions.Controls.Add(btnLamMoi);
            flpActions.Controls.Add(btnTaoMoi);
            flpActions.Dock = DockStyle.Fill;
            flpActions.FlowDirection = FlowDirection.RightToLeft;
            flpActions.Location = new Point(895, 3);
            flpActions.Name = "flpActions";
            flpActions.Padding = new Padding(0, 4, 0, 0);
            flpActions.Size = new Size(254, 42);
            flpActions.TabIndex = 1;
            flpActions.WrapContents = false;
            // 
            // btnTaoMoi
            // 
            btnTaoMoi.BackColor = Color.FromArgb(30, 120, 215);
            btnTaoMoi.FlatAppearance.BorderSize = 0;
            btnTaoMoi.FlatStyle = FlatStyle.Flat;
            btnTaoMoi.ForeColor = Color.White;
            btnTaoMoi.Location = new Point(14, 7);
            btnTaoMoi.Margin = new Padding(8, 3, 0, 3);
            btnTaoMoi.Name = "btnTaoMoi";
            btnTaoMoi.Size = new Size(112, 32);
            btnTaoMoi.TabIndex = 1;
            btnTaoMoi.Text = "Tạo mới";
            btnTaoMoi.UseVisualStyleBackColor = false;
            btnTaoMoi.Click += btnTaoMoi_Click;
            // 
            // btnLamMoi
            // 
            btnLamMoi.Location = new Point(134, 7);
            btnLamMoi.Margin = new Padding(8, 3, 0, 3);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(112, 32);
            btnLamMoi.TabIndex = 0;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;
            // 
            // flpFilter
            // 
            flpFilter.Controls.Add(lblTimKiem);
            flpFilter.Controls.Add(txtTimKiem);
            flpFilter.Controls.Add(lblLocUuTien);
            flpFilter.Controls.Add(cboLocUuTien);
            flpFilter.Controls.Add(lblLocNhanVien);
            flpFilter.Controls.Add(cboLocNhanVien);
            flpFilter.Controls.Add(btnTimKiem);
            flpFilter.Controls.Add(btnXoaBoLoc);
            flpFilter.Dock = DockStyle.Fill;
            flpFilter.Location = new Point(3, 51);
            flpFilter.Name = "flpFilter";
            flpFilter.Padding = new Padding(0, 8, 0, 0);
            flpFilter.Size = new Size(1146, 66);
            flpFilter.TabIndex = 2;
            flpFilter.WrapContents = true;
            // 
            // lblTimKiem
            // 
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(0, 14);
            lblTimKiem.Margin = new Padding(0, 14, 8, 0);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(55, 15);
            lblTimKiem.TabIndex = 0;
            lblTimKiem.Text = "Tìm kiếm";
            // 
            // txtTimKiem
            // 
            txtTimKiem.Location = new Point(63, 10);
            txtTimKiem.Margin = new Padding(0, 10, 18, 0);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.Size = new Size(260, 23);
            txtTimKiem.TabIndex = 1;
            // 
            // lblLocUuTien
            // 
            lblLocUuTien.AutoSize = true;
            lblLocUuTien.Location = new Point(341, 14);
            lblLocUuTien.Margin = new Padding(0, 14, 8, 0);
            lblLocUuTien.Name = "lblLocUuTien";
            lblLocUuTien.Size = new Size(44, 15);
            lblLocUuTien.TabIndex = 2;
            lblLocUuTien.Text = "Ưu tiên";
            // 
            // cboLocUuTien
            // 
            cboLocUuTien.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocUuTien.Location = new Point(393, 10);
            cboLocUuTien.Margin = new Padding(0, 10, 18, 0);
            cboLocUuTien.Name = "cboLocUuTien";
            cboLocUuTien.Size = new Size(130, 23);
            cboLocUuTien.TabIndex = 3;
            cboLocUuTien.SelectedIndexChanged += BoLoc_Changed;
            // 
            // lblLocNhanVien
            // 
            lblLocNhanVien.AutoSize = true;
            lblLocNhanVien.Location = new Point(541, 14);
            lblLocNhanVien.Margin = new Padding(0, 14, 8, 0);
            lblLocNhanVien.Name = "lblLocNhanVien";
            lblLocNhanVien.Size = new Size(92, 15);
            lblLocNhanVien.TabIndex = 4;
            lblLocNhanVien.Text = "Người thực hiện";
            // 
            // cboLocNhanVien
            // 
            cboLocNhanVien.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLocNhanVien.Location = new Point(641, 10);
            cboLocNhanVien.Margin = new Padding(0, 10, 18, 0);
            cboLocNhanVien.Name = "cboLocNhanVien";
            cboLocNhanVien.Size = new Size(210, 23);
            cboLocNhanVien.TabIndex = 5;
            cboLocNhanVien.SelectedIndexChanged += BoLoc_Changed;
            // 
            // btnTimKiem
            // 
            btnTimKiem.Location = new Point(869, 8);
            btnTimKiem.Margin = new Padding(0, 8, 10, 0);
            btnTimKiem.Name = "btnTimKiem";
            btnTimKiem.Size = new Size(86, 30);
            btnTimKiem.TabIndex = 6;
            btnTimKiem.Text = "Lọc";
            btnTimKiem.UseVisualStyleBackColor = true;
            btnTimKiem.Click += btnTimKiem_Click;
            // 
            // btnXoaBoLoc
            // 
            btnXoaBoLoc.Location = new Point(965, 8);
            btnXoaBoLoc.Margin = new Padding(0, 8, 0, 0);
            btnXoaBoLoc.Name = "btnXoaBoLoc";
            btnXoaBoLoc.Size = new Size(112, 30);
            btnXoaBoLoc.TabIndex = 7;
            btnXoaBoLoc.Text = "Xóa bộ lọc";
            btnXoaBoLoc.UseVisualStyleBackColor = true;
            btnXoaBoLoc.Click += btnXoaBoLoc_Click;
            // 
            // tlpBangCongViec
            // 
            tlpBangCongViec.ColumnCount = 3;
            tlpBangCongViec.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpBangCongViec.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpBangCongViec.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
            tlpBangCongViec.Controls.Add(pnlToDo, 0, 0);
            tlpBangCongViec.Controls.Add(pnlInProgress, 1, 0);
            tlpBangCongViec.Controls.Add(pnlDone, 2, 0);
            tlpBangCongViec.Dock = DockStyle.Fill;
            tlpBangCongViec.Location = new Point(0, 142);
            tlpBangCongViec.Name = "tlpBangCongViec";
            tlpBangCongViec.Padding = new Padding(12);
            tlpBangCongViec.RowCount = 1;
            tlpBangCongViec.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpBangCongViec.Size = new Size(1184, 519);
            tlpBangCongViec.TabIndex = 1;
            // 
            // pnlToDo
            // 
            pnlToDo.BackColor = Color.White;
            pnlToDo.BorderStyle = BorderStyle.FixedSingle;
            pnlToDo.Controls.Add(flpToDo);
            pnlToDo.Controls.Add(lblToDo);
            pnlToDo.Dock = DockStyle.Fill;
            pnlToDo.Location = new Point(16, 16);
            pnlToDo.Margin = new Padding(4);
            pnlToDo.Name = "pnlToDo";
            pnlToDo.Padding = new Padding(10);
            pnlToDo.Size = new Size(378, 487);
            pnlToDo.TabIndex = 0;
            // 
            // lblToDo
            // 
            lblToDo.BackColor = Color.FromArgb(245, 248, 252);
            lblToDo.Dock = DockStyle.Top;
            lblToDo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblToDo.Location = new Point(10, 10);
            lblToDo.Name = "lblToDo";
            lblToDo.Size = new Size(356, 40);
            lblToDo.TabIndex = 0;
            lblToDo.Text = "CHƯA THỰC HIỆN";
            lblToDo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flpToDo
            // 
            flpToDo.AllowDrop = true;
            flpToDo.AutoScroll = true;
            flpToDo.BackColor = Color.FromArgb(248, 250, 252);
            flpToDo.Dock = DockStyle.Fill;
            flpToDo.FlowDirection = FlowDirection.TopDown;
            flpToDo.Location = new Point(10, 50);
            flpToDo.Name = "flpToDo";
            flpToDo.Size = new Size(356, 425);
            flpToDo.TabIndex = 1;
            flpToDo.Tag = "To Do";
            flpToDo.WrapContents = false;
            flpToDo.DragDrop += KanbanColumn_DragDrop;
            flpToDo.DragEnter += KanbanColumn_DragEnter;
            // 
            // pnlInProgress
            // 
            pnlInProgress.BackColor = Color.White;
            pnlInProgress.BorderStyle = BorderStyle.FixedSingle;
            pnlInProgress.Controls.Add(flpInProgress);
            pnlInProgress.Controls.Add(lblInProgress);
            pnlInProgress.Dock = DockStyle.Fill;
            pnlInProgress.Location = new Point(402, 16);
            pnlInProgress.Margin = new Padding(4);
            pnlInProgress.Name = "pnlInProgress";
            pnlInProgress.Padding = new Padding(10);
            pnlInProgress.Size = new Size(378, 487);
            pnlInProgress.TabIndex = 1;
            // 
            // lblInProgress
            // 
            lblInProgress.BackColor = Color.FromArgb(255, 247, 230);
            lblInProgress.Dock = DockStyle.Top;
            lblInProgress.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblInProgress.Location = new Point(10, 10);
            lblInProgress.Name = "lblInProgress";
            lblInProgress.Size = new Size(356, 40);
            lblInProgress.TabIndex = 0;
            lblInProgress.Text = "ĐANG THỰC HIỆN";
            lblInProgress.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flpInProgress
            // 
            flpInProgress.AllowDrop = true;
            flpInProgress.AutoScroll = true;
            flpInProgress.BackColor = Color.FromArgb(255, 251, 235);
            flpInProgress.Dock = DockStyle.Fill;
            flpInProgress.FlowDirection = FlowDirection.TopDown;
            flpInProgress.Location = new Point(10, 50);
            flpInProgress.Name = "flpInProgress";
            flpInProgress.Size = new Size(356, 425);
            flpInProgress.TabIndex = 1;
            flpInProgress.Tag = "In Progress";
            flpInProgress.WrapContents = false;
            flpInProgress.DragDrop += KanbanColumn_DragDrop;
            flpInProgress.DragEnter += KanbanColumn_DragEnter;
            // 
            // pnlDone
            // 
            pnlDone.BackColor = Color.White;
            pnlDone.BorderStyle = BorderStyle.FixedSingle;
            pnlDone.Controls.Add(flpDone);
            pnlDone.Controls.Add(lblDone);
            pnlDone.Dock = DockStyle.Fill;
            pnlDone.Location = new Point(788, 16);
            pnlDone.Margin = new Padding(4);
            pnlDone.Name = "pnlDone";
            pnlDone.Padding = new Padding(10);
            pnlDone.Size = new Size(380, 487);
            pnlDone.TabIndex = 2;
            // 
            // lblDone
            // 
            lblDone.BackColor = Color.FromArgb(236, 253, 245);
            lblDone.Dock = DockStyle.Top;
            lblDone.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblDone.Location = new Point(10, 10);
            lblDone.Name = "lblDone";
            lblDone.Size = new Size(358, 40);
            lblDone.TabIndex = 0;
            lblDone.Text = "HOÀN THÀNH";
            lblDone.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // flpDone
            // 
            flpDone.AllowDrop = true;
            flpDone.AutoScroll = true;
            flpDone.BackColor = Color.FromArgb(240, 253, 244);
            flpDone.Dock = DockStyle.Fill;
            flpDone.FlowDirection = FlowDirection.TopDown;
            flpDone.Location = new Point(10, 50);
            flpDone.Name = "flpDone";
            flpDone.Size = new Size(358, 425);
            flpDone.TabIndex = 1;
            flpDone.Tag = "Done";
            flpDone.WrapContents = false;
            flpDone.DragDrop += KanbanColumn_DragDrop;
            flpDone.DragEnter += KanbanColumn_DragEnter;
            // 
            // frmKanbanBoard
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            ClientSize = new Size(1184, 661);
            Controls.Add(tlpBangCongViec);
            Controls.Add(pnlHeader);
            MinimumSize = new Size(1120, 620);
            Name = "frmKanbanBoard";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bảng công việc";
            pnlHeader.ResumeLayout(false);
            tlpHeader.ResumeLayout(false);
            flpActions.ResumeLayout(false);
            flpFilter.ResumeLayout(false);
            flpFilter.PerformLayout();
            tlpBangCongViec.ResumeLayout(false);
            pnlToDo.ResumeLayout(false);
            pnlInProgress.ResumeLayout(false);
            pnlDone.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
