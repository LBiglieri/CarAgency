CREATE or ALTER proc Invoice_Delete	@Id uniqueidentifier as

delete Invoice
where Id = @Id

select 'success' as SQLResultType,'Invoice deleted succesfully!' as message
go

