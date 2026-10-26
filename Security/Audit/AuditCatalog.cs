using System;
using System.Collections.Generic;
using System.Linq;
using CarAgency.BE.Audit;

namespace CarAgency.Security.Audit
{
    public sealed class AuditDefinition
    {
        public AuditEventType Type { get; private set; }
        public AuditModule Module { get; private set; }
        public int Criticality { get; private set; }
        internal AuditDefinition(AuditEventType type, AuditModule module, int criticality)
        { Type = type; Module = module; Criticality = criticality; }
    }

    public static class AuditCatalog
    {
        private static readonly Dictionary<AuditEventType, AuditDefinition> definitions = Build();
        public static IEnumerable<AuditDefinition> All { get { return definitions.Values.OrderBy(d => d.Type); } }
        public static AuditDefinition Get(AuditEventType type)
        {
            AuditDefinition definition;
            if (!definitions.TryGetValue(type, out definition)) throw new ArgumentOutOfRangeException(nameof(type));
            return definition;
        }
        private static Dictionary<AuditEventType, AuditDefinition> Build()
        {
            var result = new Dictionary<AuditEventType, AuditDefinition>();
            // 1 compromete la seguridad o integridad del sistema; 5 es trazabilidad sin riesgo.
            Add(result, AuditModule.Users, 1, AuditEventType.UserBlocked, AuditEventType.UserUnblocked);
            Add(result, AuditModule.Users, 2, AuditEventType.LoginFailed, AuditEventType.UserCreated,
                AuditEventType.UserUpdated, AuditEventType.UserDeleted, AuditEventType.PasswordChanged);
            Add(result, AuditModule.Users, 5, AuditEventType.Login, AuditEventType.Logout, AuditEventType.LanguageChanged);
            Add(result, AuditModule.Permissions, 1, AuditEventType.PermissionCreated, AuditEventType.PermissionDeleted,
                AuditEventType.FamilyCreated, AuditEventType.FamilyUpdated, AuditEventType.FamilyDeleted);
            Add(result, AuditModule.Clients, 4, AuditEventType.ClientCreated);
            Add(result, AuditModule.Vehicles, 4, AuditEventType.VehicleCreated, AuditEventType.VehicleUpdated,
                AuditEventType.MakeCreated, AuditEventType.ModelCreated, AuditEventType.VersionCreated);
            Add(result, AuditModule.Vehicles, 3, AuditEventType.VehicleDeleted, AuditEventType.MakeDeleted,
                AuditEventType.ModelDeleted, AuditEventType.VersionDeleted);
            Add(result, AuditModule.Sales, 4, AuditEventType.QuotationCreated, AuditEventType.ReservationCreated);
            Add(result, AuditModule.Billing, 4, AuditEventType.InvoiceCreated, AuditEventType.PaymentCreated);
            Add(result, AuditModule.Billing, 2, AuditEventType.InvoiceUpdated, AuditEventType.InvoiceDeleted, AuditEventType.PaymentDeleted);
            Add(result, AuditModule.Billing, 3, AuditEventType.InvoiceExported);
            Add(result, AuditModule.Management, 4, AuditEventType.PaperworkCreated, AuditEventType.PaperworkUpdated, AuditEventType.PaperworkFileCreated);
            Add(result, AuditModule.Management, 3, AuditEventType.PaperworkDeleted, AuditEventType.PaperworkFileDeleted);
            Add(result, AuditModule.Security, 1, AuditEventType.RestoreCompleted, AuditEventType.RestoreFailed,
                AuditEventType.IntegrityRecalculated);
            Add(result, AuditModule.Security, 2, AuditEventType.BackupCreated, AuditEventType.RestoreStarted);
            Add(result, AuditModule.Audit, 5, AuditEventType.AuditViewed);
            Add(result, AuditModule.Audit, 3, AuditEventType.AuditPrintRequested);
            return result;
        }
        private static void Add(Dictionary<AuditEventType, AuditDefinition> result, AuditModule module, int criticality, params AuditEventType[] types)
        { foreach (var type in types) result.Add(type, new AuditDefinition(type, module, criticality)); }
    }
}
