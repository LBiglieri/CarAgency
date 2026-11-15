CREATE or ALTER proc Factura_GetById @Factura_Id uniqueidentifier as

select	a.Factura_Id,
        a.Vehiculo_Id,  (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id,
        a.Reserva_Id, 
        a.Detalle, 
        a.CuilCuit,
        a.RazonSocial, 
        a.Valor,
        a.EstadoPago, 
        a.Fecha
from	Factura a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
where	a.Factura_Id = @Factura_Id

go


