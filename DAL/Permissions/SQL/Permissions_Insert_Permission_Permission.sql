CREATE or ALTER proc Permissions_Insert_Permission_Permission @Father_Id uniqueidentifier, @Child_Id uniqueidentifier as

insert into Permission_Permission(Father_Id,Child_Id) values (@Father_Id,@Child_Id);

go