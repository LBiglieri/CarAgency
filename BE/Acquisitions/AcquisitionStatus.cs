
namespace CarAgency.BE
{
    // Se guarda como int en Acquisitions.Status.
    public enum AcquisitionStatus
    {
        InspectionScheduled = 1,
        Inspected = 2,
        Appraised = 3,
        PurchaseFormalized = 4,
        DocumentationComplete = 5,
        Paid = 6,
        Transferred = 7,
        Finished = 8,
        Rejected = 9,
        Cancelled = 10
    }
}
