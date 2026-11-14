CREATE or ALTER proc PagoCompra_Add	@PagoCompra_Id uniqueidentifier, @Compra_Id uniqueidentifier, @TipoPago_Id uniqueidentifier, @Cajero_Id uniqueidentifier,
											@Monto float, @Banco varchar(100), @CbuAlias varchar(50), @NumeroOperacion varchar(50),
											@NumeroCheque varchar(30), @FechaPago datetime, @FechaCreacion datetime, @DVH char(64) as

-- La FK contra AcquisitionPaymentTerms impide pagar con un medio que no se pacto en CUF10.
insert into PagoCompra(PagoCompra_Id, Compra_Id, TipoPago_Id, Cajero_Id, Monto, Banco, CbuAlias, NumeroOperacion, NumeroCheque, FechaPago, FechaCreacion, DVH)
values (@PagoCompra_Id, @Compra_Id, @TipoPago_Id, @Cajero_Id, @Monto, @Banco, @CbuAlias, @NumeroOperacion, @NumeroCheque, @FechaPago, @FechaCreacion, @DVH)

select 'success' as SQLResultType,'Payment added succesfully!' as message
go
