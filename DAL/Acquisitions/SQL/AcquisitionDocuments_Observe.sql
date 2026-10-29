CREATE or ALTER proc AcquisitionDocuments_Observe	@Id uniqueidentifier, @Acquisition_Id uniqueidentifier, @DocumentType_Id uniqueidentifier, @Status int,
												@Issue_Date datetime, @Observation varchar(max), @User_Id uniqueidentifier, @Uploaded_Date datetime, @DVH char(64) as

-- CUF10 11.1: fila completa, el DVH se recalcula con todas las columnas aunque solo cambien estado y comentario.
update AcquisitionDocuments
set DVH = @DVH,
	Acquisition_Id = @Acquisition_Id,
	DocumentType_Id = @DocumentType_Id,
	Status = @Status,
	Issue_Date = @Issue_Date,
	Observation = @Observation,
	User_Id = @User_Id,
	Uploaded_Date = @Uploaded_Date
where Id = @Id

select 'success' as SQLResultType,'Acquisition document observed succesfully!' as message
go
