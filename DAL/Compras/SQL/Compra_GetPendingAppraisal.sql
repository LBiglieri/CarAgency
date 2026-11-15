CREATE or ALTER proc Compra_GetPendingAppraisal @Filter varchar(20), @Now datetime as

-- Adquisiciones a tasar: Inspeccionado (2), o Tasada (3) con la ultima tasacion vencida.
-- El filtro busca por DNI exacto o por el comienzo de la matricula; vacio trae todas.
select	a.Compra_Id,
        a.Numero,
        b.Inspeccion_Id as Inspeccion_Id,
        a.Persona_Id,
        h.DNI,
        g.Patente,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        b.FechaRealizada,
        (select count(*) from Tasacion x where x.Compra_Id = a.Compra_Id) as TasacionesPrevias
from	Compra a
        join Inspeccion b on (b.Compra_Id = a.Compra_Id)
        join Persona h on (h.Persona_Id = a.Persona_Id)
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = a.VehiculoProspecto_Id)
        join Marca c on (c.Marca_Id = g.Marca_Id)
		join Modelo d on (d.Modelo_Id = g.Modelo_Id)
		join Version f on (f.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
where	(a.Estado = 2
            or (a.Estado = 3 and (select top 1 x.Vigencia from Tasacion x where x.Compra_Id = a.Compra_Id order by x.Fecha desc) < @Now))
        and (isnull(@Filter, '') = ''
            or h.DNI = try_cast(@Filter as int)
            or g.Patente like @Filter + '%')
order by b.FechaRealizada asc

go
