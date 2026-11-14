CREATE or ALTER proc Permiso_InsertComponent @Permiso_Id uniqueidentifier, @Nombre varchar(50), @Tipo varchar(50) , @DVH char(64) as

insert into Permiso(Permiso_Id,Nombre,Tipo,DVH) values (@Permiso_Id,@Nombre,@Tipo,@DVH);

go
