CREATE OR ALTER PROCEDURE dbo.DV_GetByTable @TableName nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @TableName;
    SELECT SchemaName,TableName,DVV,AlgorithmVersion
    FROM dbo.DV WHERE SchemaName='dbo' AND TableName=@TableName;
END
GO
