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
    public class InspeccionMapper
    {
        private readonly InspeccionDataAccess data = new InspeccionDataAccess();

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

        public SQLUpdateResult AddInspection(Inspeccion inspection)
        {
            return DigitVerifierWriteMapper.Save("Inspeccion", dvh => data.AddInspection(inspection.Inspeccion_Id, inspection.Compra_Id,
                inspection.Mecanico_Id, inspection.FechaProgramada, inspection.FechaRealizada, inspection.KilometrajeVerificado,
                (int?)inspection.Resultado, inspection.Observaciones, dvh));
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
                inspection.Puntajes.Select(ScoreRow).ToList(), CompraMapper.AcquisitionRow(acquisition), digitVerifier.Dvh("Inspeccion"),
                digitVerifier.Dvh("PuntajeInspeccion"), digitVerifier.Dvh("Compra")), "Inspeccion", "PuntajeInspeccion", "Compra");
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
    }
}
