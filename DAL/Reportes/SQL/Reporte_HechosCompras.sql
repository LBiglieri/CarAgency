CREATE OR ALTER PROCEDURE dbo.Reporte_HechosCompras
    @Desde datetime, @Hasta datetime
AS
BEGIN
    SET NOCOUNT ON;
    -- Compras (PN2) iniciadas antes de @Hasta: las del periodo [@Desde, @Hasta) y las que siguen abiertas aunque sean anteriores.
    -- Una fila por compra con la fecha en que llego a cada etapa. No calcula nada: la etapa a cada fecha de corte,
    -- las tasas y la proyeccion las resuelve ProyeccionComprasCalculator.
    -- La compra no guarda el historial de estados, asi que cada etapa se fecha con el registro que la produjo:
    --   Inspeccionada   = inspeccion realizada       Tasada        = primera tasacion
    --   Formalizada     = firma del contrato         Doc. completa = ultimo documento de la etapa B cargado
    --   Pagada          = comprobante de compra      Transferida   = ultimo documento de la etapa C (o la cita)
    --   Finalizada      = cierre
    -- Nombre y apellido salen encriptados: los desencripta el mapper.
    SELECT a.Compra_Id, a.Numero, a.Estado, a.MotivoRechazo, a.Fecha,
        i.FechaRealizada AS FechaInspeccion,
        t.PrimeraFecha AS FechaTasacion, t.UltimaFecha AS FechaUltimaTasacion, t.UltimoValor AS ValorTasacion,
        a.PrecioAcordado, a.FechaFirmaContrato,
        d.FechaEtapaB AS FechaDocumentacion,
        cc.Fecha AS FechaPago,
        ISNULL(d.FechaEtapaC, a.FechaCitaTransferencia) AS FechaTransferencia,
        a.FechaCierre,
        ISNULL(p.MontoPagado, 0) AS MontoPagado,
        f.Descripcion AS Marca,
        (CAST(b.Anio AS varchar) + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + e.Descripcion + ' ' + b.Patente) AS Vehiculo_Descripcion,
        g.Nombre AS Persona_Nombre, g.Apellido AS Persona_Apellido, g.DNI AS Persona_DNI
    FROM Compra a
        JOIN VehiculoProspecto b ON (b.VehiculoProspecto_Id = a.VehiculoProspecto_Id)
        JOIN Modelo c ON (c.Modelo_Id = b.Modelo_Id)
        JOIN Version e ON (e.Version_Id = b.Version_Id)
        JOIN Marca f ON (f.Marca_Id = b.Marca_Id)
        JOIN Persona g ON (g.Persona_Id = a.Persona_Id)
        LEFT JOIN Inspeccion i ON (i.Compra_Id = a.Compra_Id)
        LEFT JOIN ComprobanteCompra cc ON (cc.Compra_Id = a.Compra_Id)
        OUTER APPLY (
            SELECT MIN(x.Fecha) AS PrimeraFecha, MAX(x.Fecha) AS UltimaFecha,
                (SELECT TOP 1 y.Valor FROM Tasacion y WHERE y.Compra_Id = a.Compra_Id ORDER BY y.Fecha DESC) AS UltimoValor
            FROM Tasacion x
            WHERE x.Compra_Id = a.Compra_Id
        ) t
        OUTER APPLY (
            SELECT MAX(CASE WHEN y.Etapa = 1 THEN x.FechaCarga END) AS FechaEtapaB,
                MAX(CASE WHEN y.Etapa = 2 THEN x.FechaCarga END) AS FechaEtapaC
            FROM DocumentoCompra x
                JOIN TipoDocumentoCompra y ON (y.TipoDocumentoCompra_Id = x.TipoDocumentoCompra_Id)
            WHERE x.Compra_Id = a.Compra_Id
        ) d
        OUTER APPLY (
            SELECT SUM(x.Monto) AS MontoPagado FROM PagoCompra x WHERE x.Compra_Id = a.Compra_Id
        ) p
    WHERE a.Fecha < @Hasta AND (a.Fecha >= @Desde OR a.Estado < 8)
    ORDER BY a.Fecha;
END
GO
