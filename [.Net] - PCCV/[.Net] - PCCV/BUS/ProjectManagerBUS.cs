using System;
using System.Data;
using _Net____PCCV.DAL;
using _Net____PCCV.DTO;

namespace _Net____PCCV.BUS
{
    public class ProjectManagerBUS
    {
        private ProjectManagerDAL duAnDAL = new ProjectManagerDAL();

        // Lấy danh sách dự án dưới dạng DataTable
        public DataTable LayDanhSachDuAn()
        {
            return duAnDAL.LayDanhSachDuAn();
        }

        // Thêm dự án mới
        public bool ThemDuAn(ProjectManagerDTO duAn)
        {
            if (string.IsNullOrWhiteSpace(duAn.TenDA))
            {
                throw new ArgumentException("Tên dự án không được để trống!");
            }
            if (duAn.NgayKetThuc < duAn.NgayBatDau)
            {
                throw new ArgumentException("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!");
            }
            return duAnDAL.ThemDuAn(duAn);
        }

        // Sửa dự án
        public bool SuaDuAn(ProjectManagerDTO duAn)
        {
            if (duAn.MaDA <= 0)
            {
                throw new ArgumentException("Mã dự án không hợp lệ!");
            }
            if (string.IsNullOrWhiteSpace(duAn.TenDA))
            {
                throw new ArgumentException("Tên dự án không được để trống!");
            }
            if (duAn.NgayKetThuc < duAn.NgayBatDau)
            {
                throw new ArgumentException("Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!");
            }
            return duAnDAL.SuaDuAn(duAn);
        }

        // Xóa dự án
        public bool XoaDuAn(int maDA)
        {
            if (maDA <= 0)
            {
                throw new ArgumentException("Mã dự án không hợp lệ!");
            }
            return duAnDAL.XoaDuAn(maDA);
        }

        // Tìm kiếm dự án
        public DataTable TimKiemDuAn(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return LayDanhSachDuAn();
            }
            return duAnDAL.TimKiemDuAn(keyword);
        }
    }
}