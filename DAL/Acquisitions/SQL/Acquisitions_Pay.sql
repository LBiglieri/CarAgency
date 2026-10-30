CREATE or ALTER proc Acquisitions_Pay	@Id uniqueidentifier, @Number int, @Acquisition_Id uniqueidentifier, @Issue_Date datetime, @Client_Name varchar(255),
									@Client_CUIL_CUIT varchar(13), @Detail varchar(max), @Net_Amount float, @Tax_Amount float, @Total_Amount float,
									@DVH char(64), @Status int, @Acquisition_DVH char(64) as

-- CUF11 pasos 7 y 9: el comprobante y el pase a Pagada van juntos o no se graba nada. Un comprobante con la
-- compra sin pagar no se podria volver a emitir, y una compra pagada sin comprobante no se podria reimprimir.
-- Los pagos ya estan grabados: si se cae antes de este paso, la compra queda con saldo cero y se reintenta.
set xact_abort on
begin try
	begin tran

	if not exists (select * from Acquisitions where Id = @Acquisition_Id and Status = 5)
		throw 51012, 'La adquisicion ya no esta en estado Documentacion completa.', 1;

	if exists (select * from AcquisitionPaymentTerms t
				where t.Acquisition_Id = @Acquisition_Id
				and round(t.Amount - isnull((select sum(x.Amount) from AcquisitionPayments x
										where x.Acquisition_Id = t.Acquisition_Id and x.PaymentType_Id = t.PaymentType_Id), 0), 2) <> 0)
		throw 51013, 'La compra todavia tiene saldo pendiente.', 1;

	insert into PurchaseReceipts(Id, Number, Acquisition_Id, Issue_Date, Client_Name, Client_CUIL_CUIT, Detail, Net_Amount, Tax_Amount, Total_Amount, DVH)
	values (@Id, @Number, @Acquisition_Id, @Issue_Date, @Client_Name, @Client_CUIL_CUIT, @Detail, @Net_Amount, @Tax_Amount, @Total_Amount, @DVH)

	update Acquisitions
	set DVH = @Acquisition_DVH,
		Status = @Status
	where Id = @Acquisition_Id

	commit tran
end try
begin catch
	if @@trancount > 0 rollback tran;
	throw;
end catch

select 'success' as SQLResultType,'Acquisition paid succesfully!' as message
go
