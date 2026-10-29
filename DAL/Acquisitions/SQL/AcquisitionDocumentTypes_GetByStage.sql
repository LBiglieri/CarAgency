CREATE or ALTER proc AcquisitionDocumentTypes_GetByStage @Stage int as

-- Documentos obligatorios de cada etapa de CUF10: 1 Documentar, 2 Transferir.
select	a.Id,
        a.Description,
        a.Stage,
        a.Validity_Days,
        a.Display_Order
from	AcquisitionDocumentTypes a
where	a.Stage = @Stage
order by a.Display_Order asc

go
