CREATE or ALTER proc Makes_GetAll as

select Id, Description 
from Makes 
order by Description asc
go