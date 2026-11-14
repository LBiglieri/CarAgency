CREATE or ALTER proc Permiso_DeleteFamily @Permiso_Id uniqueidentifier as

delete from Permiso_Permiso where Padre_Id=@Permiso_Id;

go