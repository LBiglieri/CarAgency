CREATE or ALTER proc Tramite_Update	@Tramite_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Factura_Id uniqueidentifier, @CodigoPrecarga08 varchar(50),
									@FechaTransferencia datetime, @Observaciones varchar(max), @Finalizado bit , @DVH char(64) as

update Tramite
set DVH = @DVH,
	Vehiculo_Id = @Vehiculo_Id,
	Persona_Id = @Persona_Id,
	Factura_Id = @Factura_Id,
	CodigoPrecarga08 = @CodigoPrecarga08,
	FechaTransferencia = @FechaTransferencia,
	Observaciones = @Observaciones,
	Finalizado = @Finalizado
where Tramite_Id = @Tramite_Id

select 'success' as SQLResultType,'Paperwork updated succesfully!' as message
go

