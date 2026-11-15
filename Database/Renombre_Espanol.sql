-- Renombre al espanol: tablas, columnas, restricciones, indices, secuencias, tipos de tabla y procedimientos.
-- Solo cambian nombres: la cantidad, el tipo y el orden de las columnas, los datos y la logica de los procedimientos
-- quedan iguales. Los unicos datos que cambian son los que son nombres: DV.NombreTabla y Permiso.Tipo.
-- El DVH y el DVV incluyen el nombre de la tabla y de cada columna, asi que despues hay que recalcularlos con
-- Recalcular DV (la pantalla de integridad aparece sola al iniciar sesion).
-- Correr completo con: sqlcmd -S . -E -d CarAgency -I -b -i Renombre_Espanol.sql
-- Va en una sola transaccion: si algo falla, sqlcmd corta y la base queda como estaba.
SET XACT_ABORT ON
GO
BEGIN TRANSACTION
GO

-- 1. Tablas
EXEC sp_rename N'dbo.People', N'Persona'
EXEC sp_rename N'dbo.Vehicles', N'Vehiculo'
EXEC sp_rename N'dbo.Quotations', N'Cotizacion'
EXEC sp_rename N'dbo.Reservation', N'Reserva'
EXEC sp_rename N'dbo.Invoice', N'Factura'
EXEC sp_rename N'dbo.Payments', N'Pago'
EXEC sp_rename N'dbo.PaymentTypes', N'TipoPago'
EXEC sp_rename N'dbo.Paperwork', N'Tramite'
EXEC sp_rename N'dbo.PaperworkFiles', N'ArchivoTramite'
EXEC sp_rename N'dbo.Makes', N'Marca'
EXEC sp_rename N'dbo.Models', N'Modelo'
EXEC sp_rename N'dbo.Versions', N'Version'
EXEC sp_rename N'dbo.Colours', N'Color'
EXEC sp_rename N'dbo.Acquisitions', N'Compra'
EXEC sp_rename N'dbo.ProspectVehicles', N'VehiculoProspecto'
EXEC sp_rename N'dbo.Inspections', N'Inspeccion'
EXEC sp_rename N'dbo.InspectionPhotos', N'FotografiaInspeccion'
EXEC sp_rename N'dbo.InspectionScores', N'PuntajeInspeccion'
EXEC sp_rename N'dbo.InspectionItems', N'ItemInspeccion'
EXEC sp_rename N'dbo.Appraisals', N'Tasacion'
EXEC sp_rename N'dbo.AcquisitionPaymentTerms', N'FormaPagoCompra'
EXEC sp_rename N'dbo.AcquisitionPayments', N'PagoCompra'
EXEC sp_rename N'dbo.AcquisitionDocuments', N'DocumentoCompra'
EXEC sp_rename N'dbo.AcquisitionDocumentTypes', N'TipoDocumentoCompra'
EXEC sp_rename N'dbo.AcquisitionDocumentFiles', N'ArchivoDocumentoCompra'
EXEC sp_rename N'dbo.PurchaseReceipts', N'ComprobanteCompra'
EXEC sp_rename N'dbo.Users', N'Usuario'
EXEC sp_rename N'dbo.Permissions', N'Permiso'
EXEC sp_rename N'dbo.Permission_Permission', N'Permiso_Permiso'
EXEC sp_rename N'dbo.Events', N'BitacoraEventos'
EXEC sp_rename N'dbo.EventTypes', N'TipoEvento'

-- 2. En esta version de SQL Server, las restricciones CHECK y los indices filtrados no dejan renombrar las columnas
--    que usan. Se borran aca y se vuelven a crear igual despues del paso 3, con los nombres nuevos.
ALTER TABLE dbo.DocumentoCompra DROP CONSTRAINT CK_AcquisitionDocuments_Status
ALTER TABLE dbo.TipoDocumentoCompra DROP CONSTRAINT CK_AcquisitionDocumentTypes_Stage
ALTER TABLE dbo.TipoDocumentoCompra DROP CONSTRAINT CK_AcquisitionDocumentTypes_Validity_Days
ALTER TABLE dbo.PagoCompra DROP CONSTRAINT CK_AcquisitionPayments_Amount
ALTER TABLE dbo.FormaPagoCompra DROP CONSTRAINT CK_AcquisitionPaymentTerms_Amount
ALTER TABLE dbo.Compra DROP CONSTRAINT CK_Acquisitions_Agreed_Price
ALTER TABLE dbo.Compra DROP CONSTRAINT CK_Acquisitions_Rejection_Reason
ALTER TABLE dbo.Compra DROP CONSTRAINT CK_Acquisitions_Status
ALTER TABLE dbo.Tasacion DROP CONSTRAINT CK_Appraisals_Amount
ALTER TABLE dbo.Tasacion DROP CONSTRAINT CK_Appraisals_Expiration_Date
ALTER TABLE dbo.DV DROP CONSTRAINT CK_DV_AlgorithmVersion
ALTER TABLE dbo.BitacoraEventos DROP CONSTRAINT CK_Events_Criticality
ALTER TABLE dbo.TipoEvento DROP CONSTRAINT CK_EventTypes_Criticality
ALTER TABLE dbo.Inspeccion DROP CONSTRAINT CK_Inspections_Result
ALTER TABLE dbo.Inspeccion DROP CONSTRAINT CK_Inspections_Verified_Kilometers
ALTER TABLE dbo.PuntajeInspeccion DROP CONSTRAINT CK_InspectionScores_Score
ALTER TABLE dbo.ComprobanteCompra DROP CONSTRAINT CK_PurchaseReceipts_Amounts
DROP INDEX UX_Acquisitions_Vehicle ON dbo.Compra

-- 3. Columnas
EXEC sp_rename N'dbo.Persona.Dni', N'DNI', 'COLUMN'
EXEC sp_rename N'dbo.Persona.Name', N'Nombre', 'COLUMN'
EXEC sp_rename N'dbo.Persona.Surname', N'Apellido', 'COLUMN'
EXEC sp_rename N'dbo.Persona.Address', N'Direccion', 'COLUMN'
EXEC sp_rename N'dbo.Persona.Phone_Number_Personal', N'TelefonoPersonal', 'COLUMN'
EXEC sp_rename N'dbo.Persona.Phone_Number_House', N'TelefonoCasa', 'COLUMN'
EXEC sp_rename N'dbo.Persona.Date_Of_Birth', N'FechaNacimiento', 'COLUMN'
EXEC sp_rename N'dbo.Vehiculo.License_Plate', N'Patente', 'COLUMN'
EXEC sp_rename N'dbo.Vehiculo.Make_Id', N'Marca_Id', 'COLUMN'
EXEC sp_rename N'dbo.Vehiculo.Model_Id', N'Modelo_Id', 'COLUMN'
EXEC sp_rename N'dbo.Vehiculo.Colour_Id', N'Color_Id', 'COLUMN'
EXEC sp_rename N'dbo.Vehiculo.Price', N'Precio', 'COLUMN'
EXEC sp_rename N'dbo.Vehiculo.Opcionals', N'Opcionales', 'COLUMN'
EXEC sp_rename N'dbo.Vehiculo.Observations', N'Observaciones', 'COLUMN'
EXEC sp_rename N'dbo.Vehiculo.Doors', N'Puertas', 'COLUMN'
EXEC sp_rename N'dbo.Vehiculo.Year', N'Anio', 'COLUMN'
EXEC sp_rename N'dbo.Vehiculo.Kilometers', N'Kilometraje', 'COLUMN'
EXEC sp_rename N'dbo.Vehiculo.ImageLink', N'LinkImagen', 'COLUMN'
EXEC sp_rename N'dbo.Cotizacion.Vehicle_Id', N'Vehiculo_Id', 'COLUMN'
EXEC sp_rename N'dbo.Cotizacion.Person_Id', N'Persona_Id', 'COLUMN'
EXEC sp_rename N'dbo.Cotizacion.Price', N'Precio', 'COLUMN'
EXEC sp_rename N'dbo.Cotizacion.Creation_Date', N'Fecha', 'COLUMN'
EXEC sp_rename N'dbo.Reserva.Vehicle_Id', N'Vehiculo_Id', 'COLUMN'
EXEC sp_rename N'dbo.Reserva.Person_Id', N'Persona_Id', 'COLUMN'
EXEC sp_rename N'dbo.Reserva.Price', N'Precio', 'COLUMN'
EXEC sp_rename N'dbo.Reserva.Creation_Date', N'Fecha', 'COLUMN'
EXEC sp_rename N'dbo.Reserva.Expiration_Date', N'FechaVencimiento', 'COLUMN'
EXEC sp_rename N'dbo.Factura.Vehicle_Id', N'Vehiculo_Id', 'COLUMN'
EXEC sp_rename N'dbo.Factura.Person_Id', N'Persona_Id', 'COLUMN'
EXEC sp_rename N'dbo.Factura.Reservation_Id', N'Reserva_Id', 'COLUMN'
EXEC sp_rename N'dbo.Factura.Detail', N'Detalle', 'COLUMN'
EXEC sp_rename N'dbo.Factura.CUIL_CUIT_Person', N'CuilCuit', 'COLUMN'
EXEC sp_rename N'dbo.Factura.Razon_Social', N'RazonSocial', 'COLUMN'
EXEC sp_rename N'dbo.Factura.Amount', N'Valor', 'COLUMN'
EXEC sp_rename N'dbo.Factura.Payment_Status', N'EstadoPago', 'COLUMN'
EXEC sp_rename N'dbo.Factura.Creation_Date', N'Fecha', 'COLUMN'
EXEC sp_rename N'dbo.Pago.Invoice_Id', N'Factura_Id', 'COLUMN'
EXEC sp_rename N'dbo.Pago.PaymentType_Id', N'TipoPago_Id', 'COLUMN'
EXEC sp_rename N'dbo.Pago.Amount', N'Monto', 'COLUMN'
EXEC sp_rename N'dbo.Pago.Detail', N'Detalle', 'COLUMN'
EXEC sp_rename N'dbo.TipoPago.Description', N'Descripcion', 'COLUMN'
EXEC sp_rename N'dbo.Tramite.Vehicle_Id', N'Vehiculo_Id', 'COLUMN'
EXEC sp_rename N'dbo.Tramite.Person_Id', N'Persona_Id', 'COLUMN'
EXEC sp_rename N'dbo.Tramite.Invoice_Id', N'Factura_Id', 'COLUMN'
EXEC sp_rename N'dbo.Tramite.Paperwork_Precharge_Code', N'CodigoPrecarga08', 'COLUMN'
EXEC sp_rename N'dbo.Tramite.Transfer_Date', N'FechaTransferencia', 'COLUMN'
EXEC sp_rename N'dbo.Tramite.Observations', N'Observaciones', 'COLUMN'
EXEC sp_rename N'dbo.Tramite.IsFinished', N'Finalizado', 'COLUMN'
EXEC sp_rename N'dbo.ArchivoTramite.Paperwork_Id', N'Tramite_Id', 'COLUMN'
EXEC sp_rename N'dbo.ArchivoTramite.FileName', N'NombreArchivo', 'COLUMN'
EXEC sp_rename N'dbo.ArchivoTramite.FileContent', N'Contenido', 'COLUMN'
EXEC sp_rename N'dbo.ArchivoTramite.UploadedDate', N'FechaCarga', 'COLUMN'
EXEC sp_rename N'dbo.Marca.Description', N'Descripcion', 'COLUMN'
EXEC sp_rename N'dbo.Modelo.Make_Id', N'Marca_Id', 'COLUMN'
EXEC sp_rename N'dbo.Modelo.Description', N'Descripcion', 'COLUMN'
EXEC sp_rename N'dbo.Version.Make_Id', N'Marca_Id', 'COLUMN'
EXEC sp_rename N'dbo.Version.Model_Id', N'Modelo_Id', 'COLUMN'
EXEC sp_rename N'dbo.Version.Description', N'Descripcion', 'COLUMN'
EXEC sp_rename N'dbo.Color.Description', N'Descripcion', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Number', N'Numero', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Person_Id', N'Persona_Id', 'COLUMN'
EXEC sp_rename N'dbo.Compra.ProspectVehicle_Id', N'VehiculoProspecto_Id', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Salesman_Id', N'Vendedor_Id', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Status', N'Estado', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Rejection_Reason', N'MotivoRechazo', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Creation_Date', N'Fecha', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Accepted_Appraisal_Id', N'TasacionAceptada_Id', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Agreed_Price', N'PrecioAcordado', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Owner_CUIL_CUIT', N'CuilCuitTitular', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Contract_Signed_Date', N'FechaFirmaContrato', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Precharge_Code_08', N'CodigoPrecarga08', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Transfer_Appointment_Date', N'FechaCitaTransferencia', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Vehicle_Id', N'Vehiculo_Id', 'COLUMN'
EXEC sp_rename N'dbo.Compra.Closing_Date', N'FechaCierre', 'COLUMN'
EXEC sp_rename N'dbo.VehiculoProspecto.License_Plate', N'Patente', 'COLUMN'
EXEC sp_rename N'dbo.VehiculoProspecto.Chassis_Number', N'NumeroChasis', 'COLUMN'
EXEC sp_rename N'dbo.VehiculoProspecto.Engine_Number', N'NumeroMotor', 'COLUMN'
EXEC sp_rename N'dbo.VehiculoProspecto.Make_Id', N'Marca_Id', 'COLUMN'
EXEC sp_rename N'dbo.VehiculoProspecto.Model_Id', N'Modelo_Id', 'COLUMN'
EXEC sp_rename N'dbo.VehiculoProspecto.Colour_Id', N'Color_Id', 'COLUMN'
EXEC sp_rename N'dbo.VehiculoProspecto.Year', N'Anio', 'COLUMN'
EXEC sp_rename N'dbo.VehiculoProspecto.Doors', N'Puertas', 'COLUMN'
EXEC sp_rename N'dbo.VehiculoProspecto.Declared_Kilometers', N'Kilometraje', 'COLUMN'
EXEC sp_rename N'dbo.Inspeccion.Acquisition_Id', N'Compra_Id', 'COLUMN'
EXEC sp_rename N'dbo.Inspeccion.Mechanic_Id', N'Mecanico_Id', 'COLUMN'
EXEC sp_rename N'dbo.Inspeccion.Scheduled_Date', N'FechaProgramada', 'COLUMN'
EXEC sp_rename N'dbo.Inspeccion.Performed_Date', N'FechaRealizada', 'COLUMN'
EXEC sp_rename N'dbo.Inspeccion.Verified_Kilometers', N'KilometrajeVerificado', 'COLUMN'
EXEC sp_rename N'dbo.Inspeccion.Result', N'Resultado', 'COLUMN'
EXEC sp_rename N'dbo.Inspeccion.Observations', N'Observaciones', 'COLUMN'
EXEC sp_rename N'dbo.FotografiaInspeccion.Inspection_Id', N'Inspeccion_Id', 'COLUMN'
EXEC sp_rename N'dbo.FotografiaInspeccion.FileName', N'NombreArchivo', 'COLUMN'
EXEC sp_rename N'dbo.FotografiaInspeccion.FileContent', N'Contenido', 'COLUMN'
EXEC sp_rename N'dbo.FotografiaInspeccion.UploadedDate', N'FechaCarga', 'COLUMN'
EXEC sp_rename N'dbo.PuntajeInspeccion.Inspection_Id', N'Inspeccion_Id', 'COLUMN'
EXEC sp_rename N'dbo.PuntajeInspeccion.InspectionItem_Id', N'ItemInspeccion_Id', 'COLUMN'
EXEC sp_rename N'dbo.PuntajeInspeccion.Score', N'Puntaje', 'COLUMN'
EXEC sp_rename N'dbo.PuntajeInspeccion.Observations', N'Observaciones', 'COLUMN'
EXEC sp_rename N'dbo.ItemInspeccion.Description', N'Descripcion', 'COLUMN'
EXEC sp_rename N'dbo.ItemInspeccion.Display_Order', N'Orden', 'COLUMN'
EXEC sp_rename N'dbo.Tasacion.Acquisition_Id', N'Compra_Id', 'COLUMN'
EXEC sp_rename N'dbo.Tasacion.Inspection_Id', N'Inspeccion_Id', 'COLUMN'
EXEC sp_rename N'dbo.Tasacion.User_Id', N'Tasador_Id', 'COLUMN'
EXEC sp_rename N'dbo.Tasacion.Amount', N'Valor', 'COLUMN'
EXEC sp_rename N'dbo.Tasacion.Justification', N'Justificacion', 'COLUMN'
EXEC sp_rename N'dbo.Tasacion.Creation_Date', N'Fecha', 'COLUMN'
EXEC sp_rename N'dbo.Tasacion.Expiration_Date', N'Vigencia', 'COLUMN'
EXEC sp_rename N'dbo.FormaPagoCompra.Acquisition_Id', N'Compra_Id', 'COLUMN'
EXEC sp_rename N'dbo.FormaPagoCompra.PaymentType_Id', N'TipoPago_Id', 'COLUMN'
EXEC sp_rename N'dbo.FormaPagoCompra.Amount', N'Monto', 'COLUMN'
EXEC sp_rename N'dbo.PagoCompra.Acquisition_Id', N'Compra_Id', 'COLUMN'
EXEC sp_rename N'dbo.PagoCompra.PaymentType_Id', N'TipoPago_Id', 'COLUMN'
EXEC sp_rename N'dbo.PagoCompra.Cashier_Id', N'Cajero_Id', 'COLUMN'
EXEC sp_rename N'dbo.PagoCompra.Amount', N'Monto', 'COLUMN'
EXEC sp_rename N'dbo.PagoCompra.Bank', N'Banco', 'COLUMN'
EXEC sp_rename N'dbo.PagoCompra.Cbu_Alias', N'CbuAlias', 'COLUMN'
EXEC sp_rename N'dbo.PagoCompra.Operation_Number', N'NumeroOperacion', 'COLUMN'
EXEC sp_rename N'dbo.PagoCompra.Check_Number', N'NumeroCheque', 'COLUMN'
EXEC sp_rename N'dbo.PagoCompra.Payment_Date', N'FechaPago', 'COLUMN'
EXEC sp_rename N'dbo.PagoCompra.Creation_Date', N'FechaCreacion', 'COLUMN'
EXEC sp_rename N'dbo.DocumentoCompra.Acquisition_Id', N'Compra_Id', 'COLUMN'
EXEC sp_rename N'dbo.DocumentoCompra.DocumentType_Id', N'TipoDocumentoCompra_Id', 'COLUMN'
EXEC sp_rename N'dbo.DocumentoCompra.Status', N'Estado', 'COLUMN'
EXEC sp_rename N'dbo.DocumentoCompra.Issue_Date', N'FechaEmision', 'COLUMN'
EXEC sp_rename N'dbo.DocumentoCompra.Observation', N'Observacion', 'COLUMN'
EXEC sp_rename N'dbo.DocumentoCompra.User_Id', N'UsuarioCarga_Id', 'COLUMN'
EXEC sp_rename N'dbo.DocumentoCompra.Uploaded_Date', N'FechaCarga', 'COLUMN'
EXEC sp_rename N'dbo.TipoDocumentoCompra.Description', N'Descripcion', 'COLUMN'
EXEC sp_rename N'dbo.TipoDocumentoCompra.Stage', N'Etapa', 'COLUMN'
EXEC sp_rename N'dbo.TipoDocumentoCompra.Validity_Days', N'DiasVigencia', 'COLUMN'
EXEC sp_rename N'dbo.TipoDocumentoCompra.Display_Order', N'Orden', 'COLUMN'
EXEC sp_rename N'dbo.ArchivoDocumentoCompra.Document_Id', N'DocumentoCompra_Id', 'COLUMN'
EXEC sp_rename N'dbo.ArchivoDocumentoCompra.FileName', N'NombreArchivo', 'COLUMN'
EXEC sp_rename N'dbo.ArchivoDocumentoCompra.FileContent', N'Contenido', 'COLUMN'
EXEC sp_rename N'dbo.ComprobanteCompra.Number', N'Numero', 'COLUMN'
EXEC sp_rename N'dbo.ComprobanteCompra.Acquisition_Id', N'Compra_Id', 'COLUMN'
EXEC sp_rename N'dbo.ComprobanteCompra.Issue_Date', N'Fecha', 'COLUMN'
EXEC sp_rename N'dbo.ComprobanteCompra.Person_Name', N'NombrePersona', 'COLUMN'
EXEC sp_rename N'dbo.ComprobanteCompra.Person_CUIL_CUIT', N'CuilCuit', 'COLUMN'
EXEC sp_rename N'dbo.ComprobanteCompra.Detail', N'Detalle', 'COLUMN'
EXEC sp_rename N'dbo.ComprobanteCompra.Net_Amount', N'MontoNeto', 'COLUMN'
EXEC sp_rename N'dbo.ComprobanteCompra.Tax_Amount', N'MontoImpuesto', 'COLUMN'
EXEC sp_rename N'dbo.ComprobanteCompra.Total_Amount', N'MontoTotal', 'COLUMN'
EXEC sp_rename N'dbo.Usuario.Dni', N'DNI', 'COLUMN'
EXEC sp_rename N'dbo.Usuario.Username', N'NombreUsuario', 'COLUMN'
EXEC sp_rename N'dbo.Usuario.Password', N'Clave', 'COLUMN'
EXEC sp_rename N'dbo.Usuario.Name', N'Nombre', 'COLUMN'
EXEC sp_rename N'dbo.Usuario.Surname', N'Apellido', 'COLUMN'
EXEC sp_rename N'dbo.Usuario.Role_Id', N'Rol_Id', 'COLUMN'
EXEC sp_rename N'dbo.Usuario.Blocked', N'Bloqueado', 'COLUMN'
EXEC sp_rename N'dbo.Usuario.Active', N'Activo', 'COLUMN'
EXEC sp_rename N'dbo.Usuario.Available_Login_Attempts', N'IntentosDisponibles', 'COLUMN'
EXEC sp_rename N'dbo.Usuario.Language_Code', N'CodigoIdioma', 'COLUMN'
EXEC sp_rename N'dbo.Permiso.Name', N'Nombre', 'COLUMN'
EXEC sp_rename N'dbo.Permiso.Type', N'Tipo', 'COLUMN'
EXEC sp_rename N'dbo.Permiso_Permiso.Father_Id', N'Padre_Id', 'COLUMN'
EXEC sp_rename N'dbo.Permiso_Permiso.Child_Id', N'Hijo_Id', 'COLUMN'
EXEC sp_rename N'dbo.BitacoraEventos.UserId', N'Usuario_Id', 'COLUMN'
EXEC sp_rename N'dbo.BitacoraEventos.AttemptedLogin', N'LoginIntentado', 'COLUMN'
EXEC sp_rename N'dbo.BitacoraEventos.OccurredAt', N'Fecha', 'COLUMN'
EXEC sp_rename N'dbo.BitacoraEventos.Module', N'Modulo', 'COLUMN'
EXEC sp_rename N'dbo.BitacoraEventos.EventType', N'TipoEvento', 'COLUMN'
EXEC sp_rename N'dbo.BitacoraEventos.Criticality', N'Criticidad', 'COLUMN'
EXEC sp_rename N'dbo.BitacoraEventos.TargetId', N'Objetivo_Id', 'COLUMN'
EXEC sp_rename N'dbo.TipoEvento.EventType', N'TipoEvento', 'COLUMN'
EXEC sp_rename N'dbo.TipoEvento.Module', N'Modulo', 'COLUMN'
EXEC sp_rename N'dbo.TipoEvento.Criticality', N'Criticidad', 'COLUMN'
EXEC sp_rename N'dbo.DV.SchemaName', N'NombreEsquema', 'COLUMN'
EXEC sp_rename N'dbo.DV.TableName', N'NombreTabla', 'COLUMN'
EXEC sp_rename N'dbo.DV.AlgorithmVersion', N'VersionAlgoritmo', 'COLUMN'
GO

-- 4. Se recrean las restricciones CHECK y los indices filtrados del paso 2, con la misma expresion y el mismo estado
ALTER TABLE dbo.DocumentoCompra WITH CHECK ADD CONSTRAINT CK_DocumentoCompra_Estado CHECK ([Estado]>=(1) AND [Estado]<=(2))
ALTER TABLE dbo.TipoDocumentoCompra WITH CHECK ADD CONSTRAINT CK_TipoDocumentoCompra_Etapa CHECK ([Etapa]>=(1) AND [Etapa]<=(2))
ALTER TABLE dbo.TipoDocumentoCompra WITH CHECK ADD CONSTRAINT CK_TipoDocumentoCompra_DiasVigencia CHECK ([DiasVigencia] IS NULL OR [DiasVigencia]>(0))
ALTER TABLE dbo.PagoCompra WITH CHECK ADD CONSTRAINT CK_PagoCompra_Monto CHECK ([Monto]>(0))
ALTER TABLE dbo.FormaPagoCompra WITH CHECK ADD CONSTRAINT CK_FormaPagoCompra_Monto CHECK ([Monto]>(0))
ALTER TABLE dbo.Compra WITH CHECK ADD CONSTRAINT CK_Compra_PrecioAcordado CHECK ([PrecioAcordado] IS NULL OR [PrecioAcordado]>(0))
ALTER TABLE dbo.Compra WITH CHECK ADD CONSTRAINT CK_Compra_MotivoRechazo CHECK ([MotivoRechazo] IS NULL OR [MotivoRechazo]>=(1) AND [MotivoRechazo]<=(3))
ALTER TABLE dbo.Compra WITH CHECK ADD CONSTRAINT CK_Compra_Estado CHECK ([Estado]>=(1) AND [Estado]<=(10))
ALTER TABLE dbo.Tasacion WITH CHECK ADD CONSTRAINT CK_Tasacion_Valor CHECK ([Valor]>(0))
ALTER TABLE dbo.Tasacion WITH CHECK ADD CONSTRAINT CK_Tasacion_Vigencia CHECK ([Vigencia]>[Fecha])
ALTER TABLE dbo.DV WITH CHECK ADD CONSTRAINT CK_DV_VersionAlgoritmo CHECK ([VersionAlgoritmo]=(1))
ALTER TABLE dbo.BitacoraEventos WITH CHECK ADD CONSTRAINT CK_BitacoraEventos_Criticidad CHECK ([Criticidad]>=(1) AND [Criticidad]<=(5))
ALTER TABLE dbo.TipoEvento WITH CHECK ADD CONSTRAINT CK_TipoEvento_Criticidad CHECK ([Criticidad]>=(1) AND [Criticidad]<=(5))
ALTER TABLE dbo.Inspeccion WITH CHECK ADD CONSTRAINT CK_Inspeccion_Resultado CHECK ([Resultado] IS NULL OR [Resultado]>=(1) AND [Resultado]<=(3))
ALTER TABLE dbo.Inspeccion WITH CHECK ADD CONSTRAINT CK_Inspeccion_KilometrajeVerificado CHECK ([KilometrajeVerificado] IS NULL OR [KilometrajeVerificado]>=(0))
ALTER TABLE dbo.PuntajeInspeccion WITH CHECK ADD CONSTRAINT CK_PuntajeInspeccion_Puntaje CHECK ([Puntaje]>=(1) AND [Puntaje]<=(3))
ALTER TABLE dbo.ComprobanteCompra WITH CHECK ADD CONSTRAINT CK_ComprobanteCompra_Montos CHECK ([MontoNeto]>(0) AND [MontoImpuesto]>=(0) AND [MontoTotal]>(0))
CREATE UNIQUE NONCLUSTERED INDEX UX_Compra_Vehiculo ON dbo.Compra ([Vehiculo_Id] ASC) WHERE ([Vehiculo_Id] IS NOT NULL) WITH (IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]

-- 5. Resto de las restricciones e indices
EXEC sp_rename N'dbo.FK_AcquisitionDocumentFiles_AcquisitionDocuments', N'FK_ArchivoDocumentoCompra_DocumentoCompra', 'OBJECT'
EXEC sp_rename N'dbo.PK_AcquisitionDocumentFiles', N'PK_ArchivoDocumentoCompra', 'OBJECT'
EXEC sp_rename N'dbo.CK_AcquisitionDocuments_DVH', N'CK_DocumentoCompra_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_AcquisitionDocuments_AcquisitionDocumentTypes', N'FK_DocumentoCompra_TipoDocumentoCompra', 'OBJECT'
EXEC sp_rename N'dbo.FK_AcquisitionDocuments_Acquisitions', N'FK_DocumentoCompra_Compra', 'OBJECT'
EXEC sp_rename N'dbo.FK_AcquisitionDocuments_Users', N'FK_DocumentoCompra_Usuario', 'OBJECT'
EXEC sp_rename N'dbo.PK_AcquisitionDocuments', N'PK_DocumentoCompra', 'OBJECT'
EXEC sp_rename N'dbo.UQ_AcquisitionDocuments_Type', N'UQ_DocumentoCompra_TipoDocumentoCompra', 'OBJECT'
EXEC sp_rename N'dbo.CK_AcquisitionDocumentTypes_DVH', N'CK_TipoDocumentoCompra_DVH', 'OBJECT'
EXEC sp_rename N'dbo.PK_AcquisitionDocumentTypes', N'PK_TipoDocumentoCompra', 'OBJECT'
EXEC sp_rename N'dbo.CK_AcquisitionPayments_DVH', N'CK_PagoCompra_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_AcquisitionPayments_AcquisitionPaymentTerms', N'FK_PagoCompra_FormaPagoCompra', 'OBJECT'
EXEC sp_rename N'dbo.FK_AcquisitionPayments_Users', N'FK_PagoCompra_Usuario', 'OBJECT'
EXEC sp_rename N'dbo.PagoCompra.IX_AcquisitionPayments_Acquisition', N'IX_PagoCompra_Compra', 'INDEX'
EXEC sp_rename N'dbo.PK_AcquisitionPayments', N'PK_PagoCompra', 'OBJECT'
EXEC sp_rename N'dbo.CK_AcquisitionPaymentTerms_DVH', N'CK_FormaPagoCompra_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_AcquisitionPaymentTerms_Acquisitions', N'FK_FormaPagoCompra_Compra', 'OBJECT'
EXEC sp_rename N'dbo.FK_AcquisitionPaymentTerms_PaymentTypes', N'FK_FormaPagoCompra_TipoPago', 'OBJECT'
EXEC sp_rename N'dbo.PK_AcquisitionPaymentTerms', N'PK_FormaPagoCompra', 'OBJECT'
EXEC sp_rename N'dbo.CK_Acquisitions_DVH', N'CK_Compra_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_Acquisitions_Appraisals', N'FK_Compra_Tasacion', 'OBJECT'
EXEC sp_rename N'dbo.FK_Acquisitions_People', N'FK_Compra_Persona', 'OBJECT'
EXEC sp_rename N'dbo.FK_Acquisitions_ProspectVehicles', N'FK_Compra_VehiculoProspecto', 'OBJECT'
EXEC sp_rename N'dbo.FK_Acquisitions_Users', N'FK_Compra_Usuario', 'OBJECT'
EXEC sp_rename N'dbo.FK_Acquisitions_Vehicles', N'FK_Compra_Vehiculo', 'OBJECT'
EXEC sp_rename N'dbo.Compra.IX_Acquisitions_Person', N'IX_Compra_Persona', 'INDEX'
EXEC sp_rename N'dbo.Compra.IX_Acquisitions_Status', N'IX_Compra_Estado', 'INDEX'
EXEC sp_rename N'dbo.PK_Acquisitions', N'PK_Compra', 'OBJECT'
EXEC sp_rename N'dbo.UQ_Acquisitions_Number', N'UQ_Compra_Numero', 'OBJECT'
EXEC sp_rename N'dbo.UQ_Acquisitions_ProspectVehicle', N'UQ_Compra_VehiculoProspecto', 'OBJECT'
EXEC sp_rename N'dbo.CK_Appraisals_DVH', N'CK_Tasacion_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_Appraisals_Acquisitions', N'FK_Tasacion_Compra', 'OBJECT'
EXEC sp_rename N'dbo.FK_Appraisals_Inspections', N'FK_Tasacion_Inspeccion', 'OBJECT'
EXEC sp_rename N'dbo.FK_Appraisals_Users', N'FK_Tasacion_Usuario', 'OBJECT'
EXEC sp_rename N'dbo.Tasacion.IX_Appraisals_Acquisition', N'IX_Tasacion_Compra', 'INDEX'
EXEC sp_rename N'dbo.PK_Appraisals', N'PK_Tasacion', 'OBJECT'
EXEC sp_rename N'dbo.CK_Colours_DVH', N'CK_Color_DVH', 'OBJECT'
EXEC sp_rename N'dbo.PK_Colours', N'PK_Color', 'OBJECT'
EXEC sp_rename N'dbo.FK_Events_Type', N'FK_BitacoraEventos_TipoEvento', 'OBJECT'
EXEC sp_rename N'dbo.FK_Events_User', N'FK_BitacoraEventos_Usuario', 'OBJECT'
EXEC sp_rename N'dbo.BitacoraEventos.IX_Events_Date', N'IX_BitacoraEventos_Fecha', 'INDEX'
EXEC sp_rename N'dbo.BitacoraEventos.IX_Events_User', N'IX_BitacoraEventos_Usuario', 'INDEX'
EXEC sp_rename N'dbo.PK_Events', N'PK_BitacoraEventos', 'OBJECT'
EXEC sp_rename N'dbo.PK_EventTypes', N'PK_TipoEvento', 'OBJECT'
EXEC sp_rename N'dbo.UQ_EventTypes_Definition', N'UQ_TipoEvento_Definition', 'OBJECT'
EXEC sp_rename N'dbo.CK_InspectionItems_DVH', N'CK_ItemInspeccion_DVH', 'OBJECT'
EXEC sp_rename N'dbo.PK_InspectionItems', N'PK_ItemInspeccion', 'OBJECT'
EXEC sp_rename N'dbo.DF_InspectionPhotos_Id', N'DF_FotografiaInspeccion_Id', 'OBJECT'
EXEC sp_rename N'dbo.DF_InspectionPhotos_UploadedDate', N'DF_FotografiaInspeccion_FechaCarga', 'OBJECT'
EXEC sp_rename N'dbo.FK_InspectionPhotos_Inspections', N'FK_FotografiaInspeccion_Inspeccion', 'OBJECT'
EXEC sp_rename N'dbo.FotografiaInspeccion.IX_InspectionPhotos_Inspection', N'IX_FotografiaInspeccion_Inspeccion', 'INDEX'
EXEC sp_rename N'dbo.PK_InspectionPhotos', N'PK_FotografiaInspeccion', 'OBJECT'
EXEC sp_rename N'dbo.CK_Inspections_DVH', N'CK_Inspeccion_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_Inspections_Acquisitions', N'FK_Inspeccion_Compra', 'OBJECT'
EXEC sp_rename N'dbo.FK_Inspections_Users', N'FK_Inspeccion_Usuario', 'OBJECT'
EXEC sp_rename N'dbo.Inspeccion.IX_Inspections_Mechanic_Schedule', N'IX_Inspeccion_Mecanico_FechaProgramada', 'INDEX'
EXEC sp_rename N'dbo.PK_Inspections', N'PK_Inspeccion', 'OBJECT'
EXEC sp_rename N'dbo.UQ_Inspections_Acquisition', N'UQ_Inspeccion_Compra', 'OBJECT'
EXEC sp_rename N'dbo.CK_InspectionScores_DVH', N'CK_PuntajeInspeccion_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_InspectionScores_InspectionItems', N'FK_PuntajeInspeccion_ItemInspeccion', 'OBJECT'
EXEC sp_rename N'dbo.FK_InspectionScores_Inspections', N'FK_PuntajeInspeccion_Inspeccion', 'OBJECT'
EXEC sp_rename N'dbo.PK_InspectionScores', N'PK_PuntajeInspeccion', 'OBJECT'
EXEC sp_rename N'dbo.CK_Invoice_DVH', N'CK_Factura_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_Invoice_People', N'FK_Factura_Persona', 'OBJECT'
EXEC sp_rename N'dbo.FK_Invoice_Reservations', N'FK_Factura_Reserva', 'OBJECT'
EXEC sp_rename N'dbo.FK_Invoice_Vehicles', N'FK_Factura_Vehiculo', 'OBJECT'
EXEC sp_rename N'dbo.PK_Invoice', N'PK_Factura', 'OBJECT'
EXEC sp_rename N'dbo.CK_Makes_DVH', N'CK_Marca_DVH', 'OBJECT'
EXEC sp_rename N'dbo.PK_Makes', N'PK_Marca', 'OBJECT'
EXEC sp_rename N'dbo.CK_Models_DVH', N'CK_Modelo_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_Models_Makes', N'FK_Modelo_Marca', 'OBJECT'
EXEC sp_rename N'dbo.Modelo.IXFK_Models_Makes', N'IXFK_Modelo_Marca', 'INDEX'
EXEC sp_rename N'dbo.PK_Models', N'PK_Modelo', 'OBJECT'
EXEC sp_rename N'dbo.CK_Paperwork_DVH', N'CK_Tramite_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_Paperwork_Invoice', N'FK_Tramite_Factura', 'OBJECT'
EXEC sp_rename N'dbo.FK_Paperwork_Vehicles', N'FK_Tramite_Vehiculo', 'OBJECT'
EXEC sp_rename N'dbo.PK_PaperWork', N'PK_Tramite', 'OBJECT'
EXEC sp_rename N'dbo.DF__Paperwork__Uploa__2AA05119', N'DF_ArchivoTramite_FechaCarga', 'OBJECT'
EXEC sp_rename N'dbo.DF__PaperworkFil__Id__29AC2CE0', N'DF_ArchivoTramite_Id', 'OBJECT'
EXEC sp_rename N'dbo.FK_PaperworkFiles_Paperwork', N'FK_ArchivoTramite_Tramite', 'OBJECT'
EXEC sp_rename N'dbo.PK__Paperwor__3214EC07AC3AAEA4', N'PK_ArchivoTramite', 'OBJECT'
EXEC sp_rename N'dbo.CK_Payments_DVH', N'CK_Pago_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_Payments_Invoice', N'FK_Pago_Factura', 'OBJECT'
EXEC sp_rename N'dbo.FK_Payments_PaymentTypes', N'FK_Pago_TipoPago', 'OBJECT'
EXEC sp_rename N'dbo.PK_Payments', N'PK_Pago', 'OBJECT'
EXEC sp_rename N'dbo.CK_PaymentTypes_DVH', N'CK_TipoPago_DVH', 'OBJECT'
EXEC sp_rename N'dbo.PK_PaymentTypes', N'PK_TipoPago', 'OBJECT'
EXEC sp_rename N'dbo.CK_People_DVH', N'CK_Persona_DVH', 'OBJECT'
EXEC sp_rename N'dbo.PK_People', N'PK_Persona', 'OBJECT'
EXEC sp_rename N'dbo.Dni', N'UQ_Persona_DNI', 'OBJECT'
EXEC sp_rename N'dbo.CK_Permission_Permission_DVH', N'CK_Permiso_Permiso_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_Permissions_Permission_Permission', N'FK_Permiso_Permiso_Padre', 'OBJECT'
EXEC sp_rename N'dbo.FK_Permissions_Permission_Permission2', N'FK_Permiso_Permiso_Hijo', 'OBJECT'
EXEC sp_rename N'dbo.PK_Permission_Permission', N'PK_Permiso_Permiso', 'OBJECT'
EXEC sp_rename N'dbo.CK_Permissions_DVH', N'CK_Permiso_DVH', 'OBJECT'
EXEC sp_rename N'dbo.PK_Permissions', N'PK_Permiso', 'OBJECT'
EXEC sp_rename N'dbo.CK_ProspectVehicles_DVH', N'CK_VehiculoProspecto_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_ProspectVehicles_Colours', N'FK_VehiculoProspecto_Color', 'OBJECT'
EXEC sp_rename N'dbo.FK_ProspectVehicles_Makes', N'FK_VehiculoProspecto_Marca', 'OBJECT'
EXEC sp_rename N'dbo.FK_ProspectVehicles_Models', N'FK_VehiculoProspecto_Modelo', 'OBJECT'
EXEC sp_rename N'dbo.FK_ProspectVehicles_Versions', N'FK_VehiculoProspecto_Version', 'OBJECT'
EXEC sp_rename N'dbo.VehiculoProspecto.IX_ProspectVehicles_Chassis_Number', N'IX_VehiculoProspecto_NumeroChasis', 'INDEX'
EXEC sp_rename N'dbo.VehiculoProspecto.IX_ProspectVehicles_Engine_Number', N'IX_VehiculoProspecto_NumeroMotor', 'INDEX'
EXEC sp_rename N'dbo.VehiculoProspecto.IX_ProspectVehicles_License_Plate', N'IX_VehiculoProspecto_Patente', 'INDEX'
EXEC sp_rename N'dbo.PK_ProspectVehicles', N'PK_VehiculoProspecto', 'OBJECT'
EXEC sp_rename N'dbo.CK_PurchaseReceipts_DVH', N'CK_ComprobanteCompra_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_PurchaseReceipts_Acquisitions', N'FK_ComprobanteCompra_Compra', 'OBJECT'
EXEC sp_rename N'dbo.PK_PurchaseReceipts', N'PK_ComprobanteCompra', 'OBJECT'
EXEC sp_rename N'dbo.UQ_PurchaseReceipts_Acquisition', N'UQ_ComprobanteCompra_Compra', 'OBJECT'
EXEC sp_rename N'dbo.UQ_PurchaseReceipts_Number', N'UQ_ComprobanteCompra_Numero', 'OBJECT'
EXEC sp_rename N'dbo.CK_Quotations_DVH', N'CK_Cotizacion_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_Quotation_People', N'FK_Cotizacion_Persona', 'OBJECT'
EXEC sp_rename N'dbo.FK_Quotation_Vehicles', N'FK_Cotizacion_Vehiculo', 'OBJECT'
EXEC sp_rename N'dbo.Cotizacion.IXFK_Quotation_Vehicles', N'IXFK_Cotizacion_Vehiculo', 'INDEX'
EXEC sp_rename N'dbo.PK_Quotation', N'PK_Cotizacion', 'OBJECT'
EXEC sp_rename N'dbo.CK_Reservation_DVH', N'CK_Reserva_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_Reservation_People', N'FK_Reserva_Persona', 'OBJECT'
EXEC sp_rename N'dbo.FK_Reservation_Vehicles', N'FK_Reserva_Vehiculo', 'OBJECT'
EXEC sp_rename N'dbo.Reserva.IXFK_Reservation_Vehicles', N'IXFK_Reserva_Vehiculo', 'INDEX'
EXEC sp_rename N'dbo.PK_Reservation', N'PK_Reserva', 'OBJECT'
EXEC sp_rename N'dbo.CK_Users_DVH', N'CK_Usuario_DVH', 'OBJECT'
EXEC sp_rename N'dbo.PK_Users', N'PK_Usuario', 'OBJECT'
EXEC sp_rename N'dbo.CK_Vehicles_DVH', N'CK_Vehiculo_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_Vehicles_Colours', N'FK_Vehiculo_Color', 'OBJECT'
EXEC sp_rename N'dbo.FK_Vehicles_Makes', N'FK_Vehiculo_Marca', 'OBJECT'
EXEC sp_rename N'dbo.FK_Vehicles_Models', N'FK_Vehiculo_Modelo', 'OBJECT'
EXEC sp_rename N'dbo.FK_Vehicles_Versions', N'FK_Vehiculo_Version', 'OBJECT'
EXEC sp_rename N'dbo.Vehiculo.IXFK_Vehicles_Colours', N'IXFK_Vehiculo_Color', 'INDEX'
EXEC sp_rename N'dbo.Vehiculo.IXFK_Vehicles_Makes', N'IXFK_Vehiculo_Marca', 'INDEX'
EXEC sp_rename N'dbo.Vehiculo.IXFK_Vehicles_Models', N'IXFK_Vehiculo_Modelo', 'INDEX'
EXEC sp_rename N'dbo.Vehiculo.IXFK_Vehicles_Versions', N'IXFK_Vehiculo_Version', 'INDEX'
EXEC sp_rename N'dbo.PK_Vehicles', N'PK_Vehiculo', 'OBJECT'
EXEC sp_rename N'dbo.Licence_plate', N'UQ_Vehiculo_Patente', 'OBJECT'
EXEC sp_rename N'dbo.CK_Versions_DVH', N'CK_Version_DVH', 'OBJECT'
EXEC sp_rename N'dbo.FK_Versions_Makes', N'FK_Version_Marca', 'OBJECT'
EXEC sp_rename N'dbo.FK_Versions_Models', N'FK_Version_Modelo', 'OBJECT'
EXEC sp_rename N'dbo.Version.IXFK_Versions_Makes', N'IXFK_Version_Marca', 'INDEX'
EXEC sp_rename N'dbo.Version.IXFK_Versions_Models', N'IXFK_Version_Modelo', 'INDEX'
EXEC sp_rename N'dbo.PK_Versions', N'PK_Version', 'OBJECT'

-- 6. Secuencias
EXEC sp_rename N'dbo.AcquisitionNumbers', N'CompraNumbers', 'OBJECT'
EXEC sp_rename N'dbo.PurchaseReceiptNumbers', N'ComprobanteCompraNumbers', 'OBJECT'
GO

-- 7. Datos que son nombres (en otro lote: SQL Server compila el lote antes de correr los sp_rename)
UPDATE dbo.DV SET NombreTabla = CASE NombreTabla
    WHEN N'People' THEN N'Persona'
    WHEN N'Vehicles' THEN N'Vehiculo'
    WHEN N'Quotations' THEN N'Cotizacion'
    WHEN N'Reservation' THEN N'Reserva'
    WHEN N'Invoice' THEN N'Factura'
    WHEN N'Payments' THEN N'Pago'
    WHEN N'PaymentTypes' THEN N'TipoPago'
    WHEN N'Paperwork' THEN N'Tramite'
    WHEN N'PaperworkFiles' THEN N'ArchivoTramite'
    WHEN N'Makes' THEN N'Marca'
    WHEN N'Models' THEN N'Modelo'
    WHEN N'Versions' THEN N'Version'
    WHEN N'Colours' THEN N'Color'
    WHEN N'Acquisitions' THEN N'Compra'
    WHEN N'ProspectVehicles' THEN N'VehiculoProspecto'
    WHEN N'Inspections' THEN N'Inspeccion'
    WHEN N'InspectionPhotos' THEN N'FotografiaInspeccion'
    WHEN N'InspectionScores' THEN N'PuntajeInspeccion'
    WHEN N'InspectionItems' THEN N'ItemInspeccion'
    WHEN N'Appraisals' THEN N'Tasacion'
    WHEN N'AcquisitionPaymentTerms' THEN N'FormaPagoCompra'
    WHEN N'AcquisitionPayments' THEN N'PagoCompra'
    WHEN N'AcquisitionDocuments' THEN N'DocumentoCompra'
    WHEN N'AcquisitionDocumentTypes' THEN N'TipoDocumentoCompra'
    WHEN N'AcquisitionDocumentFiles' THEN N'ArchivoDocumentoCompra'
    WHEN N'PurchaseReceipts' THEN N'ComprobanteCompra'
    WHEN N'Users' THEN N'Usuario'
    WHEN N'Permissions' THEN N'Permiso'
    WHEN N'Permission_Permission' THEN N'Permiso_Permiso'
    WHEN N'Events' THEN N'BitacoraEventos'
    WHEN N'EventTypes' THEN N'TipoEvento'
    ELSE NombreTabla END
WHERE NombreTabla IN (N'People', N'Vehicles', N'Quotations', N'Reservation', N'Invoice', N'Payments', N'PaymentTypes', N'Paperwork', N'PaperworkFiles', N'Makes', N'Models', N'Versions', N'Colours', N'Acquisitions', N'ProspectVehicles', N'Inspections', N'InspectionPhotos', N'InspectionScores', N'InspectionItems', N'Appraisals', N'AcquisitionPaymentTerms', N'AcquisitionPayments', N'AcquisitionDocuments', N'AcquisitionDocumentTypes', N'AcquisitionDocumentFiles', N'PurchaseReceipts', N'Users', N'Permissions', N'Permission_Permission', N'Events', N'EventTypes')
UPDATE dbo.Permiso SET Tipo = CASE Tipo
    WHEN 'GenerateQuotationForm' THEN 'GenerateCotizacionForm'
    WHEN 'CreatePersonForm' THEN 'CreatePersonaForm'
    WHEN 'GenerateReservationForm' THEN 'GenerateReservaForm'
    WHEN 'GenerateInvoiceForm' THEN 'GenerateFacturaForm'
    WHEN 'AddPaymentForm' THEN 'AddPagoForm'
    WHEN 'ManagePaperworkForm' THEN 'ManageTramiteForm'
    WHEN 'PaperworkForm' THEN 'TramiteForm'
    WHEN 'RegisterAcquisitionForm' THEN 'RegisterCompraForm'
    WHEN 'RegisterInspectionForm' THEN 'RegisterInspeccionForm'
    WHEN 'AppraiseVehicleForm' THEN 'AppraiseVehiculoForm'
    WHEN 'ManageAcquisitionForm' THEN 'ManageCompraForm'
    WHEN 'PayAcquisitionForm' THEN 'PayCompraForm'
    WHEN 'UserManagementForm' THEN 'UsuarioManagementForm'
    WHEN 'PermissionManagementForm' THEN 'PermisoManagementForm'
    WHEN 'EventLogForm' THEN 'BitacoraEventosForm'
    WHEN 'VehicleManagementForm' THEN 'VehiculoManagementForm'
    WHEN 'VehicleModelConfigurationForm' THEN 'VehiculoModeloConfigurationForm'
    ELSE Tipo END
WHERE Tipo IN ('GenerateQuotationForm', 'CreatePersonForm', 'GenerateReservationForm', 'GenerateInvoiceForm', 'AddPaymentForm', 'ManagePaperworkForm', 'PaperworkForm', 'RegisterAcquisitionForm', 'RegisterInspectionForm', 'AppraiseVehicleForm', 'ManageAcquisitionForm', 'PayAcquisitionForm', 'UserManagementForm', 'PermissionManagementForm', 'EventLogForm', 'VehicleManagementForm', 'VehicleModelConfigurationForm')
GO

-- 8. Tipos de tabla nuevos (los de antes no se pueden renombrar por columna)
CREATE TYPE [dbo].[PuntajeInspeccionRows] AS TABLE(
	[ItemInspeccion_Id] [uniqueidentifier] NOT NULL,
	[Puntaje] [int] NOT NULL,
	[Observaciones] [varchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[ItemInspeccion_Id] ASC
)WITH (IGNORE_DUP_KEY = OFF),
	CHECK ((len([DVH])=(64) AND NOT [DVH] like '%[^0-9A-F]%'))
)
GO
CREATE TYPE [dbo].[UsuarioDvhUpdates] AS TABLE(
	[Id] [uniqueidentifier] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
	PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF),
	CHECK ((len([DVH])=(64) AND NOT [DVH] like '%[^0-9A-F]%'))
)
GO

-- 9. Procedimientos: se borran los viejos y se crean con los nombres nuevos, con el mismo cuerpo
DROP PROCEDURE dbo.AcquisitionDocumentFiles_Add, dbo.AcquisitionDocumentFiles_GetByDocument, dbo.AcquisitionDocumentTypes_GetByStage, dbo.AcquisitionDocuments_Add, dbo.AcquisitionDocuments_Delete, dbo.AcquisitionDocuments_GetByAcquisition, dbo.AcquisitionDocuments_Observe, dbo.AcquisitionPaymentTerms_Add
DROP PROCEDURE dbo.AcquisitionPaymentTerms_DeleteByAcquisition, dbo.AcquisitionPaymentTerms_GetByAcquisition, dbo.AcquisitionPayments_Add, dbo.AcquisitionPayments_GetByAcquisition, dbo.Acquisitions_Add, dbo.Acquisitions_Finish, dbo.Acquisitions_GetById, dbo.Acquisitions_GetDuplicates
DROP PROCEDURE dbo.Acquisitions_GetNextNumber, dbo.Acquisitions_GetPendingAppraisal, dbo.Acquisitions_GetPendingManagement, dbo.Acquisitions_GetPendingPayment, dbo.Acquisitions_GetSummary, dbo.Acquisitions_Pay, dbo.Acquisitions_Update, dbo.Appraisals_Add
DROP PROCEDURE dbo.Appraisals_GetLastByAcquisition, dbo.Colours_GetAll, dbo.DV_GetBaseRoles, dbo.DV_GetByTable, dbo.DV_GetColumns, dbo.DV_GetHorizontal, dbo.DV_GetUsersByRole, dbo.DV_Save
DROP PROCEDURE dbo.DV_ValidateTable, dbo.Events_ActorExists, dbo.Events_GetLogins, dbo.Events_Insert, dbo.Events_Query, dbo.InspectionItems_GetAll, dbo.InspectionPhotos_Add, dbo.InspectionPhotos_Delete
DROP PROCEDURE dbo.InspectionPhotos_GetByInspection, dbo.InspectionScores_GetByInspection, dbo.Inspections_Add, dbo.Inspections_GetById, dbo.Inspections_GetDetail, dbo.Inspections_GetMechanics, dbo.Inspections_GetNextFreeSlot, dbo.Inspections_GetScheduledByMechanic
DROP PROCEDURE dbo.Inspections_IsMechanicBusy, dbo.Inspections_Record, dbo.Invoice_Add, dbo.Invoice_Delete, dbo.Invoice_GetAllPendingOfPaperworkByPerson, dbo.Invoice_GetById, dbo.Invoice_Update, dbo.Makes_Delete
DROP PROCEDURE dbo.Makes_GetAll, dbo.Makes_Insert, dbo.Models_Delete, dbo.Models_GetAllByMake, dbo.Models_Insert, dbo.PaperworkFile_Add, dbo.PaperworkFile_Delete, dbo.PaperworkFile_GetByPaperWork
DROP PROCEDURE dbo.Paperwork_Add, dbo.Paperwork_Delete, dbo.Paperwork_GetAllActiveByPerson, dbo.Paperwork_GetAllByPerson, dbo.Paperwork_GetById, dbo.Paperwork_Update, dbo.PaymentTypes_GetAll, dbo.Payments_Add
DROP PROCEDURE dbo.Payments_Delete, dbo.Payments_GetAllByInvoice, dbo.People_Add, dbo.People_GetByDni, dbo.People_GetById, dbo.Permissions_DeleteCompleteFamily, dbo.Permissions_DeleteFamily, dbo.Permissions_DeletePatent
DROP PROCEDURE dbo.Permissions_GetAll, dbo.Permissions_GetAllByUser, dbo.Permissions_GetFamilies, dbo.Permissions_GetPatents, dbo.Permissions_InsertComponent, dbo.Permissions_Insert_Permission_Permission, dbo.ProspectVehicles_Add, dbo.PurchaseReceipts_GetByAcquisition
DROP PROCEDURE dbo.PurchaseReceipts_GetNextNumber, dbo.Quotations_Add, dbo.Quotations_GetAll, dbo.Quotations_GetAllActiveByPerson, dbo.Quotations_GetById, dbo.Reservation_Add, dbo.Reservation_GetAll, dbo.Reservation_GetAllActiveByPerson
DROP PROCEDURE dbo.Reservation_GetById, dbo.User_Add, dbo.User_Delete, dbo.User_DeleteAllPermissions, dbo.User_GetAll, dbo.User_GetAllByState, dbo.User_GetUserByDni, dbo.User_GetUserById
DROP PROCEDURE dbo.User_GetUserByUsername, dbo.User_InsertPermission, dbo.User_Update, dbo.Vehicles_Add, dbo.Vehicles_Delete, dbo.Vehicles_GetActiveVehiclesByFilters, dbo.Vehicles_GetAll, dbo.Vehicles_GetById
DROP PROCEDURE dbo.Vehicles_Update, dbo.Versions_Delete, dbo.Versions_GetAllByMakeModel, dbo.Versions_Insert
GO
DROP TYPE dbo.InspectionScoreRows
DROP TYPE dbo.UserDvhUpdates
GO

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
-- Alcance de digitos verificadores. Tiene que coincidir con BE/Integrity/DVTables.cs.
CREATE   PROCEDURE dbo.DV_ValidateTable @NombreTabla nvarchar(128)
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
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ArchivoDocumentoCompra_Add @DocumentoCompra_Id uniqueidentifier, @NombreArchivo varchar(255), @Contenido varbinary(max) as

insert into ArchivoDocumentoCompra(DocumentoCompra_Id, NombreArchivo, Contenido)
values (@DocumentoCompra_Id, @NombreArchivo, @Contenido)

select 'success' as SQLResultType,'Acquisition document file added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ArchivoDocumentoCompra_GetByDocument @DocumentoCompra_Id uniqueidentifier as

select	a.DocumentoCompra_Id,
        a.NombreArchivo,
        a.Contenido
from	ArchivoDocumentoCompra a
where	a.DocumentoCompra_Id = @DocumentoCompra_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc TipoDocumentoCompra_GetByStage @Etapa int as

-- Documentos obligatorios de cada etapa de CUF10: 1 Documentar, 2 Transferir.
select	a.Id,
        a.Descripcion,
        a.Etapa,
        a.DiasVigencia,
        a.Orden
from	TipoDocumentoCompra a
where	a.Etapa = @Etapa
order by a.Orden asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc DocumentoCompra_Add	@Id uniqueidentifier, @Compra_Id uniqueidentifier, @TipoDocumentoCompra_Id uniqueidentifier, @Estado int,
											@FechaEmision datetime, @Observacion varchar(max), @UsuarioCarga_Id uniqueidentifier, @FechaCarga datetime, @DVH char(64) as

insert into DocumentoCompra(Id, Compra_Id, TipoDocumentoCompra_Id, Estado, FechaEmision, Observacion, UsuarioCarga_Id, FechaCarga, DVH)
values (@Id, @Compra_Id, @TipoDocumentoCompra_Id, @Estado, @FechaEmision, @Observacion, @UsuarioCarga_Id, @FechaCarga, @DVH)

select 'success' as SQLResultType,'Acquisition document added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc DocumentoCompra_Delete @Id uniqueidentifier as

-- El archivo escaneado se borra en cascada (AcquisitionDocumentFiles).
delete from DocumentoCompra
where Id = @Id

select 'success' as SQLResultType,'Acquisition document deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc DocumentoCompra_GetByAcquisition @Compra_Id uniqueidentifier as

-- Documentos cargados de las dos etapas. Un tipo sin fila es un documento Pendiente.
select	a.Id,
        a.Compra_Id,
        a.TipoDocumentoCompra_Id,
        a.Estado,
        a.FechaEmision,
        a.Observacion,
        a.UsuarioCarga_Id,
        a.FechaCarga,
        b.Descripcion,
        b.Etapa,
        c.NombreArchivo
from	DocumentoCompra a
        join TipoDocumentoCompra b on (b.Id = a.TipoDocumentoCompra_Id)
        left join ArchivoDocumentoCompra c on (c.DocumentoCompra_Id = a.Id)
where	a.Compra_Id = @Compra_Id
order by b.Orden asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc DocumentoCompra_Observe	@Id uniqueidentifier, @Compra_Id uniqueidentifier, @TipoDocumentoCompra_Id uniqueidentifier, @Estado int,
												@FechaEmision datetime, @Observacion varchar(max), @UsuarioCarga_Id uniqueidentifier, @FechaCarga datetime, @DVH char(64) as

-- CUF10 11.1: fila completa, el DVH se recalcula con todas las columnas aunque solo cambien estado y comentario.
update DocumentoCompra
set DVH = @DVH,
	Compra_Id = @Compra_Id,
	TipoDocumentoCompra_Id = @TipoDocumentoCompra_Id,
	Estado = @Estado,
	FechaEmision = @FechaEmision,
	Observacion = @Observacion,
	UsuarioCarga_Id = @UsuarioCarga_Id,
	FechaCarga = @FechaCarga
where Id = @Id

select 'success' as SQLResultType,'Acquisition document observed succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FormaPagoCompra_Add	@Compra_Id uniqueidentifier, @TipoPago_Id uniqueidentifier, @Monto float, @DVH char(64) as

insert into FormaPagoCompra(Compra_Id, TipoPago_Id, Monto, DVH)
values (@Compra_Id, @TipoPago_Id, @Monto, @DVH)

select 'success' as SQLResultType,'Payment term added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FormaPagoCompra_DeleteByAcquisition @Compra_Id uniqueidentifier as

-- La forma de pago se reemplaza completa mientras el contrato no este firmado.
delete from FormaPagoCompra
where Compra_Id = @Compra_Id

select 'success' as SQLResultType,'Payment terms deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FormaPagoCompra_GetByAcquisition @Compra_Id uniqueidentifier as

-- Forma de pago pactada y lo que el cajero ya pago por cada medio (CUF11).
select	a.Compra_Id,
        a.TipoPago_Id,
        b.Descripcion as TipoPago_Descripcion,
        a.Monto,
        isnull((select sum(x.Monto) from PagoCompra x
                where x.Compra_Id = a.Compra_Id and x.TipoPago_Id = a.TipoPago_Id), 0) as MontoPagado
from	FormaPagoCompra a
        join TipoPago b on (b.Id = a.TipoPago_Id)
where	a.Compra_Id = @Compra_Id
order by a.Monto desc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc PagoCompra_Add	@Id uniqueidentifier, @Compra_Id uniqueidentifier, @TipoPago_Id uniqueidentifier, @Cajero_Id uniqueidentifier,
											@Monto float, @Banco varchar(100), @CbuAlias varchar(50), @NumeroOperacion varchar(50),
											@NumeroCheque varchar(30), @FechaPago datetime, @FechaCreacion datetime, @DVH char(64) as

-- La FK contra AcquisitionPaymentTerms impide pagar con un medio que no se pacto en CUF10.
insert into PagoCompra(Id, Compra_Id, TipoPago_Id, Cajero_Id, Monto, Banco, CbuAlias, NumeroOperacion, NumeroCheque, FechaPago, FechaCreacion, DVH)
values (@Id, @Compra_Id, @TipoPago_Id, @Cajero_Id, @Monto, @Banco, @CbuAlias, @NumeroOperacion, @NumeroCheque, @FechaPago, @FechaCreacion, @DVH)

select 'success' as SQLResultType,'Payment added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc PagoCompra_GetByAcquisition @Compra_Id uniqueidentifier as

-- Pagos registrados por el cajero (CUF11 paso 5), con el medio y quien pago.
select	a.Id,
        a.Compra_Id,
        a.TipoPago_Id,
        b.Descripcion as TipoPago_Descripcion,
        a.Cajero_Id,
        (u.Nombre + ' ' + u.Apellido) as Cajero,
        a.Monto,
        a.Banco,
        a.CbuAlias,
        a.NumeroOperacion,
        a.NumeroCheque,
        a.FechaPago,
        a.FechaCreacion
from	PagoCompra a
        join TipoPago b on (b.Id = a.TipoPago_Id)
        join Usuario u on (u.Id = a.Cajero_Id)
where	a.Compra_Id = @Compra_Id
order by a.FechaCreacion asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_Add	@Id uniqueidentifier, @Numero int, @Persona_Id uniqueidentifier, @VehiculoProspecto_Id uniqueidentifier, @Vendedor_Id uniqueidentifier,
									@Estado int, @MotivoRechazo int, @Fecha datetime, @TasacionAceptada_Id uniqueidentifier, @PrecioAcordado float,
									@CuilCuitTitular varchar(13), @FechaFirmaContrato datetime, @CodigoPrecarga08 varchar(50), @FechaCitaTransferencia datetime,
									@Vehiculo_Id uniqueidentifier, @FechaCierre datetime, @DVH char(64) as

insert into Compra(Id, Numero, Persona_Id, VehiculoProspecto_Id, Vendedor_Id, Estado, MotivoRechazo, Fecha, TasacionAceptada_Id, PrecioAcordado,
						 CuilCuitTitular, FechaFirmaContrato, CodigoPrecarga08, FechaCitaTransferencia, Vehiculo_Id, FechaCierre, DVH)
values (@Id, @Numero, @Persona_Id, @VehiculoProspecto_Id, @Vendedor_Id, @Estado, @MotivoRechazo, @Fecha, @TasacionAceptada_Id, @PrecioAcordado,
		@CuilCuitTitular, @FechaFirmaContrato, @CodigoPrecarga08, @FechaCitaTransferencia, @Vehiculo_Id, @FechaCierre, @DVH)

select 'success' as SQLResultType,'Acquisition added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_Finish	@Vehiculo_Id uniqueidentifier, @Patente varchar(255), @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier,
										@Version_Id uniqueidentifier, @Color_Id uniqueidentifier, @Precio float, @Opcionales varchar(255),
										@Observaciones varchar(max), @Puertas int, @Anio int, @Kilometraje int, @LinkImagen nvarchar(max), @DVH char(64),
										@Compra_Id uniqueidentifier, @Estado int, @FechaCierre datetime, @Acquisition_DVH char(64) as

-- CUF10 pasos 23 y 24: el vehiculo entra al inventario y la compra se finaliza juntos o no se graba nada.
-- Si quedara solo el vehiculo, apareceria disponible en CUF01 con la compra sin cerrar.
-- .NET calcula cada DVH con la fila completa; en Acquisitions solo se escriben las columnas que cambian.
set xact_abort on
begin try
	begin tran

	if not exists (select * from Compra where Id = @Compra_Id and Estado = 7)
		throw 51011, 'La adquisicion ya no esta en estado Transferida.', 1;

	insert into Vehiculo(Id,Patente,Marca_Id,Modelo_Id,Version_Id,Color_Id,Precio,Opcionales,Observaciones,Puertas,Anio,Kilometraje,LinkImagen,DVH)
	values (@Vehiculo_Id,@Patente,@Marca_Id,@Modelo_Id,@Version_Id,@Color_Id,@Precio,@Opcionales,@Observaciones,@Puertas,@Anio,@Kilometraje,@LinkImagen,@DVH)

	update Compra
	set DVH = @Acquisition_DVH,
		Estado = @Estado,
		Vehiculo_Id = @Vehiculo_Id,
		FechaCierre = @FechaCierre
	where Id = @Compra_Id

	commit tran
end try
begin catch
	if @@trancount > 0 rollback tran;
	throw;
end catch

select 'success' as SQLResultType,'Acquisition finished succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetById @Id uniqueidentifier as

-- Fila completa: cualquier cambio de estado recalcula el DVH con todas las columnas.
select	a.Id,
        a.Numero,
        a.Persona_Id,
        a.VehiculoProspecto_Id,
        a.Vendedor_Id,
        a.Estado,
        a.MotivoRechazo,
        a.Fecha,
        a.TasacionAceptada_Id,
        a.PrecioAcordado,
        a.CuilCuitTitular,
        a.FechaFirmaContrato,
        a.CodigoPrecarga08,
        a.FechaCitaTransferencia,
        a.Vehiculo_Id,
        a.FechaCierre
from	Compra a
where	a.Id = @Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetDuplicates @Patente varchar(10), @NumeroChasis varchar(17), @NumeroMotor varchar(20) as

-- Inventario (Number = 0): vehiculos sin factura pagada. Chasis y motor solo se conocen
-- de los que entraron por una adquisicion; el resto se compara por patente.
select	0 as Numero,
        a.Patente,
        isnull(g.NumeroChasis, '') as NumeroChasis,
        isnull(g.NumeroMotor, '') as NumeroMotor,
        (cast(a.Anio as varchar) + ' ' + e.Descripcion + ' ' + b.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + a.Patente) as Vehiculo_Descripcion
from	Vehiculo a
        join Marca b on (b.Id = a.Marca_Id)
		join Modelo c on (c.Id = a.Modelo_Id)
		join Version d on (d.Id = a.Version_Id)
		join Color e on (e.Id = a.Color_Id)
        left join Compra f on (f.Vehiculo_Id = a.Id)
        left join VehiculoProspecto g on (g.Id = f.VehiculoProspecto_Id)
where	(a.Patente = @Patente or g.NumeroChasis = @NumeroChasis or g.NumeroMotor = @NumeroMotor)
        and not exists (select * from Factura i where i.Vehiculo_Id = a.Id and i.EstadoPago = 1)

union all

-- Adquisiciones abiertas: de Inspeccion programada (1) a Transferida (7).
select	a.Numero,
        g.Patente,
        g.NumeroChasis,
        g.NumeroMotor,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + b.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + g.Patente) as Vehiculo_Descripcion
from	Compra a
        join VehiculoProspecto g on (g.Id = a.VehiculoProspecto_Id)
        join Marca b on (b.Id = g.Marca_Id)
		join Modelo c on (c.Id = g.Modelo_Id)
		join Version d on (d.Id = g.Version_Id)
		join Color e on (e.Id = g.Color_Id)
where	a.Estado between 1 and 7
        and (g.Patente = @Patente or g.NumeroChasis = @NumeroChasis or g.NumeroMotor = @NumeroMotor)
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetNextNumber as

-- El numero entra en el DVH: se pide antes del insert y no puede salir de un IDENTITY.
select NEXT VALUE FOR dbo.CompraNumbers as Numero
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetPendingAppraisal @Filter varchar(20), @Now datetime as

-- Adquisiciones a tasar: Inspeccionado (2), o Tasada (3) con la ultima tasacion vencida.
-- El filtro busca por DNI exacto o por el comienzo de la matricula; vacio trae todas.
select	a.Id,
        a.Numero,
        b.Id as Inspeccion_Id,
        a.Persona_Id,
        h.DNI,
        g.Patente,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        b.FechaRealizada,
        (select count(*) from Tasacion x where x.Compra_Id = a.Id) as TasacionesPrevias
from	Compra a
        join Inspeccion b on (b.Compra_Id = a.Id)
        join Persona h on (h.Id = a.Persona_Id)
        join VehiculoProspecto g on (g.Id = a.VehiculoProspecto_Id)
        join Marca c on (c.Id = g.Marca_Id)
		join Modelo d on (d.Id = g.Modelo_Id)
		join Version f on (f.Id = g.Version_Id)
		join Color e on (e.Id = g.Color_Id)
where	(a.Estado = 2
            or (a.Estado = 3 and (select top 1 x.Vigencia from Tasacion x where x.Compra_Id = a.Id order by x.Fecha desc) < @Now))
        and (isnull(@Filter, '') = ''
            or h.DNI = try_cast(@Filter as int)
            or g.Patente like @Filter + '%')
order by b.FechaRealizada asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetPendingManagement @Filter varchar(20) as

-- Compras en gestion (CUF10): Tasada (3), Compra formalizada (4), Documentacion completa (5), Pagada (6) y Transferida (7).
-- El filtro busca por DNI exacto o por el comienzo de la matricula; vacio trae todas.
select	a.Id,
        a.Numero,
        b.Id as Inspeccion_Id,
        a.Persona_Id,
        h.DNI,
        g.Patente,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        a.Estado
from	Compra a
        join Inspeccion b on (b.Compra_Id = a.Id)
        join Persona h on (h.Id = a.Persona_Id)
        join VehiculoProspecto g on (g.Id = a.VehiculoProspecto_Id)
        join Marca c on (c.Id = g.Marca_Id)
		join Modelo d on (d.Id = g.Modelo_Id)
		join Version f on (f.Id = g.Version_Id)
		join Color e on (e.Id = g.Color_Id)
where	a.Estado between 3 and 7
        and (isnull(@Filter, '') = ''
            or h.DNI = try_cast(@Filter as int)
            or g.Patente like @Filter + '%')
order by a.Numero asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetPendingPayment @Persona_Id uniqueidentifier as

-- Compras de la persona para el cajero (CUF11 paso 2): las de Documentacion completa (5), con saldo o con el
-- saldo cancelado y el comprobante sin emitir, y las que ya tienen comprobante para volver a imprimirlo.
select	a.Id,
        a.Numero,
        g.Patente,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        a.PrecioAcordado,
        isnull((select sum(y.Monto) from PagoCompra y where y.Compra_Id = a.Id), 0) as MontoPagado,
        r.Numero as ComprobanteCompra_Numero,
        a.Estado
from	Compra a
        join VehiculoProspecto g on (g.Id = a.VehiculoProspecto_Id)
        join Marca c on (c.Id = g.Marca_Id)
		join Modelo d on (d.Id = g.Modelo_Id)
		join Version f on (f.Id = g.Version_Id)
		join Color e on (e.Id = g.Color_Id)
        left join ComprobanteCompra r on (r.Compra_Id = a.Id)
where	a.Persona_Id = @Persona_Id
        and (a.Estado = 5 or r.Id is not null)
order by a.Numero asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_GetSummary @Id uniqueidentifier as

-- Resumen completo de la compra (CUF10 paso 20): vehiculo, inspeccion, tasacion aceptada, acuerdo,
-- lo pagado y el comprobante de CUF11 y la transferencia. Los datos de la persona se leen aparte porque estan encriptados.
select	a.Id,
        a.Numero,
        a.Persona_Id,
        h.DNI,
        g.Patente,
        g.NumeroChasis,
        g.NumeroMotor,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        g.Marca_Id,
        g.Modelo_Id,
        g.Version_Id,
        g.Color_Id,
        g.Anio,
        g.Puertas,
        g.Kilometraje,
        b.Id as Inspeccion_Id,
        b.FechaRealizada,
        b.KilometrajeVerificado,
        x.Valor as Tasacion_Valor,
        x.Fecha as Tasacion_Fecha,
        a.PrecioAcordado,
        a.CuilCuitTitular,
        a.FechaFirmaContrato,
        isnull((select sum(y.Monto) from PagoCompra y where y.Compra_Id = a.Id), 0) as MontoPagado,
        r.Numero as ComprobanteCompra_Numero,
        r.Fecha as ComprobanteCompra_Fecha,
        a.CodigoPrecarga08,
        a.FechaCitaTransferencia,
        a.FechaCierre
from	Compra a
        join Inspeccion b on (b.Compra_Id = a.Id)
        join Persona h on (h.Id = a.Persona_Id)
        join VehiculoProspecto g on (g.Id = a.VehiculoProspecto_Id)
        join Marca c on (c.Id = g.Marca_Id)
		join Modelo d on (d.Id = g.Modelo_Id)
		join Version f on (f.Id = g.Version_Id)
		join Color e on (e.Id = g.Color_Id)
        left join Tasacion x on (x.Id = a.TasacionAceptada_Id)
        left join ComprobanteCompra r on (r.Compra_Id = a.Id)
where	a.Id = @Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_Pay	@Id uniqueidentifier, @Numero int, @Compra_Id uniqueidentifier, @Fecha datetime, @NombrePersona varchar(255),
									@CuilCuit varchar(13), @Detalle varchar(max), @MontoNeto float, @MontoImpuesto float, @MontoTotal float,
									@DVH char(64), @Estado int, @Acquisition_DVH char(64) as

-- CUF11 pasos 7 y 9: el comprobante y el pase a Pagada van juntos o no se graba nada. Un comprobante con la
-- compra sin pagar no se podria volver a emitir, y una compra pagada sin comprobante no se podria reimprimir.
-- Los pagos ya estan grabados: si se cae antes de este paso, la compra queda con saldo cero y se reintenta.
set xact_abort on
begin try
	begin tran

	if not exists (select * from Compra where Id = @Compra_Id and Estado = 5)
		throw 51012, 'La adquisicion ya no esta en estado Documentacion completa.', 1;

	if exists (select * from FormaPagoCompra t
				where t.Compra_Id = @Compra_Id
				and round(t.Monto - isnull((select sum(x.Monto) from PagoCompra x
										where x.Compra_Id = t.Compra_Id and x.TipoPago_Id = t.TipoPago_Id), 0), 2) <> 0)
		throw 51013, 'La compra todavia tiene saldo pendiente.', 1;

	insert into ComprobanteCompra(Id, Numero, Compra_Id, Fecha, NombrePersona, CuilCuit, Detalle, MontoNeto, MontoImpuesto, MontoTotal, DVH)
	values (@Id, @Numero, @Compra_Id, @Fecha, @NombrePersona, @CuilCuit, @Detalle, @MontoNeto, @MontoImpuesto, @MontoTotal, @DVH)

	update Compra
	set DVH = @Acquisition_DVH,
		Estado = @Estado
	where Id = @Compra_Id

	commit tran
end try
begin catch
	if @@trancount > 0 rollback tran;
	throw;
end catch

select 'success' as SQLResultType,'Acquisition paid succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Compra_Update	@Id uniqueidentifier, @Numero int, @Persona_Id uniqueidentifier, @VehiculoProspecto_Id uniqueidentifier, @Vendedor_Id uniqueidentifier,
											@Estado int, @MotivoRechazo int, @Fecha datetime, @TasacionAceptada_Id uniqueidentifier, @PrecioAcordado float,
											@CuilCuitTitular varchar(13), @FechaFirmaContrato datetime, @CodigoPrecarga08 varchar(50), @FechaCitaTransferencia datetime,
											@Vehiculo_Id uniqueidentifier, @FechaCierre datetime, @DVH char(64) as

-- Fila completa: el DVH se recalcula con todas las columnas aunque solo cambie el estado.
update Compra
set DVH = @DVH,
	Numero = @Numero,
	Persona_Id = @Persona_Id,
	VehiculoProspecto_Id = @VehiculoProspecto_Id,
	Vendedor_Id = @Vendedor_Id,
	Estado = @Estado,
	MotivoRechazo = @MotivoRechazo,
	Fecha = @Fecha,
	TasacionAceptada_Id = @TasacionAceptada_Id,
	PrecioAcordado = @PrecioAcordado,
	CuilCuitTitular = @CuilCuitTitular,
	FechaFirmaContrato = @FechaFirmaContrato,
	CodigoPrecarga08 = @CodigoPrecarga08,
	FechaCitaTransferencia = @FechaCitaTransferencia,
	Vehiculo_Id = @Vehiculo_Id,
	FechaCierre = @FechaCierre
where Id = @Id

select 'success' as SQLResultType,'Acquisition updated succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tasacion_Add	@Id uniqueidentifier, @Compra_Id uniqueidentifier, @Inspeccion_Id uniqueidentifier, @Tasador_Id uniqueidentifier,
									@Valor float, @Justificacion varchar(max), @Fecha datetime, @Vigencia datetime, @DVH char(64) as

insert into Tasacion(Id, Compra_Id, Inspeccion_Id, Tasador_Id, Valor, Justificacion, Fecha, Vigencia, DVH)
values (@Id, @Compra_Id, @Inspeccion_Id, @Tasador_Id, @Valor, @Justificacion, @Fecha, @Vigencia, @DVH)

select 'success' as SQLResultType,'Appraisal added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tasacion_GetLastByAcquisition @Compra_Id uniqueidentifier as

-- La tasacion vigente de una adquisicion es la ultima que se registro.
select	top 1
        a.Id,
        a.Compra_Id,
        a.Inspeccion_Id,
        a.Tasador_Id,
        a.Valor,
        a.Justificacion,
        a.Fecha,
        a.Vigencia,
        (b.Nombre + ' ' + b.Apellido) as Tasador
from	Tasacion a
        join Usuario b on (b.Id = a.Tasador_Id)
where	a.Compra_Id = @Compra_Id
order by a.Fecha desc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Color_GetAll as

select Id, Descripcion 
from Color 
order by Descripcion asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.DV_GetBaseRoles
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id,Nombre FROM dbo.Permiso WHERE Nombre='Base User' AND Tipo IS NULL;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.DV_GetByTable @NombreTabla nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @NombreTabla;
    SELECT NombreEsquema,NombreTabla,DVV,VersionAlgoritmo
    FROM dbo.DV WHERE NombreEsquema='dbo' AND NombreTabla=@NombreTabla;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.DV_GetColumns @NombreTabla nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @NombreTabla;
    SELECT c.name AS Name, ty.name AS SqlType, CONVERT(int,c.max_length) AS MaxLength,
        c.is_nullable AS IsNullable,
        ISNULL(CONVERT(int,COLLATIONPROPERTY(c.collation_name,'CodePage')),0) AS CodePage,
        CONVERT(bit,CASE WHEN EXISTS (
            SELECT 1 FROM sys.indexes i
            JOIN sys.index_columns ic ON ic.object_id=i.object_id AND ic.index_id=i.index_id
            WHERE i.object_id=c.object_id AND i.is_primary_key=1 AND ic.column_id=c.column_id
        ) THEN 1 ELSE 0 END) AS IsKey
    FROM sys.columns c JOIN sys.types ty ON ty.user_type_id=c.user_type_id
    WHERE c.object_id=OBJECT_ID(N'dbo.'+QUOTENAME(@NombreTabla)) AND c.name<>'DVH'
    ORDER BY c.column_id;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.DV_GetHorizontal @NombreTabla nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @NombreTabla;
    DECLARE @Sql nvarchar(max)=N'SELECT DVH FROM dbo.'+QUOTENAME(@NombreTabla);
    EXEC sys.sp_executesql @Sql;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.DV_GetUsersByRole @RoleId uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Usuario WHERE Rol_Id=@RoleId;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.DV_Save
    @NombreTabla nvarchar(128), @DVV char(64)
AS
BEGIN
    SET NOCOUNT ON;
    EXEC dbo.DV_ValidateTable @NombreTabla;
    UPDATE dbo.DV SET DVV=@DVV, VersionAlgoritmo=1
        WHERE NombreEsquema='dbo' AND NombreTabla=@NombreTabla;
    IF @@ROWCOUNT=0
        INSERT INTO dbo.DV(NombreEsquema,NombreTabla,DVV,VersionAlgoritmo)
        VALUES ('dbo',@NombreTabla,@DVV,1);
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE dbo.BitacoraEventos_ActorExists @Usuario_Id uniqueidentifier
AS
BEGIN
    SET NOCOUNT ON;
    -- Una restauracion puede dejar sin fila al actor de un evento posterior. La aplicacion
    -- consulta antes de armar la fila para no guardar una referencia que la FK rechazaria.
    SELECT CONVERT(bit,CASE WHEN EXISTS(SELECT 1 FROM dbo.Usuario WHERE Id=@Usuario_Id) THEN 1 ELSE 0 END) AS ActorExists;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE dbo.BitacoraEventos_GetLogins
AS
BEGIN
    SET NOCOUNT ON;
    SELECT DISTINCT COALESCE(u.NombreUsuario,e.LoginIntentado,'') AS Login
    FROM dbo.BitacoraEventos e
    LEFT JOIN dbo.Usuario u ON u.Id=e.Usuario_Id
    WHERE COALESCE(u.NombreUsuario,e.LoginIntentado,'')<>''
    ORDER BY Login;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE dbo.BitacoraEventos_Insert
    @Id uniqueidentifier, @Usuario_Id uniqueidentifier, @LoginIntentado nvarchar(256),
    @Fecha datetime, @Modulo varchar(32), @TipoEvento varchar(64),
    @Criticidad int, @Objetivo_Id uniqueidentifier, @DVH char(64)
AS
BEGIN
    SET NOCOUNT ON;
    -- El actor se resuelve en la aplicacion antes de calcular el DVH. Este procedimiento
    -- no puede alterar ningun valor: cualquier cambio invalidaria el digito horizontal.
    INSERT dbo.BitacoraEventos(Id,Usuario_Id,LoginIntentado,Fecha,Modulo,TipoEvento,Criticidad,Objetivo_Id,DVH)
    VALUES(@Id,@Usuario_Id,@LoginIntentado,@Fecha,@Modulo,@TipoEvento,@Criticidad,@Objetivo_Id,@DVH);
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER OFF
GO
CREATE   PROCEDURE dbo.BitacoraEventos_Query
    @Login nvarchar(256)=NULL, @From datetime, @Until datetime,
    @Modulo varchar(32)=NULL, @TipoEvento varchar(64)=NULL, @Criticidad int=NULL
AS
BEGIN
    SET NOCOUNT ON;
    -- El evento se relaciona con el usuario por Id. El login se resuelve en la consulta;
    -- AttemptedLogin solo tiene valor cuando el intento no correspondio a ningun usuario.
    SELECT e.Id, e.Usuario_Id,
        COALESCE(u.NombreUsuario,e.LoginIntentado,'') AS Login,
        ISNULL(u.Nombre,'') AS Nombre,
        ISNULL(u.Apellido,'') AS Apellido,
        e.Fecha, e.Modulo, e.TipoEvento, e.Criticidad, e.Objetivo_Id
    FROM dbo.BitacoraEventos e
    LEFT JOIN dbo.Usuario u ON u.Id=e.Usuario_Id
    WHERE e.Fecha>=@From AND e.Fecha<@Until
      AND (@Login IS NULL OR COALESCE(u.NombreUsuario,e.LoginIntentado,'')=@Login)
      AND (@Modulo IS NULL OR e.Modulo=@Modulo)
      AND (@TipoEvento IS NULL OR e.TipoEvento=@TipoEvento)
      AND (@Criticidad IS NULL OR e.Criticidad=@Criticidad)
    ORDER BY e.Fecha DESC,e.Id DESC;
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ItemInspeccion_GetAll as

select	a.Id,
        a.Descripcion,
        a.Orden
from	ItemInspeccion a
order by a.Orden asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FotografiaInspeccion_Add	@Id uniqueidentifier, @Inspeccion_Id uniqueidentifier, @NombreArchivo varchar(255), @Contenido varbinary(max), @FechaCarga datetime as

insert into FotografiaInspeccion(Id, Inspeccion_Id, NombreArchivo, Contenido, FechaCarga)
values (@Id, @Inspeccion_Id, @NombreArchivo, @Contenido, @FechaCarga)

select 'success' as SQLResultType,'Inspection photo added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FotografiaInspeccion_Delete @Id uniqueidentifier as

delete from FotografiaInspeccion
where Id = @Id

select 'success' as SQLResultType,'Inspection photo deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc FotografiaInspeccion_GetByInspection @Inspeccion_Id uniqueidentifier as

select	a.Id,
        a.Inspeccion_Id,
        a.NombreArchivo,
        a.Contenido,
        a.FechaCarga
from	FotografiaInspeccion a
where	a.Inspeccion_Id = @Inspeccion_Id
order by a.FechaCarga asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc PuntajeInspeccion_GetByInspection @Inspeccion_Id uniqueidentifier as

select	a.Inspeccion_Id,
        a.ItemInspeccion_Id,
        b.Descripcion,
        b.Orden,
        a.Puntaje,
        a.Observaciones
from	PuntajeInspeccion a
        join ItemInspeccion b on (b.Id = a.ItemInspeccion_Id)
where	a.Inspeccion_Id = @Inspeccion_Id
order by b.Orden asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_Add	@Id uniqueidentifier, @Compra_Id uniqueidentifier, @Mecanico_Id uniqueidentifier, @FechaProgramada datetime,
									@FechaRealizada datetime, @KilometrajeVerificado int, @Resultado int, @Observaciones varchar(max), @DVH char(64) as

insert into Inspeccion(Id, Compra_Id, Mecanico_Id, FechaProgramada, FechaRealizada, KilometrajeVerificado, Resultado, Observaciones, DVH)
values (@Id, @Compra_Id, @Mecanico_Id, @FechaProgramada, @FechaRealizada, @KilometrajeVerificado, @Resultado, @Observaciones, @DVH)

select 'success' as SQLResultType,'Inspection added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_GetById @Id uniqueidentifier as

-- Resultado de la inspeccion registrada en CUF08.
select	a.Id,
        a.Compra_Id,
        a.Mecanico_Id,
        a.FechaProgramada,
        a.FechaRealizada,
        a.KilometrajeVerificado,
        a.Resultado,
        a.Observaciones
from	Inspeccion a
where	a.Id = @Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_GetDetail @Id uniqueidentifier as

-- Datos del vehiculo que se inspecciona, con lo declarado en CUF07.
select	a.Id,
        a.Compra_Id,
        b.Numero,
        g.Patente,
        g.NumeroChasis,
        g.NumeroMotor,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        g.Puertas,
        g.Kilometraje
from	Inspeccion a
        join Compra b on (b.Id = a.Compra_Id)
        join VehiculoProspecto g on (g.Id = b.VehiculoProspecto_Id)
        join Marca c on (c.Id = g.Marca_Id)
		join Modelo d on (d.Id = g.Modelo_Id)
		join Version f on (f.Id = g.Version_Id)
		join Color e on (e.Id = g.Color_Id)
where	a.Id = @Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_GetMechanics @Tipo varchar(50) as

-- Mecanico: usuario activo y no bloqueado cuyo rol tiene la patente @Type en cualquier nivel.
with recursive as	(
						select	sp2.Padre_Id as Rol_Id, sp2.Hijo_Id
						from	Permiso_Permiso SP2
						UNION ALL
						select	r.Rol_Id, sp.Hijo_Id
						from	Permiso_Permiso sp
								inner join recursive r on (r.Hijo_Id=sp.Padre_Id)
					)
select	u.Id, u.DNI, u.NombreUsuario, Clave='' , u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u
where	u.Activo = 1
		and u.Bloqueado = 0
		and exists	(
						select	*
						from	recursive r
								inner join Permiso p on (r.Hijo_Id = p.Id)
						where	r.Rol_Id = u.Rol_Id
								and p.Tipo = @Tipo
					)
order by u.NombreUsuario asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_GetNextFreeSlot	@Mecanico_Id uniqueidentifier, @From datetime, @Slot_Minutes int, @Day_Start_Hour int,
												@Day_End_Hour int, @Max_Days int as

-- Turnos candidatos desde el dia de @From. El 01/01/1900 fue lunes: datediff(day, 0, fecha) % 7
-- da 0 a 4 de lunes a viernes sin depender de SET DATEFIRST.
with slots as	(
					select	dateadd(hour, @Day_Start_Hour, cast(cast(@From as date) as datetime)) as Turno
					UNION ALL
					select	dateadd(minute, @Slot_Minutes, s.Turno)
					from	slots s
					where	s.Turno < dateadd(day, @Max_Days, @From)
				)
select	top 1 s.Turno as FechaProgramada
from	slots s
where	s.Turno >= @From
		and datediff(day, 0, s.Turno) % 7 < 5
		and datepart(hour, s.Turno) >= @Day_Start_Hour
		and dateadd(minute, @Slot_Minutes, s.Turno) <= dateadd(hour, @Day_End_Hour, cast(cast(s.Turno as date) as datetime))
		and not exists	(
							select	*
							from	Inspeccion a
									join Compra b on (b.Id = a.Compra_Id)
							where	a.Mecanico_Id = @Mecanico_Id
									and b.Estado = 1
									and a.FechaProgramada > dateadd(minute, -@Slot_Minutes, s.Turno)
									and a.FechaProgramada < dateadd(minute, @Slot_Minutes, s.Turno)
						)
order by s.Turno
option (maxrecursion 0)
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_GetScheduledByMechanic @Mecanico_Id uniqueidentifier as

-- Inspecciones pendientes del mecanico: adquisiciones en estado Inspeccion programada (1).
select	a.Id,
        a.Compra_Id,
        a.Mecanico_Id,
        a.FechaProgramada,
        b.Numero,
        g.Patente,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion
from	Inspeccion a
        join Compra b on (b.Id = a.Compra_Id)
        join VehiculoProspecto g on (g.Id = b.VehiculoProspecto_Id)
        join Marca c on (c.Id = g.Marca_Id)
		join Modelo d on (d.Id = g.Modelo_Id)
		join Version f on (f.Id = g.Version_Id)
		join Color e on (e.Id = g.Color_Id)
where	a.Mecanico_Id = @Mecanico_Id
        and b.Estado = 1
order by a.FechaProgramada asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_IsMechanicBusy @Mecanico_Id uniqueidentifier, @FechaProgramada datetime, @Slot_Minutes int as

-- Solo ocupan la agenda las inspecciones de adquisiciones en estado Inspeccion programada (1).
select	count(*) as Inspecciones
from	Inspeccion a
        join Compra b on (b.Id = a.Compra_Id)
where	a.Mecanico_Id = @Mecanico_Id
        and b.Estado = 1
        and a.FechaProgramada > dateadd(minute, -@Slot_Minutes, @FechaProgramada)
        and a.FechaProgramada < dateadd(minute, @Slot_Minutes, @FechaProgramada)
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Inspeccion_Record	@Id uniqueidentifier, @Compra_Id uniqueidentifier, @FechaRealizada datetime, @KilometrajeVerificado int,
									@Resultado int, @Observaciones varchar(max), @DVH char(64), @Scores dbo.PuntajeInspeccionRows READONLY,
									@Estado int, @MotivoRechazo int, @Acquisition_DVH char(64) as

-- CUF08: la inspeccion, sus calificaciones y el estado de la adquisicion se graban juntos o no se graba nada.
-- .NET calcula cada DVH con la fila completa; aca solo se escriben las columnas que cambian.
set xact_abort on
begin try
	begin tran

	if not exists (select * from Compra where Id = @Compra_Id and Estado = 1)
		throw 51010, 'La adquisicion ya no esta en estado Inspeccion programada.', 1;

	update Inspeccion
	set DVH = @DVH,
		FechaRealizada = @FechaRealizada,
		KilometrajeVerificado = @KilometrajeVerificado,
		Resultado = @Resultado,
		Observaciones = @Observaciones
	where Id = @Id
		and Compra_Id = @Compra_Id

	insert into PuntajeInspeccion(Inspeccion_Id, ItemInspeccion_Id, Puntaje, Observaciones, DVH)
	select @Id, a.ItemInspeccion_Id, a.Puntaje, a.Observaciones, a.DVH
	from @Scores a

	update Compra
	set DVH = @Acquisition_DVH,
		Estado = @Estado,
		MotivoRechazo = @MotivoRechazo
	where Id = @Compra_Id

	commit tran
end try
begin catch
	if @@trancount > 0 rollback tran;
	throw;
end catch

select 'success' as SQLResultType,'Inspection recorded succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Factura_Add	@Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Reserva_Id uniqueidentifier, @Detalle varchar(max),
									@CuilCuit varchar(max), @RazonSocial varchar(max), @Valor float, @EstadoPago bit, @Fecha datetime , @DVH char(64) as

insert into Factura(Id, Vehiculo_Id, Persona_Id, Reserva_Id, Detalle, CuilCuit, RazonSocial, Valor, EstadoPago, Fecha,DVH)
values (@Id, @Vehiculo_Id, @Persona_Id, @Reserva_Id, @Detalle, @CuilCuit, @RazonSocial, @Valor, @EstadoPago, @Fecha,@DVH)

select 'success' as SQLResultType,'Invoice added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Factura_Delete	@Id uniqueidentifier as

delete Factura
where Id = @Id

select 'success' as SQLResultType,'Invoice deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Factura_GetAllPendingOfPaperworkByPerson @Persona_Id uniqueidentifier as

select	a.Id,
        a.Vehiculo_Id,  (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id,
        a.Reserva_Id, 
        a.Detalle, 
        a.CuilCuit,
        a.RazonSocial, 
        a.Valor,
        a.EstadoPago, 
        a.Fecha
from	Factura a
        join Vehiculo b on (b.Id = a.Vehiculo_Id)
		join Modelo c on (c.Id = b.Modelo_Id)
		join Version d on (d.Id = b.Version_Id)
		join Color e on (e.Id = b.Color_Id)
        join Marca f on (f.Id = b.Marca_Id)
where   a.EstadoPago = 1
        and a.Persona_Id = @Persona_Id
        and not exists 
        (
            select * from Tramite x where x.Factura_Id = a.Id
        )
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Factura_GetById @Id uniqueidentifier as

select	a.Id,
        a.Vehiculo_Id,  (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id,
        a.Reserva_Id, 
        a.Detalle, 
        a.CuilCuit,
        a.RazonSocial, 
        a.Valor,
        a.EstadoPago, 
        a.Fecha
from	Factura a
        join Vehiculo b on (b.Id = a.Vehiculo_Id)
		join Modelo c on (c.Id = b.Modelo_Id)
		join Version d on (d.Id = b.Version_Id)
		join Color e on (e.Id = b.Color_Id)
        join Marca f on (f.Id = b.Marca_Id)
where	a.Id = @Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Factura_Update	@Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Reserva_Id uniqueidentifier, @Detalle varchar(max),
										@CuilCuit varchar(max), @RazonSocial varchar(max), @Valor float, @EstadoPago bit, @Fecha datetime , @DVH char(64) as

update Factura
set DVH = @DVH,
	Vehiculo_Id = @Vehiculo_Id,
	Persona_Id = @Persona_Id,
	Reserva_Id = @Reserva_Id,
	Detalle = @Detalle,
	CuilCuit = @CuilCuit,
	RazonSocial = @RazonSocial,
	Valor = @Valor,
	EstadoPago = @EstadoPago,
	Fecha = @Fecha
where Id = @Id

select 'success' as SQLResultType,'Invoice updated succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Marca_Delete	@Id uniqueidentifier as

delete Marca
where Id = @Id

select 'success' as SQLResultType,'Make deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Marca_GetAll as

select Id, Descripcion 
from Marca 
order by Descripcion asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Marca_Insert @Id uniqueidentifier, @Descripcion varchar(50) , @DVH char(64) as

if not exists (select * from Marca where Descripcion = @Descripcion)
begin
	insert into Marca(Id,Descripcion,DVH) values (@Id,@Descripcion,@DVH);
end

select 'success' as SQLResultType,'Make added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Modelo_Delete	@Id uniqueidentifier as

delete Modelo
where Id = @Id

select 'success' as SQLResultType,'Model deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Modelo_GetAllByMake @Marca_Id uniqueidentifier as

select	a.Id,a.Marca_Id, b.Descripcion as Marca_Descripcion, a.Descripcion 
from	Modelo a
		join Marca b on (b.Id = Marca_Id)
where	a.Marca_Id = @Marca_Id
order by a.Descripcion asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Modelo_Insert @Id uniqueidentifier, @Marca_Id uniqueidentifier, @Descripcion varchar(255) , @DVH char(64) as

if not exists (select * from Modelo where Marca_Id = @Marca_Id and Descripcion = @Descripcion)
begin
	insert into Modelo(Id,Marca_Id,Descripcion,DVH) values (@Id,@Marca_Id,@Descripcion,@DVH);
end

select 'success' as SQLResultType, 'Operation completed.' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ArchivoTramite_Add	@Id uniqueidentifier, @Tramite_Id uniqueidentifier, @NombreArchivo varchar(255), @Contenido varbinary(max), @FechaCarga datetime as

insert into ArchivoTramite(Id, Tramite_Id, NombreArchivo, Contenido, FechaCarga) 
values (@Id, @Tramite_Id, @NombreArchivo, @Contenido, @FechaCarga)

select 'success' as SQLResultType,'Paperwork file added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ArchivoTramite_Delete	@Id uniqueidentifier as

delete ArchivoTramite
where Id = @Id

select 'success' as SQLResultType,'Paperwork file deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ArchivoTramite_GetByPaperWork @Tramite_Id uniqueidentifier as

select	a.Id,
        a.Tramite_Id, 
        a.NombreArchivo, 
        a.Contenido,
        a.FechaCarga
from	ArchivoTramite a
where	a.Tramite_Id = @Tramite_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_Add	@Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Factura_Id uniqueidentifier, @CodigoPrecarga08 varchar(50),
									@FechaTransferencia datetime, @Observaciones varchar(max), @Finalizado bit , @DVH char(64) as

insert into Tramite(Id, Vehiculo_Id, Persona_Id, Factura_Id, CodigoPrecarga08, FechaTransferencia, Observaciones, Finalizado,DVH)
values (@Id, @Vehiculo_Id, @Persona_Id, @Factura_Id, @CodigoPrecarga08, @FechaTransferencia, @Observaciones, @Finalizado,@DVH)

select 'success' as SQLResultType,'Paperwork added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_Delete	@Id uniqueidentifier as

delete Tramite
where Id = @Id

select 'success' as SQLResultType,'Paperwork deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_GetAllActiveByPerson @Persona_Id uniqueidentifier as

select	a.Id,
        a.Vehiculo_Id,  (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id,
        a.Factura_Id, 
        a.CodigoPrecarga08, 
        a.FechaTransferencia,
        a.Observaciones, 
        a.Finalizado
from	Tramite a
        join Vehiculo b on (b.Id = a.Vehiculo_Id)
		join Modelo c on (c.Id = b.Modelo_Id)
		join Version d on (d.Id = b.Version_Id)
		join Color e on (e.Id = b.Color_Id)
        join Marca f on (f.Id = b.Marca_Id)
where	(a.Persona_Id = @Persona_Id or (@Persona_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
		and a.Finalizado = 0
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_GetAllByPerson @Persona_Id uniqueidentifier as

select	a.Id,
        a.Vehiculo_Id,  (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id,
        a.Factura_Id, 
        a.CodigoPrecarga08, 
        a.FechaTransferencia,
        a.Observaciones, 
        a.Finalizado
from	Tramite a
        join Vehiculo b on (b.Id = a.Vehiculo_Id)
		join Modelo c on (c.Id = b.Modelo_Id)
		join Version d on (d.Id = b.Version_Id)
		join Color e on (e.Id = b.Color_Id)
        join Marca f on (f.Id = b.Marca_Id)
where	(a.Persona_Id = @Persona_Id or (@Persona_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_GetById @Id uniqueidentifier as

select	a.Id,
        a.Vehiculo_Id,  (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id,
        a.Factura_Id, 
        a.CodigoPrecarga08, 
        a.FechaTransferencia,
        a.Observaciones, 
        a.Finalizado
from	Tramite a
        join Vehiculo b on (b.Id = a.Vehiculo_Id)
		join Modelo c on (c.Id = b.Modelo_Id)
		join Version d on (d.Id = b.Version_Id)
		join Color e on (e.Id = b.Color_Id)
        join Marca f on (f.Id = b.Marca_Id)
where	a.Id = @Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Tramite_Update	@Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Factura_Id uniqueidentifier, @CodigoPrecarga08 varchar(50),
									@FechaTransferencia datetime, @Observaciones varchar(max), @Finalizado bit , @DVH char(64) as

update Tramite
set DVH = @DVH,
	Vehiculo_Id = @Vehiculo_Id,
	Persona_Id = @Persona_Id,
	Factura_Id = @Factura_Id,
	CodigoPrecarga08 = @CodigoPrecarga08,
	FechaTransferencia = @FechaTransferencia,
	Observaciones = @Observaciones,
	Finalizado = @Finalizado
where Id = @Id

select 'success' as SQLResultType,'Paperwork updated succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc TipoPago_GetAll as

select Id, Descripcion 
from TipoPago 
order by Descripcion asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Pago_Add	@Id uniqueidentifier, @Factura_Id uniqueidentifier, @TipoPago_Id uniqueidentifier, @Monto float, @Detalle varchar(255) , @DVH char(64) as

insert into Pago(Id, Factura_Id, TipoPago_Id, Monto, Detalle,DVH)
values (@Id, @Factura_Id, @TipoPago_Id, @Monto, @Detalle,@DVH)

select 'success' as SQLResultType,'Payment added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Pago_Delete	@Id uniqueidentifier as

delete Pago
where Id = @Id

select 'success' as SQLResultType,'Payment deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Pago_GetAllByInvoice @Factura_Id uniqueidentifier as

select	a.Id,
        a.Factura_Id,
        a.TipoPago_Id, b.Descripcion as TipoPago_Descripcion,
        a.Monto,
        a.Detalle
from	Pago a
		join TipoPago b on (b.Id = a.TipoPago_Id)
where	a.Factura_Id = @Factura_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Persona_Add	@Id uniqueidentifier, @DNI int, @Nombre varchar(max), @Apellido varchar(max), @Direccion varchar(max),
									@TelefonoPersonal varchar(max), @TelefonoCasa varchar(max), @Email varchar(max), @FechaNacimiento datetime , @DVH char(64) as

insert into Persona(Id,DNI,Nombre,Apellido,Direccion,TelefonoPersonal,TelefonoCasa,Email,FechaNacimiento,DVH)
values (@Id,@DNI,@Nombre,@Apellido,@Direccion,@TelefonoPersonal,@TelefonoCasa,@Email,@FechaNacimiento,@DVH)

select 'success' as SQLResultType,'Person added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Persona_GetByDni @DNI int as

select	u.Id,u.DNI,u.Nombre,u.Apellido,u.Direccion,u.TelefonoPersonal,u.TelefonoCasa,u.Email,u.FechaNacimiento
from	Persona  u
where	u.DNI = @DNI
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Persona_GetById @Id uniqueidentifier as

select	Id,DNI,Nombre,Apellido,Direccion,TelefonoPersonal,TelefonoCasa,Email,FechaNacimiento
from	Persona  
where	Id = @Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.Permiso_DeleteCompleteFamily
    @Id uniqueidentifier,
    @BaseRoleId uniqueidentifier,
    @UserDigests dbo.UsuarioDvhUpdates READONLY
AS
BEGIN
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;
        -- .NET calcula un DVH por usuario con el nuevo Role_Id antes de llamar al SP.
        IF EXISTS (SELECT Id FROM dbo.Usuario WHERE Rol_Id=@Id EXCEPT SELECT Id FROM @UserDigests)
            OR EXISTS (SELECT Id FROM @UserDigests EXCEPT SELECT Id FROM dbo.Usuario WHERE Rol_Id=@Id)
            THROW 51001, 'Los usuarios afectados cambiaron. Reintente la eliminacion de la familia.', 1;
        IF EXISTS (SELECT 1 FROM @UserDigests) AND
            (@BaseRoleId IS NULL OR @BaseRoleId=@Id OR NOT EXISTS
                (SELECT 1 FROM dbo.Permiso WHERE Id=@BaseRoleId AND Nombre='Base User' AND Tipo IS NULL))
            THROW 51002, 'No existe una familia Base User valida para reasignar los usuarios.', 1;
        DELETE FROM dbo.Permiso_Permiso WHERE Padre_Id=@Id OR Hijo_Id=@Id;
        UPDATE u SET Rol_Id=@BaseRoleId, DVH=d.DVH
            FROM dbo.Usuario u JOIN @UserDigests d ON d.Id=u.Id;
        DELETE FROM dbo.Permiso WHERE Id=@Id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_DeleteFamily @Id uniqueidentifier as

delete from Permiso_Permiso where Padre_Id=@Id;
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   PROCEDURE dbo.Permiso_DeletePatent @Id uniqueidentifier
AS
BEGIN
    SET XACT_ABORT ON;
    BEGIN TRY
        BEGIN TRAN;
        DELETE FROM dbo.Permiso_Permiso WHERE Padre_Id=@Id OR Hijo_Id=@Id;
        DELETE FROM dbo.Permiso WHERE Id=@Id;
        COMMIT TRAN;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRAN;
        THROW;
    END CATCH
END
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_GetAll @Family uniqueidentifier as

with recursive as	(
						select	sp2.Padre_Id, sp2.Hijo_Id  
						from	Permiso_Permiso SP2
						where	(sp2.Padre_Id = @Family or @Family is null)
						UNION ALL 
						select sp.Padre_Id, sp.Hijo_Id 
						from	Permiso_Permiso sp 
								inner join recursive r on (r.Hijo_Id=sp.Padre_Id)
					)
select	r.Padre_Id,
		r.Hijo_Id,
		p.Id,
		p.Nombre, 
		p.Tipo
from	recursive r 
		inner join Permiso p on (r.Hijo_Id = p.id)
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_GetAllByUser @User_Id uniqueidentifier as

select p.* from Users_Permissions up inner join Permiso p on up.Permission_Id=p.Id where up.User_Id=@User_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_GetFamilies as

select	* 
from	Permiso p 
where	p.Tipo is  null
order by p.Nombre asc;
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_GetPatents  as

select	* 
from	Permiso p 
where	p.Tipo is not null
order by p.Nombre asc;
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_InsertComponent @Id uniqueidentifier, @Nombre varchar(50), @Tipo varchar(50) , @DVH char(64) as

insert into Permiso(Id,Nombre,Tipo,DVH) values (@Id,@Nombre,@Tipo,@DVH);
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Permiso_Insert_Permission_Permission @Padre_Id uniqueidentifier, @Hijo_Id uniqueidentifier , @DVH char(64) as

insert into Permiso_Permiso(Padre_Id,Hijo_Id,DVH) values (@Padre_Id,@Hijo_Id,@DVH);
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc VehiculoProspecto_Add	@Id uniqueidentifier, @Patente varchar(10), @NumeroChasis varchar(17), @NumeroMotor varchar(20), @Marca_Id uniqueidentifier,
										@Modelo_Id uniqueidentifier, @Version_Id uniqueidentifier, @Color_Id uniqueidentifier, @Anio int, @Puertas int,
										@Kilometraje int, @DVH char(64) as

insert into VehiculoProspecto(Id, Patente, NumeroChasis, NumeroMotor, Marca_Id, Modelo_Id, Version_Id, Color_Id, Anio, Puertas, Kilometraje, DVH)
values (@Id, @Patente, @NumeroChasis, @NumeroMotor, @Marca_Id, @Modelo_Id, @Version_Id, @Color_Id, @Anio, @Puertas, @Kilometraje, @DVH)

select 'success' as SQLResultType,'Prospect vehicle added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ComprobanteCompra_GetByAcquisition @Compra_Id uniqueidentifier as

-- El PDF del comprobante se arma siempre con lo que se guardo al emitirlo.
select	a.Id,
        a.Numero,
        a.Compra_Id,
        a.Fecha,
        a.NombrePersona,
        a.CuilCuit,
        a.Detalle,
        a.MontoNeto,
        a.MontoImpuesto,
        a.MontoTotal
from	ComprobanteCompra a
where	a.Compra_Id = @Compra_Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc ComprobanteCompra_GetNextNumber as

-- Igual que el numero de adquisicion: entra en el DVH, asi que se pide antes del insert.
select NEXT VALUE FOR dbo.ComprobanteCompraNumbers as Numero
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Cotizacion_Add	@Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Precio float, @Fecha datetime , @DVH char(64) as

insert into Cotizacion(Id, Vehiculo_Id, Persona_Id, Precio, Fecha,DVH)
values (@Id, @Vehiculo_Id, @Persona_Id, @Precio, @Fecha,@DVH)

select 'success' as SQLResultType,'Quotations added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Cotizacion_GetAll @Id uniqueidentifier as

select	a.Id,
        a.Vehiculo_Id, (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id, g.Nombre as Persona_Nombre, g.Apellido as Persona_Apellido, g.DNI as Persona_DNI, 
        a.Precio,
        a.Fecha
from	Cotizacion a
        join Vehiculo b on (b.Id = a.Vehiculo_Id)
		join Modelo c on (c.Id = b.Modelo_Id)
		join Version d on (d.Id = b.Version_Id)
		join Color e on (e.Id = b.Color_Id)
        join Marca f on (f.Id = b.Marca_Id)
        join Persona g on (g.Id = a.Persona_Id)
where	a.Id = @Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Cotizacion_GetAllActiveByPerson @Persona_Id uniqueidentifier as

select	a.Id,
        a.Vehiculo_Id, (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id, g.Nombre as Persona_Nombre, g.Apellido as Persona_Apellido, g.DNI as Persona_DNI, 
        a.Precio,
        a.Fecha
from	Cotizacion a
        join Vehiculo b on (b.Id = a.Vehiculo_Id)
		join Modelo c on (c.Id = b.Modelo_Id)
		join Version d on (d.Id = b.Version_Id)
		join Color e on (e.Id = b.Color_Id)
        join Marca f on (f.Id = b.Marca_Id)
        join Persona g on (g.Id = a.Persona_Id)
where	a.Persona_Id = @Persona_Id
        and cast(a.Fecha as date) = cast(GETDATE() as date)
        and not exists 
        (
            select * 
            from Reserva r 
            left join Factura i on (i.Reserva_Id = r.Id and i.EstadoPago = 1)
            where r.Vehiculo_Id = a.Vehiculo_Id 
            and 
            (
                i.Reserva_Id is not null
                or 
                (cast(getdate() as date) >= cast(r.Fecha as date)) and (cast(getdate() as date) <= cast(r.FechaVencimiento as date))
            )
        )
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Cotizacion_GetById @Id uniqueidentifier as

select	a.Id,
        a.Vehiculo_Id, (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id, g.Nombre as Persona_Nombre, g.Apellido as Persona_Apellido, g.DNI as Persona_DNI, 
        a.Precio,
        a.Fecha
from	Cotizacion a
        join Vehiculo b on (b.Id = a.Vehiculo_Id)
		join Modelo c on (c.Id = b.Modelo_Id)
		join Version d on (d.Id = b.Version_Id)
		join Color e on (e.Id = b.Color_Id)
        join Marca f on (f.Id = b.Marca_Id)
        join Persona g on (g.Id = a.Persona_Id)
where	a.Id = @Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Reserva_Add	@Id uniqueidentifier, @Vehiculo_Id uniqueidentifier, @Persona_Id uniqueidentifier, @Precio float, @Fecha datetime, @FechaVencimiento datetime , @DVH char(64) as

insert into Reserva(Id, Vehiculo_Id, Persona_Id, Precio, Fecha,FechaVencimiento,DVH)
values (@Id, @Vehiculo_Id, @Persona_Id, @Precio, @Fecha,@FechaVencimiento,@DVH)

select 'success' as SQLResultType,'Quotations added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Reserva_GetAll @Id uniqueidentifier as

select	a.Id,
        a.Vehiculo_Id, (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id, g.Nombre as Persona_Nombre, g.Apellido as Persona_Apellido, g.DNI as Persona_DNI, 
        a.Precio,
        a.Fecha,
        a.FechaVencimiento
from	Reserva a
        join Vehiculo b on (b.Id = a.Vehiculo_Id)
		join Modelo c on (c.Id = b.Modelo_Id)
		join Version d on (d.Id = b.Version_Id)
		join Color e on (e.Id = b.Color_Id)
        join Marca f on (f.Id = b.Marca_Id)
        join Persona g on (g.Id = a.Persona_Id)
where	a.Id = @Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Reserva_GetAllActiveByPerson @Persona_Id uniqueidentifier as

select	a.Id,
        a.Vehiculo_Id, (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id, g.Nombre as Persona_Nombre, g.Apellido as Persona_Apellido, g.DNI as Persona_DNI, 
        a.Precio,
        a.Fecha,
        a.FechaVencimiento
from	Reserva a
        join Vehiculo b on (b.Id = a.Vehiculo_Id)
		join Modelo c on (c.Id = b.Modelo_Id)
		join Version d on (d.Id = b.Version_Id)
		join Color e on (e.Id = b.Color_Id)
        join Marca f on (f.Id = b.Marca_Id)
        join Persona g on (g.Id = a.Persona_Id)
        left join Factura i on (i.Reserva_Id = a.Id and i.EstadoPago = 1)
where	a.Persona_Id = @Persona_Id
        and (cast(getdate() as date) >= cast(a.Fecha as date)) and (cast(getdate() as date) <= cast(a.FechaVencimiento as date))
        and i.Id is null
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Reserva_GetById @Id uniqueidentifier as

select	a.Id,
        a.Vehiculo_Id, (cast(b.Anio as varchar) + ' ' + e.Descripcion + ' ' + f.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + b.Patente) as Vehiculo_Descripcion,
        a.Persona_Id, g.Nombre as Persona_Nombre, g.Apellido as Persona_Apellido, g.DNI as Persona_DNI, 
        a.Precio,
        a.Fecha,
        a.FechaVencimiento
from	Reserva a
        join Vehiculo b on (b.Id = a.Vehiculo_Id)
		join Modelo c on (c.Id = b.Modelo_Id)
		join Version d on (d.Id = b.Version_Id)
		join Color e on (e.Id = b.Color_Id)
        join Marca f on (f.Id = b.Marca_Id)
        join Persona g on (g.Id = a.Persona_Id)
where	a.Id = @Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_Add	@Id uniqueidentifier, @DNI int, @NombreUsuario varchar(50), @Clave varchar(max), @Nombre nvarchar(max), @Apellido nvarchar(max),
											@Rol_Id uniqueidentifier, @Bloqueado bit, @Activo bit, @IntentosDisponibles int, @CodigoIdioma varchar(2) , @DVH char(64) as

insert into Usuario(Id,DNI,NombreUsuario,Clave,Nombre,Apellido,Rol_Id,Bloqueado,Activo,IntentosDisponibles,CodigoIdioma,DVH)
values (@Id,@DNI,@NombreUsuario,@Clave,@Nombre,@Apellido,@Rol_Id,@Bloqueado,@Activo,@IntentosDisponibles,@CodigoIdioma,@DVH)

select 'success' as SQLResultType,'User added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_Delete	@Id uniqueidentifier as

delete Usuario
where Id = @Id

select 'success' as SQLResultType,'User deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_DeleteAllPermissions @Id varchar(50) as

delete from Users_Permissions where User_id=@Id;
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetAll as

select	u.Id,u.NombreUsuario
from	Usuario u 
order by u.NombreUsuario asc	
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetAllByState @Activo bit as

select	u.Id, u.DNI, u.NombreUsuario, Clave='' , u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	(@Activo = 1 and u.Activo = @Activo) or @Activo = 0 --Si active es 0 muestro todos los usuarios. 
order by u.NombreUsuario asc	
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetUserByDni @DNI int as

select	u.Id, u.DNI, u.NombreUsuario,u.Clave, u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	u.DNI = @DNI
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetUserById @Id uniqueidentifier as

select	u.Id, u.DNI, u.NombreUsuario,u.Clave, u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	u.Id = @Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_GetUserByUsername @NombreUsuario varchar(50) as

select	u.Id, u.DNI,u.NombreUsuario,u.Clave, u.Nombre, u.Apellido, u.Rol_Id, u.Bloqueado, u.Activo, u.IntentosDisponibles, u.CodigoIdioma
from	Usuario u 
where	u.NombreUsuario = @NombreUsuario
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_InsertPermission @User_Id UniqueIdentifier, @Permission_Id UniqueIdentifier as

insert into Users_Permissions(User_id,Permission_id) values (@User_Id,@Permission_Id)
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Usuario_Update	@Id uniqueidentifier, @DNI int, @NombreUsuario varchar(50), @Clave varchar(max), @Nombre nvarchar(max), @Apellido nvarchar(max),
											@Rol_Id uniqueidentifier, @Bloqueado bit, @Activo bit, @IntentosDisponibles int, @CodigoIdioma varchar(2) , @DVH char(64) as

update Usuario
set DVH = @DVH,
	DNI = @DNI,
	NombreUsuario = @NombreUsuario,
	Clave = @Clave,
	Nombre = @Nombre,
	Apellido = @Apellido,
	Rol_Id = @Rol_Id,
	Bloqueado = @Bloqueado,
	Activo = @Activo,
	IntentosDisponibles = @IntentosDisponibles,
	CodigoIdioma = @CodigoIdioma
where Id = @Id

select 'success' as SQLResultType,'User updated succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_Add	@Id uniqueidentifier, @Patente varchar(255), @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier, @Version_Id uniqueidentifier, @Color_Id uniqueidentifier,
									@Precio float, @Opcionales varchar(255), @Observaciones varchar(max), @Puertas int, @Anio int, @Kilometraje int, @LinkImagen nvarchar(max) , @DVH char(64) as

insert into Vehiculo(Id,Patente,Marca_Id,Modelo_Id,Version_Id,Color_Id,Precio,Opcionales,Observaciones,Puertas,Anio,Kilometraje,LinkImagen,DVH)
values (@Id,@Patente,@Marca_Id,@Modelo_Id,@Version_Id,@Color_Id,@Precio,@Opcionales,@Observaciones,@Puertas,@Anio,@Kilometraje,@LinkImagen,@DVH)

select 'success' as SQLResultType,'Vehicle added succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_Delete	@Id uniqueidentifier as

delete Vehiculo
where Id = @Id

select 'success' as SQLResultType,'Vehicle deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_GetActiveVehiclesByFilters @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier , @Version_Id uniqueidentifier , @Color_Id uniqueidentifier , @Price_From float, 
                                                         @Price_To float, @Year_From int, @Year_To int, @Doors_From int, @Doors_To int, @Kilometers_From int, @Kilometers_To int as

select	a.Id,
        a.Patente,
        a.Marca_Id, b.Descripcion as Marca_Descripcion,
        a.Modelo_Id, c.Descripcion as Modelo_Descripcion, 
        a.Version_Id, d.Descripcion as Version_Descripcion, 
        a.Color_Id, e.Descripcion as Color_Descripcion, 
        a.Precio,
        a.Opcionales, 
        a.Observaciones,
        a.Puertas, 
        a.Anio, 
        a.Kilometraje, 
        a.LinkImagen
from	Vehiculo a
		join Marca b on (b.Id = a.Marca_Id)
		join Modelo c on (c.Id = a.Modelo_Id)
		join Version d on (d.Id = a.Version_Id)
		join Color e on (e.Id = a.Color_Id)
where	(a.Marca_Id = @Marca_Id or (@Marca_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Modelo_Id = @Modelo_Id or (@Modelo_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Version_Id = @Version_Id or (@Version_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Color_Id = @Color_Id or (@Color_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
        and (a.Precio >= @Price_From or (@Price_From = 0 or @Price_From is null))
        and (a.Precio <= @Price_To or (@Price_To = 0 or @Price_To is null))
        and (a.Kilometraje >= @Kilometers_From or (@Kilometers_From = 0 or @Kilometers_From is null))
        and (a.Kilometraje <= @Kilometers_To or (@Kilometers_To = 0 or @Kilometers_To is null))
        and (a.Puertas >= @Doors_From or (@Doors_From = 0 or @Doors_From is null))
        and (a.Puertas <= @Doors_To or (@Doors_To = 0 or @Doors_To is null))
        and (a.Anio >= @Year_From or (@Year_From = 0 or @Year_From is null))
        and (a.Anio <= @Year_To or (@Year_To = 0 or @Year_To is null))
        and not exists 
        (
            select * 
            from Reserva r 
            left join Factura i on (i.Reserva_Id = r.Id and i.EstadoPago = 1)
            where r.Vehiculo_Id = a.Id 
            and 
            (
                i.Reserva_Id is not null
                or 
                (cast(getdate() as date) >= cast(r.Fecha as date)) and (cast(getdate() as date) <= cast(r.FechaVencimiento as date))
            )
        )
order by b.Descripcion, c.Descripcion, d.Descripcion, e.Descripcion, a.Anio, a.Kilometraje
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_GetAll as

select	a.Id,
        a.Patente,
        a.Marca_Id, b.Descripcion as Marca_Descripcion,
        a.Modelo_Id, c.Descripcion as Modelo_Descripcion, 
        a.Version_Id, d.Descripcion as Version_Descripcion, 
        a.Color_Id, e.Descripcion as Color_Descripcion, 
        a.Precio,
        a.Opcionales, 
        a.Observaciones,
        a.Puertas, 
        a.Anio, 
        a.Kilometraje, 
        a.LinkImagen
from	Vehiculo a
		join Marca b on (b.Id = a.Marca_Id)
		join Modelo c on (c.Id = a.Modelo_Id)
		join Version d on (d.Id = a.Version_Id)
		join Color e on (e.Id = a.Color_Id)
order by b.Descripcion, c.Descripcion, d.Descripcion, e.Descripcion, a.Anio, a.Kilometraje
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_GetById @Id uniqueidentifier as

select	a.Id,
        a.Patente,
        a.Marca_Id, b.Descripcion as Marca_Descripcion,
        a.Modelo_Id, c.Descripcion as Modelo_Descripcion, 
        a.Version_Id, d.Descripcion as Version_Descripcion, 
        a.Color_Id, e.Descripcion as Color_Descripcion, 
        a.Precio,
        a.Opcionales, 
        a.Observaciones,
        a.Puertas, 
        a.Anio, 
        a.Kilometraje, 
        a.LinkImagen
from	Vehiculo a
		join Marca b on (b.Id = a.Marca_Id)
		join Modelo c on (c.Id = a.Modelo_Id)
		join Version d on (d.Id = a.Version_Id)
		join Color e on (e.Id = a.Color_Id)
where	a.Id = @Id
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Vehiculo_Update	@Id uniqueidentifier, @Patente varchar(255), @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier, @Version_Id uniqueidentifier, @Color_Id uniqueidentifier,
									@Precio float, @Opcionales varchar(255), @Observaciones varchar(max), @Puertas int, @Anio int, @Kilometraje int, @LinkImagen nvarchar(max) , @DVH char(64) as

update Vehiculo
set DVH = @DVH,
	Patente = @Patente,
	Marca_Id = @Marca_Id,
	Modelo_Id = @Modelo_Id,
	Version_Id = @Version_Id,
	Color_Id = @Color_Id,
	Precio = @Precio,
	Opcionales = @Opcionales,
	Observaciones = @Observaciones,
	Puertas = @Puertas,
	Anio = @Anio,
	Kilometraje = @Kilometraje,
	LinkImagen = @LinkImagen
where Id = @Id

select 'success' as SQLResultType,'Vehicle updated succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Version_Delete	@Id uniqueidentifier as

delete Version
where Id = @Id

select 'success' as SQLResultType,'Version deleted succesfully!' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Version_GetAllByMakeModel @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier as

select	a.Id, a.Marca_Id, b.Descripcion as Marca_Descripcion, a.Modelo_Id, c.Descripcion as Modelo_Descripcion, a.Descripcion 
from	Version a
		join Marca b on (b.Id = a.Marca_Id)
		join Modelo c on (c.Id = a.Modelo_Id)
where	a.Marca_Id = @Marca_Id
		and a.Modelo_Id = @Modelo_Id
order by a.Descripcion asc
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO
CREATE   proc Version_Insert @Id uniqueidentifier, @Marca_Id uniqueidentifier, @Modelo_Id uniqueidentifier, @Descripcion varchar(255) , @DVH char(64) as

if not exists (select * from Version where Marca_Id = @Marca_Id and Modelo_Id = @Modelo_Id and Descripcion = @Descripcion)
begin
	insert into Version(Id,Marca_Id,Modelo_Id,Descripcion,DVH) values (@Id,@Marca_Id,@Modelo_Id,@Descripcion,@DVH);
end

select 'success' as SQLResultType, 'Operation completed.' as message
GO
SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
GO

COMMIT TRANSACTION
GO
