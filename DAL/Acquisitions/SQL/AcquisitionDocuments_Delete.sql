CREATE or ALTER proc AcquisitionDocuments_Delete @Id uniqueidentifier as

-- El archivo escaneado se borra en cascada (AcquisitionDocumentFiles).
delete from AcquisitionDocuments
where Id = @Id

select 'success' as SQLResultType,'Acquisition document deleted succesfully!' as message
go
