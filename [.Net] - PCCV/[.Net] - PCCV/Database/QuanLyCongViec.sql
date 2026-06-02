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
-- 1. Táº O Báº¢NG
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
    TrangThai NVARCHAR(20) NOT NULL DEFAULT N'Chá» duyá»‡t',
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
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'Má»›i khá»Ÿi táº¡o',
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
    MucDoUuTien NVARCHAR(20) NOT NULL DEFAULT N'Trung bĂ¬nh',
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
    TrangThai NVARCHAR(30) NOT NULL DEFAULT N'ChÆ°a HoĂ n ThĂ nh',
    CONSTRAINT CK_LichLamViec_TrangThai
        CHECK (TrangThai IN (N'ChÆ°a HoĂ n ThĂ nh', N'Äang Tiáº¿n HĂ nh', N'HoĂ n ThĂ nh', N'Cháº­m Tiáº¿n Äá»™')),
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
-- 2. Táº O VIEW
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
        WHEN cv.TrangThai <> 'Done' AND cv.Deadline < GETDATE() THEN N'Trá»… háº¡n'
        WHEN cv.TrangThai = 'Done' THEN N'HoĂ n thĂ nh'
        ELSE N'Äang cháº¡y Ä‘Ăºng háº¡n'
    END AS TinhTrang
FROM CongViec cv
INNER JOIN DuAn da ON cv.MaDA = da.MaDA
INNER JOIN NhanVien nv ON cv.MaNguoiThucHien = nv.MaNV;
GO

-- =========================
-- 3. Táº O TRIGGER
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
        RAISERROR (N'Lá»—i há»‡ thá»‘ng: Äá»‹nh dáº¡ng Email tĂ i khoáº£n khĂ´ng há»£p lá»‡, vui lĂ²ng kiá»ƒm tra láº¡i!', 16, 1);
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
        RAISERROR (N'Lá»—i phĂ¢n cĂ´ng: NhĂ¢n viĂªn nĂ y chÆ°a Ä‘Æ°á»£c gĂ¡n vĂ o dá»± Ă¡n nĂªn khĂ´ng thá»ƒ giao viá»‡c!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
GO

-- =========================
-- 4. Táº O STORED PROCEDURE
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
        RAISERROR(N'KhĂ´ng Ä‘Æ°á»£c phĂ©p bá» trá»‘ng cĂ¡c trÆ°á»ng thĂ´ng tin báº¯t buá»™c!', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap)
    BEGIN
        RAISERROR(N'TĂªn Ä‘Äƒng nháº­p nĂ y Ä‘Ă£ tá»“n táº¡i trĂªn há»‡ thá»‘ng!', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM NhanVien WHERE Email = @Email)
    BEGIN
        RAISERROR(N'Äá»‹a chá»‰ Email nĂ y Ä‘Ă£ Ä‘Æ°á»£c sá»­ dá»¥ng bá»Ÿi má»™t nhĂ¢n sá»± khĂ¡c!', 16, 1);
        RETURN;
    END

    BEGIN TRANSACTION;

    BEGIN TRY
        DECLARE @NewMaNV INT;

        INSERT INTO NhanVien (HoTen, Email, ViTri)
        VALUES (@HoTen, @Email, @ViTri);

        SET @NewMaNV = SCOPE_IDENTITY();

        INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, TrangThai, MaNV)
        VALUES (@TenDangNhap, @MatKhau, 'Staff', N'Chá» duyá»‡t', @NewMaNV);

        COMMIT TRANSACTION;

        PRINT N'ÄÄƒng kĂ½ tĂ i khoáº£n nhĂ¢n viĂªn thĂ nh cĂ´ng, Ä‘ang chá» Admin duyá»‡t kĂ­ch hoáº¡t!';
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
-- 5. THĂM Dá»® LIá»†U MáºªU
-- =========================

INSERT INTO PhongBan (TenPB, MoTa) VALUES
(N'Ban GiĂ¡m Äá»‘c', N'Äiá»u hĂ nh toĂ n bá»™ cĂ´ng ty'),
(N'PhĂ²ng Ká»¹ Thuáº­t Pháº§n Má»m', N'Äá»™i ngÅ© láº­p trĂ¬nh vĂ  phĂ¡t triá»ƒn há»‡ thá»‘ng'),
(N'PhĂ²ng Kiá»ƒm Thá»­ Cháº¥t LÆ°á»£ng', N'Äá»™i ngÅ© QA/Tester Ä‘áº£m báº£o cháº¥t lÆ°á»£ng á»©ng dá»¥ng');
GO

INSERT INTO NhanVien
(HoTen, NgaySinh, GioiTinh, SoDienThoai, Email, LuongBaoHiem, ViTri, MaPB)
VALUES
(N'Nguyá»…n Doanh Nghiá»‡p', '1990-01-01', N'Nam', '0912345678', 'doanhnghiep@gmail.com', 20000000, N'Quáº£n trá»‹ viĂªn', 1),
(N'Nguyá»…n VÄƒn Hoan', '2004-02-15', N'Nam', '0981111111', 'hophoan@gmail.com', 10000000, N'Developer', 2),
(N'Tráº§n Thá»‹ Mai', '2004-05-20', N'Ná»¯', '0982222222', 'mailisa@gmail.com', 10000000, N'Developer', 2),
(N'LĂª Minh HĂ ', '2004-08-10', N'Nam', '0983333333', 'hathienlo@gmail.com', 9500000, N'Tester', 3),
(N'Pháº¡m Há»“ng VĂ¢n', '2004-11-25', N'Ná»¯', '0984444444', 'vanmay@gmail.com', 9500000, N'Tester', 3),
(N'Quang Äá»—', '2004-03-30', N'Nam', '0985555555', 'quangdo@gmail.com', 15000000, N'Project Manager', 2);
GO

INSERT INTO TaiKhoan (TenDangNhap, MatKhau, VaiTro, TrangThai, MaNV) VALUES
('admin', '8d969eef6ecad3c29a3a629280e686cf0c3f5d5a86aff3ca12020c923adc6c92', 'Admin', N'Hoạt động', 1),
('hoanhao', 'HASHED_PASSWORD_MEMBER1', 'Staff', N'Hoáº¡t Ä‘á»™ng', 2),
('mailisa', 'HASHED_PASSWORD_MEMBER2', 'Staff', N'Hoáº¡t Ä‘á»™ng', 3),
('hahaha', 'HASHED_PASSWORD_MEMBER3', 'Staff', N'Chá» duyá»‡t', 4),
('vanvu', 'HASHED_PASSWORD_MEMBER4', 'Staff', N'Hoáº¡t Ä‘á»™ng', 5),
('quangdo', 'HASHED_QUANGDO_LEADER', 'Manager', N'Hoáº¡t Ä‘á»™ng', 6);
GO

INSERT INTO DuAn
(TenDA, MoTa, NgayBatDau, NgayKetThuc, TrangThai)
VALUES
(N'XĂ¢y dá»±ng á»¨ng dá»¥ng PhĂ¢n CĂ´ng CĂ´ng Viá»‡c .NET', N'Äá»“ Ă¡n ká»³ phĂ¢n cĂ´ng phá»‘i há»£p quáº£n lĂ½ cĂ´ng viá»‡c nhĂ³m', '2026-05-01', '2026-06-15', N'Äang cháº¡y'),
(N'NĂ¢ng cáº¥p Há»‡ thá»‘ng Báº£o máº­t Doanh nghiá»‡p v2', N'Tá»‘i Æ°u phĂ¢n quyá»n há»‡ thá»‘ng SQL', '2026-07-01', '2026-09-30', N'Má»›i khá»Ÿi táº¡o');
GO

INSERT INTO ThanhVienDuAn
(MaDA, MaNV, VaiTroTrongDuAn)
VALUES
(1, 2, N'Láº­p trĂ¬nh Kanban & Calendar'),
(1, 3, N'Láº­p trĂ¬nh Login & Register'),
(1, 4, N'Láº­p trĂ¬nh User & Team Manager'),
(1, 5, N'Láº­p trĂ¬nh Task Details & Action'),
(1, 6, N'Quáº£n trá»‹ cÆ¡ sá»Ÿ dá»¯ liá»‡u & Tá»•ng quan há»‡ thá»‘ng');
GO

INSERT INTO CongViec
(TenTask, MoTa, MucDoUuTien, TrangThai, Deadline, MaDA, MaNguoiThucHien)
VALUES
(N'KĂ©o giao diá»‡n Ä‘á»“ há»a cĂ¡c Form há»‡ thá»‘ng', N'Dá»±ng hoĂ n thiá»‡n UI tÄ©nh cho 11 form', N'Cao', 'Done', '2026-05-15', 1, 2),
(N'Thiáº¿t káº¿ kiáº¿n trĂºc Database trĂªn SQL Server', N'Viáº¿t file script thiáº¿t káº¿ báº£ng, khĂ³a chĂ­nh khĂ³a ngoáº¡i', N'Cao', 'Done', '2026-05-20', 1, 6),
(N'Láº­p trĂ¬nh káº¿t ná»‘i dá»¯ liá»‡u frmLogin vĂ  SQL', N'Viáº¿t táº§ng káº¿t ná»‘i kiá»ƒm tra chuá»—i báº£o máº­t tĂ i khoáº£n', N'Trung bĂ¬nh', 'In Progress', '2026-05-28', 1, 3),
(N'XĂ¢y dá»±ng thuáº­t toĂ¡n gom dá»¯ liá»‡u váº½ biá»ƒu Ä‘á»“ hiá»‡u nÄƒng', N'Viáº¿t hĂ m Ä‘áº¿m tráº¡ng thĂ¡i cĂ´ng viá»‡c táº¡i frmPerformanceReport', N'Tháº¥p', 'To Do', '2026-06-05', 1, 6);
GO

INSERT INTO LichLamViec
(TieuDe, DiaDiem, MoTa, ThoiGianBatDau, ThoiGianKetThuc, TrangThai)
VALUES
(N'Há»p khá»Ÿi Ä‘á»™ng dá»± Ă¡n PCCV', N'PhĂ²ng há»p A', N'PhĂ¢n chia pháº¡m vi cĂ´ng viá»‡c vĂ  thá»‘ng nháº¥t deadline sprint Ä‘áº§u tiĂªn', '2026-05-01 08:30:00', '2026-05-01 10:00:00', N'HoĂ n ThĂ nh'),
(N'Review giao diá»‡n Kanban vĂ  Dashboard', N'PhĂ²ng ká»¹ thuáº­t', N'Kiá»ƒm tra luá»“ng mĂ n hĂ¬nh chĂ­nh, board cĂ´ng viá»‡c vĂ  form táº¡o má»›i', '2026-05-24 14:00:00', '2026-05-24 15:30:00', N'HoĂ n ThĂ nh'),
(N'Kiá»ƒm thá»­ káº¿t ná»‘i SQL Server', N'Online', N'XĂ¡c nháº­n connection string, dá»¯ liá»‡u máº«u vĂ  cĂ¡c mĂ n hĂ¬nh dĂ¹ng database', '2026-05-28 09:00:00', '2026-05-28 11:00:00', N'Äang Tiáº¿n HĂ nh'),
(N'Tá»•ng há»£p bĂ¡o cĂ¡o tiáº¿n Ä‘á»™', N'PhĂ²ng dá»± Ă¡n', N'Cáº­p nháº­t tĂ¬nh tráº¡ng task vĂ  chuáº©n bá»‹ bĂ¡o cĂ¡o hiá»‡u suáº¥t', '2026-06-03 13:30:00', '2026-06-03 15:00:00', N'ChÆ°a HoĂ n ThĂ nh');
GO

INSERT INTO BinhLuanTask
(MaTask, MaNV, NoiDung, LinkDinhKem)
VALUES
(1, 2, N'ÄĂ£ hoĂ n thĂ nh kĂ©o tháº£ UI, Ä‘áº©y mĂ£ nguá»“n lĂªn nhĂ¡nh quangdo thĂ nh cĂ´ng!', 'https://github.com/quangdo/PCCV/pull/1'),
(3, 3, N'Äang gáº·p chĂºt vÆ°á»›ng máº¯c vá» mĂ£ hĂ³a chuá»—i máº­t kháº©u khi Ä‘á»‘i chiáº¿u SQL, Ä‘ang fix.', NULL);
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
