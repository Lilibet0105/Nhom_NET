CREATE DATABASE QuanLyCongViec;
GO
USE QuanLyCongViec;
GO

CREATE TABLE PhongBan (
    MaPB INT IDENTITY(1,1) PRIMARY KEY,
    TenPB NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(255) NULL
);

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
    CONSTRAINT FK_NhanVien_PhongBan FOREIGN KEY (MaPB) REFERENCES PhongBan(MaPB) ON DELETE SET NULL
);

CREATE TABLE TaiKhoan (
    TenDangNhap VARCHAR(50) PRIMARY KEY,
    MatKhau VARCHAR(255) NOT NULL, -- Lưu chuỗi hash mật khẩu mã hóa phục vụ frmLogin
    VaiTro NVARCHAR(20) NOT NULL DEFAULT 'Staff', -- 'Admin', 'Manager', 'Staff' (Phục vụ phân quyền frmMainDashboard)
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Chờ duyệt', -- N'Chờ duyệt', N'Hoạt động', N'Bị khóa' (Phục vụ frmUserManagement)
    NgayTao DATETIME DEFAULT GETDATE(),
    MaNV INT UNIQUE NOT NULL,
    CONSTRAINT FK_TaiKhoan_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV) ON DELETE CASCADE
);

CREATE TABLE DuAn (
    MaDA INT IDENTITY(1,1) PRIMARY KEY,
    TenDA NVARCHAR(150) NOT NULL UNIQUE,
    MoTa NVARCHAR(MAX) NULL,
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Mới khởi tạo', -- N'Mới khởi tạo', N'Đang chạy', N'Hoàn thành', N'Tạm dừng'
    CONSTRAINT CK_NgayDuAn CHECK (NgayKetThuc >= NgayBatDau)
);
CREATE TABLE ThanhVienDuAn (
    MaDA INT,
    MaNV INT,
    NgayThamGia DATETIME DEFAULT GETDATE(),
    VaiTroTrongDuAn NVARCHAR(50) NULL, -- Lead, Member...
    PRIMARY KEY (MaDA, MaNV),
    CONSTRAINT FK_TVDA_DuAn FOREIGN KEY (MaDA) REFERENCES DuAn(MaDA) ON DELETE CASCADE,
    CONSTRAINT FK_TVDA_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV) ON DELETE CASCADE
);
CREATE TABLE CongViec (
    MaTask INT IDENTITY(1,1) PRIMARY KEY,
    TenTask NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(MAX) NULL,
    MucDoUuTien NVARCHAR(20) NOT NULL DEFAULT N'Trung bình', -- N'Cao', N'Trung bình', N'Thấp' (Phục vụ lọc nâng cao)
    TrangThai NVARCHAR(20) NOT NULL DEFAULT 'To Do', -- 'To Do', 'In Progress', 'Done' (Phục vụ 3 cột Kanban)
    Deadline DATETIME NOT NULL,
    NgayTao DATETIME DEFAULT GETDATE(),
    MaDA INT NOT NULL,
    MaNguoiThucHien INT NOT NULL,
    CONSTRAINT FK_CongViec_DuAn FOREIGN KEY (MaDA) REFERENCES DuAn(MaDA) ON DELETE CASCADE,
    CONSTRAINT FK_CongViec_NhanVien FOREIGN KEY (MaNguoiThucHien) REFERENCES NhanVien(MaNV),
    CONSTRAINT CK_TrangThaiKanban CHECK (TrangThai IN ('To Do', 'In Progress', 'Done'))
);
CREATE TABLE LichLamViec (
    MaLich INT IDENTITY(1,1) PRIMARY KEY,
    TieuDe NVARCHAR(200) NOT NULL,
    DiaDiem NVARCHAR(255) NULL,
    MoTa NVARCHAR(MAX) NULL,
    ThoiGianBatDau DATETIME NOT NULL,
    ThoiGianKetThuc DATETIME NOT NULL,
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Chưa Hoàn Thành', -- N'Chưa Hoàn Thành', N'Đang Tiến Hành', N'Hoàn Thành', N'Chậm Tiến Độ'
    CONSTRAINT CK_LichLamViec_TrangThai CHECK (TrangThai IN (N'Chưa Hoàn Thành', N'Đang Tiến Hành', N'Hoàn Thành', N'Chậm Tiến Độ')),
    CONSTRAINT CK_LichLamViec_ThoiGian CHECK (ThoiGianKetThuc >= ThoiGianBatDau)
);
CREATE TABLE BinhLuanTask (
    MaBL INT IDENTITY(1,1) PRIMARY KEY,
    MaTask INT NOT NULL,
    MaNV INT NOT NULL,
    NoiDung NVARCHAR(MAX) NOT NULL,
    ThoiGian DATETIME DEFAULT GETDATE(),
    LinkDinhKem VARCHAR(500) NULL, -- Lưu trữ link báo cáo kết quả hoặc file tiến độ
    CONSTRAINT FK_BinhLuan_CongViec FOREIGN KEY (MaTask) REFERENCES CongViec(MaTask) ON DELETE CASCADE,
    CONSTRAINT FK_BinhLuan_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
GO;

-- View 1: Thống kê hiệu suất công việc chi tiết của từng nhân sự (Phục vụ biểu đồ frmPerformanceReport) 
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
GROUP BY nv.MaNV, nv.HoTen, pb.TenPB
GO

-- View 2: Tổng quan danh sách công việc đầy đủ thông tin liên kết (Phục vụ nạp dữ liệu frmKanbanBoard / frmCalendarView)
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
INNER JOIN NhanVien nv ON cv.MaNguoiThucHien = nv.MaNV
GO

-- Trigger 1: Bắt lỗi định dạng Email khi Thêm/Sửa nhân sự (Validation phục vụ frmRegister và frmTeamManager)
CREATE TRIGGER trg_ValidateEmail_NhanVien
ON NhanVien
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (
        SELECT 1 FROM inserted 
        WHERE Email NOT LIKE '%_@__%.__%' -- Kiểm tra định dạng cấu trúc Email căn bản
    )
    BEGIN
        RAISERROR (N'Lỗi hệ thống: Định dạng Email tài khoản không hợp lệ, vui lòng kiểm tra lại!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- Trigger 2: Ràng buộc giao việc - Chỉ được giao việc cho nhân sự thuộc danh sách thành viên của dự án đó (Phục vụ frmTaskAction)
CREATE TRIGGER trg_CheckTaskAssignee
ON CongViec
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS (
        SELECT 1 FROM inserted i
        LEFT JOIN ThanhVienDuAn tvda ON i.MaDA = tvda.MaDA AND i.MaNguoiThucHien = tvda.MaNV
        WHERE tvda.MaNV IS NULL
    )
    BEGIN
        RAISERROR (N'Lỗi phân công: Nhân viên này chưa được gán vào dự án nên không thể giao việc!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- SP 1: Xử lý chức năng đăng ký tài khoản mới (Phục vụ frmRegister - Validation trống thông tin)
CREATE PROCEDURE sp_RegisterUser
    @HoTen NVARCHAR(100),
    @Email VARCHAR(100),
    @TenDangNhap VARCHAR(50),
    @MatKhau VARCHAR(255),
    @ViTri NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Kiểm tra lỗi trống thông tin bắt buộc
    IF @HoTen = '' OR @Email = '' OR @TenDangNhap = '' OR @MatKhau = ''
    BEGIN
        RAISERROR(N'Không được phép bỏ trống các trường thông tin bắt buộc!', 16, 1);
        RETURN;
    END
    
    -- Kiểm tra trùng lặp tài khoản
    IF EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap)
    BEGIN
        RAISERROR(N'Tên đăng nhập này đã tồn tại trên hệ thống!', 16, 1);
        RETURN;
    END

    -- Kiểm tra trùng lặp Email
    IF EXISTS (SELECT 1 FROM NhanVien WHERE Email = @Email)
    BEGIN
        RAISERROR(N'Địa chỉ Email này đã được sử dụng bởi một nhân sự khác!', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;
    BEGIN TRY
        -- 1. Thêm vào bảng nhân viên trước
        DECLARE @NewMaNV INT;
        INSERT INTO NhanVien (HoTen, Email, ViTri) VALUES (@HoTen, @Email, @ViTri);
        SET @NewMaNV = SCOPE_IDENTITY();

        -- 2. Thêm vào bảng tài khoản ở trạng thái 'Chờ duyệt'
        INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, TrangThai, MaNV)
        VALUES (@TenDangNhap, @MatKhau, 'Staff', N'Chờ duyệt', @NewMaNV);

        COMMIT TRANSACTION;
        PRINT N'Đăng ký tài khoản nhân viên thành công, đang chờ Admin duyệt kích hoạt!';
    END TRY
    BEGIN CATCH
        ROLLBACK TRANSACTION;
        DECLARE @ErrMsg NVARCHAR(4000) = ERROR_MESSAGE();
        RAISERROR(@ErrMsg, 16, 1);
    END CATCH
END;
GO

-- SP 2: Tìm kiếm thành viên nhóm đa năng (Phục vụ chức năng tìm kiếm tại frmTeamManager)
CREATE PROCEDURE sp_SearchTeamMembers
    @Keyword NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        nv.MaNV, nv.HoTen, nv.NgaySinh, nv.GioiTinh, nv.SoDienThoai, nv.Email, nv.LuongBaoHiem, nv.ViTri, pb.TenPB, tk.TenDangNhap, tk.VaiTro, tk.TrangThai
    FROM NhanVien nv
    LEFT JOIN PhongBan pb ON nv.MaPB = pb.MaPB
    LEFT JOIN TaiKhoan tk ON nv.MaNV = tk.MaNV
    WHERE nv.HoTen LIKE '%' + @Keyword + '%' 
       OR nv.Email LIKE '%' + @Keyword + '%' 
       OR nv.ViTri LIKE '%' + @Keyword + '%'
       OR pb.TenPB LIKE '%' + @Keyword + '%';
END;
GO

-- SP 3: Tìm kiếm lọc công việc nâng cao (Phục vụ tính năng tìm kiếm bộ lọc tại frmKanbanBoard / frmCalendarView)
CREATE PROCEDURE sp_SearchTasksAdvanced
    @MaDA INT = NULL,
    @SearchText NVARCHAR(200) = NULL,
    @MucDoUuTien NVARCHAR(20) = NULL,
    @MaNV INT = NULL
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM v_TaskDetailsAll
    WHERE (@MaDA IS NULL OR MaDA = @MaDA)
      AND (@SearchText IS NULL OR TenTask LIKE '%' + @SearchText + '%' OR MoTa LIKE '%' + @SearchText + '%')
      AND (@MucDoUuTien IS NULL OR MucDoUuTien = @MucDoUuTien)
      AND (@MaNV IS NULL OR MaNguoiNhan = @MaNV)
    ORDER BY Deadline ASC;
END;

INSERT INTO PhongBan (TenPB, MoTa) VALUES 
(N'Ban Giám Đốc', N'Điều hành toàn bộ công ty'),
(N'Phòng Kỹ Thuật Phần Mềm', N'Đội ngũ lập trình và phát triển hệ thống'),
(N'Phòng Kiểm Thử Chất Lượng', N'Đội ngũ QA/Tester đảm bảo chất lượng ứng dụng');

INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, LuongBaoHiem, ViTri, MaPB) VALUES 
(N'Nguyễn Doanh Nghiệp', '1990-01-01', N'Nam', '0912345678', 'doanhnghiep@gmail.com', 20000000, N'Quản trị viên', 1),
(N'Nguyễn Văn Hoan', '2004-02-15', N'Nam', '0981111111', 'hophoan@gmail.com', 10000000, N'Developer', 2),
(N'Trần Thị Mai', '2004-05-20', N'Nữ', '0982222222', 'mailisa@gmail.com', 10000000, N'Developer', 2),
(N'Lê Minh Hà', '2004-08-10', N'Nam', '0983333333', 'hathienlo@gmail.com', 9500000, N'Tester', 3),
(N'Phạm Hồng Vân', '2004-11-25', N'Nữ', '0984444444', 'vanmay@gmail.com', 9500000, N'Tester', 3),
(N'Quang Đỗ', '2004-03-30', N'Nam', '0985555555', 'quangdo@gmail.com', 15000000, N'Project Manager', 2);

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, TrangThai, MaNV) VALUES 
('admin', 'HASHED_PASSWORD_XYZ_123', 'Admin', N'Hoạt động', 1),
('hoanhao', 'HASHED_PASSWORD_MEMBER1', 'Staff', N'Hoạt động', 2),
('mailisa', 'HASHED_PASSWORD_MEMBER2', 'Staff', N'Hoạt động', 3),
('hahaha', 'HASHED_PASSWORD_MEMBER3', 'Staff', N'Chờ duyệt', 4), -- Tài khoản chờ duyệt để demo frmUserManagement
('vanvu', 'HASHED_PASSWORD_MEMBER4', 'Staff', N'Hoạt động', 5),
('quangdo', 'HASHED_QUANGDO_LEADER', 'Manager', N'Hoạt động', 6);

INSERT INTO DuAn (TenDA, MoTa, NgayBatDau, NgayKetThuc, TrangThai) VALUES 
(N'Xây dựng Ứng dụng Phân Công Công Việc .NET', N'Đồ án kỳ phân công phối hợp quản lý công việc nhóm', '2026-05-01', '2026-06-15', N'Đang chạy'),
(N'Nâng cấp Hệ thống Bảo mật Doanh nghiệp v2', N'Tối ưu phân quyền hệ thống SQL', '2026-07-01', '2026-09-30', N'Mới khởi tạo');

INSERT INTO ThanhVienDuAn (MaDA, MaNV, VaiTroTrongDuAn) VALUES 
(1, 2, N'Lập trình Kanban & Calendar'),
(1, 3, N'Lập trình Login & Register'),
(1, 4, N'Lập trình User & Team Manager'),
(1, 5, N'Lập trình Task Details & Action'),
(1, 6, N'Quản trị cơ sở dữ liệu & Tổng quan hệ thống');

INSERT INTO CongViec (TenTask, MoTa, MucDoUuTien, TrangThai, Deadline, MaDA, MaNguoiThucHien) VALUES 
(N'Kéo giao diện đồ họa các Form hệ thống', N'Dựng hoàn thiện UI tĩnh cho 11 form', N'Cao', 'Done', '2026-05-15', 1, 2),
(N'Thiết kế kiến trúc Database trên SQL Server', N'Viết file script thiết kế bảng, khóa chính khóa ngoại', N'Cao', 'Done', '2026-05-20', 1, 6),
(N'Lập trình kết nối dữ liệu frmLogin và SQL', N'Viết tầng kết nối kiểm tra chuỗi bảo mật tài khoản', N'Trung bình', 'In Progress', '2026-05-28', 1, 3),
(N'Xây dựng thuật toán gom dữ liệu vẽ biểu đồ hiệu năng', N'Viết hàm đếm trạng thái công việc tại frmPerformanceReport', N'Thấp', 'To Do', '2026-06-05', 1, 6);

INSERT INTO BinhLuanTask (MaTask, MaNV, NoiDung, LinkDinhKem) VALUES 
(1, 2, N'Đã hoàn thành kéo thả UI, đẩy mã nguồn lên nhánh quangdo thành công!', 'https://github.com/quangdo/PCCV/pull/1'),
(3, 3, N'Đang gặp chút vướng mắc về mã hóa chuỗi mật khẩu khi đối chiếu SQL, đang fix.', NULL);

