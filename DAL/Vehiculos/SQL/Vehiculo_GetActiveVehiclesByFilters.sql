CREATE or ALTER proc Vehiculo_GetActiveVehiclesByFilters @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier , @Version_Id uniqueidentifier , @Color_Id uniqueidentifier , @Price_From float, 
                                                         @Price_To float, @Year_From int, @Year_To int, @Doors_From int, @Doors_To int, @Kilometers_From int, @Kilometers_To int as

select	a.Vehiculo_Id,
        a.Patente,
        a.Marca_Id, b.Descripcion as Marca_Descripcion,
        a.Modelo_Id, c.Descripcion as Modelo_Descripcion, 
        a.Version_Id, d.Descripcion as Version_Descripcion, 
        a.Color_Id, e.Descripcion as Color_Descripcion, 
        a.Precio,
        a.Opcionales, 
        a.Observaciones,
        a.Puertas, 
        a.Anio, 
        a.Kilometraje, 
        a.LinkImagen
from	Vehiculo a
		join Marca b on (b.Marca_Id = a.Marca_Id)
		join Modelo c on (c.Modelo_Id = a.Modelo_Id)
		join Version d on (d.Version_Id = a.Version_Id)
		join Color e on (e.Color_Id = a.Color_Id)
where	(a.Marca_Id = @Marca_Id or (@Marca_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Modelo_Id = @Modelo_Id or (@Modelo_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Version_Id = @Version_Id or (@Version_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Color_Id = @Color_Id or (@Color_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Precio >= @Price_From or (@Price_From = 0 or @Price_From is null))
        and (a.Precio <= @Price_To or (@Price_To = 0 or @Price_To is null))
        and (a.Kilometraje >= @Kilometers_From or (@Kilometers_From = 0 or @Kilometers_From is null))
        and (a.Kilometraje <= @Kilometers_To or (@Kilometers_To = 0 or @Kilometers_To is null))
        and (a.Puertas >= @Doors_From or (@Doors_From = 0 or @Doors_From is null))
        and (a.Puertas <= @Doors_To or (@Doors_To = 0 or @Doors_To is null))
        and (a.Anio >= @Year_From or (@Year_From = 0 or @Year_From is null))
        and (a.Anio <= @Year_To or (@Year_To = 0 or @Year_To is null))
        and not exists 
        (
            select * 
            from Reserva r 
            left join Factura i on (i.Reserva_Id = r.Reserva_Id and i.EstadoPago = 1)
            where r.Vehiculo_Id = a.Vehiculo_Id 
            and 
            (
                i.Reserva_Id is not null
                or 
                (cast(getdate() as date) >= cast(r.Fecha as date)) and (cast(getdate() as date) <= cast(r.FechaVencimiento as date))
            )
        )
order by b.Descripcion, c.Descripcion, d.Descripcion, e.Descripcion, a.Anio, a.Kilometraje

go


