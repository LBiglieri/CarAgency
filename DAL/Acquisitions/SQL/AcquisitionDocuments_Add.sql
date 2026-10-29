CREATE or ALTER proc AcquisitionDocuments_Add	@Id uniqueidentifier, @Acquisition_Id uniqueidentifier, @DocumentType_Id uniqueidentifier, @Status int,
											@Issue_Date datetime, @Observation varchar(max), @User_Id uniqueidentifier, @Uploaded_Date datetime, @DVH char(64) as

insert into AcquisitionDocuments(Id, Acquisition_Id, DocumentType_Id, Status, Issue_Date, Observation, User_Id, Uploaded_Date, DVH)
values (@Id, @Acquisition_Id, @DocumentType_Id, @Status, @Issue_Date, @Observation, @User_Id, @Uploaded_Date, @DVH)

select 'success' as SQLResultType,'Acquisition document added succesfully!' as message
go
