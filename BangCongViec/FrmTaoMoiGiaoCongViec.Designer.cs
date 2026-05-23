namespace BangCongViec;

partial class FrmTaoMoiGiaoCongViec
{
    private System.ComponentModel.IContainer components = null;

    private Panel pnlNoiDung;
    private Label lblTieuDeForm;
    private Label lblMaCongViec;
    private TextBox txtMaCongViec;
    private Label lblTieuDe;
    private TextBox txtTieuDe;
    private Label lblNguoiGiao;
    private ComboBox cboNguoiGiao;
    private Label lblNguoiNhan;
    private ComboBox cboNguoiNhan;
    private Label lblHanXuLy;
    private DateTimePicker dtpHanXuLy;
    private Label lblMucUuTien;
    private ComboBox cboMucUuTien;
    private Label lblTrangThai;
    private ComboBox cboTrangThai;
    private Label lblMoTa;
    private TextBox txtMoTa;
    private CheckBox chkThongBao;
    private Panel pnlNutLenh;
    private Button btnLuu;
    private Button btnHuy;

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
        pnlNoiDung = new Panel();
        lblTieuDeForm = new Label();
        lblMaCongViec = new Label();
        txtMaCongViec = new TextBox();
        lblTieuDe = new Label();
        txtTieuDe = new TextBox();
        lblNguoiGiao = new Label();
        cboNguoiGiao = new ComboBox();
        lblNguoiNhan = new Label();
        cboNguoiNhan = new ComboBox();
        lblHanXuLy = new Label();
        dtpHanXuLy = new DateTimePicker();
        lblMucUuTien = new Label();
        cboMucUuTien = new ComboBox();
        lblTrangThai = new Label();
        cboTrangThai = new ComboBox();
        lblMoTa = new Label();
        txtMoTa = new TextBox();
        chkThongBao = new CheckBox();
        pnlNutLenh = new Panel();
        btnLuu = new Button();
        btnHuy = new Button();
        pnlNoiDung.SuspendLayout();
        pnlNutLenh.SuspendLayout();
        SuspendLayout();
        // 
        // pnlNoiDung
        // 
        pnlNoiDung.BackColor = Color.White;
        pnlNoiDung.Controls.Add(lblTieuDeForm);
        pnlNoiDung.Controls.Add(lblMaCongViec);
        pnlNoiDung.Controls.Add(txtMaCongViec);
        pnlNoiDung.Controls.Add(lblTieuDe);
        pnlNoiDung.Controls.Add(txtTieuDe);
        pnlNoiDung.Controls.Add(lblNguoiGiao);
        pnlNoiDung.Controls.Add(cboNguoiGiao);
        pnlNoiDung.Controls.Add(lblNguoiNhan);
        pnlNoiDung.Controls.Add(cboNguoiNhan);
        pnlNoiDung.Controls.Add(lblHanXuLy);
        pnlNoiDung.Controls.Add(dtpHanXuLy);
        pnlNoiDung.Controls.Add(lblMucUuTien);
        pnlNoiDung.Controls.Add(cboMucUuTien);
        pnlNoiDung.Controls.Add(lblTrangThai);
        pnlNoiDung.Controls.Add(cboTrangThai);
        pnlNoiDung.Controls.Add(lblMoTa);
        pnlNoiDung.Controls.Add(txtMoTa);
        pnlNoiDung.Controls.Add(chkThongBao);
        pnlNoiDung.Dock = DockStyle.Fill;
        pnlNoiDung.Location = new Point(0, 0);
        pnlNoiDung.Name = "pnlNoiDung";
        pnlNoiDung.Padding = new Padding(24);
        pnlNoiDung.Size = new Size(684, 461);
        pnlNoiDung.TabIndex = 0;
        // 
        // lblTieuDeForm
        // 
        lblTieuDeForm.AutoSize = true;
        lblTieuDeForm.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTieuDeForm.Location = new Point(24, 20);
        lblTieuDeForm.Name = "lblTieuDeForm";
        lblTieuDeForm.Size = new Size(304, 30);
        lblTieuDeForm.TabIndex = 0;
        lblTieuDeForm.Text = "TẠO MỚI / GIAO CÔNG VIỆC";
        // 
        // lblMaCongViec
        // 
        lblMaCongViec.AutoSize = true;
        lblMaCongViec.Location = new Point(27, 76);
        lblMaCongViec.Name = "lblMaCongViec";
        lblMaCongViec.Size = new Size(79, 15);
        lblMaCongViec.TabIndex = 1;
        lblMaCongViec.Text = "Mã công việc";
        // 
        // txtMaCongViec
        // 
        txtMaCongViec.Location = new Point(27, 98);
        txtMaCongViec.Name = "txtMaCongViec";
        txtMaCongViec.Size = new Size(290, 23);
        txtMaCongViec.TabIndex = 2;
        // 
        // lblTieuDe
        // 
        lblTieuDe.AutoSize = true;
        lblTieuDe.Location = new Point(367, 76);
        lblTieuDe.Name = "lblTieuDe";
        lblTieuDe.Size = new Size(42, 15);
        lblTieuDe.TabIndex = 3;
        lblTieuDe.Text = "Tiêu đề";
        // 
        // txtTieuDe
        // 
        txtTieuDe.Location = new Point(367, 98);
        txtTieuDe.Name = "txtTieuDe";
        txtTieuDe.Size = new Size(290, 23);
        txtTieuDe.TabIndex = 4;
        // 
        // lblNguoiGiao
        // 
        lblNguoiGiao.AutoSize = true;
        lblNguoiGiao.Location = new Point(27, 141);
        lblNguoiGiao.Name = "lblNguoiGiao";
        lblNguoiGiao.Size = new Size(64, 15);
        lblNguoiGiao.TabIndex = 5;
        lblNguoiGiao.Text = "Người giao";
        // 
        // cboNguoiGiao
        // 
        cboNguoiGiao.DropDownStyle = ComboBoxStyle.DropDownList;
        cboNguoiGiao.FormattingEnabled = true;
        cboNguoiGiao.Items.AddRange(new object[] { "An", "Bình", "Chi", "Dũng", "Hà", "Linh" });
        cboNguoiGiao.Location = new Point(27, 163);
        cboNguoiGiao.Name = "cboNguoiGiao";
        cboNguoiGiao.Size = new Size(290, 23);
        cboNguoiGiao.TabIndex = 6;
        // 
        // lblNguoiNhan
        // 
        lblNguoiNhan.AutoSize = true;
        lblNguoiNhan.Location = new Point(367, 141);
        lblNguoiNhan.Name = "lblNguoiNhan";
        lblNguoiNhan.Size = new Size(69, 15);
        lblNguoiNhan.TabIndex = 7;
        lblNguoiNhan.Text = "Người nhận";
        // 
        // cboNguoiNhan
        // 
        cboNguoiNhan.DropDownStyle = ComboBoxStyle.DropDownList;
        cboNguoiNhan.FormattingEnabled = true;
        cboNguoiNhan.Items.AddRange(new object[] { "An", "Bình", "Chi", "Dũng", "Hà", "Linh" });
        cboNguoiNhan.Location = new Point(367, 163);
        cboNguoiNhan.Name = "cboNguoiNhan";
        cboNguoiNhan.Size = new Size(290, 23);
        cboNguoiNhan.TabIndex = 8;
        // 
        // lblHanXuLy
        // 
        lblHanXuLy.AutoSize = true;
        lblHanXuLy.Location = new Point(27, 206);
        lblHanXuLy.Name = "lblHanXuLy";
        lblHanXuLy.Size = new Size(55, 15);
        lblHanXuLy.TabIndex = 9;
        lblHanXuLy.Text = "Hạn xử lý";
        // 
        // dtpHanXuLy
        // 
        dtpHanXuLy.CustomFormat = "dd/MM/yyyy";
        dtpHanXuLy.Format = DateTimePickerFormat.Custom;
        dtpHanXuLy.Location = new Point(27, 228);
        dtpHanXuLy.Name = "dtpHanXuLy";
        dtpHanXuLy.Size = new Size(290, 23);
        dtpHanXuLy.TabIndex = 10;
        // 
        // lblMucUuTien
        // 
        lblMucUuTien.AutoSize = true;
        lblMucUuTien.Location = new Point(367, 206);
        lblMucUuTien.Name = "lblMucUuTien";
        lblMucUuTien.Size = new Size(68, 15);
        lblMucUuTien.TabIndex = 11;
        lblMucUuTien.Text = "Mức ưu tiên";
        // 
        // cboMucUuTien
        // 
        cboMucUuTien.DropDownStyle = ComboBoxStyle.DropDownList;
        cboMucUuTien.FormattingEnabled = true;
        cboMucUuTien.Items.AddRange(new object[] { "Thấp", "Trung bình", "Cao" });
        cboMucUuTien.Location = new Point(367, 228);
        cboMucUuTien.Name = "cboMucUuTien";
        cboMucUuTien.Size = new Size(290, 23);
        cboMucUuTien.TabIndex = 12;
        // 
        // lblTrangThai
        // 
        lblTrangThai.AutoSize = true;
        lblTrangThai.Location = new Point(27, 271);
        lblTrangThai.Name = "lblTrangThai";
        lblTrangThai.Size = new Size(59, 15);
        lblTrangThai.TabIndex = 13;
        lblTrangThai.Text = "Trạng thái";
        // 
        // cboTrangThai
        // 
        cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
        cboTrangThai.FormattingEnabled = true;
        cboTrangThai.Items.AddRange(new object[] { "Cần làm", "Đang làm", "Hoàn thành" });
        cboTrangThai.Location = new Point(27, 293);
        cboTrangThai.Name = "cboTrangThai";
        cboTrangThai.Size = new Size(290, 23);
        cboTrangThai.TabIndex = 14;
        // 
        // lblMoTa
        // 
        lblMoTa.AutoSize = true;
        lblMoTa.Location = new Point(367, 271);
        lblMoTa.Name = "lblMoTa";
        lblMoTa.Size = new Size(39, 15);
        lblMoTa.TabIndex = 15;
        lblMoTa.Text = "Mô tả";
        // 
        // txtMoTa
        // 
        txtMoTa.Location = new Point(367, 293);
        txtMoTa.Multiline = true;
        txtMoTa.Name = "txtMoTa";
        txtMoTa.ScrollBars = ScrollBars.Vertical;
        txtMoTa.Size = new Size(290, 92);
        txtMoTa.TabIndex = 16;
        // 
        // chkThongBao
        // 
        chkThongBao.AutoSize = true;
        chkThongBao.Location = new Point(27, 337);
        chkThongBao.Name = "chkThongBao";
        chkThongBao.Size = new Size(185, 19);
        chkThongBao.TabIndex = 17;
        chkThongBao.Text = "Gửi thông báo cho người nhận";
        chkThongBao.UseVisualStyleBackColor = true;
        // 
        // pnlNutLenh
        // 
        pnlNutLenh.BackColor = Color.FromArgb(245, 247, 250);
        pnlNutLenh.Controls.Add(btnLuu);
        pnlNutLenh.Controls.Add(btnHuy);
        pnlNutLenh.Dock = DockStyle.Bottom;
        pnlNutLenh.Location = new Point(0, 461);
        pnlNutLenh.Name = "pnlNutLenh";
        pnlNutLenh.Padding = new Padding(24, 12, 24, 12);
        pnlNutLenh.Size = new Size(684, 60);
        pnlNutLenh.TabIndex = 1;
        // 
        // btnLuu
        // 
        btnLuu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnLuu.BackColor = Color.FromArgb(30, 120, 215);
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.ForeColor = Color.White;
        btnLuu.Location = new Point(460, 12);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new Size(92, 34);
        btnLuu.TabIndex = 0;
        btnLuu.Text = "Lưu";
        btnLuu.UseVisualStyleBackColor = false;
        // 
        // btnHuy
        // 
        btnHuy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnHuy.DialogResult = DialogResult.Cancel;
        btnHuy.Location = new Point(568, 12);
        btnHuy.Name = "btnHuy";
        btnHuy.Size = new Size(92, 34);
        btnHuy.TabIndex = 1;
        btnHuy.Text = "Hủy";
        btnHuy.UseVisualStyleBackColor = true;
        // 
        // FrmTaoMoiGiaoCongViec
        // 
        AcceptButton = btnLuu;
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(240, 242, 245);
        CancelButton = btnHuy;
        ClientSize = new Size(684, 521);
        Controls.Add(pnlNoiDung);
        Controls.Add(pnlNutLenh);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        Name = "FrmTaoMoiGiaoCongViec";
        StartPosition = FormStartPosition.CenterParent;
        Text = "Tạo mới / Giao công việc";
        pnlNoiDung.ResumeLayout(false);
        pnlNoiDung.PerformLayout();
        pnlNutLenh.ResumeLayout(false);
        ResumeLayout(false);
    }
}
