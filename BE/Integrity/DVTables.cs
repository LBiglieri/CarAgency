using System;
using System.Collections.Generic;
using System.Linq;

namespace CarAgency.BE.Integrity
{
    public static class DVTables
    {
        private static readonly string[] tables = {
            "AcquisitionDocuments", "AcquisitionDocumentTypes", "AcquisitionPayments", "AcquisitionPaymentTerms",
            "Acquisitions", "Appraisals", "Clients", "Colours", "Events", "InspectionItems", "Inspections",
            "InspectionScores", "Invoice", "Makes", "Models", "Paperwork", "Payments", "PaymentTypes",
            "Permission_Permission", "Permissions", "ProspectVehicles", "PurchaseReceipts", "Quotations",
            "Reservation", "Users", "Vehicles", "Versions"
        };

        public static IEnumerable<string> All { get { return tables.ToArray(); } }

        public static void RequireProtected(string table)
        {
            if (!tables.Contains(table, StringComparer.Ordinal))
                throw new ArgumentException("Tabla fuera del alcance de digitos verificadores.", nameof(table));
        }
    }
}
