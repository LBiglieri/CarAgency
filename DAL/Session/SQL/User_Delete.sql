CREATE or ALTER proc User_Delete	@Id uniqueidentifier as

delete Users
where Id = @Id

select 'success' as SQLResultType,'User deleted succesfully!' as message
go

