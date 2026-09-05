CREATE or ALTER proc Models_Delete	@Id uniqueidentifier as

delete Models
where Id = @Id

select 'success' as SQLResultType,'Model deleted succesfully!' as message
go

