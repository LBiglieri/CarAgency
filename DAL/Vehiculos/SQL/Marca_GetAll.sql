CREATE or ALTER proc Marca_GetAll as

select Marca_Id, Descripcion 
from Marca 
order by Descripcion asc
go