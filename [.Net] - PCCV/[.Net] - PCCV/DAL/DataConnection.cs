using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Configuration;

namespace _Net____PCCV.DAL
{
    public class DataConnection
    {
        private static string connectionString =
            ConfigurationManager.ConnectionStrings["QuanLyCongViec"]?.ConnectionString
            ?? @"Data Source=.;Initial Catalog=QuanLyCongViec;Integrated Security=True;TrustServerCertificate=True";

        // Hàm khởi tạo kết nối SQL
        public static SqlConnection GetSqlConnection()
        {
            return new SqlConnection(connectionString);
        }

        // Hàm dùng chung để thực thi các câu lệnh SELECT hoặc gọi VIEW đổ vào DataGridView
        // (Phục vụ nạp dữ liệu cho frmKanbanBoard, frmTeamManager, frmPerformanceReport...)
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
                    System.Windows.Forms.MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Hệ Thống Thông Báo", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            return dataTable;
        }

        // Hàm dùng chung để thực thi gọi các STORED PROCEDURE (Thêm, Sửa, Xóa, Đăng ký)
        // Trả về true nếu thực thi thành công, false nếu thất bại (Kích hoạt Trigger báo lỗi định dạng...)
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
                        // Không dùng CommandType.StoredProcedure vì đang dùng query trực tiếp
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }

                        int result = cmd.ExecuteNonQuery();
                        isSuccess = true; // Thực thi qua lệnh không lỗi nghĩa là thành công
                    }
                }
                catch (SqlException sqlEx)
                {
                    // Bắt trúng các lỗi chủ động ném ra từ TRIGGER (Lỗi Email sai định dạng, lỗi giao việc sai quy chế)
                    System.Windows.Forms.MessageBox.Show(sqlEx.Message, "Lỗi Nghiệp Vụ Hệ Thống", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show("Lỗi hệ thống bất ngờ: " + ex.Message, "Thông Báo Lỗi", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                }
            }
            return isSuccess;
        }
    }
}
