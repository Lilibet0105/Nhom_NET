using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace QuanLyCongViec.DAL 
{
    public class DataConnection
    {
        // private static string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\ADMIN\QuanLyCongViec.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;";
        private static string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyCongViec;Integrated Security=True;Connect Timeout=30";
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }

        // Hàm dùng chung để thực thi các câu lệnh SELECT hoặc gọi VIEW đổ vào DataGridView
        public static DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable dataTable = new DataTable();
            using (SqlConnection conn = GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            // Sử dụng Clone tham số an toàn tránh lỗi đúp tham số khi tái sử dụng
                            foreach (var p in parameters)
                            {
                                cmd.Parameters.Add((SqlParameter)((ICloneable)p).Clone());
                            }
                        }
                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Hệ Thống Thông Báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            return dataTable;
        }

        // Hàm dùng chung để thực thi gọi các STORED PROCEDURE 
        public static bool ExecuteStoredProcedure(string query, SqlParameter[] parameters)
        {
            bool isSuccess = false;
            using (SqlConnection conn = GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            foreach (var p in parameters)
                            {
                                cmd.Parameters.Add((SqlParameter)((ICloneable)p).Clone());
                            }
                        }

                        int result = cmd.ExecuteNonQuery();
                        isSuccess = true;
                    }
                }
                catch (SqlException sqlEx)
                {
                    System.Windows.Forms.MessageBox.Show(sqlEx.Message, "Lỗi Nghiệp Vụ Hệ Thống", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Lỗi hệ thống bất ngờ: " + ex.Message, "Thông Báo Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            return isSuccess;
        }

        // Hàm dùng chung để thực thi INSERT, UPDATE, DELETE thông thường
        public static bool ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            bool isSuccess = false;
            using (SqlConnection conn = GetSqlConnection())
            {
                try
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            foreach (var p in parameters)
                            {
                                cmd.Parameters.Add((SqlParameter)((ICloneable)p).Clone());
                            }
                        }

                        int result = cmd.ExecuteNonQuery();
                        isSuccess = (result > 0);
                    }
                }
                catch (SqlException sqlEx)
                {
                    System.Windows.Forms.MessageBox.Show("Lỗi SQL: " + sqlEx.Message, "Lỗi Nghiệp Vụ Hệ Thống", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    isSuccess = false;
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Lỗi hệ thống DAL: " + ex.Message, "Thông Báo Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                    isSuccess = false;
                }
            }
            return isSuccess;
        }
    }
}