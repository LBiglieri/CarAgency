CREATE or ALTER proc Permiso_Insert_Permission_Permission @Padre_Id uniqueidentifier, @Hijo_Id uniqueidentifier , @DVH char(64) as

insert into Permiso_Permiso(Padre_Id,Hijo_Id,DVH) values (@Padre_Id,@Hijo_Id,@DVH);

go
