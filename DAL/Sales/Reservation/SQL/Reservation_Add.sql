CREATE or ALTER proc Reservation_Add	@Id uniqueidentifier, @Vehicle_Id uniqueidentifier, @Client_Id uniqueidentifier, @Price float, @Creation_Date datetime, @Expiration_Date datetime , @DVH char(64) as

insert into Reservation(Id, Vehicle_Id, Client_Id, Price, Creation_Date,Expiration_Date,DVH)
values (@Id, @Vehicle_Id, @Client_Id, @Price, @Creation_Date,@Expiration_Date,@DVH)

select 'success' as SQLResultType,'Quotations added succesfully!' as message
go
