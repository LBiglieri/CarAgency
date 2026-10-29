CREATE OR ALTER PROCEDURE dbo.DV_ValidateTable @TableName nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    IF @TableName IS NULL OR @TableName COLLATE Latin1_General_100_BIN2 NOT IN
        ('AcquisitionDocuments','AcquisitionDocumentTypes','AcquisitionPayments','AcquisitionPaymentTerms','Acquisitions',
         'Appraisals','Clients','Colours','Events','InspectionItems','Inspections','InspectionScores','Invoice','Makes',
         'Models','Paperwork','Payments','PaymentTypes','Permission_Permission','Permissions','ProspectVehicles',
         'PurchaseReceipts','Quotations','Reservation','Users','Vehicles','Versions')
        THROW 51003, 'Tabla fuera del alcance de digitos verificadores.', 1;
END
GO
