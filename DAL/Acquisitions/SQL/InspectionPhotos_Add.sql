CREATE or ALTER proc InspectionPhotos_Add	@Id uniqueidentifier, @Inspection_Id uniqueidentifier, @FileName varchar(255), @FileContent varbinary(max), @UploadedDate datetime as

insert into InspectionPhotos(Id, Inspection_Id, FileName, FileContent, UploadedDate)
values (@Id, @Inspection_Id, @FileName, @FileContent, @UploadedDate)

select 'success' as SQLResultType,'Inspection photo added succesfully!' as message
go
