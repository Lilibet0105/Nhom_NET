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
            string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @user AND MatKhau = @pass AND TrangThai = N'Đang hoạt động'";

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
            string query = "SELECT COUNT(*) FROM NguoiDung WHERE TenDangNhap = @user";

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
            string query = "INSERT INTO NguoiDung (TenDangNhap, MatKhau, HoTen, Email, ChucVu, TrangThai) VALUES (@user, @pass, @name, @email, @role, N'Chờ duyệt')";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@user", user.TenDangNhap),
                new SqlParameter("@pass", user.MatKhau),
                new SqlParameter("@name", user.HoTen),
                new SqlParameter("@email", user.Email),
                new SqlParameter("@role", user.ChucVu)
            };

            return ExecuteStoredProcedure(query, parameters);
        }
    }
}