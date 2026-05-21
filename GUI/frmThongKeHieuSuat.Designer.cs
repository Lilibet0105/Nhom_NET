namespace _Net____PCCV.GUI
{
    partial class frmThongKeHieuSuat
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea5 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend5 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea6 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend6 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblChonThoiGian = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.grpTongCongViec = new System.Windows.Forms.GroupBox();
            this.grpTyLeHoanThanh = new System.Windows.Forms.GroupBox();
            this.grpCongViecTreHan = new System.Windows.Forms.GroupBox();
            this.lblTongCongViec = new System.Windows.Forms.Label();
            this.lblTyLeHoanThanh = new System.Windows.Forms.Label();
            this.lblViecTreHan = new System.Windows.Forms.Label();
            this.chartHieuSuat = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTrangThai = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvChiTietHieuSuat = new System.Windows.Forms.DataGridView();
            this.grpTongCongViec.SuspendLayout();
            this.grpTyLeHoanThanh.SuspendLayout();
            this.grpCongViecTreHan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartHieuSuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHieuSuat)).BeginInit();
            this.SuspendLayout();
            // 
            // lblChonThoiGian
            // 
            this.lblChonThoiGian.AutoSize = true;
            this.lblChonThoiGian.Location = new System.Drawing.Point(66, 36);
            this.lblChonThoiGian.Name = "lblChonThoiGian";
            this.lblChonThoiGian.Size = new System.Drawing.Size(97, 16);
            this.lblChonThoiGian.TabIndex = 0;
            this.lblChonThoiGian.Text = "Chọn thời gian: ";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Location = new System.Drawing.Point(182, 36);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 22);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Location = new System.Drawing.Point(399, 36);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 22);
            this.dtpDenNgay.TabIndex = 2;
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(717, 36);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(127, 23);
            this.btnThongKe.TabIndex = 3;
            this.btnThongKe.Text = "Xem thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.LightGreen;
            this.btnXuatExcel.Location = new System.Drawing.Point(884, 36);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(100, 23);
            this.btnXuatExcel.TabIndex = 4;
            this.btnXuatExcel.Text = "Xuất báo cáo";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            // 
            // grpTongCongViec
            // 
            this.grpTongCongViec.Controls.Add(this.lblTongCongViec);
            this.grpTongCongViec.Location = new System.Drawing.Point(69, 81);
            this.grpTongCongViec.Name = "grpTongCongViec";
            this.grpTongCongViec.Size = new System.Drawing.Size(291, 121);
            this.grpTongCongViec.TabIndex = 5;
            this.grpTongCongViec.TabStop = false;
            this.grpTongCongViec.Text = "Tổng công việc: ";
            // 
            // grpTyLeHoanThanh
            // 
            this.grpTyLeHoanThanh.Controls.Add(this.lblTyLeHoanThanh);
            this.grpTyLeHoanThanh.Location = new System.Drawing.Point(389, 81);
            this.grpTyLeHoanThanh.Name = "grpTyLeHoanThanh";
            this.grpTyLeHoanThanh.Size = new System.Drawing.Size(301, 121);
            this.grpTyLeHoanThanh.TabIndex = 6;
            this.grpTyLeHoanThanh.TabStop = false;
            this.grpTyLeHoanThanh.Text = "Tỷ lệ hoàn thành: ";
            // 
            // grpCongViecTreHan
            // 
            this.grpCongViecTreHan.Controls.Add(this.lblViecTreHan);
            this.grpCongViecTreHan.Location = new System.Drawing.Point(717, 81);
            this.grpCongViecTreHan.Name = "grpCongViecTreHan";
            this.grpCongViecTreHan.Size = new System.Drawing.Size(291, 123);
            this.grpCongViecTreHan.TabIndex = 7;
            this.grpCongViecTreHan.TabStop = false;
            this.grpCongViecTreHan.Text = "Công việc trễ hạn: ";
            // 
            // lblTongCongViec
            // 
            this.lblTongCongViec.AutoSize = true;
            this.lblTongCongViec.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongCongViec.Location = new System.Drawing.Point(122, 37);
            this.lblTongCongViec.Name = "lblTongCongViec";
            this.lblTongCongViec.Size = new System.Drawing.Size(42, 46);
            this.lblTongCongViec.TabIndex = 0;
            this.lblTongCongViec.Text = "0";
            // 
            // lblTyLeHoanThanh
            // 
            this.lblTyLeHoanThanh.AutoEllipsis = true;
            this.lblTyLeHoanThanh.AutoSize = true;
            this.lblTyLeHoanThanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTyLeHoanThanh.Location = new System.Drawing.Point(89, 37);
            this.lblTyLeHoanThanh.Name = "lblTyLeHoanThanh";
            this.lblTyLeHoanThanh.Size = new System.Drawing.Size(121, 46);
            this.lblTyLeHoanThanh.TabIndex = 0;
            this.lblTyLeHoanThanh.Text = "100%";
            // 
            // lblViecTreHan
            // 
            this.lblViecTreHan.AutoSize = true;
            this.lblViecTreHan.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViecTreHan.Location = new System.Drawing.Point(121, 37);
            this.lblViecTreHan.Name = "lblViecTreHan";
            this.lblViecTreHan.Size = new System.Drawing.Size(42, 46);
            this.lblViecTreHan.TabIndex = 0;
            this.lblViecTreHan.Text = "0";
            // 
            // chartHieuSuat
            // 
            chartArea5.Name = "ChartArea1";
            this.chartHieuSuat.ChartAreas.Add(chartArea5);
            legend5.Name = "Legend1";
            this.chartHieuSuat.Legends.Add(legend5);
            this.chartHieuSuat.Location = new System.Drawing.Point(486, 208);
            this.chartHieuSuat.Name = "chartHieuSuat";
            series5.ChartArea = "ChartArea1";
            series5.Legend = "Legend1";
            series5.Name = "Series1";
            this.chartHieuSuat.Series.Add(series5);
            this.chartHieuSuat.Size = new System.Drawing.Size(522, 196);
            this.chartHieuSuat.TabIndex = 8;
            this.chartHieuSuat.Text = "Hiệu suất công việc";
            // 
            // chartTrangThai
            // 
            chartArea6.Name = "ChartArea1";
            this.chartTrangThai.ChartAreas.Add(chartArea6);
            legend6.Name = "Legend1";
            this.chartTrangThai.Legends.Add(legend6);
            this.chartTrangThai.Location = new System.Drawing.Point(69, 208);
            this.chartTrangThai.Name = "chartTrangThai";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series6.Legend = "Legend1";
            series6.Name = "Series1";
            this.chartTrangThai.Series.Add(series6);
            this.chartTrangThai.Size = new System.Drawing.Size(403, 196);
            this.chartTrangThai.TabIndex = 9;
            this.chartTrangThai.Text = "Trạng Thái Công Việc";
            // 
            // dgvChiTietHieuSuat
            // 
            this.dgvChiTietHieuSuat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietHieuSuat.Location = new System.Drawing.Point(69, 419);
            this.dgvChiTietHieuSuat.Name = "dgvChiTietHieuSuat";
            this.dgvChiTietHieuSuat.RowHeadersWidth = 51;
            this.dgvChiTietHieuSuat.RowTemplate.Height = 24;
            this.dgvChiTietHieuSuat.Size = new System.Drawing.Size(939, 150);
            this.dgvChiTietHieuSuat.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightYellow;
            this.ClientSize = new System.Drawing.Size(1057, 572);
            this.Controls.Add(this.dgvChiTietHieuSuat);
            this.Controls.Add(this.chartTrangThai);
            this.Controls.Add(this.chartHieuSuat);
            this.Controls.Add(this.grpCongViecTreHan);
            this.Controls.Add(this.grpTyLeHoanThanh);
            this.Controls.Add(this.grpTongCongViec);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.dtpTuNgay);
            this.Controls.Add(this.lblChonThoiGian);
            this.Name = "Form1";
            this.Text = "Thống kê hiệu suất";
            this.grpTongCongViec.ResumeLayout(false);
            this.grpTongCongViec.PerformLayout();
            this.grpTyLeHoanThanh.ResumeLayout(false);
            this.grpTyLeHoanThanh.PerformLayout();
            this.grpCongViecTreHan.ResumeLayout(false);
            this.grpCongViecTreHan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartHieuSuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHieuSuat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblChonThoiGian;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.GroupBox grpTongCongViec;
        private System.Windows.Forms.GroupBox grpTyLeHoanThanh;
        private System.Windows.Forms.GroupBox grpCongViecTreHan;
        private System.Windows.Forms.Label lblTongCongViec;
        private System.Windows.Forms.Label lblTyLeHoanThanh;
        private System.Windows.Forms.Label lblViecTreHan;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHieuSuat;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTrangThai;
        private System.Windows.Forms.DataGridView dgvChiTietHieuSuat;
    }
}

