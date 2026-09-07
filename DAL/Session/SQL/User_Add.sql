CREATE or ALTER proc User_Add	@Id uniqueidentifier, @Dni int, @Username varchar(50), @Password varchar(max), @Name nvarchar(max), @Surname nvarchar(max),
											@Role_Id uniqueidentifier, @Blocked bit, @Active bit, @Available_Login_Attempts int, @Language_Code varchar(2) , @DVH char(64) as

insert into Users(Id,Dni,Username,Password,Name,Surname,Role_Id,Blocked,Active,Available_Login_Attempts,Language_Code,DVH)
values (@Id,@Dni,@Username,@Password,@Name,@Surname,@Role_Id,@Blocked,@Active,@Available_Login_Attempts,@Language_Code,@DVH)

select 'success' as SQLResultType,'User added succesfully!' as message
go

