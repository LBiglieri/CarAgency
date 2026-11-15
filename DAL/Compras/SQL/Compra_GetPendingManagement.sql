CREATE or ALTER proc Compra_GetPendingManagement @Filter varchar(20) as

-- Compras en gestion (CUF10): Tasada (3), Compra formalizada (4), Documentacion completa (5), Pagada (6) y Transferida (7).
-- El filtro busca por DNI exacto o por el comienzo de la matricula; vacio trae todas.
select	a.Compra_Id,
        a.Numero,
        b.Inspeccion_Id as Inspeccion_Id,
        a.Persona_Id,
        h.DNI,
        g.Patente,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        a.Estado
from	Compra a
        join Inspeccion b on (b.Compra_Id = a.Compra_Id)
        join Persona h on (h.Persona_Id = a.Persona_Id)
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = a.VehiculoProspecto_Id)
        join Marca c on (c.Marca_Id = g.Marca_Id)
		join Modelo d on (d.Modelo_Id = g.Modelo_Id)
		join Version f on (f.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
where	a.Estado between 3 and 7
        and (isnull(@Filter, '') = ''
            or h.DNI = try_cast(@Filter as int)
            or g.Patente like @Filter + '%')
order by a.Numero asc

go
