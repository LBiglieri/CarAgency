CREATE or ALTER proc Inspeccion_GetDetail @Inspeccion_Id uniqueidentifier as

-- Datos del vehiculo que se inspecciona, con lo declarado en CUF07.
select	a.Inspeccion_Id,
        a.Compra_Id,
        b.Numero,
        g.Patente,
        g.NumeroChasis,
        g.NumeroMotor,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        g.Puertas,
        g.Kilometraje
from	Inspeccion a
        join Compra b on (b.Compra_Id = a.Compra_Id)
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = b.VehiculoProspecto_Id)
        join Marca c on (c.Marca_Id = g.Marca_Id)
		join Modelo d on (d.Modelo_Id = g.Modelo_Id)
		join Version f on (f.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
where	a.Inspeccion_Id = @Inspeccion_Id

go
