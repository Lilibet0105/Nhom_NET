using QuanLyCongViec.DAL;
using DTO;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class NguoiDungDAL : DataConnection
    {
        public bool CheckLogin(string username, string hashedPassword)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @user AND MatKhau = @pass AND TrangThai = N'Hoạt động'";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@user", username),
                new SqlParameter("@pass", hashedPassword)
            };

            DataTable dt = DataConnection.ExecuteQuery(query, parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]) > 0;
            }
            return false;
        }

        public bool IsUsernameExist(string username)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE TenDangNhap = @user";
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@user", username)
            };

            DataTable dt = DataConnection.ExecuteQuery(query, parameters);
            if (dt != null && dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]) > 0;
            }
            return false;
        }

        public bool InsertRegister(NguoiDungDTO user)
        {
            string sqlScript = @"
                DECLARE @NewMaNV INT;

                -- 1. Chèn dữ liệu nhân viên cá nhân
                INSERT INTO NhanVien (HoTen, Email, ViTri, MaPB) 
                VALUES (@name, @email, @vitri, NULL);

                -- Lấy mã tự động tăng chính xác vừa sinh ra trong phiên làm việc này
                SET @NewMaNV = SCOPE_IDENTITY();

                -- 2. Chèn dữ liệu tài khoản liên kết chặt chẽ với mã nhân viên vừa tạo
                INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, TrangThai, MaNV) 
                VALUES (@user, @pass, @vaitro, N'Chờ duyệt', @NewMaNV);
            ";

            SqlParameter[] parameters = new SqlParameter[]
            {
                // Tham số bảng NhanVien
                new SqlParameter("@name", SqlDbType.NVarChar) { Value = user.HoTen },
                new SqlParameter("@email", SqlDbType.VarChar) { Value = user.Email },
                new SqlParameter("@vitri", SqlDbType.NVarChar) { Value = string.IsNullOrEmpty(user.ChucVu) ? (object)DBNull.Value : user.ChucVu },
                
                // Tham số bảng TaiKhoan
                new SqlParameter("@user", SqlDbType.VarChar) { Value = user.TenDangNhap },
                new SqlParameter("@pass", SqlDbType.VarChar) { Value = user.MatKhau },
                new SqlParameter("@vaitro", SqlDbType.NVarChar) { Value = "Staff" }
            };

            // Thực thi thông qua hàm ExecuteNonQuery dùng chung của dự án
            return DataConnection.ExecuteNonQuery(sqlScript, parameters);
        }
    }
}