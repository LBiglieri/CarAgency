CREATE or ALTER proc Factura_Delete	@Factura_Id uniqueidentifier as

delete Factura
where Factura_Id = @Factura_Id

select 'success' as SQLResultType,'Invoice deleted succesfully!' as message
go

