using System;

namespace QuanLyCongViec.DTO
{
    public class TaskDTO
    {
        public int MaTask { get; set; }
        public string TenTask { get; set; }
        public string MoTa { get; set; }
        public string MucDoUuTien { get; set; }
        public string TrangThai { get; set; }
        public DateTime Deadline { get; set; }
        public DateTime NgayTao { get; set; }
        public int MaDA { get; set; }
        public string TenDA { get; set; }
        public int MaNguoiNhan { get; set; }
        public string TenNguoiNhan { get; set; }
        public string TinhTrang { get; set; }

        public int TienDo
        {
            get
            {
                if (TrangThai == "Done") return 100;
                if (TrangThai == "In Progress") return 50;
                return 0;
            }
        }
    }
}
