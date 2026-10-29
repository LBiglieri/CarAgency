CREATE or ALTER proc Inspections_IsMechanicBusy @Mechanic_Id uniqueidentifier, @Scheduled_Date datetime, @Slot_Minutes int as

-- Solo ocupan la agenda las inspecciones de adquisiciones en estado Inspeccion programada (1).
select	count(*) as Inspections
from	Inspections a
        join Acquisitions b on (b.Id = a.Acquisition_Id)
where	a.Mechanic_Id = @Mechanic_Id
        and b.Status = 1
        and a.Scheduled_Date > dateadd(minute, -@Slot_Minutes, @Scheduled_Date)
        and a.Scheduled_Date < dateadd(minute, @Slot_Minutes, @Scheduled_Date)

go
