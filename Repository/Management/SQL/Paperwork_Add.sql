CREATE or ALTER proc Paperwork_Add	@Id uniqueidentifier, @Vehicle_Id uniqueidentifier, @Client_Id uniqueidentifier, @Invoice_Id uniqueidentifier, @Paperwork_Precharge_Code varchar(50), 
									@Transfer_Date datetime, @Observations varchar(max), @IsFinished bit as

insert into Paperwork(Id, Vehicle_Id, Client_Id, Invoice_Id, Paperwork_Precharge_Code, Transfer_Date, Observations, IsFinished) 
values (@Id, @Vehicle_Id, @Client_Id, @Invoice_Id, @Paperwork_Precharge_Code, @Transfer_Date, @Observations, @IsFinished)

select 'success' as SQLResultType,'Paperwork added succesfully!' as message
go

