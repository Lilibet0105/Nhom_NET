namespace QuanLyCongViec.DTO
{
    public class ThanhVienDTO
    {
        public int MaTV { get; set; }
        public string HoTen { get; set; }
        public string ChucVu { get; set; }
        public string Email { get; set; }
        public string SoDienThoai { get; set; }
    }

    public class TaiKhoanDTO
    {
        public string TenDangNhap { get; set; }
        public string VaiTro { get; set; }
        public string TrangThai { get; set; }
    }
}