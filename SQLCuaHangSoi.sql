USE master
GO

IF EXISTS (SELECT * FROM sysdatabases WHERE NAME='WebsiteQuanLyCuaHangSoi')
	DROP DATABASE WebsiteQuanLyCuaHangSoi
GO

CREATE DATABASE WebsiteQuanLyCuaHangSoi
GO

USE WebsiteQuanLyCuaHangSoi

CREATE TABLE KhachHang( makh int identity(1, 1) PRIMARY KEY NOT NULL, 
						tenkh nvarchar(50) , 
						diachi nvarchar(50), 
						email nvarchar(50), 
						sdt int, 
						username nvarchar(50), 
						pass nvarchar(50) 
						);

CREATE TABLE NhanVien( manv int identity(1, 1) PRIMARY KEY NOT NULL,
						tennv nvarchar(50),
						ngaysinh datetime,
						diachi nvarchar(50),
						email nvarchar(50),
						sdt nvarchar(50),
						username nvarchar(50),
						pass nvarchar(50),
						roles int 
						);

CREATE TABLE NhaPhanPhoi( manhaphanphoi int identity(1, 1) PRIMARY KEY NOT NULL,
							tennhaphanphoi nvarchar(50),
							diachi nvarchar(50),
							email nvarchar(50),
							sdt int
							);

CREATE TABLE QuocGia( maqg int identity(1, 1) PRIMARY KEY NOT NULL,
						tenquocgia nvarchar(50)
						);

CREATE TABLE LoaiSanPham( maloaisanpham int identity(1, 1) PRIMARY KEY NOT NULL,
							tenloaisanpham nvarchar(50)
							);

CREATE TABLE DonHang( madonhang int identity (1, 1) PRIMARY KEY NOT NULL,
						makh int,
						tinhtranggiaohang bit,
						ngaydathang date,
						ngaygiaohang date,
						FOREIGN KEY(makh) REFERENCES KhachHang(makh), 
						);

CREATE TABLE PhieuNhapHang( maphieunhaphang int identity(1, 1) PRIMARY KEY NOT NULL,
							manhaphanphoi int,
							manv int,
							ngaynhaphang date,
							FOREIGN KEY(manhaphanphoi) REFERENCES NhaPhanPhoi(manhaphanphoi), 
							FOREIGN KEY(manv) REFERENCES NhanVien(manv)
							);

CREATE TABLE SanPham( masp int identity(1, 1) PRIMARY KEY NOT NULL,
						maloaisanpham int,					
						tenloaisanpham nvarchar(50),
						mota nvarchar(500),
						hinhanh nvarchar(200),
						maqg int,
						ngaycapnhat date,
						soluong int,
						dongia decimal(18, 0)
						FOREIGN KEY(maloaisanpham) REFERENCES LoaiSanPham(maloaisanpham), 
						FOREIGN KEY(maqg) REFERENCES QuocGia(maqg)
						);

CREATE TABLE ChiTiet_DonHang_SanPham( madonhang int NOT NULL,
										masp int NOT NULL,
										soluong int,
										dongia decimal(18, 0),
										PRIMARY KEY(madonhang, masp),
										FOREIGN KEY(madonhang) REFERENCES DonHang(madonhang), 
										FOREIGN KEY(masp) REFERENCES SanPham(masp)
										);

CREATE TABLE ChiTiet_PhieuNhapHang_SanPham( maphieunhaphang int NOT NULL,
											masp int NOT NULL,
											soluong int,
											dongia decimal(18, 0),
											PRIMARY KEY (maphieunhaphang, masp),
											FOREIGN KEY(maphieunhaphang) REFERENCES PhieuNhapHang(maphieunhaphang), 
											FOREIGN KEY(masp) REFERENCES SanPham(masp)
											);


--Thêm khách hàng
INSERT INTO KhachHang VALUES (N'Lương Quốc Thành',N'Hà Tĩnh','thanhlq@gmail.com','549357815','thanhlq','123456');
INSERT INTO KhachHang VALUES (N'Đoàn Liên Tiến',N'Hà Ban','tiendl@gmail.com','125489648','tiendl','123456789');
INSERT INTO KhachHang VALUES (N'Trịnh Văn Tuấn',N'72/10 Quốc lộ 1A ','tuantv@gmail.com','515879634','Tuantv','145236');
--Thêm nhân viên
INSERT INTO NhanVien VALUES (N'Đặng Minh Khải','07-02-1999',N'Phu Chau ','tkhai@gmailcom','1023332341','khaidang','123456','1');
INSERT INTO NhanVien VALUES (N'Trịnh Minh Đức','07-02-1999',N'Tam Hà ','ductm@gmail.com','23323111','ductm','2345','0');
INSERT INTO NhanVien VALUES (N'Đoàn Liêng Tiến','07-02-1999',N'Bà Rịa ','tienld@gmail.com','23451678','tiendoan','5253','1');
--Thêm nhà phẩn phối

--Thêm quốc gia
INSERT INTO QuocGia VALUES (N'Việt Nam');
INSERT INTO QuocGia VALUES (N'Hàn Quốc');
INSERT INTO QuocGia VALUES (N'Mỹ');
INSERT INTO QuocGia VALUES (N'MaLaysia');
INSERT INTO QuocGia VALUES (N'Nga');
INSERT INTO QuocGia VALUES (N'Singapo');
--Thêm Loại sản phẩm											
INSERT INTO LoaiSanPham VALUES ('Polyester');
INSERT INTO LoaiSanPham VALUES ('Polyester POY');
INSERT INTO LoaiSanPham VALUES ('Polytester DTY');
INSERT INTO LoaiSanPham VALUES ('Polytester FDY');
INSERT INTO LoaiSanPham VALUES ('Polyamide (PA) - Nylon');
INSERT INTO LoaiSanPham VALUES ('Polypropylen (PP)');
INSERT INTO LoaiSanPham VALUES ('Polypropylen (PP)');
INSERT INTO LoaiSanPham VALUES ('Elastane (EL) – Spandex');
INSERT INTO LoaiSanPham VALUES ('Sợi bông – cotton');
INSERT INTO LoaiSanPham VALUES ('Sợi len – wool');

--Thêm Sản phẩm
INSERT INTO SanPham VALUES ('1',N'Sợi Polyester ',N'Polyester là một loại sợi tổng hợp với thành phần cấu tạo đặc trưng là ethylene (nguồn gốc từ dầu mỏ).','polyesters.jpg','1','2021-07-11','3','25000');
INSERT INTO SanPham VALUES ('2',N'Sợi polyester (POY)',N'Sợi Polyester Partially Oriented Yarn, thường được gọi là Polyester POY là dạng chính của sợi Polyester.','polyesterPoy.jpg','2','2021-07-11','5','35000');
INSERT INTO SanPham VALUES ('3',N'Sợi polyester (DTY)',N'Sợi Polyester (DTY) Drawn Textured Yarn được hình thành khi kéo và xoắn Polyester POY cùng một lúc. Sợi DTY chủ yếu được sử dụng như một phần của dệt và đan các loại vải để may quần áo, trang trí nhà cửa, bọc ghế, túi xách và nhiều thứ khác nhau. ','polyestersDTY.jpg','3','2021-07-11','6','50000');
INSERT INTO SanPham VALUES ('4',N'Sợi Polytester (FDY)',N'Sợi FDY có ánh sáng ba chiều được sử dụng rộng rãi trong việc làm rèm cửa, ga trải giường và thảm. Polyester filament có sẵn trong nhuộm trắng cũng như nhuộm. Nhuộm Dope FDY có thể được sử dụng để tạo ra vải màu trực tiếp thay vì làm vải bằng FDY Raw-trắng trước và sau đó nhuộm nó. Catonic FDY là một biến thể khác của sợi Filament. Sợi Catonic Polyester filament được làm từ Catonic PET Chips.','polyesterFTY.jpg','4','2021-07-11','4','27500');
INSERT INTO SanPham VALUES ('5',N'Sợi Polyamide (PA) – Nylon',N'Polyamide là một loại sợi tổng hợp được sản xuất từ các sợi Polyme. Chất liệu vải này được chế tạo ra từ các phản ứng carbon có trong dầu thô và than khi được làm nóng ở nhiệt độ lớn. Phản ứng tạo ra Polyamide được gọi là phản ứng trùng hợp ngưng tụ. Ngày nay, vải Polyamide thường được biết đến với tên gọi phổ biến là Nylon.','Polyamide(PA).jpg','5','2021-07-11','10','20000');