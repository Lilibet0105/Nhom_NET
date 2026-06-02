using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCongViec.DTO;

namespace QuanLyCongViec.DAL
{
    public class ThanhVienDAL
    {
        public DataTable LayDanhSach()
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = DataConnection.GetSqlConnection())
            {
                string query = "SELECT MaNV AS MaTV, HoTen, SoDienThoai, Email, ViTri AS ChucVu FROM NhanVien";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) { da.Fill(dt); }
                }
            }
            return dt;
        }

        public bool Them(ThanhVienDTO tv)
        {
            using (SqlConnection conn = DataConnection.GetSqlConnection())
            {
                string query = "INSERT INTO NhanVien (HoTen, SoDienThoai, Email, ViTri, MaPB) " +
                               "VALUES (@HoTen, @SDT, @Email, @ViTri, (SELECT TOP 1 MaPB FROM PhongBan))";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@HoTen", tv.HoTen);
                    cmd.Parameters.AddWithValue("@SDT", tv.SoDienThoai);
                    cmd.Parameters.AddWithValue("@Email", tv.Email);
                    cmd.Parameters.AddWithValue("@ViTri", tv.ChucVu);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Sua(ThanhVienDTO tv)
        {
            using (SqlConnection conn = DataConnection.GetSqlConnection())
            {
                string query = "UPDATE NhanVien SET HoTen = @HoTen, SoDienThoai = @SDT, Email = @Email, ViTri = @ViTri WHERE MaNV = @MaNV";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", tv.MaTV);
                    cmd.Parameters.AddWithValue("@HoTen", tv.HoTen);
                    cmd.Parameters.AddWithValue("@SDT", tv.SoDienThoai);
                    cmd.Parameters.AddWithValue("@Email", tv.Email);
                    cmd.Parameters.AddWithValue("@ViTri", tv.ChucVu);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public bool Xoa(string maTV)
        {
            using (SqlConnection conn = DataConnection.GetSqlConnection())
            {
                string query = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNV", maTV);
                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

        public DataTable TimKiem(string keyword)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = DataConnection.GetSqlConnection())
            {
                string query = "SELECT MaNV AS MaTV, HoTen, SoDienThoai, Email, ViTri AS ChucVu " +
                               "FROM NhanVien WHERE HoTen LIKE @Key OR MaNV LIKE @Key";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Key", "%" + keyword + "%");
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd)) { da.Fill(dt); }
                }
            }
            return dt;
        }
    }
}