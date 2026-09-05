CREATE or ALTER proc Makes_Delete	@Id uniqueidentifier as

delete Makes
where Id = @Id

select 'success' as SQLResultType,'Make deleted succesfully!' as message
go
