CREATE or ALTER proc AcquisitionDocumentFiles_Add @Document_Id uniqueidentifier, @FileName varchar(255), @FileContent varbinary(max) as

insert into AcquisitionDocumentFiles(Document_Id, FileName, FileContent)
values (@Document_Id, @FileName, @FileContent)

select 'success' as SQLResultType,'Acquisition document file added succesfully!' as message
go
