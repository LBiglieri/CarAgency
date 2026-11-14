CREATE or ALTER proc DocumentoCompra_Delete @DocumentoCompra_Id uniqueidentifier as

-- El archivo escaneado se borra en cascada (AcquisitionDocumentFiles).
delete from DocumentoCompra
where DocumentoCompra_Id = @DocumentoCompra_Id

select 'success' as SQLResultType,'Acquisition document deleted succesfully!' as message
go
