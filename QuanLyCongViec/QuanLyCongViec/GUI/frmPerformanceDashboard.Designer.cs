using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLyCongViec.GUI // ĐÃ SỬA: Đồng bộ hóa đúng namespace hệ thống để xóa lỗi Dispose
{
    partial class frmPerformanceDashboard
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.lblChonThoiGian = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.btnXuatExcel = new System.Windows.Forms.Button();
            this.grpTongCongViec = new System.Windows.Forms.GroupBox();
            this.lblTotalTasks = new System.Windows.Forms.Label();
            this.grpTyLeHoanThanh = new System.Windows.Forms.GroupBox();
            this.lblCompletionRate = new System.Windows.Forms.Label();
            this.grpViecTreHan = new System.Windows.Forms.GroupBox();
            this.lblOverdueTasks = new System.Windows.Forms.Label();
            this.chartColumn = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.chartPie = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dgvDetails = new System.Windows.Forms.DataGridView();
            this.grpTongCongViec.SuspendLayout();
            this.grpTyLeHoanThanh.SuspendLayout();
            this.grpViecTreHan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartColumn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).BeginInit();
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
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(184, 17);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 22);
            this.dtpFrom.TabIndex = 1;
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(419, 17);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 22);
            this.dtpTo.TabIndex = 2;
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(713, 16);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(133, 23);
            this.btnThongKe.TabIndex = 3;
            this.btnThongKe.Text = "Xem thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
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
            this.btnXuatExcel.Click += new System.EventHandler(this.btnXuatExcel_Click);
            // 
            // grpTongCongViec
            // 
            this.grpTongCongViec.Controls.Add(this.lblTotalTasks);
            this.grpTongCongViec.Location = new System.Drawing.Point(42, 68);
            this.grpTongCongViec.Name = "grpTongCongViec";
            this.grpTongCongViec.Size = new System.Drawing.Size(308, 125);
            this.grpTongCongViec.TabIndex = 5;
            this.grpTongCongViec.TabStop = false;
            this.grpTongCongViec.Text = "Tổng Công Việc: ";
            // 
            // lblTotalTasks
            // 
            this.lblTotalTasks.AutoSize = true;
            this.lblTotalTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalTasks.Location = new System.Drawing.Point(134, 40);
            this.lblTotalTasks.Name = "lblTotalTasks";
            this.lblTotalTasks.Size = new System.Drawing.Size(42, 46);
            this.lblTotalTasks.TabIndex = 0;
            this.lblTotalTasks.Text = "0";
            // 
            // grpTyLeHoanThanh
            // 
            this.grpTyLeHoanThanh.Controls.Add(this.lblCompletionRate);
            this.grpTyLeHoanThanh.Location = new System.Drawing.Point(395, 68);
            this.grpTyLeHoanThanh.Name = "grpTyLeHoanThanh";
            this.grpTyLeHoanThanh.Size = new System.Drawing.Size(314, 125);
            this.grpTyLeHoanThanh.TabIndex = 6;
            this.grpTyLeHoanThanh.TabStop = false;
            this.grpTyLeHoanThanh.Text = "Tỷ Lệ Hoàn Thành: ";
            // 
            // lblCompletionRate
            // 
            this.lblCompletionRate.AutoSize = true;
            this.lblCompletionRate.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCompletionRate.Location = new System.Drawing.Point(103, 40);
            this.lblCompletionRate.Name = "lblCompletionRate";
            this.lblCompletionRate.Size = new System.Drawing.Size(121, 46);
            this.lblCompletionRate.TabIndex = 0;
            this.lblCompletionRate.Text = "100%";
            // 
            // grpViecTreHan
            // 
            this.grpViecTreHan.Controls.Add(this.lblOverdueTasks);
            this.grpViecTreHan.Location = new System.Drawing.Point(756, 68);
            this.grpViecTreHan.Name = "grpViecTreHan";
            this.grpViecTreHan.Size = new System.Drawing.Size(308, 125);
            this.grpViecTreHan.TabIndex = 7;
            this.grpViecTreHan.TabStop = false;
            this.grpViecTreHan.Text = "Công Việc Trễ Hạn: ";
            // 
            // lblOverdueTasks
            // 
            this.lblOverdueTasks.AutoSize = true;
            this.lblOverdueTasks.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOverdueTasks.Location = new System.Drawing.Point(142, 40);
            this.lblOverdueTasks.Name = "lblOverdueTasks";
            this.lblOverdueTasks.Size = new System.Drawing.Size(42, 46);
            this.lblOverdueTasks.TabIndex = 0;
            this.lblOverdueTasks.Text = "0";
            // 
            // chartColumn
            // 
            chartArea1.Name = "ChartArea1";
            this.chartColumn.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartColumn.Legends.Add(legend1);
            this.chartColumn.Location = new System.Drawing.Point(577, 211);
            this.chartColumn.Name = "chartColumn";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartColumn.Series.Add(series1);
            this.chartColumn.Size = new System.Drawing.Size(487, 251);
            this.chartColumn.TabIndex = 8;
            this.chartColumn.Text = "chart1";
            // 
            // chartPie
            // 
            chartArea2.Name = "ChartArea1";
            this.chartPie.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chartPie.Legends.Add(legend2);
            this.chartPie.Location = new System.Drawing.Point(42, 211);
            this.chartPie.Name = "chartPie";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.chartPie.Series.Add(series2);
            this.chartPie.Size = new System.Drawing.Size(512, 251);
            this.chartPie.TabIndex = 9;
            this.chartPie.Text = "chart2";
            // 
            // dgvDetails
            // 
            this.dgvDetails.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetails.Location = new System.Drawing.Point(42, 479);
            this.dgvDetails.Name = "dgvDetails";
            this.dgvDetails.RowHeadersWidth = 51;
            this.dgvDetails.RowTemplate.Height = 24;
            this.dgvDetails.Size = new System.Drawing.Size(1022, 238);
            this.dgvDetails.TabIndex = 10;
            // 
            // frmPerformanceDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1143, 754);
            this.Controls.Add(this.dgvDetails);
            this.Controls.Add(this.chartPie);
            this.Controls.Add(this.chartColumn);
            this.Controls.Add(this.grpViecTreHan);
            this.Controls.Add(this.grpTyLeHoanThanh);
            this.Controls.Add(this.grpTongCongViec);
            this.Controls.Add(this.btnXuatExcel);
            this.Controls.Add(this.btnThongKe);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblChonThoiGian);
            this.Name = "frmPerformanceDashboard";
            this.Text = "Thống Kê Hiệu Suất";
            this.Load += new System.EventHandler(this.frmPerformanceDashboard_Load);
            this.grpTongCongViec.ResumeLayout(false);
            this.grpTongCongViec.PerformLayout();
            this.grpTyLeHoanThanh.ResumeLayout(false);
            this.grpTyLeHoanThanh.PerformLayout();
            this.grpViecTreHan.ResumeLayout(false);
            this.grpViecTreHan.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartColumn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartPie)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetails)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblChonThoiGian;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.Button btnXuatExcel;
        private System.Windows.Forms.GroupBox grpTongCongViec;
        private System.Windows.Forms.Label lblTotalTasks;
        private System.Windows.Forms.GroupBox grpTyLeHoanThanh;
        private System.Windows.Forms.Label lblCompletionRate;
        private System.Windows.Forms.GroupBox grpViecTreHan;
        private System.Windows.Forms.Label lblOverdueTasks;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartColumn;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartPie;
        private System.Windows.Forms.DataGridView dgvDetails;
    }
}