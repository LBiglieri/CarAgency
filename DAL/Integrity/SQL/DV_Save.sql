CREATE OR ALTER PROCEDURE dbo.DV_Save
    @TableName nvarchar(128), @DVV char(64)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @TableName;
    UPDATE dbo.DV SET DVV=@DVV, AlgorithmVersion=1
        WHERE SchemaName='dbo' AND TableName=@TableName;
    IF @@ROWCOUNT=0
        INSERT INTO dbo.DV(SchemaName,TableName,DVV,AlgorithmVersion)
        VALUES ('dbo',@TableName,@DVV,1);
END
GO
