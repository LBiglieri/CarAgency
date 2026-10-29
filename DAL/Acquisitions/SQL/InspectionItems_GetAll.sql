CREATE or ALTER proc InspectionItems_GetAll as

select	a.Id,
        a.Description,
        a.Display_Order
from	InspectionItems a
order by a.Display_Order asc

go
