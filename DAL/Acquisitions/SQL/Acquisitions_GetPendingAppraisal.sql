CREATE or ALTER proc Acquisitions_GetPendingAppraisal @Filter varchar(20), @Now datetime as

-- Adquisiciones a tasar: Inspeccionado (2), o Tasada (3) con la ultima tasacion vencida.
-- El filtro busca por DNI exacto o por el comienzo de la matricula; vacio trae todas.
select	a.Id,
        a.Number,
        b.Id as Inspection_Id,
        a.Client_Id,
        h.Dni,
        g.License_Plate,
        (cast(g.Year as varchar) + ' ' + e.Description + ' ' + c.Description + ' ' + d.Description + ' ' + f.Description) as Vehicle_Description,
        b.Performed_Date,
        (select count(*) from Appraisals x where x.Acquisition_Id = a.Id) as Previous_Appraisals
from	Acquisitions a
        join Inspections b on (b.Acquisition_Id = a.Id)
        join Clients h on (h.Id = a.Client_Id)
        join ProspectVehicles g on (g.Id = a.ProspectVehicle_Id)
        join Makes c on (c.Id = g.Make_Id)
		join Models d on (d.Id = g.Model_Id)
		join Versions f on (f.Id = g.Version_Id)
		join Colours e on (e.Id = g.Colour_Id)
where	(a.Status = 2
            or (a.Status = 3 and (select top 1 x.Expiration_Date from Appraisals x where x.Acquisition_Id = a.Id order by x.Creation_Date desc) < @Now))
        and (isnull(@Filter, '') = ''
            or h.Dni = try_cast(@Filter as int)
            or g.License_Plate like @Filter + '%')
order by b.Performed_Date asc

go
