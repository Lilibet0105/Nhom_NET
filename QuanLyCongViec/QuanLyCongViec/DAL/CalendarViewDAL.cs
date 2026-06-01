using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCongViec.DTO;

namespace QuanLyCongViec.DAL
{
    public class CalendarViewDAL
    {
        // Lấy toàn bộ danh sách lịch làm việc
        public DataTable LayDanhSachLich()
        {
            string query = "SELECT MaLich, TieuDe, DiaDiem, MoTa, ThoiGianBatDau, ThoiGianKetThuc, TrangThai FROM LichLamViec ORDER BY ThoiGianBatDau DESC";
            return DAL.DataConnection.ExecuteQuery(query);
        }

        // Thêm lịch làm việc mới
        public bool ThemLich(CalendarViewDTO lich)
        {
            string query = "INSERT INTO LichLamViec (TieuDe, DiaDiem, MoTa, ThoiGianBatDau, ThoiGianKetThuc, TrangThai) VALUES (@TieuDe, @DiaDiem, @MoTa, @ThoiGianBatDau, @ThoiGianKetThuc, @TrangThai)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TieuDe", lich.TieuDe),
                new SqlParameter("@DiaDiem", (object)lich.DiaDiem ?? DBNull.Value),
                new SqlParameter("@MoTa", (object)lich.MoTa ?? DBNull.Value),
                new SqlParameter("@ThoiGianBatDau", lich.ThoiGianBatDau),
                new SqlParameter("@ThoiGianKetThuc", lich.ThoiGianKetThuc),
                new SqlParameter("@TrangThai", lich.TrangThai)
            };
            return DAL.DataConnection.ExecuteStoredProcedure(query, parameters);
        }

        // Sửa lịch làm việc
        public bool SuaLich(CalendarViewDTO lich)
        {
            string query = "UPDATE LichLamViec SET TieuDe = @TieuDe, DiaDiem = @DiaDiem, MoTa = @MoTa, ThoiGianBatDau = @ThoiGianBatDau, ThoiGianKetThuc = @ThoiGianKetThuc, TrangThai = @TrangThai WHERE MaLich = @MaLich";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLich", lich.MaLich),
                new SqlParameter("@TieuDe", lich.TieuDe),
                new SqlParameter("@DiaDiem", (object)lich.DiaDiem ?? DBNull.Value),
                new SqlParameter("@MoTa", (object)lich.MoTa ?? DBNull.Value),
                new SqlParameter("@ThoiGianBatDau", lich.ThoiGianBatDau),
                new SqlParameter("@ThoiGianKetThuc", lich.ThoiGianKetThuc),
                new SqlParameter("@TrangThai", lich.TrangThai)
            };
            return DAL.DataConnection.ExecuteStoredProcedure(query, parameters);
        }

        // Xóa lịch làm việc
        public bool XoaLich(int maLich)
        {
            string query = "DELETE FROM LichLamViec WHERE MaLich = @MaLich";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaLich", maLich)
            };
            return DAL.DataConnection.ExecuteStoredProcedure(query, parameters);
        }

        // Tìm kiếm lịch làm việc theo tiêu đề
        public DataTable TimKiemLich(string keyword)
        {
            string query = "SELECT MaLich, TieuDe, DiaDiem, MoTa, ThoiGianBatDau, ThoiGianKetThuc, TrangThai FROM LichLamViec WHERE TieuDe LIKE @Keyword OR DiaDiem LIKE @Keyword ORDER BY ThoiGianBatDau DESC";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", "%" + keyword + "%")
            };
            return DAL.DataConnection.ExecuteQuery(query, parameters);
        }
    }
}