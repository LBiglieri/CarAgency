using System;
using System.Collections.Generic;
using System.Data;
using CarAgency.BE;
using CarAgency.DAL.Sales;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class QuotationMapper
    {
        private const string Table = "Quotations";
        private readonly QuotationDataAccess data = new QuotationDataAccess();

        public Quotation GetById(Guid id)
        {
            List<Quotation> quotations = Load(data.GetById(id));
            return quotations == null ? null : quotations[0];
        }

        public List<Quotation> GetAll()
        {
            return Load(data.GetAll());
        }

        public List<Quotation> GetAllActiveByClient(Guid client_Id)
        {
            return Load(data.GetAllActiveByClient(client_Id));
        }

        public SQLUpdateResult AddQuotation(Quotation quotation)
        {
            return DigitVerifierWriteMapper.Save(Table, dvh => data.Add(quotation.Id, quotation.Vehicle_Id,
                quotation.Client_Id, quotation.Price, quotation.Creation_Date, dvh));
        }

        // Sin filas devuelve null. El SP trae Client_Name/Client_Surname encriptados y Client_Dni para
        // armar Client_Description; las entidades salen en el mismo orden que las filas.
        private static List<Quotation> Load(DataTable table)
        {
            using (table)
            using (DataTableReader reader = table.CreateDataReader())
            {
                List<Quotation> quotations = MappingHandler.MapReaderToEntities<Quotation>(reader);
                if (quotations.Count == 0) return null;
                for (int i = 0; i < quotations.Count; i++)
                {
                    DataRow row = table.Rows[i];
                    quotations[i].Client_Description = ClientMapper.Describe(
                        (string)row["Client_Name"], (string)row["Client_Surname"], (int)row["Client_Dni"]);
                }
                return quotations;
            }
        }
    }
}
