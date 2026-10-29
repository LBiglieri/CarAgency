CREATE or ALTER proc Acquisitions_GetDuplicates @License_Plate varchar(10), @Chassis_Number varchar(17), @Engine_Number varchar(20) as

-- Inventario (Number = 0): vehiculos sin factura pagada. Chasis y motor solo se conocen
-- de los que entraron por una adquisicion; el resto se compara por patente.
select	0 as Number,
        a.License_Plate,
        isnull(g.Chassis_Number, '') as Chassis_Number,
        isnull(g.Engine_Number, '') as Engine_Number,
        (cast(a.Year as varchar) + ' ' + e.Description + ' ' + b.Description + ' ' + c.Description + ' ' + d.Description + ' ' + a.License_Plate) as Vehicle_Description
from	Vehicles a
        join Makes b on (b.Id = a.Make_Id)
		join Models c on (c.Id = a.Model_Id)
		join Versions d on (d.Id = a.Version_Id)
		join Colours e on (e.Id = a.Colour_Id)
        left join Acquisitions f on (f.Vehicle_Id = a.Id)
        left join ProspectVehicles g on (g.Id = f.ProspectVehicle_Id)
where	(a.License_Plate = @License_Plate or g.Chassis_Number = @Chassis_Number or g.Engine_Number = @Engine_Number)
        and not exists (select * from Invoice i where i.Vehicle_Id = a.Id and i.Payment_Status = 1)

union all

-- Adquisiciones abiertas: de Inspeccion programada (1) a Transferida (7).
select	a.Number,
        g.License_Plate,
        g.Chassis_Number,
        g.Engine_Number,
        (cast(g.Year as varchar) + ' ' + e.Description + ' ' + b.Description + ' ' + c.Description + ' ' + d.Description + ' ' + g.License_Plate) as Vehicle_Description
from	Acquisitions a
        join ProspectVehicles g on (g.Id = a.ProspectVehicle_Id)
        join Makes b on (b.Id = g.Make_Id)
		join Models c on (c.Id = g.Model_Id)
		join Versions d on (d.Id = g.Version_Id)
		join Colours e on (e.Id = g.Colour_Id)
where	a.Status between 1 and 7
        and (g.License_Plate = @License_Plate or g.Chassis_Number = @Chassis_Number or g.Engine_Number = @Engine_Number)

go
