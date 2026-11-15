CREATE OR ALTER PROCEDURE dbo.DV_GetByTable @NombreTabla nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @NombreTabla;
    SELECT NombreEsquema,NombreTabla,DVV,VersionAlgoritmo
    FROM dbo.DV WHERE NombreEsquema='dbo' AND NombreTabla=@NombreTabla;
END
GO
