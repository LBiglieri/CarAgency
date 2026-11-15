CREATE or ALTER proc Marca_Delete	@Marca_Id uniqueidentifier as

delete Marca
where Marca_Id = @Marca_Id

select 'success' as SQLResultType,'Make deleted succesfully!' as message
go
