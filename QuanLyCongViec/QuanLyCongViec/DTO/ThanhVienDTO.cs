using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyCongViec.DTO
{
    public class ThanhVienDTO
    {
        public string MaTV { get; set; }
        public string HoTen { get; set; }
        public string ChucVu { get; set; }
        public string SoDienThoai { get; set; }
        public string Email { get; set; }

        public ThanhVienDTO() { }
        public ThanhVienDTO(string maTV, string hoTen, string chucVu, string sdt, string email)
        {
            MaTV = maTV;
            HoTen = hoTen;
            ChucVu = chucVu;
            SoDienThoai = sdt;
            Email = email;
        }
    }
}
