CREATE or ALTER proc PaperworkFile_Delete	@Id uniqueidentifier as

delete PaperworkFiles
where Id = @Id

select 'success' as SQLResultType,'Paperwork file deleted succesfully!' as message
go

