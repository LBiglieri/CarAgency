CREATE or ALTER proc Quotations_GetAllActiveByClient @Client_Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id, (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Client_Id, (g.Name + ' ' + g.Surname + ' DNI: ' + cast(g.Dni as varchar)) as Client_Description, 
        a.Price,
        a.Creation_Date
from	Quotations a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
        join Clients g on (g.Id = a.Client_Id)
where	a.Client_Id = @Client_Id
        and cast(a.Creation_Date as date) = cast(GETDATE() as date)
        and not exists 
        (
            select * 
            from Reservation r 
            left join Invoice i on (i.Reservation_Id = r.Id and i.Payment_Status = 1)
            where r.Vehicle_Id = a.Vehicle_Id 
            and 
            (
                i.Reservation_Id is not null
                or 
                (cast(getdate() as date) >= cast(r.Creation_Date as date)) and (cast(getdate() as date) <= cast(r.Expiration_Date as date))
            )
        )
go

