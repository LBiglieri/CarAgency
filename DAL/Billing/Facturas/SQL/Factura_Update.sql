CREATE or ALTER proc Factura_Update	@Factura_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Reserva_Id uniqueidentifier, @Detalle varchar(max),
										@CuilCuit varchar(max), @RazonSocial varchar(max), @Valor float, @EstadoPago bit, @Fecha datetime , @DVH char(64) as

update Factura
set DVH = @DVH,
	Vehiculo_Id = @Vehiculo_Id,
	Persona_Id = @Persona_Id,
	Reserva_Id = @Reserva_Id,
	Detalle = @Detalle,
	CuilCuit = @CuilCuit,
	RazonSocial = @RazonSocial,
	Valor = @Valor,
	EstadoPago = @EstadoPago,
	Fecha = @Fecha
where Factura_Id = @Factura_Id

select 'success' as SQLResultType,'Invoice updated succesfully!' as message
go

