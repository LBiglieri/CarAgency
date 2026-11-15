CREATE or ALTER proc Cotizacion_Add	@Cotizacion_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Precio float, @Fecha datetime , @DVH char(64) as

insert into Cotizacion(Cotizacion_Id, Vehiculo_Id, Persona_Id, Precio, Fecha,DVH)
values (@Cotizacion_Id, @Vehiculo_Id, @Persona_Id, @Precio, @Fecha,@DVH)

select 'success' as SQLResultType,'Quotations added succesfully!' as message
go
