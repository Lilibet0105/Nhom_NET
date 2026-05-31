using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class NguoiDungDTO
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string HoTen { get; set; }
        public string Email { get; set; }
        public string ChucVu { get; set; }
        public string TrangThai { get; set; }

        public NguoiDungDTO() { }

        public NguoiDungDTO(string username, string password, string fullname, string email, string role, string status = "Chờ duyệt")
        {
            TenDangNhap = username;
            MatKhau = password;
            HoTen = fullname;
            Email = email;
            ChucVu = role;
            TrangThai = status;
        }
    }
}