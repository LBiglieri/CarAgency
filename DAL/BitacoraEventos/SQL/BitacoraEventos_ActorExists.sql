CREATE OR ALTER PROCEDURE dbo.BitacoraEventos_ActorExists @Usuario_Id uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    -- Una restauracion puede dejar sin fila al actor de un evento posterior. La aplicacion
    -- consulta antes de armar la fila para no guardar una referencia que la FK rechazaria.
    SELECT CONVERT(bit,CASE WHEN EXISTS(SELECT 1 FROM dbo.Usuario WHERE Usuario_Id=@Usuario_Id) THEN 1 ELSE 0 END) AS ActorExists;
END
GO
