CREATE or ALTER proc AcquisitionDocumentFiles_GetByDocument @Document_Id uniqueidentifier as

select	a.Document_Id,
        a.FileName,
        a.FileContent
from	AcquisitionDocumentFiles a
where	a.Document_Id = @Document_Id

go
