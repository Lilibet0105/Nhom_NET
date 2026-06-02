using QuanLyCongViec.DAL; 
using QuanLyCongViec.DTO; 
using System;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography; 
using System.Text;

namespace QuanLyCongViec.BUS
{
   

    public class TaiKhoanBUS
    {
        // Lấy danh sách tài khoản thực tế đổ lên lưới DataGridView
        public DataTable LayDanhSachTaiKhoan()
        {
            string query = "SELECT TenDangNhap, VaiTro, TrangThai FROM TaiKhoan";
            return DataConnection.ExecuteQuery(query);
        }

        // Cập nhật quyền và trạng thái thực tế xuống Database
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
        public string ComputeSHA256(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Chuyển chuỗi đầu vào thành mảng bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Chuyển mảng bytes thành chuỗi ký tự dạng Hexadecimal (thập lục phân)
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public DataTable LayDanhSachNhanVien()
        {
            string query = "SELECT MaNV, HoTen FROM NhanVien";
            return DataConnection.ExecuteQuery(query);
        }
    }
}