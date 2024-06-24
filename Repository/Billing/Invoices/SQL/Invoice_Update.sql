CREATE or ALTER proc Invoice_Update	@Id uniqueidentifier, @Vehicle_Id uniqueidentifier, @Client_Id uniqueidentifier, @Reservation_Id uniqueidentifier, @Detail varchar(max), 
										@CUIL_CUIT_Client varchar(max), @Razon_Social varchar(max), @Amount float, @Payment_Status bit, @Creation_Date datetime as

update Invoice
set Vehicle_Id = @Vehicle_Id,
	Client_Id = @Client_Id,
	Reservation_Id = @Reservation_Id,
	Detail = @Detail,
	CUIL_CUIT_Client = @CUIL_CUIT_Client,
	Razon_Social = @Razon_Social,
	Amount = @Amount,
	Payment_Status = @Payment_Status,
	Creation_Date = @Creation_Date
where Id = @Id

select 'success' as SQLResultType,'Invoice updated succesfully!' as message
go

