using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    partial class frmTaskDetails
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlHeader;
        private Label lblTieuDe;
        private Label lblMaTask;
        private Button btnSuaCongViec;
        private Button btnDong;
        private TableLayoutPanel tlpMain;
        private Panel pnlThongTin;
        private Label lblDuAn;
        private Label lblNguoiThucHien;
        private Label lblTrangThai;
        private Label lblUuTien;
        private Label lblNgayTao;
        private Label lblDeadline;
        private Label lblTinhTrang;
        private Label lblTienDo;
        private ProgressBar progressTienDo;
        private Label lblMoTa;
        private TextBox txtMoTa;
        private Panel pnlBinhLuan;
        private Label lblBinhLuanTitle;
        private DataGridView dgvBinhLuan;
        private ComboBox cboNguoiBinhLuan;
        private TextBox txtBinhLuan;
        private Button btnThemBinhLuan;

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
            lblMaTask = new Label();
            btnSuaCongViec = new Button();
            btnDong = new Button();
            tlpMain = new TableLayoutPanel();
            pnlThongTin = new Panel();
            lblDuAn = new Label();
            lblNguoiThucHien = new Label();
            lblTrangThai = new Label();
            lblUuTien = new Label();
            lblNgayTao = new Label();
            lblDeadline = new Label();
            lblTinhTrang = new Label();
            lblTienDo = new Label();
            progressTienDo = new ProgressBar();
            lblMoTa = new Label();
            txtMoTa = new TextBox();
            pnlBinhLuan = new Panel();
            lblBinhLuanTitle = new Label();
            dgvBinhLuan = new DataGridView();
            cboNguoiBinhLuan = new ComboBox();
            txtBinhLuan = new TextBox();
            btnThemBinhLuan = new Button();
            pnlHeader.SuspendLayout();
            tlpMain.SuspendLayout();
            pnlThongTin.SuspendLayout();
            pnlBinhLuan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBinhLuan).BeginInit();
            SuspendLayout();
            // 
            // pnlHeader
            // 
            pnlHeader.BackColor = Color.White;
            pnlHeader.Controls.Add(lblTieuDe);
            pnlHeader.Controls.Add(lblMaTask);
            pnlHeader.Controls.Add(btnSuaCongViec);
            pnlHeader.Controls.Add(btnDong);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Padding = new Padding(18, 12, 18, 12);
            pnlHeader.Size = new Size(920, 88);
            pnlHeader.TabIndex = 0;
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoEllipsis = true;
            lblTieuDe.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTieuDe.Location = new Point(18, 12);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(610, 34);
            lblTieuDe.TabIndex = 0;
            lblTieuDe.Text = "Chi tiết công việc";
            // 
            // lblMaTask
            // 
            lblMaTask.AutoSize = true;
            lblMaTask.ForeColor = Color.FromArgb(80, 80, 80);
            lblMaTask.Location = new Point(21, 52);
            lblMaTask.Name = "lblMaTask";
            lblMaTask.Size = new Size(80, 15);
            lblMaTask.TabIndex = 1;
            lblMaTask.Text = "Mã công việc";
            // 
            // btnSuaCongViec
            // 
            btnSuaCongViec.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSuaCongViec.BackColor = Color.FromArgb(30, 120, 215);
            btnSuaCongViec.FlatStyle = FlatStyle.Flat;
            btnSuaCongViec.ForeColor = Color.White;
            btnSuaCongViec.Location = new Point(694, 26);
            btnSuaCongViec.Name = "btnSuaCongViec";
            btnSuaCongViec.Size = new Size(96, 34);
            btnSuaCongViec.TabIndex = 2;
            btnSuaCongViec.Text = "Sửa";
            btnSuaCongViec.UseVisualStyleBackColor = false;
            btnSuaCongViec.Click += btnSuaCongViec_Click;
            // 
            // btnDong
            // 
            btnDong.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnDong.DialogResult = DialogResult.Cancel;
            btnDong.Location = new Point(806, 26);
            btnDong.Name = "btnDong";
            btnDong.Size = new Size(96, 34);
            btnDong.TabIndex = 3;
            btnDong.Text = "Đóng";
            btnDong.UseVisualStyleBackColor = true;
            btnDong.Click += btnDong_Click;
            // 
            // tlpMain
            // 
            tlpMain.ColumnCount = 2;
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42F));
            tlpMain.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 58F));
            tlpMain.Controls.Add(pnlThongTin, 0, 0);
            tlpMain.Controls.Add(pnlBinhLuan, 1, 0);
            tlpMain.Dock = DockStyle.Fill;
            tlpMain.Location = new Point(0, 88);
            tlpMain.Name = "tlpMain";
            tlpMain.Padding = new Padding(14);
            tlpMain.RowCount = 1;
            tlpMain.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpMain.Size = new Size(920, 572);
            tlpMain.TabIndex = 1;
            // 
            // pnlThongTin
            // 
            pnlThongTin.BackColor = Color.White;
            pnlThongTin.BorderStyle = BorderStyle.FixedSingle;
            pnlThongTin.Controls.Add(lblDuAn);
            pnlThongTin.Controls.Add(lblNguoiThucHien);
            pnlThongTin.Controls.Add(lblTrangThai);
            pnlThongTin.Controls.Add(lblUuTien);
            pnlThongTin.Controls.Add(lblNgayTao);
            pnlThongTin.Controls.Add(lblDeadline);
            pnlThongTin.Controls.Add(lblTinhTrang);
            pnlThongTin.Controls.Add(lblTienDo);
            pnlThongTin.Controls.Add(progressTienDo);
            pnlThongTin.Controls.Add(lblMoTa);
            pnlThongTin.Controls.Add(txtMoTa);
            pnlThongTin.Dock = DockStyle.Fill;
            pnlThongTin.Location = new Point(17, 17);
            pnlThongTin.Name = "pnlThongTin";
            pnlThongTin.Padding = new Padding(16);
            pnlThongTin.Size = new Size(368, 538);
            pnlThongTin.TabIndex = 0;
            // 
            // labels
            // 
            lblDuAn.Location = new Point(16, 18);
            lblDuAn.Size = new Size(330, 24);
            lblNguoiThucHien.Location = new Point(16, 50);
            lblNguoiThucHien.Size = new Size(330, 24);
            lblTrangThai.Location = new Point(16, 82);
            lblTrangThai.Size = new Size(330, 24);
            lblUuTien.Location = new Point(16, 114);
            lblUuTien.Size = new Size(330, 24);
            lblNgayTao.Location = new Point(16, 146);
            lblNgayTao.Size = new Size(330, 24);
            lblDeadline.Location = new Point(16, 178);
            lblDeadline.Size = new Size(330, 24);
            lblTinhTrang.Location = new Point(16, 210);
            lblTinhTrang.Size = new Size(330, 24);
            lblTienDo.Location = new Point(16, 250);
            lblTienDo.Size = new Size(330, 24);
            progressTienDo.Location = new Point(16, 280);
            progressTienDo.Size = new Size(330, 18);
            lblMoTa.Location = new Point(16, 322);
            lblMoTa.Size = new Size(330, 22);
            lblMoTa.Text = "Mô tả";
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(16, 350);
            txtMoTa.Multiline = true;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.ReadOnly = true;
            txtMoTa.ScrollBars = ScrollBars.Vertical;
            txtMoTa.Size = new Size(330, 166);
            txtMoTa.TabIndex = 10;
            // 
            // pnlBinhLuan
            // 
            pnlBinhLuan.BackColor = Color.White;
            pnlBinhLuan.BorderStyle = BorderStyle.FixedSingle;
            pnlBinhLuan.Controls.Add(lblBinhLuanTitle);
            pnlBinhLuan.Controls.Add(dgvBinhLuan);
            pnlBinhLuan.Controls.Add(cboNguoiBinhLuan);
            pnlBinhLuan.Controls.Add(txtBinhLuan);
            pnlBinhLuan.Controls.Add(btnThemBinhLuan);
            pnlBinhLuan.Dock = DockStyle.Fill;
            pnlBinhLuan.Location = new Point(391, 17);
            pnlBinhLuan.Name = "pnlBinhLuan";
            pnlBinhLuan.Padding = new Padding(16);
            pnlBinhLuan.Size = new Size(512, 538);
            pnlBinhLuan.TabIndex = 1;
            // 
            // lblBinhLuanTitle
            // 
            lblBinhLuanTitle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            lblBinhLuanTitle.Location = new Point(16, 16);
            lblBinhLuanTitle.Name = "lblBinhLuanTitle";
            lblBinhLuanTitle.Size = new Size(470, 24);
            lblBinhLuanTitle.TabIndex = 0;
            lblBinhLuanTitle.Text = "Bình luận";
            // 
            // dgvBinhLuan
            // 
            dgvBinhLuan.AllowUserToAddRows = false;
            dgvBinhLuan.AllowUserToDeleteRows = false;
            dgvBinhLuan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBinhLuan.BackgroundColor = Color.White;
            dgvBinhLuan.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBinhLuan.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "HoTen", HeaderText = "Người bình luận", FillWeight = 30 });
            dgvBinhLuan.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NoiDung", HeaderText = "Nội dung", FillWeight = 50 });
            dgvBinhLuan.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "ThoiGian", HeaderText = "Thời gian", FillWeight = 30 });
            dgvBinhLuan.Location = new Point(16, 48);
            dgvBinhLuan.Name = "dgvBinhLuan";
            dgvBinhLuan.ReadOnly = true;
            dgvBinhLuan.RowHeadersVisible = false;
            dgvBinhLuan.Size = new Size(478, 310);
            dgvBinhLuan.TabIndex = 1;
            // 
            // cboNguoiBinhLuan
            // 
            cboNguoiBinhLuan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNguoiBinhLuan.Location = new Point(16, 376);
            cboNguoiBinhLuan.Name = "cboNguoiBinhLuan";
            cboNguoiBinhLuan.Size = new Size(478, 23);
            cboNguoiBinhLuan.TabIndex = 2;
            // 
            // txtBinhLuan
            // 
            txtBinhLuan.Location = new Point(16, 411);
            txtBinhLuan.Multiline = true;
            txtBinhLuan.Name = "txtBinhLuan";
            txtBinhLuan.ScrollBars = ScrollBars.Vertical;
            txtBinhLuan.Size = new Size(478, 70);
            txtBinhLuan.TabIndex = 3;
            // 
            // btnThemBinhLuan
            // 
            btnThemBinhLuan.BackColor = Color.FromArgb(30, 120, 215);
            btnThemBinhLuan.FlatStyle = FlatStyle.Flat;
            btnThemBinhLuan.ForeColor = Color.White;
            btnThemBinhLuan.Location = new Point(366, 491);
            btnThemBinhLuan.Name = "btnThemBinhLuan";
            btnThemBinhLuan.Size = new Size(128, 30);
            btnThemBinhLuan.TabIndex = 4;
            btnThemBinhLuan.Text = "Thêm bình luận";
            btnThemBinhLuan.UseVisualStyleBackColor = false;
            btnThemBinhLuan.Click += btnThemBinhLuan_Click;
            // 
            // frmTaskDetails
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            CancelButton = btnDong;
            ClientSize = new Size(920, 660);
            Controls.Add(tlpMain);
            Controls.Add(pnlHeader);
            MinimizeBox = false;
            Name = "frmTaskDetails";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Chi tiết công việc";
            pnlHeader.ResumeLayout(false);
            pnlHeader.PerformLayout();
            tlpMain.ResumeLayout(false);
            pnlThongTin.ResumeLayout(false);
            pnlThongTin.PerformLayout();
            pnlBinhLuan.ResumeLayout(false);
            pnlBinhLuan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvBinhLuan).EndInit();
            ResumeLayout(false);
        }
    }
}
