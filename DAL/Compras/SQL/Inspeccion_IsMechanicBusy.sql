CREATE or ALTER proc Inspeccion_IsMechanicBusy @Mecanico_Id uniqueidentifier, @FechaProgramada datetime, @Slot_Minutes int as

-- Solo ocupan la agenda las inspecciones de adquisiciones en estado Inspeccion programada (1).
select	count(*) as Inspecciones
from	Inspeccion a
        join Compra b on (b.Compra_Id = a.Compra_Id)
where	a.Mecanico_Id = @Mecanico_Id
        and b.Estado = 1
        and a.FechaProgramada > dateadd(minute, -@Slot_Minutes, @FechaProgramada)
        and a.FechaProgramada < dateadd(minute, @Slot_Minutes, @FechaProgramada)

go
