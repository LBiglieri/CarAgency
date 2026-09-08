using System;

namespace CarAgency.BE.Audit
{
    public enum AuditModule { Users, Permissions, Clients, Vehicles, Sales, Billing, Management, Security, Audit }

    public enum AuditEventType
    {
        Login, Logout, LoginFailed, UserBlocked, UserUnblocked, UserCreated, UserUpdated,
        UserDeleted, PasswordChanged, LanguageChanged, PermissionCreated, PermissionDeleted,
        FamilyCreated, FamilyUpdated, FamilyDeleted, ClientCreated, VehicleCreated, VehicleUpdated,
        VehicleDeleted, MakeCreated, MakeDeleted, ModelCreated, ModelDeleted, VersionCreated,
        VersionDeleted, QuotationCreated, ReservationCreated, InvoiceCreated, InvoiceUpdated,
        InvoiceDeleted, InvoiceExported, PaymentCreated, PaymentDeleted, PaperworkCreated,
        PaperworkUpdated, PaperworkDeleted, PaperworkFileCreated, PaperworkFileDeleted,
        BackupCreated, RestoreStarted, RestoreCompleted, RestoreFailed, IntegrityRecalculated,
        AuditViewed, AuditPrintRequested
    }

    public sealed class AuditEvent
    {
        // Columnas de dbo.Events, en el orden en que entran al DVH.
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string AttemptedLogin { get; set; }
        public DateTime OccurredAt { get; set; }
        public AuditModule Module { get; set; }
        public AuditEventType EventType { get; set; }
        public int Criticality { get; set; }
        public Guid? TargetId { get; set; }

        // Proyecciones del join con Users. No se guardan en Events ni entran al DVH:
        // el evento se relaciona con el usuario por UserId y la consulta resuelve el resto.
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public sealed class AuditFilter
    {
        public string Login { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public AuditModule? Module { get; set; }
        public AuditEventType? EventType { get; set; }
        public int? Criticality { get; set; }

        public static AuditFilter LastThreeDays()
        {
            return new AuditFilter { From = DateTime.Today.AddDays(-2), To = DateTime.Today };
        }
    }
}
