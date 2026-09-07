CREATE OR ALTER PROCEDURE dbo.Permissions_DeletePatent @Id uniqueidentifier
AS
BEGIN
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;
        DELETE FROM dbo.Permission_Permission WHERE Father_Id=@Id OR Child_Id=@Id;
        DELETE FROM dbo.Permissions WHERE Id=@Id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
