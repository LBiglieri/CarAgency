using System;
using System.Collections.Generic;
using System.Data;
using CarAgency.BE;
using CarAgency.DAL.Sales;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class CotizacionMapper
    {
        private const string Table = "Cotizacion";
        private readonly CotizacionDataAccess data = new CotizacionDataAccess();

        public Cotizacion GetById(Guid id)
        {
            List<Cotizacion> quotations = Load(data.GetById(id));
            return quotations == null ? null : quotations[0];
        }

        public List<Cotizacion> GetAll()
        {
            return Load(data.GetAll());
        }

        public List<Cotizacion> GetAllActiveByPerson(Guid person_Id)
        {
            return Load(data.GetAllActiveByPerson(person_Id));
        }

        public SQLUpdateResult AddQuotation(Cotizacion quotation)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(quotation.Cotizacion_Id, quotation.Vehiculo_Id,
                quotation.Persona_Id, quotation.Precio, quotation.Fecha, dvh));
        }

        // Sin filas devuelve null. El SP trae Person_Name/Person_Surname encriptados y Person_Dni para
        // armar Person_Description; las entidades salen en el mismo orden que las filas.
        private static List<Cotizacion> Load(DataTable table)
        {
            using (table)
            using (DataTableReader reader = table.CreateDataReader())
            {
                List<Cotizacion> quotations = MappingHandler.MapReaderToEntities<Cotizacion>(reader);
                if (quotations.Count == 0) return null;
                for (int i = 0; i < quotations.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    quotations[i].Persona_Descripcion = PersonaMapper.Describe(
                        (string)row["Persona_Nombre"], (string)row["Persona_Apellido"], (int)row["Persona_DNI"]);
                }
                return quotations;
            }
        }
    }
}
