using System;
using System.Data;
using System.Data.SqlClient;
using _Net____PCCV.DTO;

namespace _Net____PCCV.DAL
{
    public class ProjectManagerDAL
    {
        // Lấy toàn bộ danh sách dự án
        public DataTable LayDanhSachDuAn()
        {
            string query = "SELECT MaDA, 'DA' + RIGHT('0000' + CAST(MaDA AS VARCHAR(10)), 4) AS MaDAHienThi, TenDA, MoTa, NgayBatDau, NgayKetThuc, TrangThai FROM DuAn ORDER BY NgayBatDau DESC";
            return DAL.DataConnection.ExecuteQuery(query);
        }

        // Thêm dự án mới
        public bool ThemDuAn(ProjectManagerDTO duAn)
        {
            string query = "INSERT INTO DuAn (TenDA, MoTa, NgayBatDau, NgayKetThuc, TrangThai) VALUES (@TenDA, @MoTa, @NgayBatDau, @NgayKetThuc, @TrangThai)";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@TenDA", duAn.TenDA),
                new SqlParameter("@MoTa", (object)duAn.MoTa ?? DBNull.Value),
                new SqlParameter("@NgayBatDau", duAn.NgayBatDau),
                new SqlParameter("@NgayKetThuc", duAn.NgayKetThuc),
                new SqlParameter("@TrangThai", duAn.TrangThai)
            };
            return DAL.DataConnection.ExecuteStoredProcedure(query, parameters);
        }

        // Sửa dự án
        public bool SuaDuAn(ProjectManagerDTO duAn)
        {
            string query = "UPDATE DuAn SET TenDA = @TenDA, MoTa = @MoTa, NgayBatDau = @NgayBatDau, NgayKetThuc = @NgayKetThuc, TrangThai = @TrangThai WHERE MaDA = @MaDA";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDA", duAn.MaDA),
                new SqlParameter("@TenDA", duAn.TenDA),
                new SqlParameter("@MoTa", (object)duAn.MoTa ?? DBNull.Value),
                new SqlParameter("@NgayBatDau", duAn.NgayBatDau),
                new SqlParameter("@NgayKetThuc", duAn.NgayKetThuc),
                new SqlParameter("@TrangThai", duAn.TrangThai)
            };
            return DAL.DataConnection.ExecuteStoredProcedure(query, parameters);
        }

        // Xóa dự án
        public bool XoaDuAn(int maDA)
        {
            string query = "DELETE FROM DuAn WHERE MaDA = @MaDA";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@MaDA", maDA)
            };
            return DAL.DataConnection.ExecuteStoredProcedure(query, parameters);
        }

        // Tìm kiếm dự án theo tên
        public DataTable TimKiemDuAn(string keyword)
        {
            string query = "SELECT MaDA, 'DA' + RIGHT('0000' + CAST(MaDA AS VARCHAR(10)), 4) AS MaDAHienThi, TenDA, MoTa, NgayBatDau, NgayKetThuc, TrangThai FROM DuAn WHERE TenDA LIKE @Keyword OR MoTa LIKE @Keyword ORDER BY NgayBatDau DESC";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Keyword", "%" + keyword + "%")
            };
            return DAL.DataConnection.ExecuteQuery(query, parameters);
        }
    }
}
