CREATE or ALTER proc User_Add	@Id uniqueidentifier, @Dni int, @Username varchar(50), @Password varchar(max), @Name varchar(max), @Surname varchar(max), 
											@Role_Id uniqueidentifier, @Blocked bit, @Active bit, @Available_Login_Attempts int, @Language_Code varchar(2) as

insert into Users(Id,Dni,Username,Password,Name,Surname,Role_Id,Blocked,Active,Available_Login_Attempts,Language_Code) 
values (@Id,@Dni,@Username,@Password,@Name,@Surname,@Role_Id,@Blocked,@Active,@Available_Login_Attempts,@Language_Code)

select 'success' as SQLResultType,'User added succesfully!' as message
go

