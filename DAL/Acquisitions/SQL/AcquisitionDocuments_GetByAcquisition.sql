CREATE or ALTER proc AcquisitionDocuments_GetByAcquisition @Acquisition_Id uniqueidentifier as

-- Documentos cargados de las dos etapas. Un tipo sin fila es un documento Pendiente.
select	a.Id,
        a.Acquisition_Id,
        a.DocumentType_Id,
        a.Status,
        a.Issue_Date,
        a.Observation,
        a.User_Id,
        a.Uploaded_Date,
        b.Description,
        b.Stage,
        c.FileName
from	AcquisitionDocuments a
        join AcquisitionDocumentTypes b on (b.Id = a.DocumentType_Id)
        left join AcquisitionDocumentFiles c on (c.Document_Id = a.Id)
where	a.Acquisition_Id = @Acquisition_Id
order by b.Display_Order asc

go
