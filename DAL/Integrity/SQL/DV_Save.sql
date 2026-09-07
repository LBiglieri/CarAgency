CREATE OR ALTER PROCEDURE dbo.DV_Save
    @TableName nvarchar(128), @DVV char(64), @KeyId char(64)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @TableName;
    IF EXISTS (SELECT 1 FROM dbo.DV WHERE SchemaName='dbo' AND TableName=@TableName AND KeyId<>@KeyId)
        THROW 51004, 'La clave de digitos no coincide con la registrada.', 1;
    UPDATE dbo.DV SET DVV=@DVV, AlgorithmVersion=1
        WHERE SchemaName='dbo' AND TableName=@TableName;
    IF @@ROWCOUNT=0
        INSERT INTO dbo.DV(SchemaName,TableName,DVV,AlgorithmVersion,KeyId)
        VALUES ('dbo',@TableName,@DVV,1,@KeyId);
END
GO
