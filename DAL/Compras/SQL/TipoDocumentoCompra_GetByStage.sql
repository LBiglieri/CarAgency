CREATE or ALTER proc TipoDocumentoCompra_GetByStage @Etapa int as

-- Documentos obligatorios de cada etapa de CUF10: 1 Documentar, 2 Transferir.
select	a.TipoDocumentoCompra_Id,
        a.Descripcion,
        a.Etapa,
        a.DiasVigencia,
        a.Orden
from	TipoDocumentoCompra a
where	a.Etapa = @Etapa
order by a.Orden asc

go
