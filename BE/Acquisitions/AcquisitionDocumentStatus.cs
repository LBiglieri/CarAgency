
namespace CarAgency.BE
{
    // Se guarda como int en AcquisitionDocuments.Status. Un documento Pendiente es el que no tiene fila.
    public enum AcquisitionDocumentStatus
    {
        Uploaded = 1,
        Observed = 2
    }
}
