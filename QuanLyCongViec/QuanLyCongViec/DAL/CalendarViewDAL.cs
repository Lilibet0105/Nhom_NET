using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCongViec.DTO;

namespace QuanLyCongViec.DAL
{
    public class CalendarViewDAL
    {
        // Lấy toàn bộ danh sách công việc làm lịch trình
        public DataTable LayDanhSachLich()
        {
            string query = "SELECT MaTask AS MaLich, TenTask AS TieuDe, MoTa, " +
                   "Deadline AS ThoiGianBatDau, Deadline AS ThoiGianKetThuc, TrangThai, " +
                   "MaDA, MaNguoiThucHien FROM CongViec ORDER BY Deadline DESC";
            return DAL.DataConnection.ExecuteQuery(query);
        }

        // Thêm lịch làm việc mới vào bảng công việc
        public bool ThemLich(CalendarViewDTO lich)
        {
            string query = "INSERT INTO CongViec (TenTask, MoTa, MucDoUuTien, TrangThai, Deadline, MaDA, MaNguoiThucHien) " +
                   "VALUES (@TieuDe, @MoTa, N'Trung bình', @TrangThai, @ThoiGianBatDau, @MaDA, @MaNguoiThucHien)";

            SqlParameter[] parameters = new SqlParameter[]
            {
        new SqlParameter("@TieuDe", lich.TieuDe),
        new SqlParameter("@MoTa", (object)lich.MoTa ?? DBNull.Value),
        new SqlParameter("@TrangThai", lich.TrangThai),
        new SqlParameter("@ThoiGianBatDau", lich.ThoiGianBatDau),
        new SqlParameter("@MaDA", lich.MaDA),
        new SqlParameter("@MaNguoiThucHien", lich.MaNguoiThucHien)
            };
            return DAL.DataConnection.ExecuteNonQuery(query, parameters);
        }

        // Sửa lịch làm việc
        public bool SuaLich(CalendarViewDTO lich)
        {
            string query = "UPDATE CongViec SET TenTask = @TieuDe, MoTa = @MoTa, TrangThai = @TrangThai, Deadline = @ThoiGianBatDau WHERE MaTask = @MaLich";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLich", lich.MaLich),
                new SqlParameter("@TieuDe", lich.TieuDe),
                new SqlParameter("@MoTa", (object)lich.MoTa ?? DBNull.Value),
                new SqlParameter("@TrangThai", lich.TrangThai),
                new SqlParameter("@ThoiGianBatDau", lich.ThoiGianBatDau)
            };
            return DAL.DataConnection.ExecuteNonQuery(query, parameters);
        }

        // Xóa lịch làm việc
        public bool XoaLich(int maLich)
        {
            string query = "DELETE FROM CongViec WHERE MaTask = @MaLich";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLich", maLich)
            };

            return DAL.DataConnection.ExecuteNonQuery(query, parameters);
        }

        // Tìm kiếm lịch làm việc
        public DataTable TimKiemLich(string keyword)
        {
            string query = "SELECT MaTask AS MaLich, TenTask AS TieuDe, N'' AS DiaDiem, MoTa, Deadline AS ThoiGianBatDau, Deadline AS ThoiGianKetThuc, TrangThai FROM CongViec WHERE TenTask LIKE @Keyword ORDER BY Deadline DESC";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", "%" + keyword + "%")
            };
            return DAL.DataConnection.ExecuteQuery(query, parameters);
        }
    }
}