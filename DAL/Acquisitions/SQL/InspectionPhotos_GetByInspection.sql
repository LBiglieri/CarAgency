CREATE or ALTER proc InspectionPhotos_GetByInspection @Inspection_Id uniqueidentifier as

select	a.Id,
        a.Inspection_Id,
        a.FileName,
        a.FileContent,
        a.UploadedDate
from	InspectionPhotos a
where	a.Inspection_Id = @Inspection_Id
order by a.UploadedDate asc

go
