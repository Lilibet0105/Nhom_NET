using System.Data;
using _Net____PCCV.DTO;

namespace _Net____PCCV.BUS
{
    public class ThanhVienBUS
    {
        public DataTable LayDanhSachThanhVien() => new DataTable(); // Viết lệnh nạp từ DAO/DAL lên
        public bool ThemThanhVien(ThanhVienDTO tv) => true;
        public bool SuaThanhVien(ThanhVienDTO tv) => true;
        public bool XoaThanhVien(int maTV) => true;
        public DataTable TimKiemThanhVien(string tuKhoa) => new DataTable();
    }

    public class TaiKhoanBUS
    {
        public DataTable LayDanhSachTaiKhoan() => new DataTable();
        public bool CapNhatQuyenVaTrangThai(TaiKhoanDTO tk) => true; // Viết lệnh kết nối xuống DB cập nhật vai trò, trạng thái
    }
}