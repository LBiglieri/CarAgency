CREATE or ALTER proc PaperworkFile_Add	@Id uniqueidentifier, @Paperwork_Id uniqueidentifier, @FileName varchar(255), @FileContent varbinary(max), @UploadedDate datetime as

insert into PaperworkFiles(Id, Paperwork_Id, FileName, FileContent, UploadedDate) 
values (@Id, @Paperwork_Id, @FileName, @FileContent, @UploadedDate)

select 'success' as SQLResultType,'Paperwork file added succesfully!' as message
go

