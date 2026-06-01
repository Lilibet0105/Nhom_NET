using System;
using System.Data;
using System.Data.SqlClient;
using QuanLyCongViec.DTO;

namespace QuanLyCongViec.DAL
{
    public class TaskDAL
    {
        public DataTable LayDanhSachCongViec(string searchText = null, string mucDoUuTien = null, int? maNhanVien = null)
        {
            string query = @"
SELECT *
FROM v_TaskDetailsAll
WHERE (@SearchText IS NULL OR TenTask LIKE @SearchText OR MoTa LIKE @SearchText)
  AND (@MucDoUuTien IS NULL OR MucDoUuTien = @MucDoUuTien)
  AND (@MaNV IS NULL OR MaNguoiNhan = @MaNV)
ORDER BY Deadline ASC";

            SqlParameter[] parameters =
            {
                new SqlParameter("@SearchText", string.IsNullOrWhiteSpace(searchText) ? (object)DBNull.Value : "%" + searchText.Trim() + "%"),
                new SqlParameter("@MucDoUuTien", string.IsNullOrWhiteSpace(mucDoUuTien) ? (object)DBNull.Value : mucDoUuTien),
                new SqlParameter("@MaNV", maNhanVien.HasValue ? (object)maNhanVien.Value : DBNull.Value)
            };

            return DataConnection.ExecuteQuery(query, parameters);
        }

        public DataTable LayCongViecTheoMa(int maTask)
        {
            string query = "SELECT * FROM v_TaskDetailsAll WHERE MaTask = @MaTask";
            SqlParameter[] parameters = { new SqlParameter("@MaTask", maTask) };
            return DataConnection.ExecuteQuery(query, parameters);
        }

        public DataTable LayDanhSachNhanVien()
        {
            return DataConnection.ExecuteQuery("SELECT MaNV, HoTen FROM NhanVien ORDER BY HoTen");
        }

        public DataTable LayDanhSachDuAn()
        {
            return DataConnection.ExecuteQuery("SELECT MaDA, TenDA FROM DuAn ORDER BY TenDA");
        }

        public DataTable LayThanhVienTheoDuAn(int maDA)
        {
            string query = @"
SELECT nv.MaNV, nv.HoTen
FROM NhanVien nv
LEFT JOIN ThanhVienDuAn tv
    ON nv.MaNV = tv.MaNV
   AND tv.MaDA = @MaDA
ORDER BY
    CASE WHEN tv.MaNV IS NULL THEN 1 ELSE 0 END,
    nv.HoTen";
            SqlParameter[] parameters = { new SqlParameter("@MaDA", maDA) };
            return DataConnection.ExecuteQuery(query, parameters);
        }

        public bool ThemCongViec(TaskDTO task)
        {
            string query = @"
IF NOT EXISTS (SELECT 1 FROM ThanhVienDuAn WHERE MaDA = @MaDA AND MaNV = @MaNguoiThucHien)
BEGIN
    INSERT INTO ThanhVienDuAn (MaDA, MaNV, VaiTroTrongDuAn)
    VALUES (@MaDA, @MaNguoiThucHien, N'Người thực hiện công việc');
END

INSERT INTO CongViec (TenTask, MoTa, MucDoUuTien, TrangThai, Deadline, NgayTao, MaDA, MaNguoiThucHien)
VALUES (@TenTask, @MoTa, @MucDoUuTien, @TrangThai, @Deadline, @NgayTao, @MaDA, @MaNguoiThucHien)";
            return DataConnection.ExecuteStoredProcedure(query, TaoThamSoCongViec(task, includeId: false));
        }

        public bool SuaCongViec(TaskDTO task)
        {
            string query = @"
IF NOT EXISTS (SELECT 1 FROM ThanhVienDuAn WHERE MaDA = @MaDA AND MaNV = @MaNguoiThucHien)
BEGIN
    INSERT INTO ThanhVienDuAn (MaDA, MaNV, VaiTroTrongDuAn)
    VALUES (@MaDA, @MaNguoiThucHien, N'Người thực hiện công việc');
END

UPDATE CongViec
SET TenTask = @TenTask,
    MoTa = @MoTa,
    MucDoUuTien = @MucDoUuTien,
    TrangThai = @TrangThai,
    Deadline = @Deadline,
    NgayTao = @NgayTao,
    MaDA = @MaDA,
    MaNguoiThucHien = @MaNguoiThucHien
WHERE MaTask = @MaTask";
            return DataConnection.ExecuteStoredProcedure(query, TaoThamSoCongViec(task, includeId: true));
        }

        public bool CapNhatTrangThai(int maTask, string trangThai)
        {
            string query = "UPDATE CongViec SET TrangThai = @TrangThai WHERE MaTask = @MaTask";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaTask", maTask),
                new SqlParameter("@TrangThai", trangThai)
            };
            return DataConnection.ExecuteStoredProcedure(query, parameters);
        }

        public bool XoaCongViec(int maTask)
        {
            string query = "DELETE FROM CongViec WHERE MaTask = @MaTask";
            SqlParameter[] parameters = { new SqlParameter("@MaTask", maTask) };
            return DataConnection.ExecuteStoredProcedure(query, parameters);
        }

        public DataTable LayBinhLuanTheoCongViec(int maTask)
        {
            string query = @"
SELECT bl.MaBL, bl.MaTask, bl.MaNV, nv.HoTen, bl.NoiDung, bl.ThoiGian, bl.LinkDinhKem
FROM BinhLuanTask bl
INNER JOIN NhanVien nv ON bl.MaNV = nv.MaNV
WHERE bl.MaTask = @MaTask
ORDER BY bl.ThoiGian DESC";
            SqlParameter[] parameters = { new SqlParameter("@MaTask", maTask) };
            return DataConnection.ExecuteQuery(query, parameters);
        }

        public bool ThemBinhLuan(TaskCommentDTO comment)
        {
            string query = @"
INSERT INTO BinhLuanTask (MaTask, MaNV, NoiDung, LinkDinhKem)
VALUES (@MaTask, @MaNV, @NoiDung, @LinkDinhKem)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaTask", comment.MaTask),
                new SqlParameter("@MaNV", comment.MaNV),
                new SqlParameter("@NoiDung", comment.NoiDung),
                new SqlParameter("@LinkDinhKem", string.IsNullOrWhiteSpace(comment.LinkDinhKem) ? (object)DBNull.Value : comment.LinkDinhKem)
            };
            return DataConnection.ExecuteStoredProcedure(query, parameters);
        }

        private SqlParameter[] TaoThamSoCongViec(TaskDTO task, bool includeId)
        {
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaTask", task.MaTask),
                new SqlParameter("@TenTask", task.TenTask),
                new SqlParameter("@MoTa", string.IsNullOrWhiteSpace(task.MoTa) ? (object)DBNull.Value : task.MoTa),
                new SqlParameter("@MucDoUuTien", task.MucDoUuTien),
                new SqlParameter("@TrangThai", task.TrangThai),
                new SqlParameter("@Deadline", task.Deadline),
                new SqlParameter("@NgayTao", task.NgayTao),
                new SqlParameter("@MaDA", task.MaDA),
                new SqlParameter("@MaNguoiThucHien", task.MaNguoiNhan)
            };

            if (includeId)
            {
                return parameters;
            }

            SqlParameter[] parametersWithoutId = new SqlParameter[parameters.Length - 1];
            Array.Copy(parameters, 1, parametersWithoutId, 0, parametersWithoutId.Length);
            return parametersWithoutId;
        }
    }
}
