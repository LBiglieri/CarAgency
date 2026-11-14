CREATE OR ALTER PROCEDURE dbo.BitacoraEventos_Query
    @Login nvarchar(256)=NULL, @From datetime, @Until datetime,
    @Modulo varchar(32)=NULL, @TipoEvento varchar(64)=NULL, @Criticidad int=NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- El evento se relaciona con el usuario por Id. El login se resuelve en la consulta;
    -- AttemptedLogin solo tiene valor cuando el intento no correspondio a ningun usuario.
    SELECT e.BitacoraEventos_Id, e.Usuario_Id,
        COALESCE(u.NombreUsuario,e.LoginIntentado,'') AS Login,
        ISNULL(u.Nombre,'') AS Nombre,
        ISNULL(u.Apellido,'') AS Apellido,
        e.Fecha, e.Modulo, e.TipoEvento, e.Criticidad, e.Objetivo_Id
    FROM dbo.BitacoraEventos e
    LEFT JOIN dbo.Usuario u ON u.Usuario_Id=e.Usuario_Id
    WHERE e.Fecha>=@From AND e.Fecha<@Until
      AND (@Login IS NULL OR COALESCE(u.NombreUsuario,e.LoginIntentado,'')=@Login)
      AND (@Modulo IS NULL OR e.Modulo=@Modulo)
      AND (@TipoEvento IS NULL OR e.TipoEvento=@TipoEvento)
      AND (@Criticidad IS NULL OR e.Criticidad=@Criticidad)
    ORDER BY e.Fecha DESC,e.BitacoraEventos_Id DESC;
END
GO
