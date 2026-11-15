CREATE or ALTER proc Pago_Add	@Pago_Id uniqueidentifier, @Factura_Id uniqueidentifier, @TipoPago_Id uniqueidentifier, @Monto float, @Detalle varchar(255) , @DVH char(64) as

insert into Pago(Pago_Id, Factura_Id, TipoPago_Id, Monto, Detalle,DVH)
values (@Pago_Id, @Factura_Id, @TipoPago_Id, @Monto, @Detalle,@DVH)

select 'success' as SQLResultType,'Payment added succesfully!' as message
go

