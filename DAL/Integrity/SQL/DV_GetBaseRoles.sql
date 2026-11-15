CREATE OR ALTER PROCEDURE dbo.DV_GetBaseRoles
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Permiso_Id,Nombre FROM dbo.Permiso WHERE Nombre='Base User' AND Tipo IS NULL;
END
GO
