CREATE or ALTER proc Inspections_GetById @Id uniqueidentifier as

-- Resultado de la inspeccion registrada en CUF08.
select	a.Id,
        a.Acquisition_Id,
        a.Mechanic_Id,
        a.Scheduled_Date,
        a.Performed_Date,
        a.Verified_Kilometers,
        a.Result,
        a.Observations
from	Inspections a
where	a.Id = @Id

go
