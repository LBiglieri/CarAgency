using System;

namespace CarAgency.BE.BitacoraEventos
{
    public enum BitacoraEventosModule { Users, Permissions, People, Vehicles, Sales, Billing, Management, Security, Audit, Acquisitions }

    public enum TipoEvento
    {
        Login, Logout, LoginFailed, UserBlocked, UserUnblocked, UserCreated, UserUpdated,
        UserDeleted, PasswordChanged, LanguageChanged, PermissionCreated, PermissionDeleted,
        FamilyCreated, FamilyUpdated, FamilyDeleted, PersonCreated, VehicleCreated, VehicleUpdated,
        VehicleDeleted, MakeCreated, MakeDeleted, ModelCreated, ModelDeleted, VersionCreated,
        VersionDeleted, QuotationCreated, ReservationCreated, InvoiceCreated, InvoiceUpdated,
        InvoiceDeleted, InvoiceExported, PaymentCreated, PaymentDeleted, PaperworkCreated,
        PaperworkUpdated, PaperworkDeleted, PaperworkFileCreated, PaperworkFileDeleted,
        BackupCreated, RestoreStarted, RestoreCompleted, RestoreFailed, IntegrityRecalculated,
        AuditViewed, AuditPrintRequested, AuditExported, AcquisitionCreated, AcquisitionCancelled, AcquisitionRejected,
        InspectionRecorded, AppraisalCreated, AcquisitionFormalized, AcquisitionDocumentCreated,
        AcquisitionDocumentObserved, AcquisitionDocumentDeleted, AcquisitionPaymentCreated,
        PurchaseReceiptIssued, AcquisitionTransferScheduled, AcquisitionTransferred, AcquisitionFinished
    }

    public sealed class BitacoraEvento
    {
        // Columnas de dbo.Events, en el orden en que entran al DVH.
        public Guid BitacoraEventos_Id { get; set; }
        public Guid? Usuario_Id { get; set; }
        public string LoginIntentado { get; set; }
        public DateTime Fecha { get; set; }
        public BitacoraEventosModule Modulo { get; set; }
        public TipoEvento TipoEvento { get; set; }
        public int Criticidad { get; set; }
        public Guid? Objetivo_Id { get; set; }

        // Proyecciones del join con Users. No se guardan en Events ni entran al DVH:
        // el evento se relaciona con el usuario por UserId y la consulta resuelve el resto.
        public string Login { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
    }

    public sealed class BitacoraEventosFilter
    {
        public string Login { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public BitacoraEventosModule? Modulo { get; set; }
        public TipoEvento? TipoEvento { get; set; }
        public int? Criticidad { get; set; }

        public static BitacoraEventosFilter LastThreeDays()
        {
            return new BitacoraEventosFilter { From = DateTime.Today.AddDays(-2), To = DateTime.Today };
        }
    }
}
