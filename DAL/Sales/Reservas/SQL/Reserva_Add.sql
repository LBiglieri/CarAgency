CREATE or ALTER proc Reserva_Add	@Reserva_Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Precio float, @Fecha datetime, @FechaVencimiento datetime , @DVH char(64) as

insert into Reserva(Reserva_Id, Vehiculo_Id, Persona_Id, Precio, Fecha,FechaVencimiento,DVH)
values (@Reserva_Id, @Vehiculo_Id, @Persona_Id, @Precio, @Fecha,@FechaVencimiento,@DVH)

select 'success' as SQLResultType,'Quotations added succesfully!' as message
go
