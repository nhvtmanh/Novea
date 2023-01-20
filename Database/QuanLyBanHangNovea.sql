﻿set dateformat dmy

CREATE DATABASE QuanLyBanHangNovea

USE QuanLyBanHangNovea
---------------------
CREATE TABLE KHACH
(
	MAKH varchar(128) primary key,
	TAIKHOAN varchar(max),
	MATKHAU varchar(max),
	HOTEN nvarchar(max),
	NGSINH datetime,
	GIOITINH nvarchar(3),
	SDT varchar(15),
	EMAIL varchar(max),
	DIACHI nvarchar(max),
	NGDK datetime,
	DOANHSO money,
	AVATAR varchar(max),
	IMGdb image
)

CREATE TABLE CUAHANG
(
	MACH varchar(128) primary key,
	TAIKHOAN varchar(max),
	MATKHAU varchar(max),
	TENCH nvarchar(max),
	DIADIEM nvarchar(max),
	SDT varchar(15),
	EMAIL varchar(max),
	NGDK datetime,	
	DOANHTHU money,
	AVATAR varchar(max),	
	IMGdb image
)

CREATE TABLE SANPHAM
(
	MASP varchar(128) primary key,
	TENSP nvarchar(max),
	LOAISP nvarchar(max),
	DONVI nvarchar(max),
	DONGIA money,
	SIZE varchar(10),
	MOTA nvarchar(max),
	AVAILABLE bit,
	HINHSP varchar(max),
	IMGdb image,
	MACH varchar(128) FOREIGN KEY REFERENCES CUAHANG(MACH)
)

CREATE TABLE HOADON
(
	SOHD varchar(128) primary key,
	NGMH datetime,
	TONGTIEN money,
	MAKH varchar(128) FOREIGN KEY REFERENCES KHACH(MAKH),
	MACH varchar(128) FOREIGN KEY REFERENCES CUAHANG(MACH),
)

CREATE TABLE CTHD
(
	SOHD varchar(128) FOREIGN KEY REFERENCES HOADON(SOHD),
	MASP varchar(128) FOREIGN KEY REFERENCES SANPHAM(MASP),
		CONSTRAINT PK_CTHD PRIMARY KEY (SOHD,MASP),
	SOLUONG int,
	TRIGIA money,
	LuongDa varchar(10),
	LuongDuong varchar(10)
)



------ ------
INSERT INTO KHACH(MAKH,HOTEN,NGSINH,NGDK) VALUES ('KH01',N'Nguyễn Tuân','20/10/1980','13/12/2022')
INSERT INTO KHACH(MAKH,HOTEN,NGSINH,NGDK) VALUES ('KH02',N'Nguyễn Minh Đức','20/10/2002','13/12/2022')
UPDATE KHACH SET NGDK = '15/12/2022' WHERE MAKH = 'KH01'

INSERT INTO CUAHANG(MACH, TENCH, DIADIEM) VALUES ('CH01', N'Hai Lần Coffee', N'số 5 Võ Văn Tần, Thủ Đức')
INSERT INTO CUAHANG(MACH, TENCH, DIADIEM) VALUES ('CH02', N'Mini NonStop', N'số 7 Hàn Thuyên, Thủ Đức')
INSERT INTO CUAHANG(MACH, TENCH, DIADIEM) VALUES ('CH03', N'Độc lạ Bình Dương', N'số 10 Nguyễn An Ninh, Dĩ An')
------ ------
------ ------
INSERT INTO SANPHAM(MASP, TENSP, DONGIA, MACH) VALUES ('SP001', N'Cà phê sữa', 25000, 'CH01')
INSERT INTO SANPHAM(MASP, TENSP, DONGIA, MACH) VALUES ('SP002', N'Trà lài chanh sả', 33000, 'CH01')
INSERT INTO SANPHAM(MASP, TENSP, DONGIA, MACH) VALUES ('SP003', N'Trà sữa việt quất', 40000, 'CH02')
INSERT INTO SANPHAM(MASP, TENSP, DONGIA, MACH) VALUES ('SP004', N'Hồng trà phúc bồn tử', 30000, 'CH03')

INSERT INTO SANPHAM(MASP, TENSP, DONGIA, MACH) VALUES ('SP005', N'Socola đá xay', 29000, 'CH02')
INSERT INTO SANPHAM(MASP, TENSP, DONGIA, MACH) VALUES ('SP006', N'Trà sữa thái xanh', 25000, 'CH03')
INSERT INTO SANPHAM(MASP, TENSP, DONGIA, MACH) VALUES ('SP007', N'Sữa chua chanh dây', 12000, 'CH02')
INSERT INTO SANPHAM(MASP, TENSP, DONGIA, MACH) VALUES ('SP008', N'Oreo đá xay', 30000, 'CH03')
------ ------
------ ------

INSERT INTO CTHD(SOHD, MASP, SOLUONG, TRIGIA) VALUES ('HD10', 'SP001', 5, 200000)
INSERT INTO CTHD(SOHD, MASP, SOLUONG, TRIGIA) VALUES ('HD10', 'SP002', 3, 100000)
------ ------
INSERT INTO HOADON(SOHD, NGMH) VALUES ('HD10', '2022/12/24')
INSERT INTO HOADON(SOHD, NGMH) VALUES ('HD11', '2022/12/14')
------ ------
------ ------




/*--I.Rang buoc toan ven cho KHACH--*/
----1.Ngay sinh phai nho hon ngay dang ky thanh vien
--ALTER TABLE KHACH
--ADD CONSTRAINT CK_KHACH_NGDK CHECK (NGSINH < NGDK)
--2.Doanh so ban ra cho 1 khach hang bang tong tat ca tong tien trong hoa don ma khach hang do mua
	--a.Trigger cho bang KHACH--
		---INSERT---
CREATE TRIGGER TRG_KHACH_DOANHSO_INSERT
ON KHACH
AFTER INSERT
AS
BEGIN
	DECLARE @MAKH varchar(128)

	SELECT @MAKH = MAKH
	FROM  inserted

	UPDATE KHACH
	SET  DOANHSO = 0
	WHERE MAKH = @MAKH

	PRINT N'Đã thêm 1 khách hàng với doanh số ban đầu là 0 VNĐ'
END
		---UPDATE---
CREATE TRIGGER TRG_KHACH_DOANHSO_UPDATE
ON KHACH
AFTER UPDATE
AS IF(UPDATE(DOANHSO))
BEGIN
	DECLARE @MAKH varchar(128)
	DECLARE @DOANHSO money
	DECLARE @TONGDS money
	
	SET @TONGDS = 0
	
	SELECT @MAKH = MAKH, @DOANHSO = DOANHSO 
	FROM inserted

	SELECT @TONGDS = SUM(TONGTIEN) 
	FROM HOADON
	WHERE MAKH = @MAKH
	GROUP BY MAKH

	IF(@DOANHSO <> @TONGDS)
	BEGIN
		PRINT N'Lỗi! Tổng doanh số của một khách hàng phải bằng tổng của tổng tiền các hóa đơn mà khách hàng đó mua '
		ROLLBACK TRANSACTION
	END
	ELSE
		PRINT N'Doanh số không bị thay đổi'
END
	--b.Trigger cho bang HOADON--
		---INSERT, DELETE---
CREATE TRIGGER TRG_HOADON_DOANHSO_INSERT_DELETE
ON HOADON
AFTER INSERT, DELETE
AS 
BEGIN
	DECLARE @MAKH varchar(128)
	DECLARE @TONGDSTHEM money
	DECLARE @TONGDSTRU money

	SET @TONGDSTHEM = 0
	SET @TONGDSTRU = 0

	SELECT @TONGDSTHEM = TONGTIEN, @MAKH = MAKH 
	FROM inserted

	SELECT @TONGDSTRU = TONGTIEN, @MAKH = MAKH 
	FROM deleted

	UPDATE KHACH
	SET DOANHSO = DOANHSO + @TONGDSTHEM - @TONGDSTRU
	WHERE MAKH = @MAKH

	PRINT N'Doanh số của khách hàng có thay đổi.'
END
		---UPDATE---
CREATE TRIGGER TRG_HOADON_DOANHSO_UPDATE
ON HOADON
AFTER UPDATE
AS IF(UPDATE(TONGTIEN) OR UPDATE(MAKH))
BEGIN
	DECLARE @MAKH varchar(128)
	DECLARE @TONGDSTHEM money
	DECLARE @TONGDSTRU money

	SET @TONGDSTHEM = 0
	SET @TONGDSTRU = 0

	SELECT @TONGDSTHEM = TONGTIEN, @MAKH = MAKH 
	FROM inserted

	SELECT @TONGDSTRU = TONGTIEN, @MAKH = MAKH 
	FROM deleted

	UPDATE KHACH
	SET DOANHSO = DOANHSO + @TONGDSTHEM - @TONGDSTRU
	WHERE MAKH = @MAKH

	PRINT N'Đã cập nhật doanh số của các khách hàng thành công'
END

/*--II.Rang buoc toan ven cho HOADON--*/
----1.Ngay mua hang phai lon hon hoac bang ngay dang ky
--	--a.Trigger cho HOADON--
--		---INSERT---
--CREATE TRIGGER TRG_HOADON_NGMH_INSERT
--ON HOADON
--AFTER INSERT
--AS
--BEGIN
--	DECLARE @MAKH varchar(128)
--	DECLARE @NGMH smalldatetime
--	DECLARE @NGDK smalldatetime

--	SELECT @MAKH = inserted.MAKH, @NGMH = NGMH, @NGDK = NGDK
--	FROM KHACH, inserted
--	WHERE KHACH.MAKH = inserted.MAKH

--	IF (@NGMH < @NGDK)
--	BEGIN
--		PRINT N'Ngày mua hàng phải lớn hơn hoặc bằng ngày đăng ký'
--		ROLLBACK TRANSACTION
--	END
--END
--		---UPDATE---
--CREATE TRIGGER TRG_HOADON_NGMH_UPDATE
--ON HOADON
--AFTER UPDATE
--AS IF (UPDATE(NGMH) OR UPDATE(MAKH))
--BEGIN
--	DECLARE @MAKH varchar(128)
--	DECLARE @NGMH smalldatetime
--	DECLARE @NGDK smalldatetime

--	SELECT @MAKH = inserted.MAKH, @NGMH = NGMH, @NGDK = NGDK
--	FROM KHACH, inserted
--	WHERE KHACH.MAKH = inserted.MAKH

--	IF (@NGMH < @NGDK)
--	BEGIN
--		PRINT N'Lỗi! Ngày mua hàng phải lớn hơn hoặc bằng ngày đăng ký'
--		ROLLBACK TRANSACTION
--	END
--END
--	--b.Trigger cho KHACH--
--			---UPDATE---
--CREATE TRIGGER TRG_KHACH_NGMH_UPDATE
--ON KHACH
--AFTER UPDATE
--AS IF (UPDATE(NGDK))
--BEGIN
--	DECLARE @MAKH varchar(128)
--	DECLARE @NGMH smalldatetime
--	DECLARE @NGDK smalldatetime

--	SELECT @MAKH = inserted.MAKH, @NGMH = NGMH, @NGDK = NGDK
--	FROM HOADON, inserted
--	WHERE HOADON.MAKH = inserted.MAKH

--	IF (@NGMH < @NGDK)
--	BEGIN
--		PRINT N'Không thể cập nhật cho khách vì ngày mua hàng phải lớn hơn hoặc bằng ngày đăng ký'
--		ROLLBACK TRANSACTION
--	END
--END
--2.Tong tien bang tong tri gia cac chi tiet hoa don
	--a.Trigger cho bang HOADON--
		---INSERT---
CREATE TRIGGER TRG_HOADON_TONGTIEN_INSERT
ON HOADON
AFTER INSERT
AS
BEGIN
	DECLARE @SOHD varchar(128)

	SELECT @SOHD = SOHD
	FROM  inserted

	UPDATE HOADON
	SET  TONGTIEN = 0
	WHERE SOHD = @SOHD

	PRINT N'Đã thêm 1 hóa đơn với tổng tiền ban đầu là 0 VNĐ'
END
		---UPDATE---
CREATE TRIGGER TRG_HOADON_TONGTIEN_UPDATE
ON HOADON
AFTER UPDATE
AS IF(UPDATE(TONGTIEN))
BEGIN
	DECLARE @SOHD varchar(128)
	DECLARE @TONGTIEN money
	DECLARE @TONGTRIGIA money
	
	SET @TONGTRIGIA = 0
	
	SELECT @SOHD = SOHD, @TONGTIEN = TONGTIEN 
	FROM inserted

	SELECT @TONGTRIGIA = SUM(TRIGIA) 
	FROM CTHD
	WHERE SOHD = @SOHD
	GROUP BY SOHD

	IF(@TONGTIEN <> @TONGTRIGIA)
	BEGIN
		PRINT N'Lỗi! Tổng tiền của một hóa đơn phải bằng tổng trị giá các chi tiết hóa đơn'
		ROLLBACK TRANSACTION
	END
	ELSE
		PRINT N'Tổng tiền không bị thay đổi'
END
	--b.Trigger cho bang CTHD--
		---INSERT, DELETE, UPDATE---
CREATE TRIGGER TRG_CTHD_TONGTIEN_INSERT_DELETE_UPDATE
ON CTHD
AFTER INSERT, DELETE, UPDATE
AS 
BEGIN
	DECLARE @SOHD varchar(128)
	DECLARE @TONGTRIGIATHEM money
	DECLARE @TONGTRIGIATRU money

	SET @TONGTRIGIATHEM = 0
	SET @TONGTRIGIATRU = 0

	SELECT @TONGTRIGIATHEM = TRIGIA, @SOHD = SOHD 
	FROM inserted

	SELECT @TONGTRIGIATRU = TRIGIA, @SOHD = SOHD 
	FROM deleted

	UPDATE HOADON
	SET TONGTIEN = TONGTIEN + @TONGTRIGIATHEM - @TONGTRIGIATRU
	WHERE SOHD = @SOHD

	PRINT N'Tổng tiền của hóa đơn có biến thiên'
END

/*--III.Rang buoc toan ven cho CTHD--*/
--1.Tri gia bang so luong nhan don gia
	--Trigger cho bang CTHD--
		---INSERT---
CREATE TRIGGER TRG_CTHD_TRIGIA_INSERT
ON CTHD
AFTER INSERT
AS
BEGIN
	DECLARE @MASP varchar(128)
	DECLARE @SOHD varchar(128)
	DECLARE @SOLUONG int
	DECLARE @TRIGIA money
	DECLARE @DONGIA money

	SELECT @MASP = inserted.MASP, @SOHD = inserted.SOHD, @TRIGIA = inserted.TRIGIA, @SOLUONG = inserted.SOLUONG, @DONGIA = SANPHAM.DONGIA
	FROM inserted, SANPHAM
	WHERE inserted.MASP = SANPHAM.MASP

	IF( @TRIGIA <> (@SOLUONG * @DONGIA) )
	BEGIN
		PRINT N'Trị giá phải bằng số lượng nhân đơn giá nên trị giá thêm vào không phù hợp!'
	END
	
	UPDATE CTHD
	SET TRIGIA = @SOLUONG * @DONGIA
	WHERE MASP = @MASP AND SOHD = @SOHD

	PRINT N'Thêm thành công 1 chi tiết hóa đơn'
END
		---UPDATE_SOLUONG---
CREATE TRIGGER TRG_CTHD_TRIGIA_UPDATE_SOLUONG
ON CTHD
AFTER UPDATE
AS IF (UPDATE(SOLUONG))
BEGIN
	DECLARE @MASP varchar(128)
	DECLARE @SOHD varchar(128)
	DECLARE @SOLUONG int
	DECLARE @DONGIA money

	SELECT @MASP = inserted.MASP, @SOHD = inserted.SOHD, @SOLUONG = inserted.SOLUONG, @DONGIA = SANPHAM.DONGIA
	FROM inserted, SANPHAM
	WHERE inserted.MASP = SANPHAM.MASP

	UPDATE CTHD
	SET TRIGIA = @SOLUONG * @DONGIA
	WHERE MASP = @MASP AND SOHD = @SOHD

	PRINT N'Đã cập nhật số lượng cho chi tiết hóa đơn thành công'
END
		---UPDATE_TRIGIA---
CREATE TRIGGER TRG_CTHD_TRIGIA_UPDATE_TRIGIA
ON CTHD
AFTER UPDATE
AS IF (UPDATE(TRIGIA))
BEGIN
	DECLARE @MASP varchar(128)
	DECLARE @SOHD varchar(128)
	DECLARE @SOLUONG int
	DECLARE @TRIGIA money
	DECLARE @DONGIA money

	SELECT @MASP = inserted.MASP, @SOHD = inserted.SOHD, @TRIGIA = inserted.TRIGIA, @SOLUONG = inserted.SOLUONG, @DONGIA = SANPHAM.DONGIA
	FROM inserted, SANPHAM
	WHERE inserted.MASP = SANPHAM.MASP

	IF( @TRIGIA <> (@SOLUONG * @DONGIA) )
	BEGIN
		PRINT N'Trị giá phải bằng số lượng nhân đơn giá'
		ROLLBACK TRANSACTION
	END
	ELSE
		PRINT N'Trị giá không bị thay đổi'
END
	--Trigger cho bang SANPHAM--
		---UPDATE---
CREATE TRIGGER TRG_SANPHAM_TRIGIA_UPDATE
ON SANPHAM
AFTER UPDATE
AS IF(UPDATE(DONGIA))
BEGIN
	DECLARE @MASP varchar(128)
	DECLARE @SOHD varchar(128)
	DECLARE @SOLUONG int
	DECLARE @DONGIA money

	SELECT @MASP = inserted.MASP, @SOHD = CTHD.SOHD, @SOLUONG = CTHD.SOLUONG, @DONGIA = inserted.DONGIA
	FROM inserted, CTHD
	WHERE inserted.MASP = CTHD.MASP

	UPDATE CTHD
	SET TRIGIA = @SOLUONG * @DONGIA
	WHERE MASP = @MASP AND SOHD = @SOHD

	PRINT N'Đã cập nhật đơn giá cho sản phẩm thành công'
END


/*--IV.Rang buoc toan ven cho CUAHANG--*/
--1.Doanh thu 1 cua hang bang tong tat ca tong tien xuat tu cua hang do
	--Trigger cho bang CUAHANG--
		---INSERT---
CREATE TRIGGER TRG_CUAHANG_DOANHTHU_INSERT
ON CUAHANG
AFTER INSERT
AS 
BEGIN
	DECLARE @MACH varchar(128)

	SELECT @MACH = inserted.MACH
	FROM inserted

	UPDATE CUAHANG
	SET DOANHTHU = 0
	WHERE MACH = @MACH

	PRINT N'Đã thêm 1 cửa hàng mới với doanh thu bằng 0 VNĐ'
END
		---UPDATE---
CREATE TRIGGER TRG_CUAHANG_DOANHTHU_UPDATE
ON CUAHANG
AFTER UPDATE
AS IF(UPDATE(DOANHTHU))
BEGIN
	DECLARE @MACH varchar(128)
	DECLARE @DOANHTHU money
	DECLARE @TONGcuaTONGTIEN money

	SET @TONGcuaTONGTIEN = 0

	SELECT @MACH = MACH, @DOANHTHU = DOANHTHU
	FROM inserted

	SELECT @TONGcuaTONGTIEN = SUM(HOADON.TONGTIEN)
	FROM inserted, HOADON
	WHERE inserted.MACH = HOADON.MACH
	GROUP BY inserted.MACH 

	IF(@DOANHTHU <> @TONGcuaTONGTIEN)
	BEGIN
		PRINT N'Doanh thu của cửa hàng phải bằng tổng tiền của tất cả hóa đơn xuất ra từ cửa hàng đó'
		ROLLBACK TRANSACTION
	END
	ELSE
		PRINT N'Doanh thu cửa cửa hàng không bị thay đổi.'
END
	--Trigger cho bang HOADON--
		---INSERT, DELETE---
CREATE TRIGGER TRG_HOADON_DOANHTHU_INSERT_DELETE
ON HOADON
AFTER INSERT, DELETE
AS 
BEGIN
	DECLARE @MACH varchar(128)
	DECLARE @TONGDOANHTHUTHEM money
	DECLARE @TONGDOANHTHUTRU money

	SET @TONGDOANHTHUTHEM = 0
	SET @TONGDOANHTHUTRU = 0

	SELECT @MACH = inserted.MACH, @TONGDOANHTHUTHEM = inserted.TONGTIEN
	FROM inserted

	SELECT @MACH = deleted.MACH, @TONGDOANHTHUTRU = deleted.TONGTIEN
	FROM deleted

	UPDATE CUAHANG
	SET DOANHTHU = DOANHTHU + @TONGDOANHTHUTHEM - @TONGDOANHTHUTRU
	WHERE MACH = @MACH

	PRINT N'Doanh thu của cửa hàng có thay đổi!'
END
		---UPDATE---
CREATE TRIGGER TRG_HOADON_DOANHTHU_UPDATE
ON HOADON
AFTER UPDATE
AS IF(UPDATE(MACH) OR UPDATE(TONGTIEN))
BEGIN
	DECLARE @MACH varchar(128)
	DECLARE @TONGDOANHTHUTHEM money
	DECLARE @TONGDOANHTHUTRU money

	SET @TONGDOANHTHUTHEM = 0
	SET @TONGDOANHTHUTRU = 0

	SELECT @MACH = inserted.MACH, @TONGDOANHTHUTHEM = inserted.TONGTIEN
	FROM inserted

	SELECT @MACH = deleted.MACH, @TONGDOANHTHUTRU = deleted.TONGTIEN
	FROM deleted

	UPDATE CUAHANG
	SET DOANHTHU = DOANHTHU + @TONGDOANHTHUTHEM - @TONGDOANHTHUTRU
	WHERE MACH = @MACH

	PRINT N'Đã cập nhật doanh thu thành công!!!'
END