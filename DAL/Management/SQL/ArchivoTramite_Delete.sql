CREATE or ALTER proc ArchivoTramite_Delete	@ArchivoTramite_Id uniqueidentifier as

delete ArchivoTramite
where ArchivoTramite_Id = @ArchivoTramite_Id

select 'success' as SQLResultType,'Paperwork file deleted succesfully!' as message
go

