CREATE or ALTER proc Modelo_Insert @Modelo_Id uniqueidentifier, @Marca_Id uniqueidentifier, @Descripcion varchar(255) , @DVH char(64) as

if not exists (select * from Modelo where Marca_Id = @Marca_Id and Descripcion = @Descripcion)
begin
	insert into Modelo(Modelo_Id,Marca_Id,Descripcion,DVH) values (@Modelo_Id,@Marca_Id,@Descripcion,@DVH);
end

select 'success' as SQLResultType, 'Operation completed.' as message
go
