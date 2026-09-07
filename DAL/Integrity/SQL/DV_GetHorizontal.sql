CREATE OR ALTER PROCEDURE dbo.DV_GetHorizontal @TableName nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @TableName;
    DECLARE @Sql nvarchar(max)=N'SELECT DVH FROM dbo.'+QUOTENAME(@TableName);
    EXEC sys.sp_executesql @Sql;
END
GO
