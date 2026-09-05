CREATE or ALTER proc Payments_Delete	@Id uniqueidentifier as

delete Payments
where Id = @Id

select 'success' as SQLResultType,'Payment deleted succesfully!' as message
go

