CREATE or ALTER proc Permissions_InsertComponent @Id uniqueidentifier, @Name varchar(50), @Type varchar(50) as

insert into Permissions(Id,Name,Type) values (@Id,@Name,@Type);

go