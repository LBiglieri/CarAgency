using System;
using System.Collections.Generic;
using System.Linq;
using CarAgency.BE.BitacoraEventos;

namespace CarAgency.Security.BitacoraEventos
{
    public sealed class BitacoraEventosDefinition
    {
        public TipoEvento Type { get; private set; }
        public BitacoraEventosModule Module { get; private set; }
        public int Criticality { get; private set; }
        internal BitacoraEventosDefinition(TipoEvento type, BitacoraEventosModule module, int criticality)
        { Type = type; Module = module; Criticality = criticality; }
    }

    public static class BitacoraEventosCatalog
    {
        private static readonly Dictionary<TipoEvento, BitacoraEventosDefinition> definitions = Build();
        public static IEnumerable<BitacoraEventosDefinition> All { get { return definitions.Values.OrderBy(d => d.Type); } }
        public static BitacoraEventosDefinition Get(TipoEvento type)
        {
            BitacoraEventosDefinition definition;
            if (!definitions.TryGetValue(type, out definition)) throw new ArgumentOutOfRangeException(nameof(type));
            return definition;
        }
        private static Dictionary<TipoEvento, BitacoraEventosDefinition> Build()
        {
            var result = new Dictionary<TipoEvento, BitacoraEventosDefinition>();
            // 1 compromete la seguridad o integridad del sistema; 5 es trazabilidad sin riesgo.
            Add(result, BitacoraEventosModule.Users, 1, TipoEvento.UserBlocked, TipoEvento.UserUnblocked);
            Add(result, BitacoraEventosModule.Users, 2, TipoEvento.LoginFailed, TipoEvento.UserCreated,
                TipoEvento.UserUpdated, TipoEvento.UserDeleted, TipoEvento.PasswordChanged);
            Add(result, BitacoraEventosModule.Users, 5, TipoEvento.Login, TipoEvento.Logout, TipoEvento.LanguageChanged);
            Add(result, BitacoraEventosModule.Permissions, 1, TipoEvento.PermissionCreated, TipoEvento.PermissionDeleted,
                TipoEvento.FamilyCreated, TipoEvento.FamilyUpdated, TipoEvento.FamilyDeleted);
            Add(result, BitacoraEventosModule.People, 4, TipoEvento.PersonCreated);
            Add(result, BitacoraEventosModule.Vehicles, 4, TipoEvento.VehicleCreated, TipoEvento.VehicleUpdated,
                TipoEvento.MakeCreated, TipoEvento.ModelCreated, TipoEvento.VersionCreated);
            Add(result, BitacoraEventosModule.Vehicles, 3, TipoEvento.VehicleDeleted, TipoEvento.MakeDeleted,
                TipoEvento.ModelDeleted, TipoEvento.VersionDeleted);
            Add(result, BitacoraEventosModule.Sales, 4, TipoEvento.QuotationCreated, TipoEvento.ReservationCreated);
            Add(result, BitacoraEventosModule.Billing, 4, TipoEvento.InvoiceCreated, TipoEvento.PaymentCreated);
            Add(result, BitacoraEventosModule.Billing, 2, TipoEvento.InvoiceUpdated, TipoEvento.InvoiceDeleted, TipoEvento.PaymentDeleted);
            Add(result, BitacoraEventosModule.Billing, 3, TipoEvento.InvoiceExported);
            Add(result, BitacoraEventosModule.Management, 4, TipoEvento.PaperworkCreated, TipoEvento.PaperworkUpdated, TipoEvento.PaperworkFileCreated);
            Add(result, BitacoraEventosModule.Management, 3, TipoEvento.PaperworkDeleted, TipoEvento.PaperworkFileDeleted);
            Add(result, BitacoraEventosModule.Security, 1, TipoEvento.RestoreCompleted, TipoEvento.RestoreFailed,
                TipoEvento.IntegrityRecalculated);
            Add(result, BitacoraEventosModule.Security, 2, TipoEvento.BackupCreated, TipoEvento.RestoreStarted);
            Add(result, BitacoraEventosModule.Audit, 5, TipoEvento.AuditViewed);
            Add(result, BitacoraEventosModule.Audit, 3, TipoEvento.AuditPrintRequested, TipoEvento.AuditExported);
            Add(result, BitacoraEventosModule.Acquisitions, 4, TipoEvento.AcquisitionCreated, TipoEvento.InspectionRecorded,
                TipoEvento.AppraisalCreated, TipoEvento.AcquisitionFormalized, TipoEvento.AcquisitionDocumentCreated,
                TipoEvento.AcquisitionDocumentObserved, TipoEvento.AcquisitionPaymentCreated, TipoEvento.PurchaseReceiptIssued,
                TipoEvento.AcquisitionTransferScheduled, TipoEvento.AcquisitionTransferred, TipoEvento.AcquisitionFinished);
            Add(result, BitacoraEventosModule.Acquisitions, 3, TipoEvento.AcquisitionCancelled, TipoEvento.AcquisitionRejected,
                TipoEvento.AcquisitionDocumentDeleted);
            return result;
        }
        private static void Add(Dictionary<TipoEvento, BitacoraEventosDefinition> result, BitacoraEventosModule module, int criticality, params TipoEvento[] types)
        { foreach (var type in types) result.Add(type, new BitacoraEventosDefinition(type, module, criticality)); }
    }
}
