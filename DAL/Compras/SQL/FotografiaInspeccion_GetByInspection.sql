CREATE or ALTER proc FotografiaInspeccion_GetByInspection @Inspeccion_Id uniqueidentifier as

select	a.FotografiaInspeccion_Id,
        a.Inspeccion_Id,
        a.NombreArchivo,
        a.Contenido,
        a.FechaCarga
from	FotografiaInspeccion a
where	a.Inspeccion_Id = @Inspeccion_Id
order by a.FechaCarga asc

go
