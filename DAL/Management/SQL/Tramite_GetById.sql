CREATE or ALTER proc Tramite_GetById @Tramite_Id uniqueidentifier as

select	a.Tramite_Id,
        a.Vehiculo_Id,  (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id,
        a.Factura_Id, 
        a.CodigoPrecarga08, 
        a.FechaTransferencia,
        a.Observaciones, 
        a.Finalizado
from	Tramite a
        join Vehiculo b on (b.Vehiculo_Id = a.Vehiculo_Id)
		join Modelo c on (c.Modelo_Id = b.Modelo_Id)
		join Version d on (d.Version_Id = b.Version_Id)
		join Color e on (e.Color_Id = b.Color_Id)
        join Marca f on (f.Marca_Id = b.Marca_Id)
where	a.Tramite_Id = @Tramite_Id

go


