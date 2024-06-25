CREATE or ALTER proc Paperwork_Delete	@Id uniqueidentifier as

delete Paperwork
where Id = @Id

select 'success' as SQLResultType,'Paperwork deleted succesfully!' as message
go

