CREATE or ALTER proc Inspeccion_GetScheduledByMechanic @Mecanico_Id uniqueidentifier as

-- Inspecciones pendientes del mecanico: adquisiciones en estado Inspeccion programada (1).
select	a.Inspeccion_Id,
        a.Compra_Id,
        a.Mecanico_Id,
        a.FechaProgramada,
        b.Numero,
        g.Patente,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion
from	Inspeccion a
        join Compra b on (b.Compra_Id = a.Compra_Id)
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = b.VehiculoProspecto_Id)
        join Marca c on (c.Marca_Id = g.Marca_Id)
		join Modelo d on (d.Modelo_Id = g.Modelo_Id)
		join Version f on (f.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
where	a.Mecanico_Id = @Mecanico_Id
        and b.Estado = 1
order by a.FechaProgramada asc

go
