CREATE or ALTER proc Compra_Update	@Compra_Id uniqueidentifier, @Numero int, @Persona_Id uniqueidentifier, @VehiculoProspecto_Id uniqueidentifier, @Vendedor_Id uniqueidentifier,
											@Estado int, @MotivoRechazo int, @Fecha datetime, @TasacionAceptada_Id uniqueidentifier, @PrecioAcordado float,
											@CuilCuitTitular varchar(13), @FechaFirmaContrato datetime, @CodigoPrecarga08 varchar(50), @FechaCitaTransferencia datetime,
											@Vehiculo_Id uniqueidentifier, @FechaCierre datetime, @DVH char(64) as

-- Fila completa: el DVH se recalcula con todas las columnas aunque solo cambie el estado.
update Compra
set DVH = @DVH,
	Numero = @Numero,
	Persona_Id = @Persona_Id,
	VehiculoProspecto_Id = @VehiculoProspecto_Id,
	Vendedor_Id = @Vendedor_Id,
	Estado = @Estado,
	MotivoRechazo = @MotivoRechazo,
	Fecha = @Fecha,
	TasacionAceptada_Id = @TasacionAceptada_Id,
	PrecioAcordado = @PrecioAcordado,
	CuilCuitTitular = @CuilCuitTitular,
	FechaFirmaContrato = @FechaFirmaContrato,
	CodigoPrecarga08 = @CodigoPrecarga08,
	FechaCitaTransferencia = @FechaCitaTransferencia,
	Vehiculo_Id = @Vehiculo_Id,
	FechaCierre = @FechaCierre
where Compra_Id = @Compra_Id

select 'success' as SQLResultType,'Acquisition updated succesfully!' as message
go
