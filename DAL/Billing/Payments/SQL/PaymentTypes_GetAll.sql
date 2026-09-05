CREATE or ALTER proc PaymentTypes_GetAll as

select Id, Description 
from PaymentTypes 
order by Description asc
go