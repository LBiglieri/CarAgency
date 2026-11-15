CREATE OR ALTER PROCEDURE dbo.DV_GetUsersByRole @RoleId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Usuario WHERE Rol_Id=@RoleId;
END
GO
