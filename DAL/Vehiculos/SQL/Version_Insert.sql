CREATE or ALTER proc Version_Insert @Version_Id uniqueidentifier, @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier, @Descripcion varchar(255) , @DVH char(64) as

if not exists (select * from Version where Marca_Id = @Marca_Id and Modelo_Id = @Modelo_Id and Descripcion = @Descripcion)
begin
	insert into Version(Version_Id,Marca_Id,Modelo_Id,Descripcion,DVH) values (@Version_Id,@Marca_Id,@Modelo_Id,@Descripcion,@DVH);
end

select 'success' as SQLResultType, 'Operation completed.' as message
go
