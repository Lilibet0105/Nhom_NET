using System;

namespace _Net____PCCV.DTO
{
    public class ProjectManagerDTO
    {
        public int MaDA { get; set; }
        public string TenDA { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public string TrangThai { get; set; }

        public ProjectManagerDTO()
        {
            MaDA = 0;
            TenDA = "";
            MoTa = "";
            NgayBatDau = DateTime.Now;
            NgayKetThuc = DateTime.Now;
            TrangThai = "Mới khởi tạo";
        }
    }
}