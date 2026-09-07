CREATE or ALTER proc Permissions_InsertComponent @Id uniqueidentifier, @Name varchar(50), @Type varchar(50) , @DVH char(64) as

insert into Permissions(Id,Name,Type,DVH) values (@Id,@Name,@Type,@DVH);

go
