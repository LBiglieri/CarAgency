CREATE OR ALTER PROCEDURE dbo.DV_Save
    @NombreTabla nvarchar(128), @DVV char(64)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @NombreTabla;
    UPDATE dbo.DV SET DVV=@DVV, VersionAlgoritmo=1
        WHERE NombreEsquema='dbo' AND NombreTabla=@NombreTabla;
    IF @@ROWCOUNT=0
        INSERT INTO dbo.DV(NombreEsquema,NombreTabla,DVV,VersionAlgoritmo)
        VALUES ('dbo',@NombreTabla,@DVV,1);
END
GO
