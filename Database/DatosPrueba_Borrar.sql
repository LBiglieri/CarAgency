-- Quita los datos de prueba de DatosPrueba.sql: todas las filas cuyo Id empieza con DA7A.
-- Tambien borra lo que se haya cargado desde la aplicacion sobre esas personas, vehiculos o compras
-- (una cotizacion, una factura, un tramite), porque si no las claves foraneas impedirian el borrado.
-- Despues hay que rehacer los DVV desde la aplicacion (Seguridad > Recalcular DV, permiso RecalculateDV):
-- el DVV es un HMAC y no se puede calcular desde SQL, y dv_sellar.ps1 no rehace un DVV que no coincide.
SET QUOTED_IDENTIFIER ON
SET NOCOUNT ON
SET XACT_ABORT ON
GO

BEGIN TRAN

DECLARE @Prefijo char(5) = 'DA7A%'

-- PN1: ventas
DELETE a FROM dbo.Pago a JOIN dbo.Factura b ON b.Factura_Id = a.Factura_Id
WHERE CAST(a.Pago_Id AS char(36)) LIKE @Prefijo OR CAST(b.Factura_Id AS char(36)) LIKE @Prefijo
   OR CAST(b.Persona_Id AS char(36)) LIKE @Prefijo OR CAST(b.Vehiculo_Id AS char(36)) LIKE @Prefijo

DELETE a FROM dbo.ArchivoTramite a JOIN dbo.Tramite b ON b.Tramite_Id = a.Tramite_Id
WHERE CAST(b.Persona_Id AS char(36)) LIKE @Prefijo OR CAST(b.Vehiculo_Id AS char(36)) LIKE @Prefijo
   OR CAST(b.Factura_Id AS char(36)) LIKE @Prefijo

DELETE FROM dbo.Tramite
WHERE CAST(Persona_Id AS char(36)) LIKE @Prefijo OR CAST(Vehiculo_Id AS char(36)) LIKE @Prefijo
   OR CAST(Factura_Id AS char(36)) LIKE @Prefijo

DELETE FROM dbo.Factura
WHERE CAST(Factura_Id AS char(36)) LIKE @Prefijo OR CAST(Persona_Id AS char(36)) LIKE @Prefijo
   OR CAST(Vehiculo_Id AS char(36)) LIKE @Prefijo OR CAST(Reserva_Id AS char(36)) LIKE @Prefijo

DELETE FROM dbo.Reserva
WHERE CAST(Reserva_Id AS char(36)) LIKE @Prefijo OR CAST(Persona_Id AS char(36)) LIKE @Prefijo
   OR CAST(Vehiculo_Id AS char(36)) LIKE @Prefijo

DELETE FROM dbo.Cotizacion
WHERE CAST(Cotizacion_Id AS char(36)) LIKE @Prefijo OR CAST(Persona_Id AS char(36)) LIKE @Prefijo
   OR CAST(Vehiculo_Id AS char(36)) LIKE @Prefijo

-- PN2: compras y todo lo que cuelga de ellas
DECLARE @Compras TABLE (Compra_Id uniqueidentifier PRIMARY KEY)
INSERT @Compras
SELECT Compra_Id FROM dbo.Compra
WHERE CAST(Compra_Id AS char(36)) LIKE @Prefijo OR CAST(Persona_Id AS char(36)) LIKE @Prefijo
   OR CAST(Vehiculo_Id AS char(36)) LIKE @Prefijo

DELETE FROM dbo.ComprobanteCompra WHERE Compra_Id IN (SELECT Compra_Id FROM @Compras)
DELETE FROM dbo.PagoCompra WHERE Compra_Id IN (SELECT Compra_Id FROM @Compras)
DELETE a FROM dbo.ArchivoDocumentoCompra a JOIN dbo.DocumentoCompra b ON b.DocumentoCompra_Id = a.DocumentoCompra_Id
WHERE b.Compra_Id IN (SELECT Compra_Id FROM @Compras)
DELETE FROM dbo.DocumentoCompra WHERE Compra_Id IN (SELECT Compra_Id FROM @Compras)
DELETE FROM dbo.FormaPagoCompra WHERE Compra_Id IN (SELECT Compra_Id FROM @Compras)
UPDATE dbo.Compra SET TasacionAceptada_Id = NULL WHERE Compra_Id IN (SELECT Compra_Id FROM @Compras)
DELETE FROM dbo.Tasacion WHERE Compra_Id IN (SELECT Compra_Id FROM @Compras)
DELETE a FROM dbo.FotografiaInspeccion a JOIN dbo.Inspeccion b ON b.Inspeccion_Id = a.Inspeccion_Id
WHERE b.Compra_Id IN (SELECT Compra_Id FROM @Compras)
DELETE a FROM dbo.PuntajeInspeccion a JOIN dbo.Inspeccion b ON b.Inspeccion_Id = a.Inspeccion_Id
WHERE b.Compra_Id IN (SELECT Compra_Id FROM @Compras)
DELETE FROM dbo.Inspeccion WHERE Compra_Id IN (SELECT Compra_Id FROM @Compras)
DELETE FROM dbo.Compra WHERE Compra_Id IN (SELECT Compra_Id FROM @Compras)
DELETE FROM dbo.VehiculoProspecto WHERE CAST(VehiculoProspecto_Id AS char(36)) LIKE @Prefijo

-- Inventario, su bitacora de cambios y personas
DELETE FROM dbo.BitacoraCambios WHERE CAST(Vehiculo_Id AS char(36)) LIKE @Prefijo
DELETE FROM dbo.Vehiculo WHERE CAST(Vehiculo_Id AS char(36)) LIKE @Prefijo
DELETE FROM dbo.Persona WHERE CAST(Persona_Id AS char(36)) LIKE @Prefijo

COMMIT
GO
