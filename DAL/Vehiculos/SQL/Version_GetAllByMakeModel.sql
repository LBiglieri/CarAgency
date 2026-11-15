CREATE or ALTER proc Version_GetAllByMakeModel @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier as

select	a.Version_Id, a.Marca_Id, b.Descripcion as Marca_Descripcion, a.Modelo_Id, c.Descripcion as Modelo_Descripcion, a.Descripcion 
from	Version a
		join Marca b on (b.Marca_Id = a.Marca_Id)
		join Modelo c on (c.Modelo_Id = a.Modelo_Id)
where	a.Marca_Id = @Marca_Id
		and a.Modelo_Id = @Modelo_Id
order by a.Descripcion asc

go