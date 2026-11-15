CREATE or ALTER proc Pago_Delete	@Pago_Id uniqueidentifier as

delete Pago
where Pago_Id = @Pago_Id

select 'success' as SQLResultType,'Payment deleted succesfully!' as message
go

