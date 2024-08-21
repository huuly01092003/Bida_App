use master 
go
--drop database BiDa
create database BiDa

go
use BiDa

/*----------------- Bang Userlogin --------------------------*/

--drop table Userlogin
create table Userlogin
	(
		username	varchar(100) primary key,
		password	nvarchar(250),
		screenname	nvarchar(250),
		status		int
	)
Go
--insert default userlogin ('admin','123','', 1)
insert into Userlogin values('admin','a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3','', 1);
Go

--drop procedure spUpdateUserlogin
create procedure spUpdateUserlogin
(
		@username		varchar(100),
		@password		varchar(250)				
)
as
Begin
		update Userlogin set 
		username=@username,
		password=@password
		where username = @username
End
go
--drop procedure spCheckLogin
create procedure spCheckLogin
(
		@username		varchar(100),
		@password		varchar(250)
)
	as
	Begin
				select	* from Userlogin
				where	username= @username
				and		password=@password				
	End
Go
/*-------------------- table Ban -------------------*/	
	-- drop table Ban
	create table Ban
	(
			MaBan		bigint primary key,
			TenBan		nvarchar(500),
			LoaiBan		int,
			MoTa	nvarchar(1000),
			GiaBan		float,
			TrangThai		int
	)
	go
	-- drop function fcgeneralDeskId
	Create function fcgeneralDeskId()
		returns bigint
		As
		Begin
			Declare @Id bigint
			Declare @MaxId bigint
			
			Select @MaxId = MAX(MaBan) from Ban
							
			if exists (select MaBan from Ban)
				set @Id = @MaxId+1
			else
				set @Id = 1
			return @Id
	End
	go
	-- drop procedure spInsertDesk
	create procedure spInsertDesk
	( 
			@id				bigint,
			@name			nvarchar(500),
			@style			int,
			@description	nvarchar(1000),
			@status			int,
			@fee			float
	)
	as
	Begin
		insert into Ban(MaBan, TenBan, LoaiBan ,MoTa, TrangThai, GiaBan)
		values(@id, @name, @style, @description, @status, @fee)
	End
	go
	-- drop procedure spUpdateDesk
	create procedure spUpdateDesk
	(
			@id				bigint,
			@name			nvarchar(500),
			@style			int,
			@description	nvarchar(1000),
			@status			int,
			@fee			float
	)
	as
	Begin
		update Ban set 
			LoaiBan = @style,
			TenBan =@name,
			MoTa =@description,
			TrangThai =@status,
			GiaBan =@fee
		where MaBan =@id
	End
	go
	--drop procedure spDeleteDesk
	create procedure spDeleteDesk
	(
			@id		bigint
	)
	as
	Begin
		delete from Ban where MaBan=@id
	End
	go
	-- drop procedure spFindDesk
	create procedure spFindDesk
	as
	Begin
	select MaBan, TenBan, MoTa,
		CASE
			WHEN LoaiBan = 0 THEN N'Bida lỗ'
			ELSE N'Bida france'
		END AS style,
		CASE
			WHEN TrangThai = 0 THEN N'Đang nâng cấp'
			WHEN TrangThai = 1 THEN N'Đang sử dụng'
			ELSE N'Chưa sử dụng'
		END AS status,
		(select format(GiaBan,'#,0')) as fee
    from Ban
	End
	go
	-- drop procedure spFindByStatusDesk
	create procedure spFindByStatusDesk
	(
			@status		int
	)
	as
	Begin
		select * from Ban where TrangThai=@status
	End
	go
	--drop procedure spUpdateStatusDesk
	create procedure spUpdateStatusDesk
	(
			@id		bigint,
			@status int
	)
	as
	Begin
		update Ban set TrangThai = @status where MaBan=@id
	End
	go
	-- drop procedure spFindAllDesk
	create procedure spFindAllDesk
	as
	Begin
		select * from Ban
	End
	go
	-- drop procedure spFindByDeskid
	create procedure spFindByDeskid
	(
		@id				bigint
	)
	as
	Begin
		select * from Ban where MaBan = @id;
	End
	go

/*-------------------- table DichVu -------------------*/	
	-- drop table DichVu
	create table DichVu
	(
			MaDichVu			bigint primary key,
			TenDichVu		nvarchar(500),
			DonViTinh		nvarchar(500),
			GiaDichVu		float,
			MoTa	nvarchar(1000)
	)
	go
	-- drop function fcgetIdMenu
	Create function fcgetIdMenu()
		returns bigint
		As
		Begin
			Declare @Id bigint
			Declare @MaxId bigint
			
			Select @MaxId = MAX(MaDichVu) from DichVu
							
			if exists (select MaDichVu from DichVu)
				set @Id = @MaxId+1
			else
				set @Id = 1
			return @Id
	End
	go
	
	-- drop procedure spInsertMenu
	create procedure spInsertMenu
	( 
			@id			bigint,
			@name		nvarchar(500),
			@unit		nvarchar(500),
			@price		float,
			@description	nvarchar(1000)
	)
	as
	Begin
		insert into DichVu(MaDichVu, TenDichVu, DonViTinh, GiaDichVu, MoTa)
		values(@id, @name, @unit, @price, @description)
	End
	go
	-- drop procedure spUpdateMenu
	create procedure spUpdateMenu
	(
			@id			bigint,
			@name		nvarchar(500),
			@unit		nvarchar(500),
			@price		float,
			@description	nvarchar(1000)
	)
	as
	Begin
		update DichVu set 
			TenDichVu =@name,
			DonViTinh =@unit,
			GiaDichVu =@price,
			MoTa =@description
		where MaDichVu =@id
	End
	go
	--drop procedure spDeleteMenu
	create procedure spDeleteMenu
	(
			@id		bigint
	)
	as
	Begin
		delete from DichVu where MaDichVu=@id
	End
	go
	-- drop procedure spFindViewMenu
	create procedure spFindViewMenu
	as
	Begin
		select MaDichVu, TenDichVu, DonViTinh, (select format(GiaDichVu,'#,0')) as price, MoTa from DichVu
	End



	go
/*-------------------- table HoaDon -------------------*/	
	-- drop table HoaDon
	create table HoaDon
	(
			MaHD		bigint primary key,
			MaBan	bigint,
			TenBan		nvarchar(500),
			GiaBan	float,
			timebegin		datetime,
			timefinish		datetime,
			totalminute		int,
			Phi		float,
			TongTien		float,
			TrangThai		int
	)
	go
	-- drop function fcgetIdReceiption
	Create function fcgetIdReceiption()
		returns bigint
		As
		Begin
			Declare @Id bigint
			Declare @MaxId bigint
			
			Select @MaxId = MAX(MaHD) from HoaDon							
			if exists (select MaHD from HoaDon)
				set @Id = @MaxId+1
			else
				set @Id = 1
			return @Id
	End
	go
	-- drop procedure spInsertReceiption
	create procedure spInsertReceiption
	( 
			@id			bigint,
			@deskid		bigint,
			@deskname		nvarchar(500),
			@deskfee		float,
			@timebegin		datetime,
			@status		int
	)
	as
	Begin
		insert into HoaDon(MaHD, MaBan, TenBan, GiaBan, timebegin, TrangThai)
		values(@id, @deskid, @deskname, @deskfee, @timebegin, @status)
	End
	go
	-- drop procedure spUpdateTimebeginReceiption
	create procedure spUpdateTimebeginReceiption
	(
			@id				bigint,
			@timebegin		datetime
	)
	as
	Begin
		update HoaDon set 
			timebegin = @timebegin
		where MaHD =@id
	End
	go
	-- drop procedure spUpdateReceiptionFinish
	create procedure spUpdateReceiptionFinish
	(
			@id				bigint,
			@timefinish		datetime,
			@totalminute		int,
			@fee		float,
			@total		float,
			@status		int
	)
	as
	Begin
		update HoaDon set 
			timefinish = @timefinish,
			totalminute =@totalminute,
			Phi =@fee,--co the sai
			TongTien =@total,
			TrangThai =@status
		where MaHD =@id
	End
	go
	-- drop procedure spFindReceiptionByDeskid
	create procedure spFindReceiptionByDeskid
	(
			@deskid		bigint
	)
	as
	Begin
		select MaHD, MaBan, TenBan, (select format(GiaBan,'#,0')) as deskfee,
		(SELECT FORMAT(timebegin,'HH:mm dd/MM/yyyy')) as timebegin,
		(SELECT FORMAT(timefinish,'HH:mm dd/MM/yyyy')) as timefinish,
		totalminute, (select format(Phi,'#,0')) as fee, 
		(select format(TongTien,'#,0')) as total, 
		CASE
			WHEN TrangThai = 0 THEN N'Đang chơi'
			ELSE N'Hoàn thành'
		END AS status 
		from HoaDon where MaBan = @deskid and TrangThai =0 order by MaHD desc
	End
	go
	-- drop procedure spTransferReceiption
	create procedure spTransferReceiption
	( 
			@id			bigint,
			@deskid		bigint,
			@deskname		nvarchar(500),
			@deskfee		float,
			@timebegin		datetime
	)
	as
	Begin
		update HoaDon set 
			MaBan = @deskid,
			TenBan =@deskname,
			GiaBan =@deskfee,
			timebegin =@timebegin
		where MaHD =@id
	End
	go

	
	/*-------------------- table CTHD -------------------*/	
	-- drop table CTHD
	create table CTHD
	(
			MaCTHD			bigint primary key,
			MaHD		bigint foreign key references HoaDon(MaHD),
			TenDichVu			nvarchar(500),
			DonViTinh		nvarchar(500),
			GiaDichVu		float,
			SoLuongDichVu		int,
			TongTienDichVu		float
	)
	go
	-- drop function fcgetIdReceiptiondetail
	Create function fcgetIdReceiptiondetail()
		returns bigint
		As
		Begin
			Declare @Id bigint
			Declare @MaxId bigint
			
			Select @MaxId = MAX(MaCTHD) from CTHD
							
			if exists (select MaCTHD from CTHD)
				set @Id = @MaxId+1
			else
				set @Id = 1
			return @Id
	End
	go
	-- drop procedure spInsertReceiptiondetail
	create procedure spInsertReceiptiondetail
	( 
			@id			bigint,
			@receiptionid		bigint,
			@name			nvarchar(100),
			@unit		nvarchar(100),
			@price		float,
			@quantum		int,
			@total		float
	)
	as
	Begin
		insert into CTHD(MaCTHD, MaHD, TenDichVu, DonViTinh, GiaDichVu, SoLuongDichVu, TongTienDichVu)
		values(@id, @receiptionid,@name, @unit, @price, @quantum, @total)
	End
	go
	-- drop procedure spUpdateReceiptiondetail
	create procedure spUpdateReceiptiondetail
	(
			@id			bigint,
			
			@name			nvarchar(100),
			@unit		nvarchar(100),
			@price		float,
			@quantum		int,
			@total		float
	)
	as
	Begin
		update CTHD set 
	
			TenDichVu = @name,
			DonViTinh = @unit,
			GiaDichVu = @price,
			SoLuongDichVu = @quantum,
			TongTienDichVu =@total
		where MaCTHD =@id
	End
	go
	--drop procedure spDeleteReceiptiondetail
	create procedure spDeleteReceiptiondetail
	(
			@id		bigint
	)
	as
	Begin
		delete from CTHD where MaCTHD=@id
	End
	go
	-- drop procedure spFindByReceiptionid
	create procedure spFindByReceiptionid
	(
			@receiptionid		bigint
	)
	as
	Begin
		select MaCTHD, TenDichVu, DonViTinh, (select format(GiaDichVu,'#,0')) as price, 
		SoLuongDichVu, (select format(TongTienDichVu,'#,0')) as total  
		from CTHD where MaHD = @receiptionid
	End
	go




--backup database BiDa
--to disk = 'D:\BiDa_Full.bak'
--with init


--backup database BiDa
--to disk = 'D:\BiDa_Diff.bak'
--with differential

--restore database BiDa
--from disk='D:\BiDa_Full.bak'
--with norecovery

--restore database BiDa
--from disk='D:\BiDa_Diff.bak'
--with recovery
