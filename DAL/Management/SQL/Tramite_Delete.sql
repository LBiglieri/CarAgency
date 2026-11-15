CREATE or ALTER proc Tramite_Delete	@Tramite_Id uniqueidentifier as

delete Tramite
where Tramite_Id = @Tramite_Id

select 'success' as SQLResultType,'Paperwork deleted succesfully!' as message
go

