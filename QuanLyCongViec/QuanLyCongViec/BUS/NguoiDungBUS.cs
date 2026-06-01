using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using DAL;
using DTO;

namespace BUS
{
    public class NguoiDungBUS
    {
        private NguoiDungDAL _userDAL = new NguoiDungDAL();

        public string ComputeSHA256(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }

        public bool Login(string username, string password, out string errorMsg)
        {
            errorMsg = "";
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                errorMsg = "Tài khoản và mật khẩu không được để trống!";
                return false;
            }

            string hashedPass = ComputeSHA256(password);
            bool isValid = _userDAL.CheckLogin(username, hashedPass);

            if (!isValid)
                errorMsg = "Tài khoản, mật khẩu không đúng hoặc chưa được phê duyệt!";

            return isValid;
        }

        public bool Register(NguoiDungDTO user, out string errorMsg)
        {
            errorMsg = "";

            if (string.IsNullOrWhiteSpace(user.TenDangNhap) || string.IsNullOrWhiteSpace(user.MatKhau) ||
                string.IsNullOrWhiteSpace(user.HoTen) || string.IsNullOrWhiteSpace(user.Email))
            {
                errorMsg = "Vui lòng nhập đầy đủ tất cả các trường thông tin bắt buộc!";
                return false;
            }

            string emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(user.Email, emailPattern))
            {
                errorMsg = "Định dạng email không hợp lệ (Ví dụ đúng: abc@gmail.com)!";
                return false;
            }

            if (_userDAL.IsUsernameExist(user.TenDangNhap))
            {
                errorMsg = "Tên đăng nhập này đã tồn tại trên hệ thống!";
                return false;
            }

            user.MatKhau = ComputeSHA256(user.MatKhau);

            return _userDAL.InsertRegister(user);
        }
    }
}