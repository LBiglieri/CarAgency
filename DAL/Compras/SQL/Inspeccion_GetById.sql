CREATE or ALTER proc Inspeccion_GetById @Inspeccion_Id uniqueidentifier as

-- Resultado de la inspeccion registrada en CUF08.
select	a.Inspeccion_Id,
        a.Compra_Id,
        a.Mecanico_Id,
        a.FechaProgramada,
        a.FechaRealizada,
        a.KilometrajeVerificado,
        a.Resultado,
        a.Observaciones
from	Inspeccion a
where	a.Inspeccion_Id = @Inspeccion_Id

go
