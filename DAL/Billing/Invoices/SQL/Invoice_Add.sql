CREATE or ALTER proc Invoice_Add	@Id uniqueidentifier, @Vehicle_Id uniqueidentifier, @Client_Id uniqueidentifier, @Reservation_Id uniqueidentifier, @Detail varchar(max),
									@CUIL_CUIT_Client varchar(max), @Razon_Social varchar(max), @Amount float, @Payment_Status bit, @Creation_Date datetime , @DVH char(64) as

insert into Invoice(Id, Vehicle_Id, Client_Id, Reservation_Id, Detail, CUIL_CUIT_Client, Razon_Social, Amount, Payment_Status, Creation_Date,DVH)
values (@Id, @Vehicle_Id, @Client_Id, @Reservation_Id, @Detail, @CUIL_CUIT_Client, @Razon_Social, @Amount, @Payment_Status, @Creation_Date,@DVH)

select 'success' as SQLResultType,'Invoice added succesfully!' as message
go

