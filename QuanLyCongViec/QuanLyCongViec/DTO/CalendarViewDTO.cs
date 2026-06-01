using System;

namespace QuanLyCongViec.DTO
{
    public class CalendarViewDTO
    {
        public int MaLich { get; set; }
        public string TieuDe { get; set; }
        public string DiaDiem { get; set; }
        public string MoTa { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; }

        public CalendarViewDTO()
        {
            MaLich = 0;
            TieuDe = "";
            DiaDiem = "";
            MoTa = "";
            ThoiGianBatDau = DateTime.Now;
            ThoiGianKetThuc = DateTime.Now;
            TrangThai = "Chưa Hoàn Thành";
        }
    }
}