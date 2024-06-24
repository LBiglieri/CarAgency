CREATE or ALTER proc Reservation_Add	@Id uniqueidentifier, @Vehicle_Id uniqueidentifier, @Client_Id uniqueidentifier, @Price float, @Creation_Date datetime, @Expiration_Date datetime as

insert into Reservation(Id, Vehicle_Id, Client_Id, Price, Creation_Date,Expiration_Date) 
values (@Id, @Vehicle_Id, @Client_Id, @Price, @Creation_Date,@Expiration_Date)

select 'success' as SQLResultType,'Quotations added succesfully!' as message
go
