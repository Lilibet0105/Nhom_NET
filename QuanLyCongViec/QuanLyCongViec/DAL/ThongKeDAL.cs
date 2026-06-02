using System;
using System.Data;
using System.Data.SqlClient;

namespace QuanLyCongViec.DAL
{
    public class ThongKeDAL
    {
        /// <summary>
        /// Lấy toàn bộ danh sách công việc chi tiết phục vụ hiển thị Grid và xuất Excel
        /// </summary>
        public DataTable LayDuLieuBaoCao(DateTime tuNgay, DateTime denNgay)
        {
            string query = @"SELECT 
                                c.MaTask AS [Mã Công Việc],
                                c.TenTask AS [Tên Công Việc],
                                da.TenDA AS [Thuộc Dự Án],
                                nv.HoTen AS [Người Thực Hiện],
                                c.MucDoUuTien AS [Mức Độ Ưu Tiên],
                                c.TrangThai AS [Trạng Thái],
                                CONVERT(VARCHAR(10), c.Deadline, 103) AS [Hạn Chót]
                             FROM CongViec c
                             LEFT JOIN DuAn da ON c.MaDA = da.MaDA
                             LEFT JOIN NhanVien nv ON c.MaNguoiThucHien = nv.MaNV
                             WHERE c.Deadline BETWEEN @From AND @To
                             ORDER BY c.Deadline DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@From", tuNgay.Date),
                new SqlParameter("@To", denNgay.Date.AddDays(1).AddTicks(-1)) // Lấy đến hết ngày 23:59:59
            };

            return DataConnection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Lấy bảng thống kê số lượng trạng thái công việc để vẽ biểu đồ tròn
        /// </summary>
        public DataTable LayThongKeTrangThai(DateTime tuNgay, DateTime denNgay)
        {
            string query = @"SELECT ISNULL(TrangThai, N'Chưa rõ') AS TrangThai, COUNT(*) AS SoLuong 
                             FROM CongViec 
                             WHERE Deadline BETWEEN @From AND @To 
                             GROUP BY TrangThai";

            SqlParameter[] parameters = {
                new SqlParameter("@From", tuNgay.Date),
                new SqlParameter("@To", denNgay.Date.AddDays(1).AddTicks(-1))
            };

            return DataConnection.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// Lấy số liệu phân bổ công việc của từng nhân sự để vẽ biểu đồ cột
        /// </summary>
        public DataTable LayPhanBoCongViecNhanSu(DateTime tuNgay, DateTime denNgay)
        {
            string query = @"SELECT 
                                nv.HoTen,
                                COUNT(CASE WHEN cv.TrangThai = 'Done' OR cv.TrangThai = N'Hoàn Thành' THEN 1 END) AS SoViec_Done,
                                COUNT(CASE WHEN cv.TrangThai = 'In Progress' OR cv.TrangThai = N'Đang Làm' THEN 1 END) AS SoViec_InProgress,
                                COUNT(CASE WHEN cv.Deadline < GETDATE() AND cv.TrangThai <> 'Done' AND cv.TrangThai <> N'Hoàn Thành' THEN 1 END) AS SoViec_TreHan
                             FROM NhanVien nv
                             LEFT JOIN CongViec cv ON nv.MaNV = cv.MaNguoiThucHien AND cv.Deadline BETWEEN @From AND @To
                             GROUP BY nv.HoTen";

            SqlParameter[] parameters = {
                new SqlParameter("@From", tuNgay.Date),
                new SqlParameter("@To", denNgay.Date.AddDays(1).AddTicks(-1))
            };

            return DataConnection.ExecuteQuery(query, parameters);
        }
    }
}