CREATE or ALTER proc InspectionScores_GetByInspection @Inspection_Id uniqueidentifier as

select	a.Inspection_Id,
        a.InspectionItem_Id,
        b.Description,
        b.Display_Order,
        a.Score,
        a.Observations
from	InspectionScores a
        join InspectionItems b on (b.Id = a.InspectionItem_Id)
where	a.Inspection_Id = @Inspection_Id
order by b.Display_Order asc

go
