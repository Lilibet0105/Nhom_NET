using System;
using System.Data;
using QuanLyCongViec.DTO;
// using QuanLyCongViec.DAL; // Bỏ comment dòng này sau khi bạn đã tạo lớp ThongKeDAL

namespace QuanLyCongViec.BUS
{
    public class ThongKeBUS
    {
        // Khai báo lớp DAL sau này để lấy dữ liệu từ SQL Server
        // private ThongKeDAL tkDAL = new ThongKeDAL();

        public ThongKeDTO LayThongKeTongQuan(DateTime tuNgay, DateTime denNgay)
        {
            ThongKeDTO dto = new ThongKeDTO();

            // GIAI ĐOẠN 1: Gán dữ liệu giả lập (Mock Data) để test giao diện
            dto.TongCongViec = 256;
            dto.TyLeHoanThanh = 78.5;
            dto.ViecTreHan = 18;

            /* GIAI ĐOẠN 2 (Sau khi có database): Code thực tế sẽ như sau:
            int tongViec = tkDAL.DemTongSoViec(tuNgay, denNgay);
            int viecXong = tkDAL.DemViecHoanThanh(tuNgay, denNgay);
            int treHan = tkDAL.DemViecTreHan(tuNgay, denNgay);

            dto.TongCongViec = tongViec;
            dto.ViecTreHan = treHan;
            dto.TyLeHoanThanh = tongViec > 0 ? Math.Round(((double)viecXong / tongViec) * 100, 1) : 0;
            */

            return dto;
        }

        // Hàm lấy dữ liệu cho bảng DataGridView bên dưới
        public DataTable LayChiTietHieuSuatThanhVien()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TenThanhVien", typeof(string));
            dt.Columns.Add("ChucVu", typeof(string));
            dt.Columns.Add("ViecDuocGiao", typeof(int));
            dt.Columns.Add("ViecHoanThanh", typeof(int));
            dt.Columns.Add("DiemHieuSuat", typeof(string));

            // Thêm vài dòng dữ liệu mẫu chạy thử ở Giai đoạn 1
            dt.Rows.Add("Nguyen Van An", "Truong Nhom", 50, 45, "90%");
            dt.Rows.Add("Le Thi Binh", "Thanh Vien", 45, 35, "77.7%");
            
            return dt;
        }
    }
}