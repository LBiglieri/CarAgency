CREATE or ALTER proc DocumentoCompra_Observe	@DocumentoCompra_Id uniqueidentifier, @Compra_Id uniqueidentifier, @TipoDocumentoCompra_Id uniqueidentifier, @Estado int,
												@FechaEmision datetime, @Observacion varchar(max), @UsuarioCarga_Id uniqueidentifier, @FechaCarga datetime, @DVH char(64) as

-- CUF10 11.1: fila completa, el DVH se recalcula con todas las columnas aunque solo cambien estado y comentario.
update DocumentoCompra
set DVH = @DVH,
	Compra_Id = @Compra_Id,
	TipoDocumentoCompra_Id = @TipoDocumentoCompra_Id,
	Estado = @Estado,
	FechaEmision = @FechaEmision,
	Observacion = @Observacion,
	UsuarioCarga_Id = @UsuarioCarga_Id,
	FechaCarga = @FechaCarga
where DocumentoCompra_Id = @DocumentoCompra_Id

select 'success' as SQLResultType,'Acquisition document observed succesfully!' as message
go
