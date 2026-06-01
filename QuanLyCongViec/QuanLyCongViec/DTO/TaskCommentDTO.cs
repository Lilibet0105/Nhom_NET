using System;

namespace _Net____PCCV.DTO
{
    public class TaskCommentDTO
    {
        public int MaBL { get; set; }
        public int MaTask { get; set; }
        public int MaNV { get; set; }
        public string HoTen { get; set; }
        public string NoiDung { get; set; }
        public DateTime ThoiGian { get; set; }
        public string LinkDinhKem { get; set; }
    }
}
