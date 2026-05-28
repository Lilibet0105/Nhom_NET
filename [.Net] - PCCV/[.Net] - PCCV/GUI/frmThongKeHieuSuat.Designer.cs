using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace _.Net____PCCV
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
            ChartArea chartArea1 = new ChartArea();
Legend legend1 = new Legend();
Series series1 = new Series();
ChartArea chartArea2 = new ChartArea();
Legend legend2 = new Legend();
Series series2 = new Series();

            this.lblChonThoiGian = new System.Windows.Forms.Label();
            this.dtpTuNgay = new System.Windows.Forms.DateTimePicker();
            this.dtpDenNgay = new System.Windows.Forms.DateTimePicker();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.grpTongCongViec = new System.Windows.Forms.GroupBox();
            this.grpTyLeHoanThanh = new System.Windows.Forms.GroupBox();
            this.grpViecTreHan = new System.Windows.Forms.GroupBox();
            this.lblTongCongViec = new System.Windows.Forms.Label();
            this.lblTyLeHoanThanh = new System.Windows.Forms.Label();
            this.lblViecTreHan = new System.Windows.Forms.Label();
            this.chartHieuSuat = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartTrangThai = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvChiTietHieuSuat = new System.Windows.Forms.DataGridView();

            this.grpTongCongViec.SuspendLayout();
            this.grpTyLeHoanThanh.SuspendLayout();
            this.grpViecTreHan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartHieuSuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHieuSuat)).BeginInit();
            this.SuspendLayout();
            // 
            // lblChonThoiGian
            // 
            this.lblChonThoiGian.AutoSize = true;
            this.lblChonThoiGian.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChonThoiGian.Location = new System.Drawing.Point(38, 19);
            this.lblChonThoiGian.Name = "lblChonThoiGian";
            this.lblChonThoiGian.Size = new System.Drawing.Size(126, 20);
            this.lblChonThoiGian.TabIndex = 0;
            this.lblChonThoiGian.Text = "Chọn thời gian: ";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.Location = new System.Drawing.Point(184, 17);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Size = new System.Drawing.Size(200, 22);
            this.dtpTuNgay.TabIndex = 1;
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.Location = new System.Drawing.Point(419, 17);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Size = new System.Drawing.Size(200, 22);
            this.dtpDenNgay.TabIndex = 2;
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(713, 16);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(133, 23);
            this.btnThongKe.TabIndex = 3;
            this.btnThongKe.Text = "Xem thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.BackColor = System.Drawing.Color.LightGreen;
            this.btnXuatExcel.Location = new System.Drawing.Point(892, 16);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(136, 23);
            this.btnXuatExcel.TabIndex = 4;
            this.btnXuatExcel.Text = "Xuất báo cáo";
            this.btnXuatExcel.UseVisualStyleBackColor = false;
            // 
            // grpTongCongViec
            // 
            this.grpTongCongViec.Controls.Add(this.lblTongCongViec);
            this.grpTongCongViec.Location = new System.Drawing.Point(42, 68);
            this.grpTongCongViec.Name = "grpTongCongViec";
            this.grpTongCongViec.Size = new System.Drawing.Size(308, 125);
            this.grpTongCongViec.TabIndex = 5;
            this.grpTongCongViec.TabStop = false;
            this.grpTongCongViec.Text = "Tổng Công Việc: ";
            // 
            // grpTyLeHoanThanh
            // 
            this.grpTyLeHoanThanh.Controls.Add(this.lblTyLeHoanThanh);
            this.grpTyLeHoanThanh.Location = new System.Drawing.Point(395, 68);
            this.grpTyLeHoanThanh.Name = "grpTyLeHoanThanh";
            this.grpTyLeHoanThanh.Size = new System.Drawing.Size(314, 125);
            this.grpTyLeHoanThanh.TabIndex = 6;
            this.grpTyLeHoanThanh.TabStop = false;
            this.grpTyLeHoanThanh.Text = "Tỷ Lệ Hoàn Thành: ";
            // 
            // grpViecTreHan
            // 
            this.grpViecTreHan.Controls.Add(this.lblViecTreHan);
            this.grpViecTreHan.Location = new System.Drawing.Point(756, 68);
            this.grpViecTreHan.Name = "grpViecTreHan";
            this.grpViecTreHan.Size = new System.Drawing.Size(308, 125);
            this.grpViecTreHan.TabIndex = 7;
            this.grpViecTreHan.TabStop = false;
            this.grpViecTreHan.Text = "Công Việc Trễ Hạn: ";
            // 
            // lblTongCongViec
            // 
            this.lblTongCongViec.AutoSize = true;
            this.lblTongCongViec.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTongCongViec.Location = new System.Drawing.Point(134, 40);
            this.lblTongCongViec.Name = "lblTongCongViec";
            this.lblTongCongViec.Size = new System.Drawing.Size(42, 46);
            this.lblTongCongViec.TabIndex = 0;
            this.lblTongCongViec.Text = "0";
            // 
            // lblTyLeHoanThanh
            // 
            this.lblTyLeHoanThanh.AutoSize = true;
            this.lblTyLeHoanThanh.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTyLeHoanThanh.Location = new System.Drawing.Point(103, 40);
            this.lblTyLeHoanThanh.Name = "lblTyLeHoanThanh";
            this.lblTyLeHoanThanh.Size = new System.Drawing.Size(121, 46);
            this.lblTyLeHoanThanh.TabIndex = 0;
            this.lblTyLeHoanThanh.Text = "100%";
            // 
            // lblViecTreHan
            // 
            this.lblViecTreHan.AutoSize = true;
            this.lblViecTreHan.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblViecTreHan.Location = new System.Drawing.Point(142, 40);
            this.lblViecTreHan.Name = "lblViecTreHan";
            this.lblViecTreHan.Size = new System.Drawing.Size(42, 46);
            this.lblViecTreHan.TabIndex = 0;
            this.lblViecTreHan.Text = "0";
            // 
            // chartHieuSuat
            // 
            chartArea1.Name = "ChartArea1";
            this.chartHieuSuat.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartHieuSuat.Legends.Add(legend1);
            this.chartHieuSuat.Location = new System.Drawing.Point(577, 211);
            this.chartHieuSuat.Name = "chartHieuSuat";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartHieuSuat.Series.Add(series1);
            this.chartHieuSuat.Size = new System.Drawing.Size(487, 251);
            this.chartHieuSuat.TabIndex = 8;
            this.chartHieuSuat.Text = "chart1";
            // 
            // chartTrangThai
            // 
            chartArea2.Name = "ChartArea1";
            this.chartTrangThai.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartTrangThai.Legends.Add(legend2);
            this.chartTrangThai.Location = new System.Drawing.Point(42, 211);
            this.chartTrangThai.Name = "chartTrangThai";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartTrangThai.Series.Add(series2);
            this.chartTrangThai.Size = new System.Drawing.Size(512, 251);
            this.chartTrangThai.TabIndex = 9;
            this.chartTrangThai.Text = "chart2";
            // 
            // dgvChiTietHieuSuat
            // 
            this.dgvChiTietHieuSuat.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChiTietHieuSuat.Location = new System.Drawing.Point(42, 479);
            this.dgvChiTietHieuSuat.Name = "dgvChiTietHieuSuat";
            this.dgvChiTietHieuSuat.RowHeadersWidth = 51;
            this.dgvChiTietHieuSuat.RowTemplate.Height = 24;
            this.dgvChiTietHieuSuat.Size = new System.Drawing.Size(1022, 238);
            this.dgvChiTietHieuSuat.TabIndex = 10;
            // 
            // frmThongKeHieuSuat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 754);
            this.Controls.Add(this.dgvChiTietHieuSuat);
            this.Controls.Add(this.chartTrangThai);
            this.Controls.Add(this.chartHieuSuat);
            this.Controls.Add(this.grpViecTreHan);
            this.Controls.Add(this.grpTyLeHoanThanh);
            this.Controls.Add(this.grpTongCongViec);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.dtpDenNgay);
            this.Controls.Add(this.dtpTuNgay);
            this.Controls.Add(this.lblChonThoiGian);
            this.Name = "frmThongKeHieuSuat";
            this.Text = "Thống Kê Hiệu Suất";
            this.grpTongCongViec.ResumeLayout(false);
            this.grpTongCongViec.PerformLayout();
            this.grpTyLeHoanThanh.ResumeLayout(false);
            this.grpTyLeHoanThanh.PerformLayout();
            this.grpViecTreHan.ResumeLayout(false);
            this.grpViecTreHan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartHieuSuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartTrangThai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChiTietHieuSuat)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        // Khai báo danh sách các biến thành phần (Bị thiếu lúc trước)
        private System.Windows.Forms.Label lblChonThoiGian;
        private System.Windows.Forms.DateTimePicker dtpTuNgay;
        private System.Windows.Forms.DateTimePicker dtpDenNgay;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.GroupBox grpTongCongViec;
        private System.Windows.Forms.Label lblTongCongViec;
        private System.Windows.Forms.GroupBox grpTyLeHoanThanh;
        private System.Windows.Forms.Label lblTyLeHoanThanh;
        private System.Windows.Forms.GroupBox grpViecTreHan;
        private System.Windows.Forms.Label lblViecTreHan;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartHieuSuat;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTrangThai;
        private System.Windows.Forms.DataGridView dgvChiTietHieuSuat;
    }
}