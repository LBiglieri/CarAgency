CREATE or ALTER proc Quotations_Add	@Id uniqueidentifier, @Vehicle_Id uniqueidentifier, @Client_Id uniqueidentifier, @Price float, @Creation_Date datetime as

insert into Quotations(Id, Vehicle_Id, Client_Id, Price, Creation_Date) 
values (@Id, @Vehicle_Id, @Client_Id, @Price, @Creation_Date)

select 'success' as SQLResultType,'Quotations added succesfully!' as message
go
