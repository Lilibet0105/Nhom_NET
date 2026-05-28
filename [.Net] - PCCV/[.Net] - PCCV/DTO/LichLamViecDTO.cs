using System;

namespace _Net____PCCV.DTO
{
    public class LichLamViecDTO
    {
        public int MaLich { get; set; }
        public string TieuDe { get; set; }
        public string DiaDiem { get; set; }
        public string MoTa { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string TrangThai { get; set; }

        public LichLamViecDTO()
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