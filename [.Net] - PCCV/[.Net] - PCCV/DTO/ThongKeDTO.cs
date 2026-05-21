using System;

namespace _Net____PCCV.DTO
{
    public class ThongKeDTO
    {
        // Chứa số liệu cho GroupBox 1
        public int TongCongViec { get; set; }

        // Chứa số liệu cho GroupBox 2
        public double TyLeHoanThanh { get; set; }

        // Chứa số liệu cho GroupBox 3
        public int ViecTreHan { get; set; }

        // Hàm khởi tạo mặc định
        public ThongKeDTO()
        {
            TongCongViec = 0;
            TyLeHoanThanh = 0.0;
            ViecTreHan = 0;
        }
    }
}