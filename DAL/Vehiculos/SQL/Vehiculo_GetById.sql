CREATE or ALTER proc Vehiculo_GetById @Vehiculo_Id uniqueidentifier as

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
where	a.Vehiculo_Id = @Vehiculo_Id

go


