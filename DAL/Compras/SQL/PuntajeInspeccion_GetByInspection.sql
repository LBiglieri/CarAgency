CREATE or ALTER proc PuntajeInspeccion_GetByInspection @Inspeccion_Id uniqueidentifier as

select	a.Inspeccion_Id,
        a.ItemInspeccion_Id,
        b.Descripcion,
        b.Orden,
        a.Puntaje,
        a.Observaciones
from	PuntajeInspeccion a
        join ItemInspeccion b on (b.ItemInspeccion_Id = a.ItemInspeccion_Id)
where	a.Inspeccion_Id = @Inspeccion_Id
order by b.Orden asc

go
