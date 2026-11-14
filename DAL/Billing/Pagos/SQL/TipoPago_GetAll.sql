CREATE or ALTER proc TipoPago_GetAll as

select TipoPago_Id, Descripcion 
from TipoPago 
order by Descripcion asc
go