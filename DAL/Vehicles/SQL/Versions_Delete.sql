CREATE or ALTER proc Versions_Delete	@Id uniqueidentifier as

delete Versions
where Id = @Id

select 'success' as SQLResultType,'Version deleted succesfully!' as message
go

