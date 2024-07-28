CREATE or ALTER proc User_Update	@Id uniqueidentifier, @Dni int, @Username varchar(50), @Password varchar(max), @Name varchar(max), @Surname varchar(max), 
											@Role_Id uniqueidentifier, @Blocked bit, @Active bit, @Available_Login_Attempts int, @Language_Code varchar(2) as

update Users
set Dni = @Dni,
	Username = @Username,
	Password = @Password,
	Name = @Name,
	Surname = @Surname,
	Role_Id = @Role_Id,
	Blocked = @Blocked,
	Active = @Active,
	Available_Login_Attempts = @Available_Login_Attempts,
	Language_Code = @Language_Code 
where Id = @Id

select 'success' as SQLResultType,'User updated succesfully!' as message
go

