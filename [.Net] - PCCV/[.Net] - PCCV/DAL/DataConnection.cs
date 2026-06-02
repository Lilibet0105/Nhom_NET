using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace _Net____PCCV.DAL
{
    public class DataConnection
    {
        private static readonly string connectionString = BuildConnectionString();

        private static string BuildConnectionString()
        {
            string outputMdf = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "QuanLyCongViec.mdf");
            if (File.Exists(outputMdf))
            {
                return CreateLocalDbConnection(outputMdf);
            }

            string configured = ConfigurationManager.ConnectionStrings["QuanLyCongViec"]?.ConnectionString;
            if (!string.IsNullOrWhiteSpace(configured))
            {
                return configured;
            }

            return @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=QuanLyCongViec;Integrated Security=True;TrustServerCertificate=True";
        }

        private static string CreateLocalDbConnection(string mdfPath)
        {
            return "Data Source=(LocalDB)\\MSSQLLocalDB;" +
                   "AttachDbFilename=" + mdfPath + ";" +
                   "Integrated Security=True;Connect Timeout=30;TrustServerCertificate=True";
        }

        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }

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
                            cmd.Parameters.AddRange(parameters);
                        }

                        using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                        {
                            adapter.Fill(dataTable);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Hệ Thống Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return dataTable;
        }

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
                            cmd.Parameters.AddRange(parameters);
                        }

                        cmd.ExecuteNonQuery();
                        isSuccess = true;
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show(sqlEx.Message, "Lỗi Nghiệp Vụ Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống bất ngờ: " + ex.Message, "Thông Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            return isSuccess;
        }
    }
}
