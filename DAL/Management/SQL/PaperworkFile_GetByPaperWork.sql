CREATE or ALTER proc PaperworkFile_GetByPaperWork @Paperwork_Id uniqueidentifier as

select	a.Id,
        a.Paperwork_Id, 
        a.FileName, 
        a.FileContent,
        a.UploadedDate
from	PaperworkFiles a
where	a.Paperwork_Id = @Paperwork_Id

go


