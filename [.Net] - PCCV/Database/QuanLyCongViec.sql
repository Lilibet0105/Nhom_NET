USE master;
GO

IF DB_ID(N'QuanLyCongViec') IS NOT NULL
BEGIN
    ALTER DATABASE QuanLyCongViec SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyCongViec;
END
GO

CREATE DATABASE QuanLyCongViec;
GO

USE QuanLyCongViec;
GO

-- =========================
-- 1. TẠO BẢNG
-- =========================

CREATE TABLE PhongBan (
    MaPB INT IDENTITY(1,1) PRIMARY KEY,
    TenPB NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(255) NULL
);
GO

CREATE TABLE NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NULL,
    GioiTinh NVARCHAR(10) NULL,
    SoDienThoai VARCHAR(15) NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    LuongBaoHiem DECIMAL(18,2) DEFAULT 0,
    ViTri NVARCHAR(50) NULL,
    MaPB INT NULL,
    CONSTRAINT FK_NhanVien_PhongBan 
        FOREIGN KEY (MaPB) REFERENCES PhongBan(MaPB) ON DELETE SET NULL
);
GO

CREATE TABLE TaiKhoan (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(255) NOT NULL,
    VaiTro NVARCHAR(20) NOT NULL DEFAULT 'Staff',
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Chờ duyệt',
    NgayTao DATETIME DEFAULT GETDATE(),
    MaNV INT UNIQUE NOT NULL,
    CONSTRAINT FK_TaiKhoan_NhanVien 
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV) ON DELETE CASCADE
);
GO

CREATE TABLE DuAn (
    MaDA INT IDENTITY(1,1) PRIMARY KEY,
    TenDA NVARCHAR(150) NOT NULL UNIQUE,
    MoTa NVARCHAR(MAX) NULL,
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Mới khởi tạo',
    CONSTRAINT CK_NgayDuAn CHECK (NgayKetThuc >= NgayBatDau)
);
GO

CREATE TABLE ThanhVienDuAn (
    MaDA INT NOT NULL,
    MaNV INT NOT NULL,
    NgayThamGia DATETIME DEFAULT GETDATE(),
    VaiTroTrongDuAn NVARCHAR(50) NULL,
    PRIMARY KEY (MaDA, MaNV),
    CONSTRAINT FK_TVDA_DuAn 
        FOREIGN KEY (MaDA) REFERENCES DuAn(MaDA) ON DELETE CASCADE,
    CONSTRAINT FK_TVDA_NhanVien 
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV) ON DELETE CASCADE
);
GO

CREATE TABLE CongViec (
    MaTask INT IDENTITY(1,1) PRIMARY KEY,
    TenTask NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(MAX) NULL,
    MucDoUuTien NVARCHAR(20) NOT NULL DEFAULT N'Trung bình',
    TrangThai NVARCHAR(20) NOT NULL DEFAULT 'To Do',
    Deadline DATETIME NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    MaDA INT NOT NULL,
    MaNguoiThucHien INT NOT NULL,
    CONSTRAINT FK_CongViec_DuAn 
        FOREIGN KEY (MaDA) REFERENCES DuAn(MaDA) ON DELETE CASCADE,
    CONSTRAINT FK_CongViec_NhanVien 
        FOREIGN KEY (MaNguoiThucHien) REFERENCES NhanVien(MaNV),
    CONSTRAINT CK_TrangThaiKanban 
        CHECK (TrangThai IN ('To Do', 'In Progress', 'Done'))
);
GO

CREATE TABLE LichLamViec (
    MaLich INT IDENTITY(1,1) PRIMARY KEY,
    TieuDe NVARCHAR(200) NOT NULL,
    DiaDiem NVARCHAR(255) NULL,
    MoTa NVARCHAR(MAX) NULL,
    ThoiGianBatDau DATETIME NOT NULL,
    ThoiGianKetThuc DATETIME NOT NULL,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Chưa Hoàn Thành',
    CONSTRAINT CK_LichLamViec_TrangThai
        CHECK (TrangThai IN (N'Chưa Hoàn Thành', N'Đang Tiến Hành', N'Hoàn Thành', N'Chậm Tiến Độ')),
    CONSTRAINT CK_LichLamViec_ThoiGian
        CHECK (ThoiGianKetThuc >= ThoiGianBatDau)
);
GO

CREATE TABLE BinhLuanTask (
    MaBL INT IDENTITY(1,1) PRIMARY KEY,
    MaTask INT NOT NULL,
    MaNV INT NOT NULL,
    NoiDung NVARCHAR(MAX) NOT NULL,
    ThoiGian DATETIME DEFAULT GETDATE(),
    LinkDinhKem VARCHAR(500) NULL,
    CONSTRAINT FK_BinhLuan_CongViec 
        FOREIGN KEY (MaTask) REFERENCES CongViec(MaTask) ON DELETE CASCADE,
    CONSTRAINT FK_BinhLuan_NhanVien 
        FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO

-- =========================
-- 2. TẠO VIEW
-- =========================

CREATE VIEW v_PerformanceSummary AS
SELECT 
    nv.MaNV,
    nv.HoTen,
    pb.TenPB,
    COUNT(CASE WHEN cv.TrangThai = 'To Do' THEN 1 END) AS SoViec_ToDo,
    COUNT(CASE WHEN cv.TrangThai = 'In Progress' THEN 1 END) AS SoViec_InProgress,
    COUNT(CASE WHEN cv.TrangThai = 'Done' THEN 1 END) AS SoViec_Done,
    COUNT(CASE WHEN cv.TrangThai = 'Done' AND cv.Deadline < GETDATE() THEN 1 END) AS SoViec_TreHan,
    COUNT(cv.MaTask) AS TongSoViecPhuTrach
FROM NhanVien nv
LEFT JOIN PhongBan pb ON nv.MaPB = pb.MaPB
LEFT JOIN CongViec cv ON nv.MaNV = cv.MaNguoiThucHien
GROUP BY nv.MaNV, nv.HoTen, pb.TenPB;
GO

CREATE VIEW v_TaskDetailsAll AS
SELECT 
    cv.MaTask,
    cv.TenTask,
    cv.MoTa,
    cv.MucDoUuTien,
    cv.TrangThai,
    cv.Deadline,
    cv.NgayTao,
    da.MaDA,
    da.TenDA,
    nv.MaNV AS MaNguoiNhan,
    nv.HoTen AS TenNguoiNhan,
    CASE 
        WHEN cv.TrangThai <> 'Done' AND cv.Deadline < GETDATE() THEN N'Trễ hạn'
        WHEN cv.TrangThai = 'Done' THEN N'Hoàn thành'
        ELSE N'Đang chạy đúng hạn'
    END AS TinhTrang
FROM CongViec cv
INNER JOIN DuAn da ON cv.MaDA = da.MaDA
INNER JOIN NhanVien nv ON cv.MaNguoiThucHien = nv.MaNV;
GO

-- =========================
-- 3. TẠO TRIGGER
-- =========================

CREATE TRIGGER trg_ValidateEmail_NhanVien
ON NhanVien
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 
        FROM inserted 
        WHERE Email NOT LIKE '%_@__%.__%'
    )
    BEGIN
        RAISERROR (N'Lỗi hệ thống: Định dạng Email tài khoản không hợp lệ, vui lòng kiểm tra lại!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

CREATE TRIGGER trg_CheckTaskAssignee
ON CongViec
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    IF EXISTS (
        SELECT 1 
        FROM inserted i
        LEFT JOIN ThanhVienDuAn tvda 
            ON i.MaDA = tvda.MaDA 
           AND i.MaNguoiThucHien = tvda.MaNV
        WHERE tvda.MaNV IS NULL
    )
    BEGIN
        RAISERROR (N'Lỗi phân công: Nhân viên này chưa được gán vào dự án nên không thể giao việc!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- =========================
-- 4. TẠO STORED PROCEDURE
-- =========================

CREATE PROCEDURE sp_RegisterUser
    @HoTen NVARCHAR(100),
    @Email VARCHAR(100),
    @TenDangNhap VARCHAR(50),
    @MatKhau VARCHAR(255),
    @ViTri NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    IF @HoTen = '' OR @Email = '' OR @TenDangNhap = '' OR @MatKhau = ''
    BEGIN
        RAISERROR(N'Không được phép bỏ trống các trường thông tin bắt buộc!', 16, 1);
        RETURN;
    END
    
    IF EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap)
    BEGIN
        RAISERROR(N'Tên đăng nhập này đã tồn tại trên hệ thống!', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM NhanVien WHERE Email = @Email)
    BEGIN
        RAISERROR(N'Địa chỉ Email này đã được sử dụng bởi một nhân sự khác!', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @NewMaNV INT;

        INSERT INTO NhanVien (HoTen, Email, ViTri) 
        VALUES (@HoTen, @Email, @ViTri);

        SET @NewMaNV = SCOPE_IDENTITY();

        INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, TrangThai, MaNV)
        VALUES (@TenDangNhap, @MatKhau, 'Staff', N'Chờ duyệt', @NewMaNV);

        COMMIT TRANSACTION;

        PRINT N'Đăng ký tài khoản nhân viên thành công, đang chờ Admin duyệt kích hoạt!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;

        DECLARE @ErrMsg NVARCHAR(4000);
        SET @ErrMsg = ERROR_MESSAGE();

        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END;
GO

CREATE PROCEDURE sp_SearchTeamMembers
    @Keyword NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        nv.MaNV, 
        nv.HoTen, 
        nv.NgaySinh, 
        nv.GioiTinh, 
        nv.SoDienThoai, 
        nv.Email, 
        nv.LuongBaoHiem, 
        nv.ViTri, 
        pb.TenPB, 
        tk.TenDangNhap, 
        tk.VaiTro, 
        tk.TrangThai
    FROM NhanVien nv
    LEFT JOIN PhongBan pb ON nv.MaPB = pb.MaPB
    LEFT JOIN TaiKhoan tk ON nv.MaNV = tk.MaNV
    WHERE nv.HoTen LIKE N'%' + @Keyword + N'%' 
       OR nv.Email LIKE '%' + @Keyword + '%'
       OR nv.ViTri LIKE N'%' + @Keyword + N'%'
       OR pb.TenPB LIKE N'%' + @Keyword + N'%';
END;
GO

CREATE PROCEDURE sp_SearchTasksAdvanced
    @MaDA INT = NULL,
    @SearchText NVARCHAR(200) = NULL,
    @MucDoUuTien NVARCHAR(20) = NULL,
    @MaNV INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SELECT * 
    FROM v_TaskDetailsAll
    WHERE (@MaDA IS NULL OR MaDA = @MaDA)
      AND (
            @SearchText IS NULL 
            OR TenTask LIKE N'%' + @SearchText + N'%' 
            OR MoTa LIKE N'%' + @SearchText + N'%'
          )
      AND (@MucDoUuTien IS NULL OR MucDoUuTien = @MucDoUuTien)
      AND (@MaNV IS NULL OR MaNguoiNhan = @MaNV)
    ORDER BY Deadline ASC;
END;
GO

-- =========================
-- 5. THÊM DỮ LIỆU MẪU
-- =========================

INSERT INTO PhongBan (TenPB, MoTa) VALUES 
(N'Ban Giám Đốc', N'Điều hành toàn bộ công ty'),
(N'Phòng Kỹ Thuật Phần Mềm', N'Đội ngũ lập trình và phát triển hệ thống'),
(N'Phòng Kiểm Thử Chất Lượng', N'Đội ngũ QA/Tester đảm bảo chất lượng ứng dụng');
GO

INSERT INTO NhanVien 
(HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, LuongBaoHiem, ViTri, MaPB) 
VALUES 
(N'Nguyễn Doanh Nghiệp', '1990-01-01', N'Nam', '0912345678', 'doanhnghiep@gmail.com', 20000000, N'Quản trị viên', 1),
(N'Nguyễn Văn Hoan', '2004-02-15', N'Nam', '0981111111', 'hophoan@gmail.com', 10000000, N'Developer', 2),
(N'Trần Thị Mai', '2004-05-20', N'Nữ', '0982222222', 'mailisa@gmail.com', 10000000, N'Developer', 2),
(N'Lê Minh Hà', '2004-08-10', N'Nam', '0983333333', 'hathienlo@gmail.com', 9500000, N'Tester', 3),
(N'Phạm Hồng Vân', '2004-11-25', N'Nữ', '0984444444', 'vanmay@gmail.com', 9500000, N'Tester', 3),
(N'Quang Đỗ', '2004-03-30', N'Nam', '0985555555', 'quangdo@gmail.com', 15000000, N'Project Manager', 2);
GO

INSERT INTO TaiKhoan 
(TenDangNhap, MatKhau, VaiTro, TrangThai, MaNV) 
VALUES 
('admin', 'HASHED_PASSWORD_XYZ_123', 'Admin', N'Hoạt động', 1),
('hoanhao', 'HASHED_PASSWORD_MEMBER1', 'Staff', N'Hoạt động', 2),
('mailisa', 'HASHED_PASSWORD_MEMBER2', 'Staff', N'Hoạt động', 3),
('hahaha', 'HASHED_PASSWORD_MEMBER3', 'Staff', N'Chờ duyệt', 4),
('vanvu', 'HASHED_PASSWORD_MEMBER4', 'Staff', N'Hoạt động', 5),
('quangdo', 'HASHED_QUANGDO_LEADER', 'Manager', N'Hoạt động', 6);
GO

INSERT INTO DuAn 
(TenDA, MoTa, NgayBatDau, NgayKetThuc, TrangThai) 
VALUES 
(N'Xây dựng Ứng dụng Phân Công Công Việc .NET', N'Đồ án kỳ phân công phối hợp quản lý công việc nhóm', '2026-05-01', '2026-06-15', N'Đang chạy'),
(N'Nâng cấp Hệ thống Bảo mật Doanh nghiệp v2', N'Tối ưu phân quyền hệ thống SQL', '2026-07-01', '2026-09-30', N'Mới khởi tạo');
GO

INSERT INTO ThanhVienDuAn 
(MaDA, MaNV, VaiTroTrongDuAn) 
VALUES 
(1, 2, N'Lập trình Kanban & Calendar'),
(1, 3, N'Lập trình Login & Register'),
(1, 4, N'Lập trình User & Team Manager'),
(1, 5, N'Lập trình Task Details & Action'),
(1, 6, N'Quản trị cơ sở dữ liệu & Tổng quan hệ thống');
GO

INSERT INTO CongViec 
(TenTask, MoTa, MucDoUuTien, TrangThai, Deadline, MaDA, MaNguoiThucHien) 
VALUES 
(N'Kéo giao diện đồ họa các Form hệ thống', N'Dựng hoàn thiện UI tĩnh cho 11 form', N'Cao', 'Done', '2026-05-15', 1, 2),
(N'Thiết kế kiến trúc Database trên SQL Server', N'Viết file script thiết kế bảng, khóa chính khóa ngoại', N'Cao', 'Done', '2026-05-20', 1, 6),
(N'Lập trình kết nối dữ liệu frmLogin và SQL', N'Viết tầng kết nối kiểm tra chuỗi bảo mật tài khoản', N'Trung bình', 'In Progress', '2026-05-28', 1, 3),
(N'Xây dựng thuật toán gom dữ liệu vẽ biểu đồ hiệu năng', N'Viết hàm đếm trạng thái công việc tại frmPerformanceReport', N'Thấp', 'To Do', '2026-06-05', 1, 6);
GO

INSERT INTO LichLamViec
(TieuDe, DiaDiem, MoTa, ThoiGianBatDau, ThoiGianKetThuc, TrangThai)
VALUES
(N'Họp khởi động dự án PCCV', N'Phòng họp A', N'Phân chia phạm vi công việc và thống nhất deadline sprint đầu tiên', '2026-05-01 08:30:00', '2026-05-01 10:00:00', N'Hoàn Thành'),
(N'Review giao diện Kanban và Dashboard', N'Phòng kỹ thuật', N'Kiểm tra luồng màn hình chính, board công việc và form tạo mới', '2026-05-24 14:00:00', '2026-05-24 15:30:00', N'Hoàn Thành'),
(N'Kiểm thử kết nối SQL Server', N'Online', N'Xác nhận connection string, dữ liệu mẫu và các màn hình dùng database', '2026-05-28 09:00:00', '2026-05-28 11:00:00', N'Đang Tiến Hành'),
(N'Tổng hợp báo cáo tiến độ', N'Phòng dự án', N'Cập nhật tình trạng task và chuẩn bị báo cáo hiệu suất', '2026-06-03 13:30:00', '2026-06-03 15:00:00', N'Chưa Hoàn Thành');
GO

INSERT INTO BinhLuanTask 
(MaTask, MaNV, NoiDung, LinkDinhKem) 
VALUES 
(1, 2, N'Đã hoàn thành kéo thả UI, đẩy mã nguồn lên nhánh quangdo thành công!', 'https://github.com/quangdo/PCCV/pull/1'),
(3, 3, N'Đang gặp chút vướng mắc về mã hóa chuỗi mật khẩu khi đối chiếu SQL, đang fix.', NULL);
GO

-- =========================
-- 6. TEST NHANH
-- =========================

SELECT * FROM PhongBan;
SELECT * FROM NhanVien;
SELECT * FROM TaiKhoan;
SELECT * FROM DuAn;
SELECT * FROM ThanhVienDuAn;
SELECT * FROM CongViec;
SELECT * FROM BinhLuanTask;

SELECT * FROM v_PerformanceSummary;
SELECT * FROM v_TaskDetailsAll;
GO
