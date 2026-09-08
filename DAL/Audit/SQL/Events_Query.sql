CREATE OR ALTER PROCEDURE dbo.Events_Query
    @Login nvarchar(256)=NULL, @From datetime, @Until datetime,
    @Module varchar(32)=NULL, @EventType varchar(64)=NULL, @Criticality int=NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- El evento se relaciona con el usuario por Id. El login se resuelve en la consulta;
    -- AttemptedLogin solo tiene valor cuando el intento no correspondio a ningun usuario.
    SELECT e.Id, e.UserId,
        COALESCE(u.Username,e.AttemptedLogin,'') AS Login,
        ISNULL(u.Name,'') AS Name,
        ISNULL(u.Surname,'') AS Surname,
        e.OccurredAt, e.Module, e.EventType, e.Criticality, e.TargetId
    FROM dbo.Events e
    LEFT JOIN dbo.Users u ON u.Id=e.UserId
    WHERE e.OccurredAt>=@From AND e.OccurredAt<@Until
      AND (@Login IS NULL OR COALESCE(u.Username,e.AttemptedLogin,'')=@Login)
      AND (@Module IS NULL OR e.Module=@Module)
      AND (@EventType IS NULL OR e.EventType=@EventType)
      AND (@Criticality IS NULL OR e.Criticality=@Criticality)
    ORDER BY e.OccurredAt DESC,e.Id DESC;
END
GO
