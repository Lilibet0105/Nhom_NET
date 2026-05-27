using _Net____PCCV.DAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace _.Net____PCCV
{
    public partial class frmPerformanceDashboard : Form
    {
        public frmPerformanceDashboard()
        {
            InitializeComponent();
        }

        private void frmThongKeHieuSuat_Load(object sender, EventArgs e)
        {

        }

        private void frmPerformanceDashboard_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.Value = DateTime.Now;

            LoadDashboardData();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            LoadDashboardData();
        }

        private void LoadDashboardData()
        {
            string query = @"
                SELECT 
                    nv.HoTen,
                    COUNT(CASE WHEN cv.TrangThai = 'Done' THEN 1 END) AS SoViec_Done,
                    COUNT(CASE WHEN cv.TrangThai = 'In Progress' THEN 1 END) AS SoViec_InProgress,
                    COUNT(CASE WHEN cv.TrangThai = 'To Do' THEN 1 END) AS SoViec_ToDo,
                    COUNT(CASE WHEN cv.TrangThai = 'Done' AND cv.Deadline < GETDATE() THEN 1 
                               WHEN cv.TrangThai != 'Done' AND cv.Deadline < GETDATE() THEN 1 END) AS SoViec_TreHan
                FROM NhanVien nv
                LEFT JOIN CongViec cv ON nv.MaNV = cv.MaNguoiThucHien
                WHERE cv.NgayTao BETWEEN @From AND @To OR cv.NgayTao IS NULL
                GROUP BY nv.HoTen";

            SqlParameter[] parameters =
            {
                new SqlParameter("@From", dtpFrom.Value.Date),
                new SqlParameter("@To", dtpTo.Value.Date.AddDays(1))
            };

            DataTable dtStaff = DataConnection.ExecuteQuery(query, parameters);
            dgvDetails.DataSource = dtStaff;
            CalculateSummaryCards(dtStaff);
            BuildColumnChart(dtStaff);
        }

        private void CalculateSummaryCards(DataTable dtStaff)
        {
            int totalTasks = 0;
            int totalFinished = 0;
            int totalOverdue = 0;

            // Duyệt ma trận bảng dữ liệu để cộng dồn các cột khớp với database
            foreach (DataRow row in dtStaff.Rows)
            {
                int done = Convert.ToInt32(row["SoViec_Done"]);
                int progress = Convert.ToInt32(row["SoViec_InProgress"]);
                int todo = Convert.ToInt32(row["SoViec_ToDo"]);
                int overdue = Convert.ToInt32(row["SoViec_TreHan"]);

                totalFinished += done;
                totalTasks += (done + progress + todo);
                totalOverdue += overdue;
            }

            // Gán dữ liệu trực tiếp lên các Label trên giao diện
            lblTotalTasks.Text = totalTasks.ToString();
            lblOverdueTasks.Text = totalOverdue.ToString();

            if (totalTasks > 0)
            {
                double rate = ((double)totalFinished / totalTasks) * 100;
                lblCompletionRate.Text = $"{Math.Round(rate, 1)}%";
            }
            else
            {
                lblCompletionRate.Text = "0%";
            }

            // 4. Vẽ biểu đồ hình tròn (Tỷ lệ phần trăm trạng thái hệ thống)
            BuildPieChart(totalFinished, (totalTasks - totalFinished), totalOverdue);
        }

        private void BuildPieChart(int done, int inProgress, int overdue)
        {
            chartPie.Series.Clear();
            chartPie.Titles.Clear();
            chartPie.Titles.Add("TỶ LỆ TRẠNG THÁI CÔNG VIỆC");

            Series seriesPie = new Series("StatusSeries") { ChartType = SeriesChartType.Pie };

            // Đổ dữ liệu phân mảnh biểu đồ tròn
            if (done > 0) seriesPie.Points.AddXY("Hoàn thành", done);
            if (inProgress > 0) seriesPie.Points.AddXY("Đang thực hiện/Chờ", inProgress);
            if (overdue > 0) seriesPie.Points.AddXY("Trễ hạn", overdue);

            chartPie.Series.Add(seriesPie);
        }

        private void BuildColumnChart(DataTable dtStaff)
        {
            chartColumn.Series.Clear();
            chartColumn.ChartAreas[0].AxisX.Interval = 1; // Hiển thị đầy đủ tên nhân sự trên trục hoành
            chartColumn.Titles.Clear();
            chartColumn.Titles.Add("PHÂN BỔ CÔNG VIỆC THEO NHÂN SỰ");

            // Khởi tạo 3 cột chồng hiển thị trạng thái công việc
            Series colFinished = new Series("Hoàn thành") { ChartType = SeriesChartType.Column, Color = System.Drawing.Color.DeepSkyBlue };
            Series colProcessing = new Series("Đang làm") { ChartType = SeriesChartType.Column, Color = System.Drawing.Color.Orange };
            Series colOverdue = new Series("Trễ hạn") { ChartType = SeriesChartType.Column, Color = System.Drawing.Color.Crimson };

            foreach (DataRow row in dtStaff.Rows)
            {
                string tenNV = row["HoTen"].ToString(); // Lấy cột HoTen từ database làm nhãn trục X
                colFinished.Points.AddXY(tenNV, row["SoViec_Done"]);
                colProcessing.Points.AddXY(tenNV, row["SoViec_InProgress"]);
                colOverdue.Points.AddXY(tenNV, row["SoViec_TreHan"]);
            }

            chartColumn.Series.Add(colFinished);
            chartColumn.Series.Add(colProcessing);
            chartColumn.Series.Add(colOverdue);
        }
    }
}

