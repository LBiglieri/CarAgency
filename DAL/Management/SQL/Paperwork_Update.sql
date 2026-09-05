CREATE or ALTER proc Paperwork_Update	@Id uniqueidentifier, @Vehicle_Id uniqueidentifier, @Client_Id uniqueidentifier, @Invoice_Id uniqueidentifier, @Paperwork_Precharge_Code varchar(50), 
									@Transfer_Date datetime, @Observations varchar(max), @IsFinished bit as

update Paperwork
set Vehicle_Id = @Vehicle_Id,
	Client_Id = @Client_Id,
	Invoice_Id = @Invoice_Id,
	Paperwork_Precharge_Code = @Paperwork_Precharge_Code,
	Transfer_Date = @Transfer_Date,
	Observations = @Observations,
	IsFinished = @IsFinished
where Id = @Id

select 'success' as SQLResultType,'Paperwork updated succesfully!' as message
go

