CREATE OR ALTER PROCEDURE dbo.BitacoraEventos_GetLogins
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT COALESCE(u.NombreUsuario,e.LoginIntentado,'') AS Login
    FROM dbo.BitacoraEventos e
    LEFT JOIN dbo.Usuario u ON u.Usuario_Id=e.Usuario_Id
    WHERE COALESCE(u.NombreUsuario,e.LoginIntentado,'')<>''
    ORDER BY Login;
END
GO
