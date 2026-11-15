CREATE or ALTER proc ArchivoTramite_GetByPaperWork @Tramite_Id uniqueidentifier as

select	a.ArchivoTramite_Id,
        a.Tramite_Id, 
        a.NombreArchivo, 
        a.Contenido,
        a.FechaCarga
from	ArchivoTramite a
where	a.Tramite_Id = @Tramite_Id

go


