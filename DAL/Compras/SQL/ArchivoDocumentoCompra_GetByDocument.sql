CREATE or ALTER proc ArchivoDocumentoCompra_GetByDocument @DocumentoCompra_Id uniqueidentifier as

select	a.DocumentoCompra_Id,
        a.NombreArchivo,
        a.Contenido
from	ArchivoDocumentoCompra a
where	a.DocumentoCompra_Id = @DocumentoCompra_Id

go
