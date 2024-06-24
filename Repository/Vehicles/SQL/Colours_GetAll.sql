CREATE or ALTER proc Colours_GetAll as

select Id, Description 
from Colours 
order by Description asc
go