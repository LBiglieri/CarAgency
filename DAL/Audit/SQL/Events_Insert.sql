CREATE OR ALTER PROCEDURE dbo.Events_Insert
    @Id uniqueidentifier, @UserId uniqueidentifier, @AttemptedLogin nvarchar(256),
    @OccurredAt datetime, @Module varchar(32), @EventType varchar(64),
    @Criticality int, @TargetId uniqueidentifier, @DVH char(64)
AS
BEGIN
    SET NOCOUNT ON;
    -- El actor se resuelve en la aplicacion antes de calcular el DVH. Este procedimiento
    -- no puede alterar ningun valor: cualquier cambio invalidaria el digito horizontal.
    INSERT dbo.Events(Id,UserId,AttemptedLogin,OccurredAt,Module,EventType,Criticality,TargetId,DVH)
    VALUES(@Id,@UserId,@AttemptedLogin,@OccurredAt,@Module,@EventType,@Criticality,@TargetId,@DVH);
END
GO
