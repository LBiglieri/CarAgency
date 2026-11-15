CREATE or ALTER proc Color_GetAll as

select Color_Id, Descripcion 
from Color 
order by Descripcion asc
go