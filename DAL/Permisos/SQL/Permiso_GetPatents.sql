CREATE or ALTER proc Permiso_GetPatents  as

select	* 
from	Permiso p 
where	p.Tipo is not null
order by p.Nombre asc;

go