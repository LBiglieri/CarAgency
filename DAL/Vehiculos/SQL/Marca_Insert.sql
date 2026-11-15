CREATE or ALTER proc Marca_Insert @Marca_Id uniqueidentifier, @Descripcion varchar(50) , @DVH char(64) as

if not exists (select * from Marca where Descripcion = @Descripcion)
begin
	insert into Marca(Marca_Id,Descripcion,DVH) values (@Marca_Id,@Descripcion,@DVH);
end

select 'success' as SQLResultType,'Make added succesfully!' as message

go
