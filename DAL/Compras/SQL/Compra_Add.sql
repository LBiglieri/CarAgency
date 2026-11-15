CREATE or ALTER proc Compra_Add	@Compra_Id uniqueidentifier, @Numero int, @Persona_Id uniqueidentifier, @VehiculoProspecto_Id uniqueidentifier, @Vendedor_Id uniqueidentifier,
									@Estado int, @MotivoRechazo int, @Fecha datetime, @TasacionAceptada_Id uniqueidentifier, @PrecioAcordado float,
									@CuilCuitTitular varchar(13), @FechaFirmaContrato datetime, @CodigoPrecarga08 varchar(50), @FechaCitaTransferencia datetime,
									@Vehiculo_Id uniqueidentifier, @FechaCierre datetime, @DVH char(64) as

insert into Compra(Compra_Id, Numero, Persona_Id, VehiculoProspecto_Id, Vendedor_Id, Estado, MotivoRechazo, Fecha, TasacionAceptada_Id, PrecioAcordado,
						 CuilCuitTitular, FechaFirmaContrato, CodigoPrecarga08, FechaCitaTransferencia, Vehiculo_Id, FechaCierre, DVH)
values (@Compra_Id, @Numero, @Persona_Id, @VehiculoProspecto_Id, @Vendedor_Id, @Estado, @MotivoRechazo, @Fecha, @TasacionAceptada_Id, @PrecioAcordado,
		@CuilCuitTitular, @FechaFirmaContrato, @CodigoPrecarga08, @FechaCitaTransferencia, @Vehiculo_Id, @FechaCierre, @DVH)

select 'success' as SQLResultType,'Acquisition added succesfully!' as message
go
