CREATE or ALTER proc ItemInspeccion_GetAll as

select	a.ItemInspeccion_Id,
        a.Descripcion,
        a.Orden
from	ItemInspeccion a
order by a.Orden asc

go
