CREATE OR ALTER PROCEDURE dbo.BitacoraCambios_Query
    @Vehiculo_Id uniqueidentifier = NULL, @From datetime, @Until datetime, @Operacion int = NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- El cambio se relaciona con el usuario por Id. El login se resuelve en la consulta.
    SELECT b.BitacoraCambios_Id, b.Vehiculo_Id, b.Patente, b.Fecha, b.Operacion, b.Campo,
        b.ValorAnterior, b.ValorNuevo, b.Usuario_Id,
        ISNULL(u.NombreUsuario, '') AS Login
    FROM dbo.BitacoraCambios b
    LEFT JOIN dbo.Usuario u ON u.Usuario_Id = b.Usuario_Id
    WHERE b.Fecha >= @From AND b.Fecha < @Until
      AND (@Vehiculo_Id IS NULL OR b.Vehiculo_Id = @Vehiculo_Id)
      AND (@Operacion IS NULL OR b.Operacion = @Operacion)
    ORDER BY b.Fecha DESC, b.Patente, b.Campo;
END
GO
