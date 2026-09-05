CREATE or ALTER proc Vehicles_GetById @Id uniqueidentifier as

select	a.Id,
        a.License_Plate,
        a.Make_Id, b.Description as Make_Description,
        a.Model_Id, c.Description as Model_Description, 
        a.Version_Id, d.Description as Version_Description, 
        a.Colour_Id, e.Description as Colour_Description, 
        a.Price,
        a.Opcionals, 
        a.Observations,
        a.Doors, 
        a.Year, 
        a.Kilometers, 
        a.ImageLink
from	Vehicles a
		join Makes b on (b.Id = a.Make_Id)
		join Models c on (c.Id = a.Model_Id)
		join Versions d on (d.Id = a.Version_Id)
		join Colours e on (e.Id = a.Colour_Id)
where	a.Id = @Id

go


