CREATE or ALTER proc Factura_Add	@Factura_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Reserva_Id uniqueidentifier, @Detalle varchar(max),
									@CuilCuit varchar(max), @RazonSocial varchar(max), @Valor float, @EstadoPago bit, @Fecha datetime , @DVH char(64) as

insert into Factura(Factura_Id, Vehiculo_Id, Persona_Id, Reserva_Id, Detalle, CuilCuit, RazonSocial, Valor, EstadoPago, Fecha,DVH)
values (@Factura_Id, @Vehiculo_Id, @Persona_Id, @Reserva_Id, @Detalle, @CuilCuit, @RazonSocial, @Valor, @EstadoPago, @Fecha,@DVH)

select 'success' as SQLResultType,'Invoice added succesfully!' as message
go

