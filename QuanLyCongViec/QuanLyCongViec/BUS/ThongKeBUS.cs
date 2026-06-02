using System;
using System.Data;
using QuanLyCongViec.DTO;
using QuanLyCongViec.DAL;

namespace QuanLyCongViec.BUS
{
    public class ThongKeBUS
    {
        private ThongKeDAL tkDAL = new ThongKeDAL();

        /// <summary>
        /// Tính toán các chỉ số KPI thực tế tổng quan
        /// </summary>
        public ThongKeDTO LayThongKeTongQuan(DateTime tuNgay, DateTime denNgay)
        {
            ThongKeDTO dto = new ThongKeDTO();
            DataTable dtData = tkDAL.LayDuLieuBaoCao(tuNgay, denNgay);

            if (dtData != null && dtData.Rows.Count > 0)
            {
                int tongViec = dtData.Rows.Count;
                int viecHoanThanh = 0;
                int viecTreHan = 0;

                foreach (DataRow row in dtData.Rows)
                {
                    string trangThai = row["Trạng Thái"]?.ToString().Trim();
                    string strDeadline = row["Hạn Chót"]?.ToString();

                    // Kiểm tra trạng thái hoàn thành
                    if (trangThai == "Done" || trangThai == "Hoàn Thành")
                    {
                        viecHoanThanh++;
                    }

                    // Kiểm tra trễ hạn: Quá ngày hiện tại mà trạng thái chưa xong
                    if (DateTime.TryParseExact(strDeadline, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime deadlineDate))
                    {
                        if (deadlineDate.Date < DateTime.Now.Date && trangThai != "Done" && trangThai != "Hoàn Thành")
                        {
                            viecTreHan++;
                        }
                    }
                }

                dto.TongCongViec = tongViec;
                dto.ViecTreHan = viecTreHan;
                dto.TyLeHoanThanh = tongViec > 0 ? Math.Round(((double)viecHoanThanh / tongViec) * 100, 1) : 0;
            }

            return dto;
        }

        /// <summary>
        /// Lấy bảng hiệu suất chi tiết đổ vào DataGridView đầu ra
        /// </summary>
        public DataTable LayChiTietHieuSuatThanhVien(DateTime tuNgay, DateTime denNgay)
        {
            DataTable dtStaff = tkDAL.LayPhanBoCongViecNhanSu(tuNgay, denNgay);

            DataTable dtGrid = new DataTable();
            dtGrid.Columns.Add("Tên Nhân Viên", typeof(string));
            dtGrid.Columns.Add("Việc Được Giao", typeof(int));
            dtGrid.Columns.Add("Việc Hoàn Thành", typeof(int));
            dtGrid.Columns.Add("Hiệu Suất", typeof(string));

            foreach (DataRow row in dtStaff.Rows)
            {
                int xong = Convert.ToInt32(row["SoViec_Done"]);
                int dangLam = Convert.ToInt32(row["SoViec_InProgress"]);
                int tre = Convert.ToInt32(row["SoViec_TreHan"]);
                int tong = xong + dangLam + tre; // Tổng số việc thực tế phát sinh trong kỳ

                double hieuSuat = tong > 0 ? Math.Round(((double)xong / tong) * 100, 1) : 0;

                dtGrid.Rows.Add(row["HoTen"].ToString(), tong, xong, hieuSuat.ToString() + "%");
            }

            return dtGrid;
        }

        public DataTable LayDuLieuBaoCao(DateTime tuNgay, DateTime denNgay) => tkDAL.LayDuLieuBaoCao(tuNgay, denNgay);
        public DataTable LayThongKeTrangThai(DateTime tuNgay, DateTime denNgay) => tkDAL.LayThongKeTrangThai(tuNgay, denNgay);
        public DataTable LayPhanBoCongViecNhanSu(DateTime tuNgay, DateTime denNgay) => tkDAL.LayPhanBoCongViecNhanSu(tuNgay, denNgay);
    }
}