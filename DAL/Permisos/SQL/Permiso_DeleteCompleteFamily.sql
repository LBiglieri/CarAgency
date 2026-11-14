CREATE OR ALTER PROCEDURE dbo.Permiso_DeleteCompleteFamily
    @Permiso_Id uniqueidentifier,
    @BaseRoleId uniqueidentifier,
    @UserDigests dbo.UsuarioDvhUpdates READONLY
AS
BEGIN
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;
        -- .NET calcula un DVH por usuario con el nuevo Role_Id antes de llamar al SP.
        IF EXISTS (SELECT Usuario_Id FROM dbo.Usuario WHERE Rol_Id=@Permiso_Id EXCEPT SELECT Usuario_Id FROM @UserDigests)
            OR EXISTS (SELECT Usuario_Id FROM @UserDigests EXCEPT SELECT Usuario_Id FROM dbo.Usuario WHERE Rol_Id=@Permiso_Id)
            THROW 51001, 'Los usuarios afectados cambiaron. Reintente la eliminacion de la familia.', 1;
        IF EXISTS (SELECT 1 FROM @UserDigests) AND
            (@BaseRoleId IS NULL OR @BaseRoleId=@Permiso_Id OR NOT EXISTS
                (SELECT 1 FROM dbo.Permiso WHERE Permiso_Id=@BaseRoleId AND Nombre='Base User' AND Tipo IS NULL))
            THROW 51002, 'No existe una familia Base User valida para reasignar los usuarios.', 1;
        DELETE FROM dbo.Permiso_Permiso WHERE Padre_Id=@Permiso_Id OR Hijo_Id=@Permiso_Id;
        UPDATE u SET Rol_Id=@BaseRoleId, DVH=d.DVH
            FROM dbo.Usuario u JOIN @UserDigests d ON d.Usuario_Id=u.Usuario_Id;
        DELETE FROM dbo.Permiso WHERE Permiso_Id=@Permiso_Id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
