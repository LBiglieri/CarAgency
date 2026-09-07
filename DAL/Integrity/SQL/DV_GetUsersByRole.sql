CREATE OR ALTER PROCEDURE dbo.DV_GetUsersByRole @RoleId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Users WHERE Role_Id=@RoleId;
END
GO
