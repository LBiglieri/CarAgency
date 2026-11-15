CREATE or ALTER proc FormaPagoCompra_DeleteByAcquisition @Compra_Id uniqueidentifier as

-- La forma de pago se reemplaza completa mientras el contrato no este firmado.
delete from FormaPagoCompra
where Compra_Id = @Compra_Id

select 'success' as SQLResultType,'Payment terms deleted succesfully!' as message
go
