CREATE or ALTER proc Vehiculo_Delete	@Vehiculo_Id uniqueidentifier as

delete Vehiculo
where Vehiculo_Id = @Vehiculo_Id

select 'success' as SQLResultType,'Vehicle deleted succesfully!' as message
go

