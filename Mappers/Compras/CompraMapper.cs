using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Compras;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class CompraMapper
    {
        private readonly CompraDataAccess data = new CompraDataAccess();

        public int GetNextNumber()
        {
            using (DataTable table = data.GetNextNumber())
                return (int)table.Rows[0]["Numero"];
        }

        // Sin coincidencias devuelve null.
        public List<DuplicateVehiculo> GetDuplicates(VehiculoProspecto vehicle)
        {
            List<DuplicateVehiculo> duplicates = MappingHandler.MapTableToEntities<DuplicateVehiculo>(data.GetDuplicates(vehicle.Patente,
                vehicle.NumeroChasis, vehicle.NumeroMotor));
            return duplicates.Count == 0 ? null : duplicates;
        }

        public List<Usuario> GetMechanics(PermisoType patent)
        {
            return MappingHandler.MapTableToEntities<Usuario>(data.GetMechanics(patent.ToString()));
        }

        public bool IsMechanicBusy(Guid Mechanic_Id, DateTime Scheduled_Date, int slotMinutes)
        {
            using (DataTable table = data.IsMechanicBusy(Mechanic_Id, Scheduled_Date, slotMinutes))
                return (int)table.Rows[0]["Inspecciones"] > 0;
        }

        // Sin turnos libres en el periodo devuelve null.
        public DateTime? GetNextFreeSlot(Guid Mechanic_Id, DateTime from, int slotMinutes, int dayStartHour, int dayEndHour, int maxDays)
        {
            using (DataTable table = data.GetNextFreeSlot(Mechanic_Id, from, slotMinutes, dayStartHour, dayEndHour, maxDays))
            {
                if (table.Rows.Count == 0) return null;
                return (DateTime)table.Rows[0]["FechaProgramada"];
            }
        }

        public SQLUpdateResult AddProspectVehicle(VehiculoProspecto vehicle)
        {
            return DigitVerifierWriteMapper.Save("VehiculoProspecto", dvh => data.AddProspectVehicle(vehicle.VehiculoProspecto_Id, vehicle.Patente,
                vehicle.NumeroChasis, vehicle.NumeroMotor, vehicle.Marca_Id, vehicle.Modelo_Id, vehicle.Version_Id, vehicle.Color_Id,
                vehicle.Anio, vehicle.Puertas, vehicle.Kilometraje, dvh));
        }

        public SQLUpdateResult AddAcquisition(Compra acquisition)
        {
            return DigitVerifierWriteMapper.Save("Compra", dvh => data.Add(acquisition.Compra_Id, acquisition.Numero, acquisition.Persona_Id,
                acquisition.VehiculoProspecto_Id, acquisition.Vendedor_Id, (int)acquisition.Estado, (int?)acquisition.MotivoRechazo,
                acquisition.Fecha, acquisition.TasacionAceptada_Id, acquisition.PrecioAcordado, acquisition.CuilCuitTitular,
                acquisition.FechaFirmaContrato, acquisition.CodigoPrecarga08, acquisition.FechaCitaTransferencia,
                acquisition.Vehiculo_Id, acquisition.FechaCierre, dvh));
        }

        public SQLUpdateResult AddInspection(Inspeccion inspection)
        {
            return DigitVerifierWriteMapper.Save("Inspeccion", dvh => data.AddInspection(inspection.Inspeccion_Id, inspection.Compra_Id,
                inspection.Mecanico_Id, inspection.FechaProgramada, inspection.FechaRealizada, inspection.KilometrajeVerificado,
                (int?)inspection.Resultado, inspection.Observaciones, dvh));
        }

        // El MappingHandler no convierte NULL ni enums: la adquisicion se arma a mano.
        public Compra GetById(Guid id)
        {
            using (DataTable table = data.GetById(id))
            {
                if (table.Rows.Count == 0) return null;
                DataRow row = table.Rows[0];
                Compra acquisition = new Compra();
                acquisition.Compra_Id = (Guid)row["Compra_Id"];
                acquisition.Numero = (int)row["Numero"];
                acquisition.Persona_Id = (Guid)row["Persona_Id"];
                acquisition.VehiculoProspecto_Id = (Guid)row["VehiculoProspecto_Id"];
                acquisition.Vendedor_Id = (Guid)row["Vendedor_Id"];
                acquisition.Estado = (CompraStatus)(int)row["Estado"];
                acquisition.MotivoRechazo = (RejectionReason?)(row["MotivoRechazo"] as int?);
                acquisition.Fecha = (DateTime)row["Fecha"];
                acquisition.TasacionAceptada_Id = row["TasacionAceptada_Id"] as Guid?;
                acquisition.PrecioAcordado = row["PrecioAcordado"] as double?;
                acquisition.CuilCuitTitular = row["CuilCuitTitular"] as string;
                acquisition.FechaFirmaContrato = row["FechaFirmaContrato"] as DateTime?;
                acquisition.CodigoPrecarga08 = row["CodigoPrecarga08"] as string;
                acquisition.FechaCitaTransferencia = row["FechaCitaTransferencia"] as DateTime?;
                acquisition.Vehiculo_Id = row["Vehiculo_Id"] as Guid?;
                acquisition.FechaCierre = row["FechaCierre"] as DateTime?;
                return acquisition;
            }
        }

        // Sin inspecciones pendientes devuelve null.
        public List<ScheduledInspeccion> GetScheduledInspections(Guid Mechanic_Id)
        {
            List<ScheduledInspeccion> inspections = MappingHandler.MapTableToEntities<ScheduledInspeccion>(data.GetScheduledInspections(Mechanic_Id));
            return inspections.Count == 0 ? null : inspections;
        }

        public InspeccionDetail GetInspectionDetail(Guid id)
        {
            return MappingHandler.MapTableToEntities<InspeccionDetail>(data.GetInspectionDetail(id)).FirstOrDefault();
        }

        public List<ItemInspeccion> GetInspectionItems()
        {
            return MappingHandler.MapTableToEntities<ItemInspeccion>(data.GetInspectionItems());
        }

        // Sin fotos devuelve null.
        public List<FotografiaInspeccion> GetInspectionPhotos(Guid Inspection_Id)
        {
            List<FotografiaInspeccion> photos = MappingHandler.MapTableToEntities<FotografiaInspeccion>(data.GetInspectionPhotos(Inspection_Id));
            return photos.Count == 0 ? null : photos;
        }

        // InspectionPhotos no esta protegida con digitos verificadores.
        public SQLUpdateResult AddInspectionPhoto(FotografiaInspeccion photo)
        {
            return MappingHandler.MapUpdateResult(data.AddInspectionPhoto(photo.FotografiaInspeccion_Id, photo.Inspeccion_Id, photo.NombreArchivo,
                photo.Contenido, photo.FechaCarga));
        }

        public SQLUpdateResult DeleteInspectionPhoto(FotografiaInspeccion photo)
        {
            return MappingHandler.MapUpdateResult(data.DeleteInspectionPhoto(photo.FotografiaInspeccion_Id));
        }

        // Las tres tablas se graban en una sola transaccion y despues se rehacen sus DVV.
        public SQLUpdateResult RecordInspection(Inspeccion inspection, Compra acquisition)
        {
            return DigitVerifierWriteMapper.Save(digitVerifier => data.RecordInspection(InspectionRow(inspection),
                inspection.Puntajes.Select(ScoreRow).ToList(), AcquisitionRow(acquisition), digitVerifier.Dvh("Inspeccion"),
                digitVerifier.Dvh("PuntajeInspeccion"), digitVerifier.Dvh("Compra")), "Inspeccion", "PuntajeInspeccion", "Compra");
        }

        // Sin adquisiciones para tasar devuelve null.
        public List<PendingTasacion> GetPendingAppraisals(string filter, DateTime now)
        {
            List<PendingTasacion> appraisals = MappingHandler.MapTableToEntities<PendingTasacion>(data.GetPendingAppraisals(filter, now));
            return appraisals.Count == 0 ? null : appraisals;
        }

        // Como la adquisicion, la inspeccion registrada tiene NULL y enums: se arma a mano.
        public Inspeccion GetInspection(Guid id)
        {
            using (DataTable table = data.GetInspection(id))
            {
                if (table.Rows.Count == 0) return null;
                DataRow row = table.Rows[0];
                Inspeccion inspection = new Inspeccion();
                inspection.Inspeccion_Id = (Guid)row["Inspeccion_Id"];
                inspection.Compra_Id = (Guid)row["Compra_Id"];
                inspection.Mecanico_Id = (Guid)row["Mecanico_Id"];
                inspection.FechaProgramada = (DateTime)row["FechaProgramada"];
                inspection.FechaRealizada = row["FechaRealizada"] as DateTime?;
                inspection.KilometrajeVerificado = row["KilometrajeVerificado"] as int?;
                inspection.Resultado = (InspeccionResult?)(row["Resultado"] as int?);
                inspection.Observaciones = row["Observaciones"] as string;
                return inspection;
            }
        }

        public List<PuntajeInspeccionDetail> GetInspectionScores(Guid Inspection_Id)
        {
            List<PuntajeInspeccionDetail> scores = new List<PuntajeInspeccionDetail>();
            using (DataTable table = data.GetInspectionScores(Inspection_Id))
            {
                foreach (DataRow row in table.Rows)
                {
                    PuntajeInspeccionDetail score = new PuntajeInspeccionDetail();
                    score.Inspeccion_Id = (Guid)row["Inspeccion_Id"];
                    score.ItemInspeccion_Id = (Guid)row["ItemInspeccion_Id"];
                    score.Descripcion = (string)row["Descripcion"];
                    score.Orden = (int)row["Orden"];
                    score.Puntaje = (InspeccionRating)(int)row["Puntaje"];
                    score.Observaciones = row["Observaciones"] as string;
                    scores.Add(score);
                }
            }
            return scores;
        }

        // Sin tasaciones previas devuelve null.
        public Tasacion GetLastAppraisal(Guid Acquisition_Id)
        {
            return MappingHandler.MapTableToEntities<Tasacion>(data.GetLastAppraisal(Acquisition_Id)).FirstOrDefault();
        }

        public SQLUpdateResult AddAppraisal(Tasacion appraisal)
        {
            return DigitVerifierWriteMapper.Save("Tasacion", dvh => data.AddAppraisal(appraisal.Tasacion_Id, appraisal.Compra_Id,
                appraisal.Inspeccion_Id, appraisal.Tasador_Id, appraisal.Valor, appraisal.Justificacion, appraisal.Fecha,
                appraisal.Vigencia, dvh));
        }

        public SQLUpdateResult UpdateAcquisition(Compra acquisition)
        {
            return DigitVerifierWriteMapper.Save("Compra", dvh => data.Update(acquisition.Compra_Id, acquisition.Numero, acquisition.Persona_Id,
                acquisition.VehiculoProspecto_Id, acquisition.Vendedor_Id, (int)acquisition.Estado, (int?)acquisition.MotivoRechazo,
                acquisition.Fecha, acquisition.TasacionAceptada_Id, acquisition.PrecioAcordado, acquisition.CuilCuitTitular,
                acquisition.FechaFirmaContrato, acquisition.CodigoPrecarga08, acquisition.FechaCitaTransferencia,
                acquisition.Vehiculo_Id, acquisition.FechaCierre, dvh));
        }

        // Sin compras en gestion devuelve null.
        public List<PendingCompra> GetPendingManagement(string filter)
        {
            List<PendingCompra> acquisitions = new List<PendingCompra>();
            using (DataTable table = data.GetPendingManagement(filter))
            {
                foreach (DataRow row in table.Rows)
                {
                    PendingCompra acquisition = new PendingCompra();
                    acquisition.Compra_Id = (Guid)row["Compra_Id"];
                    acquisition.Numero = (int)row["Numero"];
                    acquisition.Inspeccion_Id = (Guid)row["Inspeccion_Id"];
                    acquisition.Persona_Id = (Guid)row["Persona_Id"];
                    acquisition.DNI = (int)row["DNI"];
                    acquisition.Patente = (string)row["Patente"];
                    acquisition.Vehiculo_Descripcion = (string)row["Vehiculo_Descripcion"];
                    acquisition.Estado = (CompraStatus)(int)row["Estado"];
                    acquisitions.Add(acquisition);
                }
            }
            return acquisitions.Count == 0 ? null : acquisitions;
        }

        // Sin forma de pago pactada devuelve null.
        public List<FormaPagoCompra> GetPaymentTerms(Guid Acquisition_Id)
        {
            List<FormaPagoCompra> terms = MappingHandler.MapTableToEntities<FormaPagoCompra>(data.GetPaymentTerms(Acquisition_Id));
            return terms.Count == 0 ? null : terms;
        }

        public SQLUpdateResult AddPaymentTerm(FormaPagoCompra term)
        {
            return DigitVerifierWriteMapper.Save("FormaPagoCompra", dvh => data.AddPaymentTerm(term.Compra_Id, term.TipoPago_Id,
                term.Monto, dvh));
        }

        public SQLUpdateResult DeletePaymentTerms(Guid Acquisition_Id)
        {
            return DigitVerifierWriteMapper.Remove(() => data.DeletePaymentTerms(Acquisition_Id), "FormaPagoCompra");
        }

        // Validity_Days puede ser NULL: los tipos se arman a mano.
        public List<TipoDocumentoCompra> GetDocumentTypes(DocumentoCompraStage stage)
        {
            List<TipoDocumentoCompra> types = new List<TipoDocumentoCompra>();
            using (DataTable table = data.GetDocumentTypes((int)stage))
            {
                foreach (DataRow row in table.Rows)
                {
                    TipoDocumentoCompra type = new TipoDocumentoCompra();
                    type.TipoDocumentoCompra_Id = (Guid)row["TipoDocumentoCompra_Id"];
                    type.Descripcion = (string)row["Descripcion"];
                    type.Etapa = (DocumentoCompraStage)(int)row["Etapa"];
                    type.DiasVigencia = row["DiasVigencia"] as int?;
                    type.Orden = (int)row["Orden"];
                    types.Add(type);
                }
            }
            return types;
        }

        public List<DocumentoCompra> GetDocuments(Guid Acquisition_Id)
        {
            List<DocumentoCompra> documents = new List<DocumentoCompra>();
            using (DataTable table = data.GetDocuments(Acquisition_Id))
            {
                foreach (DataRow row in table.Rows)
                {
                    DocumentoCompra document = new DocumentoCompra();
                    document.DocumentoCompra_Id = (Guid)row["DocumentoCompra_Id"];
                    document.Compra_Id = (Guid)row["Compra_Id"];
                    document.TipoDocumentoCompra_Id = (Guid)row["TipoDocumentoCompra_Id"];
                    document.Estado = (DocumentoCompraStatus)(int)row["Estado"];
                    document.FechaEmision = (DateTime)row["FechaEmision"];
                    document.Observacion = row["Observacion"] as string;
                    document.UsuarioCarga_Id = (Guid)row["UsuarioCarga_Id"];
                    document.FechaCarga = (DateTime)row["FechaCarga"];
                    document.Descripcion = (string)row["Descripcion"];
                    document.Etapa = (DocumentoCompraStage)(int)row["Etapa"];
                    document.NombreArchivo = row["NombreArchivo"] as string;
                    documents.Add(document);
                }
            }
            return documents;
        }

        public SQLUpdateResult AddDocument(DocumentoCompra document)
        {
            return DigitVerifierWriteMapper.Save("DocumentoCompra", dvh => data.AddDocument(document.DocumentoCompra_Id, document.Compra_Id,
                document.TipoDocumentoCompra_Id, (int)document.Estado, document.FechaEmision, document.Observacion, document.UsuarioCarga_Id,
                document.FechaCarga, dvh));
        }

        public SQLUpdateResult ObserveDocument(DocumentoCompra document)
        {
            return DigitVerifierWriteMapper.Save("DocumentoCompra", dvh => data.ObserveDocument(document.DocumentoCompra_Id, document.Compra_Id,
                document.TipoDocumentoCompra_Id, (int)document.Estado, document.FechaEmision, document.Observacion, document.UsuarioCarga_Id,
                document.FechaCarga, dvh));
        }

        // El archivo se borra en cascada y no esta protegido: solo cambia el DVV de AcquisitionDocuments.
        public SQLUpdateResult DeleteDocument(DocumentoCompra document)
        {
            return DigitVerifierWriteMapper.Remove(() => data.DeleteDocument(document.DocumentoCompra_Id), "DocumentoCompra");
        }

        // AcquisitionDocumentFiles no esta protegida con digitos verificadores.
        public SQLUpdateResult AddDocumentFile(ArchivoDocumentoCompra file)
        {
            return MappingHandler.MapUpdateResult(data.AddDocumentFile(file.DocumentoCompra_Id, file.NombreArchivo, file.Contenido));
        }

        public ArchivoDocumentoCompra GetDocumentFile(Guid Document_Id)
        {
            return MappingHandler.MapTableToEntities<ArchivoDocumentoCompra>(data.GetDocumentFile(Document_Id)).FirstOrDefault();
        }

        // Como la adquisicion, el resumen tiene NULL segun la etapa: se arma a mano.
        public CompraSummary GetSummary(Guid id)
        {
            using (DataTable table = data.GetSummary(id))
            {
                if (table.Rows.Count == 0) return null;
                DataRow row = table.Rows[0];
                CompraSummary summary = new CompraSummary();
                summary.Compra_Id = (Guid)row["Compra_Id"];
                summary.Numero = (int)row["Numero"];
                summary.Persona_Id = (Guid)row["Persona_Id"];
                summary.DNI = (int)row["DNI"];
                summary.Patente = (string)row["Patente"];
                summary.NumeroChasis = (string)row["NumeroChasis"];
                summary.NumeroMotor = (string)row["NumeroMotor"];
                summary.Vehiculo_Descripcion = (string)row["Vehiculo_Descripcion"];
                summary.Marca_Id = (Guid)row["Marca_Id"];
                summary.Modelo_Id = (Guid)row["Modelo_Id"];
                summary.Version_Id = (Guid)row["Version_Id"];
                summary.Color_Id = (Guid)row["Color_Id"];
                summary.Anio = (int)row["Anio"];
                summary.Puertas = (int)row["Puertas"];
                summary.Kilometraje = (int)row["Kilometraje"];
                summary.Inspeccion_Id = (Guid)row["Inspeccion_Id"];
                summary.FechaRealizada = row["FechaRealizada"] as DateTime?;
                summary.KilometrajeVerificado = row["KilometrajeVerificado"] as int?;
                summary.Tasacion_Valor = row["Tasacion_Valor"] as double?;
                summary.Tasacion_Fecha = row["Tasacion_Fecha"] as DateTime?;
                summary.PrecioAcordado = row["PrecioAcordado"] as double?;
                summary.CuilCuitTitular = row["CuilCuitTitular"] as string;
                summary.FechaFirmaContrato = row["FechaFirmaContrato"] as DateTime?;
                summary.MontoPagado = (double)row["MontoPagado"];
                summary.CodigoPrecarga08 = row["CodigoPrecarga08"] as string;
                summary.FechaCitaTransferencia = row["FechaCitaTransferencia"] as DateTime?;
                summary.FechaCierre = row["FechaCierre"] as DateTime?;
                summary.ComprobanteCompra_Numero = row["ComprobanteCompra_Numero"] as int?;
                summary.ComprobanteCompra_Fecha = row["ComprobanteCompra_Fecha"] as DateTime?;
                return summary;
            }
        }

        // Vehiculo y adquisicion se graban en una sola transaccion y despues se rehacen sus DVV.
        public SQLUpdateResult FinishAcquisition(Vehiculo vehicle, Compra acquisition)
        {
            return DigitVerifierWriteMapper.Save(digitVerifier => data.Finish(VehicleRow(vehicle), AcquisitionRow(acquisition),
                digitVerifier.Dvh("Vehiculo"), digitVerifier.Dvh("Compra")), "Vehiculo", "Compra");
        }

        // Sin compras para pagar ni comprobantes emitidos devuelve null.
        public List<PayableCompra> GetPendingPayment(Guid Person_Id)
        {
            List<PayableCompra> acquisitions = new List<PayableCompra>();
            using (DataTable table = data.GetPendingPayment(Person_Id))
            {
                foreach (DataRow row in table.Rows)
                {
                    PayableCompra acquisition = new PayableCompra();
                    acquisition.Compra_Id = (Guid)row["Compra_Id"];
                    acquisition.Numero = (int)row["Numero"];
                    acquisition.Patente = (string)row["Patente"];
                    acquisition.Vehiculo_Descripcion = (string)row["Vehiculo_Descripcion"];
                    acquisition.PrecioAcordado = (double)row["PrecioAcordado"];
                    acquisition.MontoPagado = (double)row["MontoPagado"];
                    acquisition.ComprobanteCompra_Numero = row["ComprobanteCompra_Numero"] as int?;
                    acquisition.Estado = (CompraStatus)(int)row["Estado"];
                    acquisitions.Add(acquisition);
                }
            }
            return acquisitions.Count == 0 ? null : acquisitions;
        }

        // Los datos de cada medio son NULL en los otros: los pagos se arman a mano.
        public List<PagoCompra> GetPayments(Guid Acquisition_Id)
        {
            List<PagoCompra> payments = new List<PagoCompra>();
            using (DataTable table = data.GetPayments(Acquisition_Id))
            {
                foreach (DataRow row in table.Rows)
                {
                    PagoCompra payment = new PagoCompra();
                    payment.PagoCompra_Id = (Guid)row["PagoCompra_Id"];
                    payment.Compra_Id = (Guid)row["Compra_Id"];
                    payment.TipoPago_Id = (Guid)row["TipoPago_Id"];
                    payment.TipoPago_Descripcion = (string)row["TipoPago_Descripcion"];
                    payment.Cajero_Id = (Guid)row["Cajero_Id"];
                    payment.Cajero = (string)row["Cajero"];
                    payment.Monto = (double)row["Monto"];
                    payment.Banco = row["Banco"] as string;
                    payment.CbuAlias = row["CbuAlias"] as string;
                    payment.NumeroOperacion = row["NumeroOperacion"] as string;
                    payment.NumeroCheque = row["NumeroCheque"] as string;
                    payment.FechaPago = row["FechaPago"] as DateTime?;
                    payment.FechaCreacion = (DateTime)row["FechaCreacion"];
                    payments.Add(payment);
                }
            }
            return payments;
        }

        public SQLUpdateResult AddPayment(PagoCompra payment)
        {
            return DigitVerifierWriteMapper.Save("PagoCompra", dvh => data.AddPayment(payment.PagoCompra_Id, payment.Compra_Id,
                payment.TipoPago_Id, payment.Cajero_Id, payment.Monto, payment.Banco, payment.CbuAlias, payment.NumeroOperacion,
                payment.NumeroCheque, payment.FechaPago, payment.FechaCreacion, dvh));
        }

        public int GetNextReceiptNumber()
        {
            using (DataTable table = data.GetNextReceiptNumber())
                return (int)table.Rows[0]["Numero"];
        }

        // Comprobante y adquisicion se graban en una sola transaccion y despues se rehacen sus DVV.
        public SQLUpdateResult PayAcquisition(ComprobanteCompra receipt, Compra acquisition)
        {
            return DigitVerifierWriteMapper.Save(digitVerifier => data.Pay(ReceiptRow(receipt), AcquisitionRow(acquisition),
                digitVerifier.Dvh("ComprobanteCompra"), digitVerifier.Dvh("Compra")), "ComprobanteCompra", "Compra");
        }

        // Sin comprobante emitido devuelve null.
        public ComprobanteCompra GetReceipt(Guid Acquisition_Id)
        {
            return MappingHandler.MapTableToEntities<ComprobanteCompra>(data.GetReceipt(Acquisition_Id)).FirstOrDefault();
        }

        // Filas completas de cada tabla, con el nombre de columna como clave.
        private static IDictionary<string, object> InspectionRow(Inspeccion inspection)
        {
            return new Dictionary<string, object>
            {
                { "Inspeccion_Id", inspection.Inspeccion_Id },
                { "Compra_Id", inspection.Compra_Id },
                { "Mecanico_Id", inspection.Mecanico_Id },
                { "FechaProgramada", inspection.FechaProgramada },
                { "FechaRealizada", inspection.FechaRealizada },
                { "KilometrajeVerificado", inspection.KilometrajeVerificado },
                { "Resultado", (int?)inspection.Resultado },
                { "Observaciones", inspection.Observaciones }
            };
        }

        private static IDictionary<string, object> ScoreRow(PuntajeInspeccion score)
        {
            return new Dictionary<string, object>
            {
                { "Inspeccion_Id", score.Inspeccion_Id },
                { "ItemInspeccion_Id", score.ItemInspeccion_Id },
                { "Puntaje", (int)score.Puntaje },
                { "Observaciones", score.Observaciones }
            };
        }

        private static IDictionary<string, object> AcquisitionRow(Compra acquisition)
        {
            return new Dictionary<string, object>
            {
                { "Compra_Id", acquisition.Compra_Id },
                { "Numero", acquisition.Numero },
                { "Persona_Id", acquisition.Persona_Id },
                { "VehiculoProspecto_Id", acquisition.VehiculoProspecto_Id },
                { "Vendedor_Id", acquisition.Vendedor_Id },
                { "Estado", (int)acquisition.Estado },
                { "MotivoRechazo", (int?)acquisition.MotivoRechazo },
                { "Fecha", acquisition.Fecha },
                { "TasacionAceptada_Id", acquisition.TasacionAceptada_Id },
                { "PrecioAcordado", acquisition.PrecioAcordado },
                { "CuilCuitTitular", acquisition.CuilCuitTitular },
                { "FechaFirmaContrato", acquisition.FechaFirmaContrato },
                { "CodigoPrecarga08", acquisition.CodigoPrecarga08 },
                { "FechaCitaTransferencia", acquisition.FechaCitaTransferencia },
                { "Vehiculo_Id", acquisition.Vehiculo_Id },
                { "FechaCierre", acquisition.FechaCierre }
            };
        }

        private static IDictionary<string, object> ReceiptRow(ComprobanteCompra receipt)
        {
            return new Dictionary<string, object>
            {
                { "ComprobanteCompra_Id", receipt.ComprobanteCompra_Id },
                { "Numero", receipt.Numero },
                { "Compra_Id", receipt.Compra_Id },
                { "Fecha", receipt.Fecha },
                { "NombrePersona", receipt.NombrePersona },
                { "CuilCuit", receipt.CuilCuit },
                { "Detalle", receipt.Detalle },
                { "MontoNeto", receipt.MontoNeto },
                { "MontoImpuesto", receipt.MontoImpuesto },
                { "MontoTotal", receipt.MontoTotal }
            };
        }

        private static IDictionary<string, object> VehicleRow(Vehiculo vehicle)
        {
            return new Dictionary<string, object>
            {
                { "Vehiculo_Id", vehicle.Vehiculo_Id },
                { "Patente", vehicle.Patente },
                { "Marca_Id", vehicle.Marca_Id },
                { "Modelo_Id", vehicle.Modelo_Id },
                { "Version_Id", vehicle.Version_Id },
                { "Color_Id", vehicle.Color_Id },
                { "Precio", vehicle.Precio },
                { "Opcionales", vehicle.Opcionales },
                { "Observaciones", vehicle.Observaciones },
                { "Puertas", vehicle.Puertas },
                { "Anio", vehicle.Anio },
                { "Kilometraje", vehicle.Kilometraje },
                { "LinkImagen", vehicle.LinkImagen }
            };
        }
    }
}
