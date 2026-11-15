CREATE OR ALTER PROCEDURE dbo.DV_ValidateTable @NombreTabla nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    IF @NombreTabla IS NULL OR @NombreTabla COLLATE Latin1_General_100_BIN2 NOT IN
        ('DocumentoCompra','TipoDocumentoCompra','PagoCompra','FormaPagoCompra','Compra',
         'Tasacion','Persona','Color','BitacoraEventos','ItemInspeccion','Inspeccion','PuntajeInspeccion','Factura','Marca',
         'Modelo','Tramite','Pago','TipoPago','Permiso_Permiso','Permiso','VehiculoProspecto',
         'ComprobanteCompra','Cotizacion','Reserva','Usuario','Vehiculo','Version')
        THROW 51003, 'Tabla fuera del alcance de digitos verificadores.', 1;
END
GO
