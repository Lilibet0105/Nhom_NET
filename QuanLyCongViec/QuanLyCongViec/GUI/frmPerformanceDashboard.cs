using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using QuanLyCongViec.BUS;
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QuanLyCongViec.GUI
{
    public partial class frmPerformanceDashboard : Form
    {
        private ThongKeBUS tkBUS = new ThongKeBUS();

        public frmPerformanceDashboard()
        {
            InitializeComponent();
        }

        private void frmPerformanceDashboard_Load(object sender, EventArgs e)
        {
            // Thiết lập khoảng thời gian mặc định: Đầu tháng này đến ngày hôm nay
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
            DateTime tuNgay = dtpFrom.Value;
            DateTime denNgay = dtpTo.Value;

            // Đổ dữ liệu lên các thẻ số liệu KPI tổng quan
            QuanLyCongViec.DTO.ThongKeDTO dto = tkBUS.LayThongKeTongQuan(tuNgay, denNgay);
            lblTotalTasks.Text = dto.TongCongViec.ToString();
            lblCompletionRate.Text = dto.TyLeHoanThanh.ToString() + "%";
            lblOverdueTasks.Text = dto.ViecTreHan.ToString();

            // Lấy dữ liệu vẽ Biểu đồ Tròn
            DataTable dtStatus = tkBUS.LayThongKeTrangThai(tuNgay, denNgay);
            BuildPieChart(dtStatus);

            // Lấy dữ liệu vẽ Biểu đồ Cột
            DataTable dtStaff = tkBUS.LayPhanBoCongViecNhanSu(tuNgay, denNgay);
            BuildColumnChart(dtStaff);

            // Đổ dữ liệu hiệu suất thật vào bảng GridView hiển thị
            dgvDetails.DataSource = tkBUS.LayChiTietHieuSuatThanhVien(tuNgay, denNgay);
        }

        private void BuildPieChart(DataTable dtStatus)
        {
            chartPie.Series.Clear();
            chartPie.Titles.Clear();
            chartPie.Titles.Add("TỶ LỆ TRẠNG THÁI CÔNG VIỆC");
            Series series = new Series("TrangThai") { ChartType = SeriesChartType.Pie };

            foreach (DataRow row in dtStatus.Rows)
            {
                series.Points.AddXY(row["TrangThai"].ToString(), row["SoLuong"]);
            }
            chartPie.Series.Add(series);
        }

        private void BuildColumnChart(DataTable dtStaff)
        {
            chartColumn.Series.Clear();
            chartColumn.ChartAreas[0].AxisX.Interval = 1;
            chartColumn.Titles.Clear();
            chartColumn.Titles.Add("PHÂN BỔ CÔNG VIỆC THEO NHÂN SỰ");

            Series colFinished = new Series("Hoàn thành") { ChartType = SeriesChartType.Column, Color = Color.DeepSkyBlue };
            Series colProcessing = new Series("Đang làm") { ChartType = SeriesChartType.Column, Color = Color.Orange };
            Series colOverdue = new Series("Trễ hạn") { ChartType = SeriesChartType.Column, Color = Color.Crimson };

            foreach (DataRow row in dtStaff.Rows)
            {
                string tenNV = row["HoTen"].ToString();
                colFinished.Points.AddXY(tenNV, row["SoViec_Done"]);
                colProcessing.Points.AddXY(tenNV, row["SoViec_InProgress"]);
                colOverdue.Points.AddXY(tenNV, row["SoViec_TreHan"]);
            }

            chartColumn.Series.Add(colFinished);
            chartColumn.Series.Add(colProcessing);
            chartColumn.Series.Add(colOverdue);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            DateTime tuNgay = dtpFrom.Value;
            DateTime denNgay = dtpTo.Value;

            DataTable dtReport = tkBUS.LayDuLieuBaoCao(tuNgay, denNgay);

            if (dtReport == null || dtReport.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy dữ liệu công việc nào trong khoảng thời gian đã chọn để xuất báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Khởi tạo ứng dụng Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            COMExcel.Worksheet exSheet = exBook.Worksheets[1];
            COMExcel.Range exRange;

            int row = 1;
            int colCount = dtReport.Columns.Count;

            exSheet.Cells.Font.Name = "Times New Roman";

            // Tạo tiêu đề báo cáo
            exRange = exSheet.Range[exSheet.Cells[row, 1], exSheet.Cells[row, colCount]];
            exRange.MergeCells = true;
            exRange.Font.Size = 14;
            exRange.Font.Bold = true;
            exRange.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Value = "BÁO CÁO HIỆU SUẤT VÀ TIẾN ĐỘ CÔNG VIỆC";
            row += 2;

            // Tạo dòng thời gian lọc
            exRange = exSheet.Range[exSheet.Cells[row, 1], exSheet.Cells[row, colCount]];
            exRange.MergeCells = true;
            exRange.Font.Italic = true;
            exRange.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Value = $"Khoảng thời gian: Từ ngày {tuNgay.ToString("dd/MM/yyyy")} đến ngày {denNgay.ToString("dd/MM/yyyy")}";
            row += 2;

            // Đẩy danh sách Tiêu đề cột
            exRange = exSheet.Range[exSheet.Cells[row, 1], exSheet.Cells[row, colCount]];
            exRange.Font.Bold = true;
            object[,] headers = new object[1, colCount];
            for (int c = 0; c < colCount; c++)
            {
                headers[0, c] = dtReport.Columns[c].ColumnName;
            }
            exRange.Value2 = headers;
            row++;

            // Đổ dữ liệu chi tiết từ SQL vào bảng tính
            foreach (DataRow r in dtReport.Rows)
            {
                for (int c = 0; c < colCount; c++)
                {
                    exSheet.Cells[row, c + 1] = r[c]?.ToString();
                }
                row++;
            }

            // Đổ thông tin chữ ký ngày lập dưới chân trang
            row += 2;
            exRange = exSheet.Range[exSheet.Cells[row, colCount - 2], exSheet.Cells[row, colCount]];
            exRange.MergeCells = true;
            exRange.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Value = "Ngày " + DateTime.Now.Day + " tháng " + DateTime.Now.Month + " năm " + DateTime.Now.Year;

            row++;
            exRange = exSheet.Range[exSheet.Cells[row, colCount - 2], exSheet.Cells[row, colCount]];
            exRange.MergeCells = true;
            exRange.HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Font.Bold = true;
            exRange.Value = "Người lập báo cáo";

            // Bật ứng dụng Excel
            exSheet.Name = "BaoCaoHieuSuat";
            exApp.Visible = true;
        }

        private void btnLammoi_Click(object sender, EventArgs e)
        {
            dtpFrom.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpTo.Value = DateTime.Now;
            LoadDashboardData();
        }
    }
}