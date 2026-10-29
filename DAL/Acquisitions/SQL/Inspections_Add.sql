CREATE or ALTER proc Inspections_Add	@Id uniqueidentifier, @Acquisition_Id uniqueidentifier, @Mechanic_Id uniqueidentifier, @Scheduled_Date datetime,
									@Performed_Date datetime, @Verified_Kilometers int, @Result int, @Observations varchar(max), @DVH char(64) as

insert into Inspections(Id, Acquisition_Id, Mechanic_Id, Scheduled_Date, Performed_Date, Verified_Kilometers, Result, Observations, DVH)
values (@Id, @Acquisition_Id, @Mechanic_Id, @Scheduled_Date, @Performed_Date, @Verified_Kilometers, @Result, @Observations, @DVH)

select 'success' as SQLResultType,'Inspection added succesfully!' as message
go
