CREATE or ALTER proc ArchivoDocumentoCompra_Add @DocumentoCompra_Id uniqueidentifier, @NombreArchivo varchar(255), @Contenido varbinary(max) as

insert into ArchivoDocumentoCompra(DocumentoCompra_Id, NombreArchivo, Contenido)
values (@DocumentoCompra_Id, @NombreArchivo, @Contenido)

select 'success' as SQLResultType,'Acquisition document file added succesfully!' as message
go
