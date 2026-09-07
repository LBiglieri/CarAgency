CREATE or ALTER proc Permissions_Insert_Permission_Permission @Father_Id uniqueidentifier, @Child_Id uniqueidentifier , @DVH char(64) as

insert into Permission_Permission(Father_Id,Child_Id,DVH) values (@Father_Id,@Child_Id,@DVH);

go
