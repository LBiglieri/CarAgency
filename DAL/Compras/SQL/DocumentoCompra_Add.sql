CREATE or ALTER proc DocumentoCompra_Add	@DocumentoCompra_Id uniqueidentifier, @Compra_Id uniqueidentifier, @TipoDocumentoCompra_Id uniqueidentifier, @Estado int,
											@FechaEmision datetime, @Observacion varchar(max), @UsuarioCarga_Id uniqueidentifier, @FechaCarga datetime, @DVH char(64) as

insert into DocumentoCompra(DocumentoCompra_Id, Compra_Id, TipoDocumentoCompra_Id, Estado, FechaEmision, Observacion, UsuarioCarga_Id, FechaCarga, DVH)
values (@DocumentoCompra_Id, @Compra_Id, @TipoDocumentoCompra_Id, @Estado, @FechaEmision, @Observacion, @UsuarioCarga_Id, @FechaCarga, @DVH)

select 'success' as SQLResultType,'Acquisition document added succesfully!' as message
go
