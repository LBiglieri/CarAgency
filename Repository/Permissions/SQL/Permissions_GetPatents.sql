CREATE or ALTER proc Permissions_GetPatents  as

select	* 
from	Permissions p 
where	p.Type is not null
order by p.Name asc;

go