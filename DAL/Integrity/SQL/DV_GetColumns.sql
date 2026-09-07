CREATE OR ALTER PROCEDURE dbo.DV_GetColumns @TableName nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @TableName;
    SELECT c.name AS Name, ty.name AS SqlType, CONVERT(int,c.max_length) AS MaxLength,
        c.is_nullable AS IsNullable,
        ISNULL(CONVERT(int,COLLATIONPROPERTY(c.collation_name,'CodePage')),0) AS CodePage,
        CONVERT(bit,CASE WHEN EXISTS (
            SELECT 1 FROM sys.indexes i
            JOIN sys.index_columns ic ON ic.object_id=i.object_id AND ic.index_id=i.index_id
            WHERE i.object_id=c.object_id AND i.is_primary_key=1 AND ic.column_id=c.column_id
        ) THEN 1 ELSE 0 END) AS IsKey
    FROM sys.columns c JOIN sys.types ty ON ty.user_type_id=c.user_type_id
    WHERE c.object_id=OBJECT_ID(N'dbo.'+QUOTENAME(@TableName)) AND c.name<>'DVH'
    ORDER BY c.column_id;
END
GO
