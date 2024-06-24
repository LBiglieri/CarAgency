CREATE or ALTER proc Reservation_GetById @Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id, (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Client_Id, (g.Name + ' ' + g.Surname + ' DNI: ' + cast(g.Dni as varchar)) as Client_Description, 
        a.Price,
        a.Creation_Date,
        a.Expiration_Date
from	Reservation a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
        join Clients g on (g.Id = a.Client_Id)
where	a.Id = @Id

go