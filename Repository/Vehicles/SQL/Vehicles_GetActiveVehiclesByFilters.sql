CREATE or ALTER proc Vehicles_GetActiveVehiclesByFilters @Make_Id uniqueidentifier, @Model_Id uniqueidentifier , @Version_Id uniqueidentifier , @Colour_Id uniqueidentifier , @Price_From float, 
                                                         @Price_To float, @Year_From int, @Year_To int, @Doors_From int, @Doors_To int, @Kilometers_From int, @Kilometers_To int as

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
where	(a.Make_Id = @Make_Id or (@Make_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Model_Id = @Model_Id or (@Model_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Version_Id = @Version_Id or (@Version_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Colour_Id = @Colour_Id or (@Colour_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Price >= @Price_From or (@Price_From = 0 or @Price_From is null))
        and (a.Price <= @Price_To or (@Price_To = 0 or @Price_To is null))
        and (a.Kilometers >= @Kilometers_From or (@Kilometers_From = 0 or @Kilometers_From is null))
        and (a.Kilometers <= @Kilometers_To or (@Kilometers_To = 0 or @Kilometers_To is null))
        and (a.Doors >= @Doors_From or (@Doors_From = 0 or @Doors_From is null))
        and (a.Doors <= @Doors_To or (@Doors_To = 0 or @Doors_To is null))
        and (a.Year >= @Year_From or (@Year_From = 0 or @Year_From is null))
        and (a.Year <= @Year_To or (@Year_To = 0 or @Year_To is null))
        and not exists 
        (
            select * 
            from Reservation r 
            left join Invoice i on (i.Reservation_Id = r.Id and i.Payment_Status = 1)
            where r.Vehicle_Id = a.Id 
            and 
            (
                i.Reservation_Id is not null
                or 
                (cast(getdate() as date) >= cast(r.Creation_Date as date)) and (cast(getdate() as date) <= cast(r.Expiration_Date as date))
            )
        )
order by b.Description, c.Description, d.Description, e.Description, a.Year, a.Kilometers

go


