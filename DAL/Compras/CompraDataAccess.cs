using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Compras
{
    // Compra de vehiculos a personas (PN2): vehiculo ofrecido, adquisicion, inspeccion mecanica, tasacion,
    // forma de pago pactada, documentos, pagos a la persona, comprobante y cierre. AcquisitionDocumentFiles no esta
    // protegida con digitos verificadores.
    public sealed class CompraDataAccess : DataAccessBase
    {
        public DataTable GetNextNumber() { return Read("Compra_GetNextNumber"); }

        public DataTable GetDuplicates(string licensePlate, string chassisNumber, string engineNumber)
        {
            return Read("Compra_GetDuplicates",
                new SqlParameter("@Patente", licensePlate),
                new SqlParameter("@NumeroChasis", chassisNumber),
                new SqlParameter("@NumeroMotor", engineNumber));
        }

        public DataTable GetMechanics(string patentType) { return Read("Inspeccion_GetMechanics", new SqlParameter("@Tipo", patentType)); }

        public DataTable IsMechanicBusy(Guid mechanicId, DateTime scheduledDate, int slotMinutes)
        {
            return Read("Inspeccion_IsMechanicBusy",
                new SqlParameter("@Mecanico_Id", mechanicId),
                new SqlParameter("@FechaProgramada", scheduledDate),
                new SqlParameter("@Slot_Minutes", slotMinutes));
        }

        public DataTable GetNextFreeSlot(Guid mechanicId, DateTime from, int slotMinutes, int dayStartHour, int dayEndHour, int maxDays)
        {
            return Read("Inspeccion_GetNextFreeSlot",
                new SqlParameter("@Mecanico_Id", mechanicId),
                new SqlParameter("@From", from),
                new SqlParameter("@Slot_Minutes", slotMinutes),
                new SqlParameter("@Day_Start_Hour", dayStartHour),
                new SqlParameter("@Day_End_Hour", dayEndHour),
                new SqlParameter("@Max_Days", maxDays));
        }

        public DataTable AddProspectVehicle(Guid id, string licensePlate, string chassisNumber, string engineNumber, Guid makeId, Guid modelId,
            Guid versionId, Guid colourId, int year, int doors, int declaredKilometers, DvhCalculator dvh)
        {
            return Read("VehiculoProspecto_Add", dvh,
                new SqlParameter("@VehiculoProspecto_Id", id),
                new SqlParameter("@Patente", licensePlate),
                new SqlParameter("@NumeroChasis", chassisNumber),
                new SqlParameter("@NumeroMotor", engineNumber),
                new SqlParameter("@Marca_Id", makeId),
                new SqlParameter("@Modelo_Id", modelId),
                new SqlParameter("@Version_Id", versionId),
                new SqlParameter("@Color_Id", colourId),
                new SqlParameter("@Anio", year),
                new SqlParameter("@Puertas", doors),
                new SqlParameter("@Kilometraje", declaredKilometers));
        }

        // Todas las columnas viajan aunque esten vacias: el DVH se calcula con la fila completa.
        public DataTable Add(Guid id, int number, Guid personId, Guid prospectVehicleId, Guid salesmanId, int status, int? rejectionReason,
            DateTime creationDate, Guid? acceptedAppraisalId, double? agreedPrice, string ownerCuilCuit, DateTime? contractSignedDate,
            string prechargeCode08, DateTime? transferAppointmentDate, Guid? vehicleId, DateTime? closingDate, DvhCalculator dvh)
        {
            return Read("Compra_Add", dvh,
                new SqlParameter("@Compra_Id", id),
                new SqlParameter("@Numero", number),
                new SqlParameter("@Persona_Id", personId),
                new SqlParameter("@VehiculoProspecto_Id", prospectVehicleId),
                new SqlParameter("@Vendedor_Id", salesmanId),
                new SqlParameter("@Estado", status),
                new SqlParameter("@MotivoRechazo", (object)rejectionReason ?? DBNull.Value),
                new SqlParameter("@Fecha", creationDate),
                new SqlParameter("@TasacionAceptada_Id", (object)acceptedAppraisalId ?? DBNull.Value),
                new SqlParameter("@PrecioAcordado", (object)agreedPrice ?? DBNull.Value),
                new SqlParameter("@CuilCuitTitular", (object)ownerCuilCuit ?? DBNull.Value),
                new SqlParameter("@FechaFirmaContrato", (object)contractSignedDate ?? DBNull.Value),
                new SqlParameter("@CodigoPrecarga08", (object)prechargeCode08 ?? DBNull.Value),
                new SqlParameter("@FechaCitaTransferencia", (object)transferAppointmentDate ?? DBNull.Value),
                new SqlParameter("@Vehiculo_Id", (object)vehicleId ?? DBNull.Value),
                new SqlParameter("@FechaCierre", (object)closingDate ?? DBNull.Value));
        }

        public DataTable AddInspection(Guid id, Guid acquisitionId, Guid mechanicId, DateTime scheduledDate, DateTime? performedDate,
            int? verifiedKilometers, int? result, string observations, DvhCalculator dvh)
        {
            return Read("Inspeccion_Add", dvh,
                new SqlParameter("@Inspeccion_Id", id),
                new SqlParameter("@Compra_Id", acquisitionId),
                new SqlParameter("@Mecanico_Id", mechanicId),
                new SqlParameter("@FechaProgramada", scheduledDate),
                new SqlParameter("@FechaRealizada", (object)performedDate ?? DBNull.Value),
                new SqlParameter("@KilometrajeVerificado", (object)verifiedKilometers ?? DBNull.Value),
                new SqlParameter("@Resultado", (object)result ?? DBNull.Value),
                new SqlParameter("@Observaciones", (object)observations ?? DBNull.Value));
        }

        public DataTable GetById(Guid id) { return Read("Compra_GetById", new SqlParameter("@Compra_Id", id)); }
        public DataTable GetScheduledInspections(Guid mechanicId) { return Read("Inspeccion_GetScheduledByMechanic", new SqlParameter("@Mecanico_Id", mechanicId)); }
        public DataTable GetInspectionDetail(Guid id) { return Read("Inspeccion_GetDetail", new SqlParameter("@Inspeccion_Id", id)); }
        public DataTable GetInspectionItems() { return Read("ItemInspeccion_GetAll"); }

        public DataTable GetInspectionPhotos(Guid inspectionId)
        {
            return Read("FotografiaInspeccion_GetByInspection", new SqlParameter("@Inspeccion_Id", inspectionId));
        }

        public DataTable AddInspectionPhoto(Guid id, Guid inspectionId, string fileName, byte[] fileContent, DateTime uploadedDate)
        {
            return Read("FotografiaInspeccion_Add",
                new SqlParameter("@FotografiaInspeccion_Id", id),
                new SqlParameter("@Inspeccion_Id", inspectionId),
                new SqlParameter("@NombreArchivo", fileName),
                new SqlParameter("@Contenido", fileContent),
                new SqlParameter("@FechaCarga", uploadedDate));
        }

        public DataTable DeleteInspectionPhoto(Guid id) { return Read("FotografiaInspeccion_Delete", new SqlParameter("@FotografiaInspeccion_Id", id)); }

        // Inspeccion, calificaciones y adquisicion van en un solo SP, dentro de una transaccion. Cada fila llega
        // completa (columna -> valor) porque el DVH se calcula con todas; al SP solo viaja lo que cambia.
        public DataTable RecordInspection(IDictionary<string, object> inspection, IList<IDictionary<string, object>> scores,
            IDictionary<string, object> acquisition, DvhCalculator inspectionDvh, DvhCalculator scoreDvh, DvhCalculator acquisitionDvh)
        {
            DvhResult inspectionRow = inspectionDvh(inspection);
            DvhResult acquisitionRow = acquisitionDvh(acquisition);
            using (var scoreRows = new DataTable())
            {
                scoreRows.Columns.Add("ItemInspeccion_Id", typeof(Guid));
                scoreRows.Columns.Add("Puntaje", typeof(int));
                scoreRows.Columns.Add("Observaciones", typeof(string));
                scoreRows.Columns.Add("DVH", typeof(string));
                foreach (IDictionary<string, object> score in scores)
                {
                    DvhResult scoreRow = scoreDvh(score);
                    scoreRows.Rows.Add(Value(scoreRow, "ItemInspeccion_Id"), Value(scoreRow, "Puntaje"), Value(scoreRow, "Observaciones"), scoreRow.Dvh);
                }

                return Read("Inspeccion_Record",
                    new SqlParameter("@Inspeccion_Id", Value(inspectionRow, "Inspeccion_Id")),
                    new SqlParameter("@Compra_Id", Value(inspectionRow, "Compra_Id")),
                    new SqlParameter("@FechaRealizada", SqlDbType.DateTime) { Value = Value(inspectionRow, "FechaRealizada") },
                    new SqlParameter("@KilometrajeVerificado", SqlDbType.Int) { Value = Value(inspectionRow, "KilometrajeVerificado") },
                    new SqlParameter("@Resultado", SqlDbType.Int) { Value = Value(inspectionRow, "Resultado") },
                    new SqlParameter("@Observaciones", SqlDbType.VarChar, -1) { Value = Value(inspectionRow, "Observaciones") },
                    new SqlParameter("@DVH", SqlDbType.Char, 64) { Value = inspectionRow.Dvh },
                    new SqlParameter("@Scores", SqlDbType.Structured) { TypeName = "dbo.PuntajeInspeccionRows", Value = scoreRows },
                    new SqlParameter("@Estado", SqlDbType.Int) { Value = Value(acquisitionRow, "Estado") },
                    new SqlParameter("@MotivoRechazo", SqlDbType.Int) { Value = Value(acquisitionRow, "MotivoRechazo") },
                    new SqlParameter("@Acquisition_DVH", SqlDbType.Char, 64) { Value = acquisitionRow.Dvh });
            }
        }

        public DataTable GetPendingAppraisals(string filter, DateTime now)
        {
            return Read("Compra_GetPendingAppraisal",
                new SqlParameter("@Filter", (object)filter ?? DBNull.Value),
                new SqlParameter("@Now", now));
        }

        public DataTable GetInspection(Guid id) { return Read("Inspeccion_GetById", new SqlParameter("@Inspeccion_Id", id)); }

        public DataTable GetInspectionScores(Guid inspectionId)
        {
            return Read("PuntajeInspeccion_GetByInspection", new SqlParameter("@Inspeccion_Id", inspectionId));
        }

        public DataTable GetLastAppraisal(Guid acquisitionId)
        {
            return Read("Tasacion_GetLastByAcquisition", new SqlParameter("@Compra_Id", acquisitionId));
        }

        public DataTable AddAppraisal(Guid id, Guid acquisitionId, Guid inspectionId, Guid userId, double amount, string justification,
            DateTime creationDate, DateTime expirationDate, DvhCalculator dvh)
        {
            return Read("Tasacion_Add", dvh,
                new SqlParameter("@Tasacion_Id", id),
                new SqlParameter("@Compra_Id", acquisitionId),
                new SqlParameter("@Inspeccion_Id", inspectionId),
                new SqlParameter("@Tasador_Id", userId),
                new SqlParameter("@Valor", amount),
                new SqlParameter("@Justificacion", justification),
                new SqlParameter("@Fecha", creationDate),
                new SqlParameter("@Vigencia", expirationDate));
        }

        // Igual que el alta: todas las columnas viajan para que el DVH se calcule con la fila completa.
        public DataTable Update(Guid id, int number, Guid personId, Guid prospectVehicleId, Guid salesmanId, int status, int? rejectionReason,
            DateTime creationDate, Guid? acceptedAppraisalId, double? agreedPrice, string ownerCuilCuit, DateTime? contractSignedDate,
            string prechargeCode08, DateTime? transferAppointmentDate, Guid? vehicleId, DateTime? closingDate, DvhCalculator dvh)
        {
            return Read("Compra_Update", dvh,
                new SqlParameter("@Compra_Id", id),
                new SqlParameter("@Numero", number),
                new SqlParameter("@Persona_Id", personId),
                new SqlParameter("@VehiculoProspecto_Id", prospectVehicleId),
                new SqlParameter("@Vendedor_Id", salesmanId),
                new SqlParameter("@Estado", status),
                new SqlParameter("@MotivoRechazo", (object)rejectionReason ?? DBNull.Value),
                new SqlParameter("@Fecha", creationDate),
                new SqlParameter("@TasacionAceptada_Id", (object)acceptedAppraisalId ?? DBNull.Value),
                new SqlParameter("@PrecioAcordado", (object)agreedPrice ?? DBNull.Value),
                new SqlParameter("@CuilCuitTitular", (object)ownerCuilCuit ?? DBNull.Value),
                new SqlParameter("@FechaFirmaContrato", (object)contractSignedDate ?? DBNull.Value),
                new SqlParameter("@CodigoPrecarga08", (object)prechargeCode08 ?? DBNull.Value),
                new SqlParameter("@FechaCitaTransferencia", (object)transferAppointmentDate ?? DBNull.Value),
                new SqlParameter("@Vehiculo_Id", (object)vehicleId ?? DBNull.Value),
                new SqlParameter("@FechaCierre", (object)closingDate ?? DBNull.Value));
        }

        public DataTable GetPendingManagement(string filter)
        {
            return Read("Compra_GetPendingManagement", new SqlParameter("@Filter", (object)filter ?? DBNull.Value));
        }

        public DataTable GetPaymentTerms(Guid acquisitionId)
        {
            return Read("FormaPagoCompra_GetByAcquisition", new SqlParameter("@Compra_Id", acquisitionId));
        }

        public DataTable AddPaymentTerm(Guid acquisitionId, Guid paymentTypeId, double amount, DvhCalculator dvh)
        {
            return Read("FormaPagoCompra_Add", dvh,
                new SqlParameter("@Compra_Id", acquisitionId),
                new SqlParameter("@TipoPago_Id", paymentTypeId),
                new SqlParameter("@Monto", amount));
        }

        public DataTable DeletePaymentTerms(Guid acquisitionId)
        {
            return Read("FormaPagoCompra_DeleteByAcquisition", new SqlParameter("@Compra_Id", acquisitionId));
        }

        public DataTable GetDocumentTypes(int stage) { return Read("TipoDocumentoCompra_GetByStage", new SqlParameter("@Etapa", stage)); }

        public DataTable GetDocuments(Guid acquisitionId)
        {
            return Read("DocumentoCompra_GetByAcquisition", new SqlParameter("@Compra_Id", acquisitionId));
        }

        public DataTable AddDocument(Guid id, Guid acquisitionId, Guid documentTypeId, int status, DateTime issueDate, string observation,
            Guid userId, DateTime uploadedDate, DvhCalculator dvh)
        {
            return Read("DocumentoCompra_Add", dvh, DocumentParameters(id, acquisitionId, documentTypeId, status, issueDate, observation,
                userId, uploadedDate));
        }

        public DataTable ObserveDocument(Guid id, Guid acquisitionId, Guid documentTypeId, int status, DateTime issueDate, string observation,
            Guid userId, DateTime uploadedDate, DvhCalculator dvh)
        {
            return Read("DocumentoCompra_Observe", dvh, DocumentParameters(id, acquisitionId, documentTypeId, status, issueDate, observation,
                userId, uploadedDate));
        }

        public DataTable DeleteDocument(Guid id) { return Read("DocumentoCompra_Delete", new SqlParameter("@DocumentoCompra_Id", id)); }

        public DataTable GetDocumentFile(Guid documentId)
        {
            return Read("ArchivoDocumentoCompra_GetByDocument", new SqlParameter("@DocumentoCompra_Id", documentId));
        }

        public DataTable AddDocumentFile(Guid documentId, string fileName, byte[] fileContent)
        {
            return Read("ArchivoDocumentoCompra_Add",
                new SqlParameter("@DocumentoCompra_Id", documentId),
                new SqlParameter("@NombreArchivo", fileName),
                new SqlParameter("@Contenido", fileContent));
        }

        public DataTable GetSummary(Guid id) { return Read("Compra_GetSummary", new SqlParameter("@Compra_Id", id)); }

        // Alta en inventario y cierre de la compra en un solo SP, dentro de una transaccion. Igual que en la
        // inspeccion, las filas llegan completas para el DVH y al SP solo viaja lo que cambia de la adquisicion.
        public DataTable Finish(IDictionary<string, object> vehicle, IDictionary<string, object> acquisition, DvhCalculator vehicleDvh,
            DvhCalculator acquisitionDvh)
        {
            DvhResult vehicleRow = vehicleDvh(vehicle);
            DvhResult acquisitionRow = acquisitionDvh(acquisition);
            return Read("Compra_Finish",
                new SqlParameter("@Vehiculo_Id", Value(vehicleRow, "Vehiculo_Id")),
                new SqlParameter("@Patente", SqlDbType.VarChar, 255) { Value = Value(vehicleRow, "Patente") },
                new SqlParameter("@Marca_Id", Value(vehicleRow, "Marca_Id")),
                new SqlParameter("@Modelo_Id", Value(vehicleRow, "Modelo_Id")),
                new SqlParameter("@Version_Id", Value(vehicleRow, "Version_Id")),
                new SqlParameter("@Color_Id", Value(vehicleRow, "Color_Id")),
                new SqlParameter("@Precio", SqlDbType.Float) { Value = Value(vehicleRow, "Precio") },
                new SqlParameter("@Opcionales", SqlDbType.VarChar, 255) { Value = Value(vehicleRow, "Opcionales") },
                new SqlParameter("@Observaciones", SqlDbType.VarChar, -1) { Value = Value(vehicleRow, "Observaciones") },
                new SqlParameter("@Puertas", SqlDbType.Int) { Value = Value(vehicleRow, "Puertas") },
                new SqlParameter("@Anio", SqlDbType.Int) { Value = Value(vehicleRow, "Anio") },
                new SqlParameter("@Kilometraje", SqlDbType.Int) { Value = Value(vehicleRow, "Kilometraje") },
                new SqlParameter("@LinkImagen", SqlDbType.NVarChar, -1) { Value = Value(vehicleRow, "LinkImagen") },
                new SqlParameter("@DVH", SqlDbType.Char, 64) { Value = vehicleRow.Dvh },
                new SqlParameter("@Compra_Id", Value(acquisitionRow, "Compra_Id")),
                new SqlParameter("@Estado", SqlDbType.Int) { Value = Value(acquisitionRow, "Estado") },
                new SqlParameter("@FechaCierre", SqlDbType.DateTime) { Value = Value(acquisitionRow, "FechaCierre") },
                new SqlParameter("@Acquisition_DVH", SqlDbType.Char, 64) { Value = acquisitionRow.Dvh });
        }

        public DataTable GetPendingPayment(Guid personId) { return Read("Compra_GetPendingPayment", new SqlParameter("@Persona_Id", personId)); }

        public DataTable GetPayments(Guid acquisitionId)
        {
            return Read("PagoCompra_GetByAcquisition", new SqlParameter("@Compra_Id", acquisitionId));
        }

        // Las columnas de los otros medios viajan en NULL: el DVH se calcula con la fila completa.
        public DataTable AddPayment(Guid id, Guid acquisitionId, Guid paymentTypeId, Guid cashierId, double amount, string bank, string cbuAlias,
            string operationNumber, string checkNumber, DateTime? paymentDate, DateTime creationDate, DvhCalculator dvh)
        {
            return Read("PagoCompra_Add", dvh,
                new SqlParameter("@PagoCompra_Id", id),
                new SqlParameter("@Compra_Id", acquisitionId),
                new SqlParameter("@TipoPago_Id", paymentTypeId),
                new SqlParameter("@Cajero_Id", cashierId),
                new SqlParameter("@Monto", amount),
                new SqlParameter("@Banco", (object)bank ?? DBNull.Value),
                new SqlParameter("@CbuAlias", (object)cbuAlias ?? DBNull.Value),
                new SqlParameter("@NumeroOperacion", (object)operationNumber ?? DBNull.Value),
                new SqlParameter("@NumeroCheque", (object)checkNumber ?? DBNull.Value),
                new SqlParameter("@FechaPago", (object)paymentDate ?? DBNull.Value),
                new SqlParameter("@FechaCreacion", creationDate));
        }

        public DataTable GetNextReceiptNumber() { return Read("ComprobanteCompra_GetNextNumber"); }

        // Comprobante y pase a Pagada en un solo SP, dentro de una transaccion. Como en el cierre de la compra,
        // las filas llegan completas para el DVH y de la adquisicion solo viaja lo que cambia.
        public DataTable Pay(IDictionary<string, object> receipt, IDictionary<string, object> acquisition, DvhCalculator receiptDvh,
            DvhCalculator acquisitionDvh)
        {
            DvhResult receiptRow = receiptDvh(receipt);
            DvhResult acquisitionRow = acquisitionDvh(acquisition);
            return Read("Compra_Pay",
                new SqlParameter("@ComprobanteCompra_Id", Value(receiptRow, "ComprobanteCompra_Id")),
                new SqlParameter("@Numero", SqlDbType.Int) { Value = Value(receiptRow, "Numero") },
                new SqlParameter("@Compra_Id", Value(receiptRow, "Compra_Id")),
                new SqlParameter("@Fecha", SqlDbType.DateTime) { Value = Value(receiptRow, "Fecha") },
                new SqlParameter("@NombrePersona", SqlDbType.VarChar, 255) { Value = Value(receiptRow, "NombrePersona") },
                new SqlParameter("@CuilCuit", SqlDbType.VarChar, 13) { Value = Value(receiptRow, "CuilCuit") },
                new SqlParameter("@Detalle", SqlDbType.VarChar, -1) { Value = Value(receiptRow, "Detalle") },
                new SqlParameter("@MontoNeto", SqlDbType.Float) { Value = Value(receiptRow, "MontoNeto") },
                new SqlParameter("@MontoImpuesto", SqlDbType.Float) { Value = Value(receiptRow, "MontoImpuesto") },
                new SqlParameter("@MontoTotal", SqlDbType.Float) { Value = Value(receiptRow, "MontoTotal") },
                new SqlParameter("@DVH", SqlDbType.Char, 64) { Value = receiptRow.Dvh },
                new SqlParameter("@Estado", SqlDbType.Int) { Value = Value(acquisitionRow, "Estado") },
                new SqlParameter("@Acquisition_DVH", SqlDbType.Char, 64) { Value = acquisitionRow.Dvh });
        }

        public DataTable GetReceipt(Guid acquisitionId)
        {
            return Read("ComprobanteCompra_GetByAcquisition", new SqlParameter("@Compra_Id", acquisitionId));
        }

        private static SqlParameter[] DocumentParameters(Guid id, Guid acquisitionId, Guid documentTypeId, int status, DateTime issueDate,
            string observation, Guid userId, DateTime uploadedDate)
        {
            return new[]
            {
                new SqlParameter("@DocumentoCompra_Id", id),
                new SqlParameter("@Compra_Id", acquisitionId),
                new SqlParameter("@TipoDocumentoCompra_Id", documentTypeId),
                new SqlParameter("@Estado", status),
                new SqlParameter("@FechaEmision", issueDate),
                new SqlParameter("@Observacion", (object)observation ?? DBNull.Value),
                new SqlParameter("@UsuarioCarga_Id", userId),
                new SqlParameter("@FechaCarga", uploadedDate)
            };
        }

        // Valor normalizado con el que se calculo el DVH: es el que tiene que quedar guardado.
        private static object Value(DvhResult row, string column)
        {
            return row.Columns.Single(c => string.Equals(c.Name, column, StringComparison.OrdinalIgnoreCase)).Value ?? DBNull.Value;
        }
    }
}
