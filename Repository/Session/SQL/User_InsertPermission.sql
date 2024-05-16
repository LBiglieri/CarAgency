CREATE or ALTER proc User_InsertPermission @User_Id uniqueidentifier, @Permission_Id uniqueidentifier as

insert into Users_Permissions(User_id,Permission_id) values (@User_Id,@Permission_Id)

go