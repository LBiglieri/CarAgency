CREATE OR ALTER PROCEDURE dbo.BitacoraEventos_Insert
    @BitacoraEventos_Id uniqueidentifier, @Usuario_Id uniqueidentifier, @LoginIntentado nvarchar(256),
    @Fecha datetime, @Modulo varchar(32), @TipoEvento varchar(64),
    @Criticidad int, @Objetivo_Id uniqueidentifier, @DVH char(64)
AS
BEGIN
    SET NOCOUNT ON;
    -- El actor se resuelve en la aplicacion antes de calcular el DVH. Este procedimiento
    -- no puede alterar ningun valor: cualquier cambio invalidaria el digito horizontal.
    INSERT dbo.BitacoraEventos(BitacoraEventos_Id,Usuario_Id,LoginIntentado,Fecha,Modulo,TipoEvento,Criticidad,Objetivo_Id,DVH)
    VALUES(@BitacoraEventos_Id,@Usuario_Id,@LoginIntentado,@Fecha,@Modulo,@TipoEvento,@Criticidad,@Objetivo_Id,@DVH);
END
GO
