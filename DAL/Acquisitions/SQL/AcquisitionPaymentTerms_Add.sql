CREATE or ALTER proc AcquisitionPaymentTerms_Add	@Acquisition_Id uniqueidentifier, @PaymentType_Id uniqueidentifier, @Amount float, @DVH char(64) as

insert into AcquisitionPaymentTerms(Acquisition_Id, PaymentType_Id, Amount, DVH)
values (@Acquisition_Id, @PaymentType_Id, @Amount, @DVH)

select 'success' as SQLResultType,'Payment term added succesfully!' as message
go
