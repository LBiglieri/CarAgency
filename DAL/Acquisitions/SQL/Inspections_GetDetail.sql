CREATE or ALTER proc Inspections_GetDetail @Id uniqueidentifier as

-- Datos del vehiculo que se inspecciona, con lo declarado en CUF07.
select	a.Id,
        a.Acquisition_Id,
        b.Number,
        g.License_Plate,
        g.Chassis_Number,
        g.Engine_Number,
        (cast(g.Year as varchar) + ' ' + e.Description + ' ' + c.Description + ' ' + d.Description + ' ' + f.Description) as Vehicle_Description,
        g.Doors,
        g.Declared_Kilometers
from	Inspections a
        join Acquisitions b on (b.Id = a.Acquisition_Id)
        join ProspectVehicles g on (g.Id = b.ProspectVehicle_Id)
        join Makes c on (c.Id = g.Make_Id)
		join Models d on (d.Id = g.Model_Id)
		join Versions f on (f.Id = g.Version_Id)
		join Colours e on (e.Id = g.Colour_Id)
where	a.Id = @Id

go
