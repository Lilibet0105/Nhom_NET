using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
partial class frmKanbanBoard
{
    private System.ComponentModel.IContainer components = null;

    private Panel pnlHeader;
    private Label lblTieuDe;
    private Button btnTaoMoi;
    private Button btnLamMoi;
    private TableLayoutPanel tlpBangCongViec;
    private Panel pnlCanLam;
    private Panel pnlDangLam;
    private Panel pnlHoanThanh;
    private Label lblCanLam;
    private Label lblDangLam;
    private Label lblHoanThanh;
    private DataGridView dgvCanLam;
    private DataGridView dgvDangLam;
    private DataGridView dgvHoanThanh;

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
        lblTieuDe = new Label();
        btnTaoMoi = new Button();
        btnLamMoi = new Button();
        tlpBangCongViec = new TableLayoutPanel();
        pnlCanLam = new Panel();
        lblCanLam = new Label();
        dgvCanLam = new DataGridView();
        pnlDangLam = new Panel();
        lblDangLam = new Label();
        dgvDangLam = new DataGridView();
        pnlHoanThanh = new Panel();
        lblHoanThanh = new Label();
        dgvHoanThanh = new DataGridView();
        pnlHeader.SuspendLayout();
        tlpBangCongViec.SuspendLayout();
        pnlCanLam.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvCanLam).BeginInit();
        pnlDangLam.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvDangLam).BeginInit();
        pnlHoanThanh.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)dgvHoanThanh).BeginInit();
        SuspendLayout();
        // 
        // pnlHeader
        // 
        pnlHeader.BackColor = Color.White;
        pnlHeader.Controls.Add(lblTieuDe);
        pnlHeader.Controls.Add(btnTaoMoi);
        pnlHeader.Controls.Add(btnLamMoi);
        pnlHeader.Dock = DockStyle.Top;
        pnlHeader.Location = new Point(0, 0);
        pnlHeader.Name = "pnlHeader";
        pnlHeader.Padding = new Padding(16, 12, 16, 12);
        pnlHeader.Size = new Size(1184, 72);
        pnlHeader.TabIndex = 0;
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTieuDe.Location = new Point(16, 19);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(230, 32);
        lblTieuDe.TabIndex = 0;
        lblTieuDe.Text = "BẢNG CÔNG VIỆC";
        // 
        // btnTaoMoi
        // 
        btnTaoMoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTaoMoi.BackColor = Color.FromArgb(30, 120, 215);
        btnTaoMoi.FlatStyle = FlatStyle.Flat;
        btnTaoMoi.ForeColor = Color.White;
        btnTaoMoi.Location = new Point(930, 18);
        btnTaoMoi.Name = "btnTaoMoi";
        btnTaoMoi.Size = new Size(112, 36);
        btnTaoMoi.TabIndex = 1;
        btnTaoMoi.Text = "Tạo mới";
        btnTaoMoi.UseVisualStyleBackColor = false;
        btnTaoMoi.Click += btnTaoMoi_Click;
        // 
        // btnLamMoi
        // 
        btnLamMoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnLamMoi.Location = new Point(1056, 18);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(112, 36);
        btnLamMoi.TabIndex = 2;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = true;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // tlpBangCongViec
        // 
        tlpBangCongViec.ColumnCount = 3;
        tlpBangCongViec.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tlpBangCongViec.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tlpBangCongViec.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.3333321F));
        tlpBangCongViec.Controls.Add(pnlCanLam, 0, 0);
        tlpBangCongViec.Controls.Add(pnlDangLam, 1, 0);
        tlpBangCongViec.Controls.Add(pnlHoanThanh, 2, 0);
        tlpBangCongViec.Dock = DockStyle.Fill;
        tlpBangCongViec.Location = new Point(0, 72);
        tlpBangCongViec.Name = "tlpBangCongViec";
        tlpBangCongViec.Padding = new Padding(12);
        tlpBangCongViec.RowCount = 1;
        tlpBangCongViec.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tlpBangCongViec.Size = new Size(1184, 589);
        tlpBangCongViec.TabIndex = 1;
        // 
        // pnlCanLam
        // 
        pnlCanLam.BackColor = Color.White;
        pnlCanLam.BorderStyle = BorderStyle.FixedSingle;
        pnlCanLam.Controls.Add(dgvCanLam);
        pnlCanLam.Controls.Add(lblCanLam);
        pnlCanLam.Dock = DockStyle.Fill;
        pnlCanLam.Location = new Point(15, 15);
        pnlCanLam.Name = "pnlCanLam";
        pnlCanLam.Padding = new Padding(10);
        pnlCanLam.Size = new Size(380, 559);
        pnlCanLam.TabIndex = 0;
        // 
        // lblCanLam
        // 
        lblCanLam.BackColor = Color.FromArgb(245, 248, 252);
        lblCanLam.Dock = DockStyle.Top;
        lblCanLam.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblCanLam.Location = new Point(10, 10);
        lblCanLam.Name = "lblCanLam";
        lblCanLam.Size = new Size(358, 42);
        lblCanLam.TabIndex = 0;
        lblCanLam.Text = "CẦN LÀM";
        lblCanLam.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dgvCanLam
        // 
        dgvCanLam.AllowUserToAddRows = false;
        dgvCanLam.AllowUserToDeleteRows = false;
        dgvCanLam.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvCanLam.BackgroundColor = Color.White;
        dgvCanLam.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvCanLam.Dock = DockStyle.Fill;
        dgvCanLam.Location = new Point(10, 52);
        dgvCanLam.Name = "dgvCanLam";
        dgvCanLam.ReadOnly = true;
        dgvCanLam.RowHeadersVisible = false;
        dgvCanLam.Size = new Size(358, 495);
        dgvCanLam.TabIndex = 1;
        CreateTaskColumns(dgvCanLam, "CanLam");
        // 
        // pnlDangLam
        // 
        pnlDangLam.BackColor = Color.White;
        pnlDangLam.BorderStyle = BorderStyle.FixedSingle;
        pnlDangLam.Controls.Add(dgvDangLam);
        pnlDangLam.Controls.Add(lblDangLam);
        pnlDangLam.Dock = DockStyle.Fill;
        pnlDangLam.Location = new Point(401, 15);
        pnlDangLam.Name = "pnlDangLam";
        pnlDangLam.Padding = new Padding(10);
        pnlDangLam.Size = new Size(380, 559);
        pnlDangLam.TabIndex = 1;
        // 
        // lblDangLam
        // 
        lblDangLam.BackColor = Color.FromArgb(255, 247, 230);
        lblDangLam.Dock = DockStyle.Top;
        lblDangLam.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblDangLam.Location = new Point(10, 10);
        lblDangLam.Name = "lblDangLam";
        lblDangLam.Size = new Size(358, 42);
        lblDangLam.TabIndex = 0;
        lblDangLam.Text = "ĐANG LÀM";
        lblDangLam.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dgvDangLam
        // 
        dgvDangLam.AllowUserToAddRows = false;
        dgvDangLam.AllowUserToDeleteRows = false;
        dgvDangLam.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvDangLam.BackgroundColor = Color.White;
        dgvDangLam.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvDangLam.Dock = DockStyle.Fill;
        dgvDangLam.Location = new Point(10, 52);
        dgvDangLam.Name = "dgvDangLam";
        dgvDangLam.ReadOnly = true;
        dgvDangLam.RowHeadersVisible = false;
        dgvDangLam.Size = new Size(358, 495);
        dgvDangLam.TabIndex = 1;
        CreateTaskColumns(dgvDangLam, "DangLam");
        // 
        // pnlHoanThanh
        // 
        pnlHoanThanh.BackColor = Color.White;
        pnlHoanThanh.BorderStyle = BorderStyle.FixedSingle;
        pnlHoanThanh.Controls.Add(dgvHoanThanh);
        pnlHoanThanh.Controls.Add(lblHoanThanh);
        pnlHoanThanh.Dock = DockStyle.Fill;
        pnlHoanThanh.Location = new Point(787, 15);
        pnlHoanThanh.Name = "pnlHoanThanh";
        pnlHoanThanh.Padding = new Padding(10);
        pnlHoanThanh.Size = new Size(382, 559);
        pnlHoanThanh.TabIndex = 2;
        // 
        // lblHoanThanh
        // 
        lblHoanThanh.BackColor = Color.FromArgb(236, 253, 245);
        lblHoanThanh.Dock = DockStyle.Top;
        lblHoanThanh.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
        lblHoanThanh.Location = new Point(10, 10);
        lblHoanThanh.Name = "lblHoanThanh";
        lblHoanThanh.Size = new Size(360, 42);
        lblHoanThanh.TabIndex = 0;
        lblHoanThanh.Text = "HOÀN THÀNH";
        lblHoanThanh.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // dgvHoanThanh
        // 
        dgvHoanThanh.AllowUserToAddRows = false;
        dgvHoanThanh.AllowUserToDeleteRows = false;
        dgvHoanThanh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvHoanThanh.BackgroundColor = Color.White;
        dgvHoanThanh.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        dgvHoanThanh.Dock = DockStyle.Fill;
        dgvHoanThanh.Location = new Point(10, 52);
        dgvHoanThanh.Name = "dgvHoanThanh";
        dgvHoanThanh.ReadOnly = true;
        dgvHoanThanh.RowHeadersVisible = false;
        dgvHoanThanh.Size = new Size(360, 495);
        dgvHoanThanh.TabIndex = 1;
        CreateTaskColumns(dgvHoanThanh, "HoanThanh");
        // 
        // frmKanbanBoard
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(240, 242, 245);
        ClientSize = new Size(1184, 661);
        Controls.Add(tlpBangCongViec);
        Controls.Add(pnlHeader);
        MinimumSize = new Size(1000, 600);
        Name = "frmKanbanBoard";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Bảng công việc";
        pnlHeader.ResumeLayout(false);
        pnlHeader.PerformLayout();
        tlpBangCongViec.ResumeLayout(false);
        pnlCanLam.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvCanLam).EndInit();
        pnlDangLam.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvDangLam).EndInit();
        pnlHoanThanh.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)dgvHoanThanh).EndInit();
        ResumeLayout(false);
    }

    private static void CreateTaskColumns(DataGridView dgvCongViec, string suffix)
    {
        dgvCongViec.Columns.Add($"colMaCongViec{suffix}", "Mã CV");
        dgvCongViec.Columns.Add($"colTieuDe{suffix}", "Tiêu đề");
        dgvCongViec.Columns.Add($"colNguoiThucHien{suffix}", "Người thực hiện");
        dgvCongViec.Columns.Add($"colHanXuLy{suffix}", "Hạn xử lý");
        dgvCongViec.Columns.Add($"colMucUuTien{suffix}", "Ưu tiên");
    }
}
}
