CREATE or ALTER proc Permiso_GetFamilies as

select	* 
from	Permiso p 
where	p.Tipo is  null
order by p.Nombre asc;

go