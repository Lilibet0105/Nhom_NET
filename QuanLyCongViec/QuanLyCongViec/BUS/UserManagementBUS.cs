using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCongViec.DTO;
using QuanLyCongViec.DAL; // Để gọi được lớp DataConnection

namespace QuanLyCongViec.BUS
{
    public class ThanhVienBUS
    {
        public DataTable LayDanhSachThanhVien() => new DataTable();
        public bool ThemThanhVien(ThanhVienDTO tv) => true;
        public bool SuaThanhVien(ThanhVienDTO tv) => true;
        public bool XoaThanhVien(int maTV) => true;
        public DataTable TimKiemThanhVien(string tuKhoa) => new DataTable();
    }

    public class TaiKhoanBUS
    {
        // 1. Hàm lấy danh sách tài khoản thực tế đổ lên lưới DataGridView
        public DataTable LayDanhSachTaiKhoan()
        {
            string query = "SELECT TenDangNhap, VaiTro, TrangThai FROM TaiKhoan";
            return DataConnection.ExecuteQuery(query);
        }

        // 2. Hàm cập nhật quyền và trạng thái thực tế xuống Database
        public bool CapNhatQuyenVaTrangThai(TaiKhoanDTO tk)
        {
            string query = "UPDATE TaiKhoan SET VaiTro = @VaiTro, TrangThai = @TrangThai WHERE TenDangNhap = @TenDangNhap";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@VaiTro", SqlDbType.NVarChar) { Value = tk.VaiTro },
                new SqlParameter("@TrangThai", SqlDbType.NVarChar) { Value = tk.TrangThai },
                new SqlParameter("@TenDangNhap", SqlDbType.VarChar) { Value = tk.TenDangNhap }
            };

            // Gọi hàm thực thi dùng chung dưới DAL để cập nhật trực tiếp vào SQL Server
            return DataConnection.ExecuteNonQuery(query, parameters);
        }
    }
}