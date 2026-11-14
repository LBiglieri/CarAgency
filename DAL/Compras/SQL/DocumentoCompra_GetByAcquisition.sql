CREATE or ALTER proc DocumentoCompra_GetByAcquisition @Compra_Id uniqueidentifier as

-- Documentos cargados de las dos etapas. Un tipo sin fila es un documento Pendiente.
select	a.DocumentoCompra_Id,
        a.Compra_Id,
        a.TipoDocumentoCompra_Id,
        a.Estado,
        a.FechaEmision,
        a.Observacion,
        a.UsuarioCarga_Id,
        a.FechaCarga,
        b.Descripcion,
        b.Etapa,
        c.NombreArchivo
from	DocumentoCompra a
        join TipoDocumentoCompra b on (b.TipoDocumentoCompra_Id = a.TipoDocumentoCompra_Id)
        left join ArchivoDocumentoCompra c on (c.DocumentoCompra_Id = a.DocumentoCompra_Id)
where	a.Compra_Id = @Compra_Id
order by b.Orden asc

go
