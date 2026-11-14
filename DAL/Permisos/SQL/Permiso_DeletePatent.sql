CREATE OR ALTER PROCEDURE dbo.Permiso_DeletePatent @Permiso_Id uniqueidentifier
AS
BEGIN
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;
        DELETE FROM dbo.Permiso_Permiso WHERE Padre_Id=@Permiso_Id OR Hijo_Id=@Permiso_Id;
        DELETE FROM dbo.Permiso WHERE Permiso_Id=@Permiso_Id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
