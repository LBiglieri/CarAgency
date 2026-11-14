CREATE or ALTER proc Cotizacion_GetById @Cotizacion_Id uniqueidentifier as

select	a.Cotizacion_Id,
        a.Vehiculo_Id, (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id, g.Nombre as Persona_Nombre, g.Apellido as Persona_Apellido, g.DNI as Persona_DNI, 
        a.Precio,
        a.Fecha
from	Cotizacion a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
        join Persona g on (g.Persona_Id = a.Persona_Id)
where	a.Cotizacion_Id = @Cotizacion_Id

go

