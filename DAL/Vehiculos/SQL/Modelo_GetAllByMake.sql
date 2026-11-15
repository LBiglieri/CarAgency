CREATE or ALTER proc Modelo_GetAllByMake @Marca_Id uniqueidentifier as

select	a.Modelo_Id,a.Marca_Id, b.Descripcion as Marca_Descripcion, a.Descripcion 
from	Modelo a
		join Marca b on (b.Marca_Id = a.Marca_Id)
where	a.Marca_Id = @Marca_Id
order by a.Descripcion asc
go