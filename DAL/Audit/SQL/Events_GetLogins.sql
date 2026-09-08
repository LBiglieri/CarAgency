CREATE OR ALTER PROCEDURE dbo.Events_GetLogins
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT COALESCE(u.Username,e.AttemptedLogin,'') AS Login
    FROM dbo.Events e
    LEFT JOIN dbo.Users u ON u.Id=e.UserId
    WHERE COALESCE(u.Username,e.AttemptedLogin,'')<>''
    ORDER BY Login;
END
GO
