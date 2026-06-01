using _Net____PCCV.DAL;
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

            DataTable dt = ExecuteQuery(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count > 0;
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

            DataTable dt = ExecuteQuery(query, parameters);

            if (dt != null && dt.Rows.Count > 0)
            {
                int count = Convert.ToInt32(dt.Rows[0][0]);
                return count > 0;
            }
            return false;
        }

        public bool InsertRegister(NguoiDungDTO user)
        {
            using (SqlConnection conn = GetSqlConnection())
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_RegisterUser", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Truyền các tham số xuống Stored Procedure
                        cmd.Parameters.AddWithValue("@HoTen", user.HoTen);
                        cmd.Parameters.AddWithValue("@Email", user.Email);
                        cmd.Parameters.AddWithValue("@TenDangNhap", user.TenDangNhap);
                        cmd.Parameters.AddWithValue("@MatKhau", user.MatKhau);
                        cmd.Parameters.AddWithValue("@ViTri", user.ChucVu);

                        // Chạy lệnh ghi xuống Database
                        cmd.ExecuteNonQuery();
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi thực thi Đăng ký: " + ex.Message);
                    return false;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open) conn.Close();
                }
            }
        }
    }
}