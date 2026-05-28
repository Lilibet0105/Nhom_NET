using System.Drawing;
using System.Windows.Forms;

namespace GUI
{
    partial class FrmTaoMoiGiaoCongViec
    {
        private System.ComponentModel.IContainer components = null;
        private Panel pnlNoiDung;
        private Label lblTieuDeForm;
        private Label lblMaCongViec;
        private TextBox txtMaCongViec;
        private Label lblDuAn;
        private ComboBox cboDuAn;
        private Label lblTieuDe;
        private TextBox txtTieuDe;
        private Label lblNguoiNhan;
        private ComboBox cboNguoiNhan;
        private Label lblNgayBatDau;
        private DateTimePicker dtpNgayBatDau;
        private Label lblHanXuLy;
        private DateTimePicker dtpHanXuLy;
        private Label lblMucUuTien;
        private ComboBox cboMucUuTien;
        private Label lblTrangThai;
        private ComboBox cboTrangThai;
        private Label lblMoTa;
        private TextBox txtMoTa;
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
            lblDuAn = new Label();
            cboDuAn = new ComboBox();
            lblTieuDe = new Label();
            txtTieuDe = new TextBox();
            lblNguoiNhan = new Label();
            cboNguoiNhan = new ComboBox();
            lblNgayBatDau = new Label();
            dtpNgayBatDau = new DateTimePicker();
            lblHanXuLy = new Label();
            dtpHanXuLy = new DateTimePicker();
            lblMucUuTien = new Label();
            cboMucUuTien = new ComboBox();
            lblTrangThai = new Label();
            cboTrangThai = new ComboBox();
            lblMoTa = new Label();
            txtMoTa = new TextBox();
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
            pnlNoiDung.Controls.Add(lblDuAn);
            pnlNoiDung.Controls.Add(cboDuAn);
            pnlNoiDung.Controls.Add(lblTieuDe);
            pnlNoiDung.Controls.Add(txtTieuDe);
            pnlNoiDung.Controls.Add(lblNguoiNhan);
            pnlNoiDung.Controls.Add(cboNguoiNhan);
            pnlNoiDung.Controls.Add(lblNgayBatDau);
            pnlNoiDung.Controls.Add(dtpNgayBatDau);
            pnlNoiDung.Controls.Add(lblHanXuLy);
            pnlNoiDung.Controls.Add(dtpHanXuLy);
            pnlNoiDung.Controls.Add(lblMucUuTien);
            pnlNoiDung.Controls.Add(cboMucUuTien);
            pnlNoiDung.Controls.Add(lblTrangThai);
            pnlNoiDung.Controls.Add(cboTrangThai);
            pnlNoiDung.Controls.Add(lblMoTa);
            pnlNoiDung.Controls.Add(txtMoTa);
            pnlNoiDung.Dock = DockStyle.Fill;
            pnlNoiDung.Location = new Point(0, 0);
            pnlNoiDung.Name = "pnlNoiDung";
            pnlNoiDung.Padding = new Padding(24);
            pnlNoiDung.Size = new Size(724, 491);
            pnlNoiDung.TabIndex = 0;
            // 
            // lblTieuDeForm
            // 
            lblTieuDeForm.AutoSize = true;
            lblTieuDeForm.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblTieuDeForm.Location = new Point(24, 20);
            lblTieuDeForm.Name = "lblTieuDeForm";
            lblTieuDeForm.Size = new Size(207, 30);
            lblTieuDeForm.TabIndex = 0;
            lblTieuDeForm.Text = "TẠO MỚI CÔNG VIỆC";
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
            txtMaCongViec.Size = new Size(300, 23);
            txtMaCongViec.TabIndex = 2;
            // 
            // lblDuAn
            // 
            lblDuAn.AutoSize = true;
            lblDuAn.Location = new Point(367, 76);
            lblDuAn.Name = "lblDuAn";
            lblDuAn.Size = new Size(39, 15);
            lblDuAn.TabIndex = 3;
            lblDuAn.Text = "Dự án";
            // 
            // cboDuAn
            // 
            cboDuAn.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDuAn.Location = new Point(367, 98);
            cboDuAn.Name = "cboDuAn";
            cboDuAn.Size = new Size(330, 23);
            cboDuAn.TabIndex = 4;
            cboDuAn.SelectedIndexChanged += cboDuAn_SelectedIndexChanged;
            // 
            // lblTieuDe
            // 
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(27, 141);
            lblTieuDe.Name = "lblTieuDe";
            lblTieuDe.Size = new Size(87, 15);
            lblTieuDe.TabIndex = 5;
            lblTieuDe.Text = "Tên công việc";
            // 
            // txtTieuDe
            // 
            txtTieuDe.Location = new Point(27, 163);
            txtTieuDe.Name = "txtTieuDe";
            txtTieuDe.Size = new Size(300, 23);
            txtTieuDe.TabIndex = 6;
            // 
            // lblNguoiNhan
            // 
            lblNguoiNhan.AutoSize = true;
            lblNguoiNhan.Location = new Point(367, 141);
            lblNguoiNhan.Name = "lblNguoiNhan";
            lblNguoiNhan.Size = new Size(92, 15);
            lblNguoiNhan.TabIndex = 7;
            lblNguoiNhan.Text = "Người thực hiện";
            // 
            // cboNguoiNhan
            // 
            cboNguoiNhan.DropDownStyle = ComboBoxStyle.DropDownList;
            cboNguoiNhan.Location = new Point(367, 163);
            cboNguoiNhan.Name = "cboNguoiNhan";
            cboNguoiNhan.Size = new Size(330, 23);
            cboNguoiNhan.TabIndex = 8;
            // 
            // lblNgayBatDau
            // 
            lblNgayBatDau.AutoSize = true;
            lblNgayBatDau.Location = new Point(27, 206);
            lblNgayBatDau.Name = "lblNgayBatDau";
            lblNgayBatDau.Size = new Size(76, 15);
            lblNgayBatDau.TabIndex = 9;
            lblNgayBatDau.Text = "Ngày bắt đầu";
            // 
            // dtpNgayBatDau
            // 
            dtpNgayBatDau.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpNgayBatDau.Format = DateTimePickerFormat.Custom;
            dtpNgayBatDau.Location = new Point(27, 228);
            dtpNgayBatDau.Name = "dtpNgayBatDau";
            dtpNgayBatDau.Size = new Size(300, 23);
            dtpNgayBatDau.TabIndex = 10;
            // 
            // lblHanXuLy
            // 
            lblHanXuLy.AutoSize = true;
            lblHanXuLy.Location = new Point(367, 206);
            lblHanXuLy.Name = "lblHanXuLy";
            lblHanXuLy.Size = new Size(87, 15);
            lblHanXuLy.TabIndex = 11;
            lblHanXuLy.Text = "Hạn hoàn thành";
            // 
            // dtpHanXuLy
            // 
            dtpHanXuLy.CustomFormat = "dd/MM/yyyy HH:mm";
            dtpHanXuLy.Format = DateTimePickerFormat.Custom;
            dtpHanXuLy.Location = new Point(367, 228);
            dtpHanXuLy.Name = "dtpHanXuLy";
            dtpHanXuLy.Size = new Size(330, 23);
            dtpHanXuLy.TabIndex = 12;
            // 
            // lblMucUuTien
            // 
            lblMucUuTien.AutoSize = true;
            lblMucUuTien.Location = new Point(27, 271);
            lblMucUuTien.Name = "lblMucUuTien";
            lblMucUuTien.Size = new Size(68, 15);
            lblMucUuTien.TabIndex = 13;
            lblMucUuTien.Text = "Mức ưu tiên";
            // 
            // cboMucUuTien
            // 
            cboMucUuTien.DropDownStyle = ComboBoxStyle.DropDownList;
            cboMucUuTien.Location = new Point(27, 293);
            cboMucUuTien.Name = "cboMucUuTien";
            cboMucUuTien.Size = new Size(300, 23);
            cboMucUuTien.TabIndex = 14;
            // 
            // lblTrangThai
            // 
            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(367, 271);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(59, 15);
            lblTrangThai.TabIndex = 15;
            lblTrangThai.Text = "Trạng thái";
            // 
            // cboTrangThai
            // 
            cboTrangThai.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTrangThai.Location = new Point(367, 293);
            cboTrangThai.Name = "cboTrangThai";
            cboTrangThai.Size = new Size(330, 23);
            cboTrangThai.TabIndex = 16;
            // 
            // lblMoTa
            // 
            lblMoTa.AutoSize = true;
            lblMoTa.Location = new Point(27, 336);
            lblMoTa.Name = "lblMoTa";
            lblMoTa.Size = new Size(39, 15);
            lblMoTa.TabIndex = 17;
            lblMoTa.Text = "Mô tả";
            // 
            // txtMoTa
            // 
            txtMoTa.Location = new Point(27, 358);
            txtMoTa.Multiline = true;
            txtMoTa.Name = "txtMoTa";
            txtMoTa.ScrollBars = ScrollBars.Vertical;
            txtMoTa.Size = new Size(670, 105);
            txtMoTa.TabIndex = 18;
            // 
            // pnlNutLenh
            // 
            pnlNutLenh.BackColor = Color.FromArgb(245, 247, 250);
            pnlNutLenh.Controls.Add(btnLuu);
            pnlNutLenh.Controls.Add(btnHuy);
            pnlNutLenh.Dock = DockStyle.Bottom;
            pnlNutLenh.Location = new Point(0, 491);
            pnlNutLenh.Name = "pnlNutLenh";
            pnlNutLenh.Padding = new Padding(24, 12, 24, 12);
            pnlNutLenh.Size = new Size(724, 60);
            pnlNutLenh.TabIndex = 1;
            // 
            // btnLuu
            // 
            btnLuu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnLuu.BackColor = Color.FromArgb(30, 120, 215);
            btnLuu.FlatStyle = FlatStyle.Flat;
            btnLuu.ForeColor = Color.White;
            btnLuu.Location = new Point(500, 12);
            btnLuu.Name = "btnLuu";
            btnLuu.Size = new Size(92, 34);
            btnLuu.TabIndex = 0;
            btnLuu.Text = "Lưu";
            btnLuu.UseVisualStyleBackColor = false;
            btnLuu.Click += btnLuu_Click;
            // 
            // btnHuy
            // 
            btnHuy.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnHuy.DialogResult = DialogResult.Cancel;
            btnHuy.Location = new Point(608, 12);
            btnHuy.Name = "btnHuy";
            btnHuy.Size = new Size(92, 34);
            btnHuy.TabIndex = 1;
            btnHuy.Text = "Hủy";
            btnHuy.UseVisualStyleBackColor = true;
            btnHuy.Click += btnHuy_Click;
            // 
            // FrmTaoMoiGiaoCongViec
            // 
            AcceptButton = btnLuu;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(240, 242, 245);
            CancelButton = btnHuy;
            ClientSize = new Size(724, 551);
            Controls.Add(pnlNoiDung);
            Controls.Add(pnlNutLenh);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmTaoMoiGiaoCongViec";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Tạo mới công việc";
            pnlNoiDung.ResumeLayout(false);
            pnlNoiDung.PerformLayout();
            pnlNutLenh.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
