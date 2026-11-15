CREATE OR ALTER PROCEDURE dbo.DV_GetHorizontal @NombreTabla nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @NombreTabla;
    DECLARE @Sql nvarchar(max)=N'SELECT DVH FROM dbo.'+QUOTENAME(@NombreTabla);
    EXEC sys.sp_executesql @Sql;
END
GO
