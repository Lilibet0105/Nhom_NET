using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCongViec.DTO;

namespace QuanLyCongViec.DAL
{
    public class CalendarViewDAL
    {
        public DataTable LayDanhSachLich()
        {
            string query = @"
SELECT MaTask AS MaLich,
       'LICH' + RIGHT('0000' + CAST(MaTask AS VARCHAR(10)), 4) AS MaLichHienThi,
       TenTask AS TieuDe,
       N'' AS DiaDiem,
       MoTa,
       Deadline AS ThoiGianBatDau,
       Deadline AS ThoiGianKetThuc,
       TrangThai
FROM CongViec
ORDER BY Deadline DESC";
            return DataConnection.ExecuteQuery(query);
        }

        public bool ThemLich(CalendarViewDTO lich)
        {
            string query = @"
DECLARE @MaDA INT = (SELECT TOP 1 MaDA FROM DuAn ORDER BY MaDA);
DECLARE @MaNV INT = (SELECT TOP 1 MaNV FROM NhanVien ORDER BY MaNV);

IF @MaDA IS NULL OR @MaNV IS NULL
BEGIN
    RAISERROR(N'Vui lòng tạo ít nhất một dự án và một nhân viên trước khi thêm lịch.', 16, 1);
    RETURN;
END

INSERT INTO CongViec (TenTask, MoTa, MucDoUuTien, TrangThai, Deadline, MaDA, MaNguoiThucHien)
VALUES (@TieuDe, @MoTa, N'Trung bình', @TrangThai, @ThoiGianBatDau, @MaDA, @MaNV)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@TieuDe", lich.TieuDe),
                new SqlParameter("@MoTa", string.IsNullOrWhiteSpace(lich.MoTa) ? (object)DBNull.Value : lich.MoTa),
                new SqlParameter("@TrangThai", lich.TrangThai),
                new SqlParameter("@ThoiGianBatDau", lich.ThoiGianBatDau)
            };
            return DataConnection.ExecuteNonQuery(query, parameters);
        }

        public bool SuaLich(CalendarViewDTO lich)
        {
            string query = @"
UPDATE CongViec
SET TenTask = @TieuDe,
    MoTa = @MoTa,
    TrangThai = @TrangThai,
    Deadline = @ThoiGianBatDau
WHERE MaTask = @MaLich";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaLich", lich.MaLich),
                new SqlParameter("@TieuDe", lich.TieuDe),
                new SqlParameter("@MoTa", string.IsNullOrWhiteSpace(lich.MoTa) ? (object)DBNull.Value : lich.MoTa),
                new SqlParameter("@TrangThai", lich.TrangThai),
                new SqlParameter("@ThoiGianBatDau", lich.ThoiGianBatDau)
            };
            return DataConnection.ExecuteNonQuery(query, parameters);
        }

        public bool XoaLich(int maLich)
        {
            string query = "DELETE FROM CongViec WHERE MaTask = @MaLich";
            SqlParameter[] parameters = { new SqlParameter("@MaLich", maLich) };
            return DataConnection.ExecuteNonQuery(query, parameters);
        }

        public DataTable TimKiemLich(string keyword)
        {
            string query = @"
SELECT MaTask AS MaLich,
       'LICH' + RIGHT('0000' + CAST(MaTask AS VARCHAR(10)), 4) AS MaLichHienThi,
       TenTask AS TieuDe,
       N'' AS DiaDiem,
       MoTa,
       Deadline AS ThoiGianBatDau,
       Deadline AS ThoiGianKetThuc,
       TrangThai
FROM CongViec
WHERE TenTask LIKE @Keyword
ORDER BY Deadline DESC";
            SqlParameter[] parameters = { new SqlParameter("@Keyword", "%" + keyword + "%") };
            return DataConnection.ExecuteQuery(query, parameters);
        }
    }
}
