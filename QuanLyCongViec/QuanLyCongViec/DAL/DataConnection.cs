using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace QuanLyCongViec.DAL
{
    public class DataConnection
    {
        private static readonly object connectionLock = new object();
        private static string connectionString;

        private static string ConnectionString
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    return connectionString;
                }

                lock (connectionLock)
                {
                    if (string.IsNullOrWhiteSpace(connectionString))
                    {
                        connectionString = ResolveConnectionString();
                    }
                }

                return connectionString;
            }
        }

        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(ConnectionString);
        }

        public static bool TestConnection(out string errorMessage)
        {
            errorMessage = string.Empty;
            try
            {
                using (SqlConnection conn = GetSqlConnection())
                {
                    conn.Open();
                }

                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        private static string ResolveConnectionString()
        {
            string configured = ConfigurationManager.ConnectionStrings["QuanLyCongViecDB"]?.ConnectionString;
            string outputMdfPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Database", "QuanLyCongViec.mdf");
            string projectMdfPath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\Database\QuanLyCongViec.mdf"));

            string[] candidates =
            {
                BuildLocalDbConnection(outputMdfPath),
                BuildLocalDbConnection(projectMdfPath),
                configured,
                @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyCongViec;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=5",
                @"Data Source=.;Initial Catalog=QuanLyCongViec;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=5",
                @"Data Source=(local);Initial Catalog=QuanLyCongViec;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=5"
            };

            foreach (string candidate in candidates.Where(c => !string.IsNullOrWhiteSpace(c)).Distinct())
            {
                if (CanOpen(candidate))
                {
                    return candidate;
                }
            }

            return configured ?? BuildLocalDbConnection(outputMdfPath);
        }

        private static string BuildLocalDbConnection(string mdfPath)
        {
            if (string.IsNullOrWhiteSpace(mdfPath) || !File.Exists(mdfPath))
            {
                return null;
            }

            return $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={mdfPath};Integrated Security=True;Connect Timeout=5;TrustServerCertificate=True";
        }

        private static bool CanOpen(string candidate)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(candidate))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
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
                            foreach (SqlParameter p in parameters)
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
                            foreach (SqlParameter p in parameters)
                            {
                                cmd.Parameters.Add((SqlParameter)((ICloneable)p).Clone());
                            }
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
                            foreach (SqlParameter p in parameters)
                            {
                                cmd.Parameters.Add((SqlParameter)((ICloneable)p).Clone());
                            }
                        }

                        int result = cmd.ExecuteNonQuery();
                        isSuccess = result > 0;
                    }
                }
                catch (SqlException sqlEx)
                {
                    MessageBox.Show("Lỗi SQL: " + sqlEx.Message, "Lỗi Nghiệp Vụ Hệ Thống", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isSuccess = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống DAL: " + ex.Message, "Thông Báo Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    isSuccess = false;
                }
            }

            return isSuccess;
        }
    }
}
