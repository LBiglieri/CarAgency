CREATE OR ALTER PROCEDURE dbo.Permissions_DeleteCompleteFamily
    @Id uniqueidentifier,
    @BaseRoleId uniqueidentifier,
    @UserDigests dbo.UserDvhUpdates READONLY
AS
BEGIN
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;
        -- .NET calcula un DVH por usuario con el nuevo Role_Id antes de llamar al SP.
        IF EXISTS (SELECT Id FROM dbo.Users WHERE Role_Id=@Id EXCEPT SELECT Id FROM @UserDigests)
            OR EXISTS (SELECT Id FROM @UserDigests EXCEPT SELECT Id FROM dbo.Users WHERE Role_Id=@Id)
            THROW 51001, 'Los usuarios afectados cambiaron. Reintente la eliminacion de la familia.', 1;
        IF EXISTS (SELECT 1 FROM @UserDigests) AND
            (@BaseRoleId IS NULL OR @BaseRoleId=@Id OR NOT EXISTS
                (SELECT 1 FROM dbo.Permissions WHERE Id=@BaseRoleId AND Name='Base User' AND Type IS NULL))
            THROW 51002, 'No existe una familia Base User valida para reasignar los usuarios.', 1;
        DELETE FROM dbo.Permission_Permission WHERE Father_Id=@Id OR Child_Id=@Id;
        UPDATE u SET Role_Id=@BaseRoleId, DVH=d.DVH
            FROM dbo.Users u JOIN @UserDigests d ON d.Id=u.Id;
        DELETE FROM dbo.Permissions WHERE Id=@Id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
