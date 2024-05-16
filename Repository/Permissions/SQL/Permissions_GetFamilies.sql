CREATE or ALTER proc Permissions_GetFamilies as

select	* 
from	Permissions p 
where	p.Type is  null
order by p.Name asc;

go