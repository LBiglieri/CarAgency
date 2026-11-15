CREATE or ALTER proc Usuario_Delete	@Usuario_Id uniqueidentifier as

delete Usuario
where Usuario_Id = @Usuario_Id

select 'success' as SQLResultType,'User deleted succesfully!' as message
go

