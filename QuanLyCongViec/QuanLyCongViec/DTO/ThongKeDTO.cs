using System;

namespace QuanLyCongViec.DTO
{
    public class ThongKeDTO
    {
        public int TongCongViec { get; set; }
        public int ViecTreHan { get; set; }
        public double TyLeHoanThanh { get; set; }

        public ThongKeDTO()
        {
            TongCongViec = 0;
            ViecTreHan = 0;
            TyLeHoanThanh = 0;
        }
    }
}