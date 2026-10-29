CREATE or ALTER proc Appraisals_Add	@Id uniqueidentifier, @Acquisition_Id uniqueidentifier, @Inspection_Id uniqueidentifier, @User_Id uniqueidentifier,
									@Amount float, @Justification varchar(max), @Creation_Date datetime, @Expiration_Date datetime, @DVH char(64) as

insert into Appraisals(Id, Acquisition_Id, Inspection_Id, User_Id, Amount, Justification, Creation_Date, Expiration_Date, DVH)
values (@Id, @Acquisition_Id, @Inspection_Id, @User_Id, @Amount, @Justification, @Creation_Date, @Expiration_Date, @DVH)

select 'success' as SQLResultType,'Appraisal added succesfully!' as message
go
