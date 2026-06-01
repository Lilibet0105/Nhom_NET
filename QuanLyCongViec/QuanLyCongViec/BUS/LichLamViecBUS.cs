using System;
using System.Data;
using QuanLyCongViec.DAL;
using QuanLyCongViec.DTO;

namespace QuanLyCongViec.BUS
{
    public class LichLamViecBUS
    {
        private LichLamViecDAL lichDAL = new LichLamViecDAL();

        // Lấy danh sách lịch làm việc dưới dạng DataTable
        public DataTable LayDanhSachLich()
        {
            return lichDAL.LayDanhSachLich();
        }

        // Thêm lịch làm việc mới
        public bool ThemLich(LichLamViecDTO lich)
        {
            // Validate dữ liệu trước khi thêm
            if (string.IsNullOrWhiteSpace(lich.TieuDe))
            {
                throw new ArgumentException("Tiêu đề không được để trống!");
            }
            if (lich.ThoiGianKetThuc < lich.ThoiGianBatDau)
            {
                throw new ArgumentException("Thời gian kết thúc phải lớn hơn hoặc bằng thời gian bắt đầu!");
            }
            return lichDAL.ThemLich(lich);
        }

        // Sửa lịch làm việc
        public bool SuaLich(LichLamViecDTO lich)
        {
            if (lich.MaLich <= 0)
            {
                throw new ArgumentException("Mã lịch không hợp lệ!");
            }
            if (string.IsNullOrWhiteSpace(lich.TieuDe))
            {
                throw new ArgumentException("Tiêu đề không được để trống!");
            }
            if (lich.ThoiGianKetThuc < lich.ThoiGianBatDau)
            {
                throw new ArgumentException("Thời gian kết thúc phải lớn hơn hoặc bằng thời gian bắt đầu!");
            }
            return lichDAL.SuaLich(lich);
        }

        // Xóa lịch làm việc
        public bool XoaLich(int maLich)
        {
            if (maLich <= 0)
            {
                throw new ArgumentException("Mã lịch không hợp lệ!");
            }
            return lichDAL.XoaLich(maLich);
        }

        // Tìm kiếm lịch làm việc
        public DataTable TimKiemLich(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return LayDanhSachLich();
            }
            return lichDAL.TimKiemLich(keyword);
        }
    }
}