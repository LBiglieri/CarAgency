CREATE or ALTER proc User_Add	@Id uniqueidentifier, @Dni int, @Username varchar(50), @Password varchar(max), @Name varchar(max), @Surname varchar(max), 
											@Role_Id uniqueidentifier, @Blocked bit, @Active bit, @Available_Login_Attempts int as

insert into Users(Id,Dni,Username,Password,Name,Surname,Role_Id,Blocked,Active,Available_Login_Attempts) 
values (@Id,@Dni,@Username,@Password,@Name,@Surname,@Role_Id,@Blocked,@Active,@Available_Login_Attempts)

select 'success' as SQLResultType,'User added succesfully!' as message
go

