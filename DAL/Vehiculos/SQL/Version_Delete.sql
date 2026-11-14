CREATE or ALTER proc Version_Delete	@Version_Id uniqueidentifier as

delete Version
where Version_Id = @Version_Id

select 'success' as SQLResultType,'Version deleted succesfully!' as message
go

