CREATE or ALTER proc Compra_Pay	@ComprobanteCompra_Id uniqueidentifier, @Numero int, @Compra_Id uniqueidentifier, @Fecha datetime, @NombrePersona varchar(255),
									@CuilCuit varchar(13), @Detalle varchar(max), @MontoNeto float, @MontoImpuesto float, @MontoTotal float,
									@DVH char(64), @Estado int, @Acquisition_DVH char(64) as

-- CUF11 pasos 7 y 9: el comprobante y el pase a Pagada van juntos o no se graba nada. Un comprobante con la
-- compra sin pagar no se podria volver a emitir, y una compra pagada sin comprobante no se podria reimprimir.
-- Los pagos ya estan grabados: si se cae antes de este paso, la compra queda con saldo cero y se reintenta.
set xact_abort on
begin try
	begin tran

	if not exists (select * from Compra where Compra_Id = @Compra_Id and Estado = 5)
		throw 51012, 'La adquisicion ya no esta en estado Documentacion completa.', 1;

	if exists (select * from FormaPagoCompra t
				where t.Compra_Id = @Compra_Id
				and round(t.Monto - isnull((select sum(x.Monto) from PagoCompra x
										where x.Compra_Id = t.Compra_Id and x.TipoPago_Id = t.TipoPago_Id), 0), 2) <> 0)
		throw 51013, 'La compra todavia tiene saldo pendiente.', 1;

	insert into ComprobanteCompra(ComprobanteCompra_Id, Numero, Compra_Id, Fecha, NombrePersona, CuilCuit, Detalle, MontoNeto, MontoImpuesto, MontoTotal, DVH)
	values (@ComprobanteCompra_Id, @Numero, @Compra_Id, @Fecha, @NombrePersona, @CuilCuit, @Detalle, @MontoNeto, @MontoImpuesto, @MontoTotal, @DVH)

	update Compra
	set DVH = @Acquisition_DVH,
		Estado = @Estado
	where Compra_Id = @Compra_Id

	commit tran
end try
begin catch
	if @@trancount > 0 rollback tran;
	throw;
end catch

select 'success' as SQLResultType,'Acquisition paid succesfully!' as message
go
