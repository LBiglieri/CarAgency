CREATE or ALTER proc Permissions_GetAllByUser @User_Id uniqueidentifier as

select p.* from Users_Permissions up inner join Permissions p on up.Permission_Id=p.Id where up.User_Id=@User_Id

go