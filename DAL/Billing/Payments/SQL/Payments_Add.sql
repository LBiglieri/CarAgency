CREATE or ALTER proc Payments_Add	@Id uniqueidentifier, @Invoice_Id uniqueidentifier, @PaymentType_Id uniqueidentifier, @Amount float, @Detail varchar(255) , @DVH char(64) as

insert into Payments(Id, Invoice_Id, PaymentType_Id, Amount, Detail,DVH)
values (@Id, @Invoice_Id, @PaymentType_Id, @Amount, @Detail,@DVH)

select 'success' as SQLResultType,'Payment added succesfully!' as message
go

