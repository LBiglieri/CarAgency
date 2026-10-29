CREATE or ALTER proc Acquisitions_GetPendingManagement @Filter varchar(20) as

-- Compras en gestion (CUF10): Tasada (3), Compra formalizada (4), Documentacion completa (5), Pagada (6) y Transferida (7).
-- El filtro busca por DNI exacto o por el comienzo de la matricula; vacio trae todas.
select	a.Id,
        a.Number,
        b.Id as Inspection_Id,
        a.Client_Id,
        h.Dni,
        g.License_Plate,
        (cast(g.Year as varchar) + ' ' + e.Description + ' ' + c.Description + ' ' + d.Description + ' ' + f.Description) as Vehicle_Description,
        a.Status
from	Acquisitions a
        join Inspections b on (b.Acquisition_Id = a.Id)
        join Clients h on (h.Id = a.Client_Id)
        join ProspectVehicles g on (g.Id = a.ProspectVehicle_Id)
        join Makes c on (c.Id = g.Make_Id)
		join Models d on (d.Id = g.Model_Id)
		join Versions f on (f.Id = g.Version_Id)
		join Colours e on (e.Id = g.Colour_Id)
where	a.Status between 3 and 7
        and (isnull(@Filter, '') = ''
            or h.Dni = try_cast(@Filter as int)
            or g.License_Plate like @Filter + '%')
order by a.Number asc

go
