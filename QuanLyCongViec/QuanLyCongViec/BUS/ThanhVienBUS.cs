using System;
using System.Data;
using System.Text.RegularExpressions;
using _Net____PCCV.DTO;
using _Net____PCCV.DAL;

namespace _Net____PCCV.BUS
{
    public class ThanhVienBUS
    {
        private ThanhVienDAL tvDAL = new ThanhVienDAL();

        public DataTable LayDanhSachThanhVien() => tvDAL.LayDanhSach();

        public bool ThemThanhVien(ThanhVienDTO tv)
        {
            if (string.IsNullOrWhiteSpace(tv.HoTen))
                throw new Exception("Họ tên không được để trống!");

            KiemTraSoDienThoai(tv.SoDienThoai);

            return tvDAL.Them(tv);
        }

        public bool SuaThanhVien(ThanhVienDTO tv)
        {
            if (string.IsNullOrWhiteSpace(tv.MaTV))
                throw new Exception("Không tìm thấy mã thành viên cần chỉnh sửa!");
            if (string.IsNullOrWhiteSpace(tv.HoTen))
                throw new Exception("Họ tên không được để trống!");

            KiemTraSoDienThoai(tv.SoDienThoai);

            return tvDAL.Sua(tv);
        }

        public bool XoaThanhVien(string maTV) => tvDAL.Xoa(maTV);

        public DataTable TimKiemThanhVien(string tuKhoa) => tvDAL.TimKiem(tuKhoa);

        private void KiemTraSoDienThoai(string sdt)
        {
            if (string.IsNullOrWhiteSpace(sdt))
            {
                throw new Exception("Số điện thoại không được để trống!");
            }

            // Giải thích định dạng @"^0\d{9}$":
            // ^0  : Bắt buộc ký tự đầu tiên phải là số 0
            // \d{9}: Theo sau phải là chính xác 9 chữ số (từ 0-9)
            // $   : Kết thúc chuỗi (không cho phép thừa ký tự nào khác)
            string pattern = @"^0\d{9}$";

            if (!Regex.IsMatch(sdt, pattern))
            {
                throw new Exception("Số điện thoại không hợp lệ!\n(Phải bắt đầu bằng số 0 và có đúng 10 chữ số).");
            }
        }
    }
}