CREATE or ALTER proc AcquisitionPaymentTerms_DeleteByAcquisition @Acquisition_Id uniqueidentifier as

-- La forma de pago se reemplaza completa mientras el contrato no este firmado.
delete from AcquisitionPaymentTerms
where Acquisition_Id = @Acquisition_Id

select 'success' as SQLResultType,'Payment terms deleted succesfully!' as message
go
