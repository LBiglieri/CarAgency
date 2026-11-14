CREATE or ALTER proc Compra_GetDuplicates @Patente varchar(10), @NumeroChasis varchar(17), @NumeroMotor varchar(20) as

-- Inventario (Number = 0): vehiculos sin factura pagada. Chasis y motor solo se conocen
-- de los que entraron por una adquisicion; el resto se compara por patente.
select	0 as Numero,
        a.Patente,
        isnull(g.NumeroChasis, '') as NumeroChasis,
        isnull(g.NumeroMotor, '') as NumeroMotor,
        (cast(a.Anio as varchar) + ' ' + e.Descripcion + ' ' + b.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + a.Patente) as Vehiculo_Descripcion
from	Vehiculo a
        join Marca b on (b.Marca_Id = a.Marca_Id)
		join Modelo c on (c.Modelo_Id = a.Modelo_Id)
		join Version d on (d.Version_Id = a.Version_Id)
		join Color e on (e.Color_Id = a.Color_Id)
        left join Compra f on (f.Vehiculo_Id = a.Vehiculo_Id)
        left join VehiculoProspecto g on (g.VehiculoProspecto_Id = f.VehiculoProspecto_Id)
where	(a.Patente = @Patente or g.NumeroChasis = @NumeroChasis or g.NumeroMotor = @NumeroMotor)
        and not exists (select * from Factura i where i.Vehiculo_Id = a.Vehiculo_Id and i.EstadoPago = 1)

union all

-- Adquisiciones abiertas: de Inspeccion programada (1) a Transferida (7).
select	a.Numero,
        g.Patente,
        g.NumeroChasis,
        g.NumeroMotor,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + b.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + g.Patente) as Vehiculo_Descripcion
from	Compra a
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = a.VehiculoProspecto_Id)
        join Marca b on (b.Marca_Id = g.Marca_Id)
		join Modelo c on (c.Modelo_Id = g.Modelo_Id)
		join Version d on (d.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
where	a.Estado between 1 and 7
        and (g.Patente = @Patente or g.NumeroChasis = @NumeroChasis or g.NumeroMotor = @NumeroMotor)

go
