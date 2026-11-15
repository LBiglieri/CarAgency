CREATE or ALTER proc Modelo_Delete	@Modelo_Id uniqueidentifier as

delete Modelo
where Modelo_Id = @Modelo_Id

select 'success' as SQLResultType,'Model deleted succesfully!' as message
go

