using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using _Net____PCCV.DAL;
using _Net____PCCV.DTO;

namespace _Net____PCCV.BUS
{
    public class TaskBUS
    {
        private readonly TaskDAL taskDAL = new TaskDAL();

        public DataTable LayDanhSachCongViec(string searchText = null, string mucDoUuTien = null, int? maNhanVien = null)
        {
            DataTable tasks = taskDAL.LayDanhSachCongViec(null, mucDoUuTien, maNhanVien);
            return LocCongViecGanDung(tasks, searchText);
        }

        public TaskDTO LayCongViecTheoMa(int maTask)
        {
            DataTable dt = taskDAL.LayCongViecTheoMa(maTask);
            if (dt.Rows.Count == 0)
            {
                return null;
            }

            return MapTask(dt.Rows[0]);
        }

        public DataTable LayDanhSachNhanVien()
        {
            return taskDAL.LayDanhSachNhanVien();
        }

        public DataTable LayDanhSachDuAn()
        {
            return taskDAL.LayDanhSachDuAn();
        }

        public DataTable LayThanhVienTheoDuAn(int maDA)
        {
            return taskDAL.LayThanhVienTheoDuAn(maDA);
        }

        public bool LuuCongViec(TaskDTO task)
        {
            ValidateTask(task);
            return task.MaTask > 0 ? taskDAL.SuaCongViec(task) : taskDAL.ThemCongViec(task);
        }

        public bool CapNhatTrangThai(int maTask, string trangThai)
        {
            if (maTask <= 0)
            {
                throw new ArgumentException("Mã công việc không hợp lệ!");
            }

            if (!LaTrangThaiHopLe(trangThai))
            {
                throw new ArgumentException("Trạng thái công việc không hợp lệ!");
            }

            return taskDAL.CapNhatTrangThai(maTask, trangThai);
        }

        public bool XoaCongViec(int maTask)
        {
            if (maTask <= 0)
            {
                throw new ArgumentException("Mã công việc không hợp lệ!");
            }

            return taskDAL.XoaCongViec(maTask);
        }

        public DataTable LayBinhLuanTheoCongViec(int maTask)
        {
            if (maTask <= 0)
            {
                throw new ArgumentException("Mã công việc không hợp lệ!");
            }

            return taskDAL.LayBinhLuanTheoCongViec(maTask);
        }

        public bool ThemBinhLuan(TaskCommentDTO comment)
        {
            if (comment.MaTask <= 0)
            {
                throw new ArgumentException("Mã công việc không hợp lệ!");
            }

            if (comment.MaNV <= 0)
            {
                throw new ArgumentException("Vui lòng chọn người bình luận!");
            }

            if (string.IsNullOrWhiteSpace(comment.NoiDung))
            {
                throw new ArgumentException("Nội dung bình luận không được để trống!");
            }

            return taskDAL.ThemBinhLuan(comment);
        }

        public TaskDTO MapTask(DataRow row)
        {
            return new TaskDTO
            {
                MaTask = Convert.ToInt32(row["MaTask"]),
                TenTask = row["TenTask"].ToString(),
                MoTa = row["MoTa"] == DBNull.Value ? string.Empty : row["MoTa"].ToString(),
                MucDoUuTien = row["MucDoUuTien"].ToString(),
                TrangThai = row["TrangThai"].ToString(),
                Deadline = Convert.ToDateTime(row["Deadline"]),
                NgayTao = Convert.ToDateTime(row["NgayTao"]),
                MaDA = Convert.ToInt32(row["MaDA"]),
                TenDA = row["TenDA"].ToString(),
                MaNguoiNhan = Convert.ToInt32(row["MaNguoiNhan"]),
                TenNguoiNhan = row["TenNguoiNhan"].ToString(),
                TinhTrang = row["TinhTrang"].ToString()
            };
        }

        private void ValidateTask(TaskDTO task)
        {
            if (task == null)
            {
                throw new ArgumentException("Dữ liệu công việc không hợp lệ!");
            }

            if (string.IsNullOrWhiteSpace(task.TenTask))
            {
                throw new ArgumentException("Tên công việc không được để trống!");
            }

            if (task.MaDA <= 0)
            {
                throw new ArgumentException("Vui lòng chọn dự án!");
            }

            if (task.MaNguoiNhan <= 0)
            {
                throw new ArgumentException("Vui lòng chọn người thực hiện!");
            }

            if (!LaMucDoHopLe(task.MucDoUuTien))
            {
                throw new ArgumentException("Mức độ ưu tiên không hợp lệ!");
            }

            if (!LaTrangThaiHopLe(task.TrangThai))
            {
                throw new ArgumentException("Trạng thái công việc không hợp lệ!");
            }

            if (task.Deadline < task.NgayTao)
            {
                throw new ArgumentException("Ngày hạn hoàn thành không được nhỏ hơn ngày bắt đầu!");
            }
        }

        private DataTable LocCongViecGanDung(DataTable tasks, string searchText)
        {
            if (tasks == null || string.IsNullOrWhiteSpace(searchText))
            {
                return tasks;
            }

            string normalizedKeyword = ChuanHoaTuKhoa(searchText);
            string[] keywordParts = normalizedKeyword
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            DataTable result = tasks.Clone();
            foreach (DataRow row in tasks.Rows)
            {
                string searchableText = ChuanHoaTuKhoa(string.Join(" ", new[]
                {
                    row["TenTask"] == DBNull.Value ? string.Empty : row["TenTask"].ToString(),
                    row["MoTa"] == DBNull.Value ? string.Empty : row["MoTa"].ToString(),
                    row["TenDA"] == DBNull.Value ? string.Empty : row["TenDA"].ToString(),
                    row["TenNguoiNhan"] == DBNull.Value ? string.Empty : row["TenNguoiNhan"].ToString()
                }));

                if (KhopGanDung(searchableText, normalizedKeyword, keywordParts))
                {
                    result.ImportRow(row);
                }
            }

            return result;
        }

        private bool KhopGanDung(string searchableText, string normalizedKeyword, string[] keywordParts)
        {
            if (searchableText.Contains(normalizedKeyword))
            {
                return true;
            }

            string[] searchableParts = searchableText
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            return keywordParts.All(keywordPart =>
                searchableParts.Any(searchablePart =>
                    searchablePart.Contains(keywordPart)
                    || keywordPart.Contains(searchablePart)
                    || KhoangCachLevenshtein(searchablePart, keywordPart) <= LayNguongSaiKhac(keywordPart.Length)));
        }

        private int LayNguongSaiKhac(int length)
        {
            if (length <= 4)
            {
                return 0;
            }

            return length <= 8 ? 1 : 2;
        }

        private string ChuanHoaTuKhoa(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            string normalized = value.Trim().ToLowerInvariant().Normalize(NormalizationForm.FormD);
            StringBuilder builder = new StringBuilder(normalized.Length);

            foreach (char c in normalized)
            {
                UnicodeCategory category = CharUnicodeInfo.GetUnicodeCategory(c);
                if (category != UnicodeCategory.NonSpacingMark)
                {
                    builder.Append(c == 'đ' ? 'd' : c);
                }
            }

            return builder.ToString().Normalize(NormalizationForm.FormC);
        }

        private int KhoangCachLevenshtein(string left, string right)
        {
            int[,] distances = new int[left.Length + 1, right.Length + 1];

            for (int i = 0; i <= left.Length; i++)
            {
                distances[i, 0] = i;
            }

            for (int j = 0; j <= right.Length; j++)
            {
                distances[0, j] = j;
            }

            for (int i = 1; i <= left.Length; i++)
            {
                for (int j = 1; j <= right.Length; j++)
                {
                    int cost = left[i - 1] == right[j - 1] ? 0 : 1;
                    distances[i, j] = Math.Min(
                        Math.Min(distances[i - 1, j] + 1, distances[i, j - 1] + 1),
                        distances[i - 1, j - 1] + cost);
                }
            }

            return distances[left.Length, right.Length];
        }

        private bool LaMucDoHopLe(string mucDo)
        {
            return mucDo == "Thấp" || mucDo == "Trung bình" || mucDo == "Cao";
        }

        private bool LaTrangThaiHopLe(string trangThai)
        {
            return trangThai == "To Do" || trangThai == "In Progress" || trangThai == "Done";
        }
    }
}
