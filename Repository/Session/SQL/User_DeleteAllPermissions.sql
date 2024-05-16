CREATE or ALTER proc User_DeleteAllPermissions @Id varchar(50) as

delete from Users_Permissions where User_id=@Id;

go