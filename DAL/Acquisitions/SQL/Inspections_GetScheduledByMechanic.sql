CREATE or ALTER proc Inspections_GetScheduledByMechanic @Mechanic_Id uniqueidentifier as

-- Inspecciones pendientes del mecanico: adquisiciones en estado Inspeccion programada (1).
select	a.Id,
        a.Acquisition_Id,
        a.Mechanic_Id,
        a.Scheduled_Date,
        b.Number,
        g.License_Plate,
        (cast(g.Year as varchar) + ' ' + e.Description + ' ' + c.Description + ' ' + d.Description + ' ' + f.Description) as Vehicle_Description
from	Inspections a
        join Acquisitions b on (b.Id = a.Acquisition_Id)
        join ProspectVehicles g on (g.Id = b.ProspectVehicle_Id)
        join Makes c on (c.Id = g.Make_Id)
		join Models d on (d.Id = g.Model_Id)
		join Versions f on (f.Id = g.Version_Id)
		join Colours e on (e.Id = g.Colour_Id)
where	a.Mechanic_Id = @Mechanic_Id
        and b.Status = 1
order by a.Scheduled_Date asc

go
