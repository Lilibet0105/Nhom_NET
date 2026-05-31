using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _.Net____PCCV
{
    public static class UserSession
    {
        // Lưu tên đăng nhập của người vừa đăng nhập thành công
        public static string Username { get; set; } = "";

        // Lưu quyền hạn: "Admin", "Manager", hoặc "Staff"
        public static string Role { get; set; } = "Staff";
    }
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new GUI.frmLogin());
        }
    }
}
