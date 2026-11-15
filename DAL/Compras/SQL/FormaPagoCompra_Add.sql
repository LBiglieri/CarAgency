CREATE or ALTER proc FormaPagoCompra_Add	@Compra_Id uniqueidentifier, @TipoPago_Id uniqueidentifier, @Monto float, @DVH char(64) as

insert into FormaPagoCompra(Compra_Id, TipoPago_Id, Monto, DVH)
values (@Compra_Id, @TipoPago_Id, @Monto, @DVH)

select 'success' as SQLResultType,'Payment term added succesfully!' as message
go
