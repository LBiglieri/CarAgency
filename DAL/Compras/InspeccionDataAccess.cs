using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Compras
{
    // Inspeccion mecanica del vehiculo ofrecido (PN2): turnos de los mecanicos, fotografias y calificaciones.
    // InspectionPhotos no esta protegida con digitos verificadores.
    public sealed class InspeccionDataAccess : DataAccessBase
    {
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

        public DataTable GetInspection(Guid id) { return Read("Inspeccion_GetById", new SqlParameter("@Inspeccion_Id", id)); }

        public DataTable GetInspectionScores(Guid inspectionId)
        {
            return Read("PuntajeInspeccion_GetByInspection", new SqlParameter("@Inspeccion_Id", inspectionId));
        }
    }
}
