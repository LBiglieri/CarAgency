CREATE or ALTER proc AcquisitionPayments_Add	@Id uniqueidentifier, @Acquisition_Id uniqueidentifier, @PaymentType_Id uniqueidentifier, @Cashier_Id uniqueidentifier,
											@Amount float, @Bank varchar(100), @Cbu_Alias varchar(50), @Operation_Number varchar(50),
											@Check_Number varchar(30), @Payment_Date datetime, @Creation_Date datetime, @DVH char(64) as

-- La FK contra AcquisitionPaymentTerms impide pagar con un medio que no se pacto en CUF10.
insert into AcquisitionPayments(Id, Acquisition_Id, PaymentType_Id, Cashier_Id, Amount, Bank, Cbu_Alias, Operation_Number, Check_Number, Payment_Date, Creation_Date, DVH)
values (@Id, @Acquisition_Id, @PaymentType_Id, @Cashier_Id, @Amount, @Bank, @Cbu_Alias, @Operation_Number, @Check_Number, @Payment_Date, @Creation_Date, @DVH)

select 'success' as SQLResultType,'Payment added succesfully!' as message
go
