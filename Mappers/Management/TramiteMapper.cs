using System;
using System.Collections.Generic;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Management;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class TramiteMapper
    {
        private const string Table = "Tramite";
        private readonly TramiteDataAccess data = new TramiteDataAccess();
        private readonly PersonaMapper people = new PersonaMapper();

        // Sin archivos devuelve null.
        public List<ArchivoTramite> GetFilesByPaperWork(Guid Paperwork_Id)
        {
            List<ArchivoTramite> files = MappingHandler.MapTableToEntities<ArchivoTramite>(data.GetFilesByPaperwork(Paperwork_Id));
            return files.Count == 0 ? null : files;
        }

        // PaperworkFile no esta protegida con digitos verificadores.
        public SQLUpdateResult AddPaperworkFile(ArchivoTramite paperwork)
        {
            return MappingHandler.MapUpdateResult(data.AddFile(paperwork.ArchivoTramite_Id, paperwork.Tramite_Id, paperwork.NombreArchivo,
                paperwork.Contenido, paperwork.FechaCarga));
        }

        public SQLUpdateResult DeletePaperworkFile(ArchivoTramite paperwork)
        {
            return MappingHandler.MapUpdateResult(data.DeleteFile(paperwork.ArchivoTramite_Id));
        }

        public Tramite GetById(Guid id)
        {
            return MappingHandler.MapTableToEntities<Tramite>(data.GetById(id)).FirstOrDefault();
        }

        // Sin tramites devuelve null. Con Guid.Empty el SP trae los tramites activos de todas las personas:
        // la descripcion se pide una sola vez por persona, con la de cada tramite y no con el filtro.
        public List<Tramite> GetAllActiveByPerson(Guid Person_Id)
        {
            List<Tramite> paperworks = MappingHandler.MapTableToEntities<Tramite>(data.GetAllActiveByPerson(Person_Id));
            if (paperworks.Count == 0) return null;
            Dictionary<Guid, string> descriptions = paperworks.Select(p => p.Persona_Id).Distinct()
                .ToDictionary(id => id, id => people.GetDescription(id));
            foreach (Tramite paperwork in paperworks)
                paperwork.Persona_Descripcion = descriptions[paperwork.Persona_Id];
            return paperworks;
        }

        public SQLUpdateResult AddPaperwork(Tramite paperwork)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(paperwork.Tramite_Id, paperwork.Vehiculo_Id, paperwork.Persona_Id, paperwork.Factura_Id,
                paperwork.CodigoPrecarga08, paperwork.FechaTransferencia, paperwork.Observaciones, paperwork.Finalizado, dvh));
        }

        public SQLUpdateResult UpdatePaperwork(Tramite paperwork)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Update(paperwork.Tramite_Id, paperwork.Vehiculo_Id, paperwork.Persona_Id, paperwork.Factura_Id,
                paperwork.CodigoPrecarga08, paperwork.FechaTransferencia, paperwork.Observaciones, paperwork.Finalizado, dvh));
        }

        public SQLUpdateResult DeletePaperwork(Tramite paperwork)
        {
            return DigitVerifierWriteMapper.Remove(() => data.Delete(paperwork.Tramite_Id), Table);
        }
    }
}
