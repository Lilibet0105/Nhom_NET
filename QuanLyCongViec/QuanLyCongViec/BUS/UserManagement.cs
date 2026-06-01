using System.Data;
using _Net____PCCV.DTO;

namespace _Net____PCCV.BUS
{

    public class TaiKhoanBUS
    {
        public DataTable LayDanhSachTaiKhoan() => new DataTable();
        public bool CapNhatQuyenVaTrangThai(TaiKhoanDTO tk) => true; // Viết lệnh kết nối xuống DB cập nhật vai trò, trạng thái
    }
}