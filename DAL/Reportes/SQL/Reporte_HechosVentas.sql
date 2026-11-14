CREATE OR ALTER PROCEDURE dbo.Reporte_HechosVentas
    @Desde datetime, @Hasta datetime
AS
BEGIN
    SET NOCOUNT ON;
    -- Hechos del embudo comercial (PN1) en el periodo [@Desde, @Hasta): cotizaciones, reservas y facturas.
    -- No calcula nada: las tasas, la vigencia y la proyeccion las resuelve ProyeccionVentasCalculator,
    -- asi la misma carga sirve para proyectar desde cualquier fecha de corte (y validar el modelo hacia atras).
    -- Tipo: 1 = Cotizacion, 2 = Reserva, 3 = Factura. Nombre y apellido salen encriptados: los desencripta el mapper.
    -- Solo cuentan las facturas pagadas (EstadoPago = 1), igual que el inventario: la factura nace impaga y si no se paga no hay venta.
    SELECT 1 AS Tipo, a.Cotizacion_Id AS Id, a.Fecha, CAST(NULL AS datetime) AS FechaVencimiento,
        a.Persona_Id, a.Vehiculo_Id, CAST(NULL AS uniqueidentifier) AS Reserva_Id, ISNULL(a.Precio, 0) AS Precio,
        f.Descripcion AS Marca,
        (CAST(b.Anio AS varchar) + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) AS Vehiculo_Descripcion,
        g.Nombre AS Persona_Nombre, g.Apellido AS Persona_Apellido, g.DNI AS Persona_DNI
    FROM Cotizacion a
        JOIN Vehiculo b ON (b.Vehiculo_Id = a.Vehiculo_Id)
        JOIN Modelo c ON (c.Modelo_Id = b.Modelo_Id)
        JOIN Version d ON (d.Version_Id = b.Version_Id)
        JOIN Marca f ON (f.Marca_Id = b.Marca_Id)
        LEFT JOIN Persona g ON (g.Persona_Id = a.Persona_Id)
    WHERE a.Fecha >= @Desde AND a.Fecha < @Hasta

    UNION ALL

    SELECT 2, a.Reserva_Id, a.Fecha, a.FechaVencimiento,
        a.Persona_Id, a.Vehiculo_Id, a.Reserva_Id, a.Precio,
        f.Descripcion,
        (CAST(b.Anio AS varchar) + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente),
        g.Nombre, g.Apellido, g.DNI
    FROM Reserva a
        JOIN Vehiculo b ON (b.Vehiculo_Id = a.Vehiculo_Id)
        JOIN Modelo c ON (c.Modelo_Id = b.Modelo_Id)
        JOIN Version d ON (d.Version_Id = b.Version_Id)
        JOIN Marca f ON (f.Marca_Id = b.Marca_Id)
        LEFT JOIN Persona g ON (g.Persona_Id = a.Persona_Id)
    WHERE a.Fecha >= @Desde AND a.Fecha < @Hasta

    UNION ALL

    SELECT 3, a.Factura_Id, a.Fecha, NULL,
        a.Persona_Id, a.Vehiculo_Id, a.Reserva_Id, a.Valor,
        f.Descripcion,
        (CAST(b.Anio AS varchar) + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente),
        g.Nombre, g.Apellido, g.DNI
    FROM Factura a
        JOIN Vehiculo b ON (b.Vehiculo_Id = a.Vehiculo_Id)
        JOIN Modelo c ON (c.Modelo_Id = b.Modelo_Id)
        JOIN Version d ON (d.Version_Id = b.Version_Id)
        JOIN Marca f ON (f.Marca_Id = b.Marca_Id)
        LEFT JOIN Persona g ON (g.Persona_Id = a.Persona_Id)
    WHERE a.Fecha >= @Desde AND a.Fecha < @Hasta AND a.EstadoPago = 1

    ORDER BY Fecha;
END
GO
