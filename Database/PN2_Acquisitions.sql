-- PN2 - Verificacion y Adquisicion de Vehiculos (CUF07 a CUF11).
-- Script incremental sobre CarAgency. Ejecutar completo en SSMS (F5).
--
-- Enumerados (se guardan como int porque el DVH no soporta tinyint):
--   Acquisitions.Status            1 InspectionScheduled, 2 Inspected, 3 Appraised, 4 PurchaseFormalized,
--                                  5 DocumentationComplete, 6 Paid, 7 Transferred, 8 Finished, 9 Rejected, 10 Cancelled
--   Acquisitions.Rejection_Reason  1 InspectionUnfit, 2 AppraisalRejected, 3 ContractNotSigned
--   Inspections.Result             1 Fit, 2 Unfit, 3 NoShow
--   InspectionScores.Score         1 Good, 2 Fair, 3 Poor
--   AcquisitionDocuments.Status    1 Uploaded, 2 Observed (un documento Pendiente es el que no tiene fila)
--   AcquisitionDocumentTypes.Stage 1 Document (CUF10 etapa B), 2 Transfer (CUF10 etapa C)
--
-- El DVH se calcula en C# antes del insert, por eso los numeros visibles salen de una SEQUENCE
-- (se pide el valor, se calcula el DVH y recien ahi se inserta) y no de una columna IDENTITY.
-- Los archivos van en tablas aparte sin DVH, igual que PaperworkFiles: varbinary no entra en el DVH.
USE [CarAgency]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE SEQUENCE [dbo].[AcquisitionNumbers] AS int START WITH 1 INCREMENT BY 1
GO
CREATE SEQUENCE [dbo].[PurchaseReceiptNumbers] AS int START WITH 1 INCREMENT BY 1
GO

-- Vehiculo que el cliente ofrece. Es una foto tomada en CUF07: si la adquisicion se cancela y se
-- vuelve a iniciar, se registra otra fila. La unicidad contra adquisiciones abiertas e inventario
-- la valida el SP, porque un UNIQUE impediria reintentar con el mismo vehiculo.
CREATE TABLE [dbo].[ProspectVehicles](
	[Id] [uniqueidentifier] NOT NULL,
	[License_Plate] [varchar](10) NOT NULL,
	[Chassis_Number] [varchar](17) NOT NULL,
	[Engine_Number] [varchar](20) NOT NULL,
	[Make_Id] [uniqueidentifier] NOT NULL,
	[Model_Id] [uniqueidentifier] NOT NULL,
	[Version_Id] [uniqueidentifier] NOT NULL,
	[Colour_Id] [uniqueidentifier] NOT NULL,
	[Year] [int] NOT NULL,
	[Doors] [int] NOT NULL,
	[Declared_Kilometers] [int] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_ProspectVehicles] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_ProspectVehicles_Makes] FOREIGN KEY ([Make_Id]) REFERENCES [dbo].[Makes] ([Id]),
 CONSTRAINT [FK_ProspectVehicles_Models] FOREIGN KEY ([Model_Id]) REFERENCES [dbo].[Models] ([Id]),
 CONSTRAINT [FK_ProspectVehicles_Versions] FOREIGN KEY ([Version_Id]) REFERENCES [dbo].[Versions] ([Id]),
 CONSTRAINT [FK_ProspectVehicles_Colours] FOREIGN KEY ([Colour_Id]) REFERENCES [dbo].[Colours] ([Id]),
 CONSTRAINT [CK_ProspectVehicles_DVH] CHECK (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
)
GO
CREATE INDEX [IX_ProspectVehicles_License_Plate] ON [dbo].[ProspectVehicles] ([License_Plate])
GO
CREATE INDEX [IX_ProspectVehicles_Chassis_Number] ON [dbo].[ProspectVehicles] ([Chassis_Number])
GO
CREATE INDEX [IX_ProspectVehicles_Engine_Number] ON [dbo].[ProspectVehicles] ([Engine_Number])
GO

-- Agregado raiz del proceso: nace en CUF07 y lleva el estado hasta que el vehiculo entra al inventario.
CREATE TABLE [dbo].[Acquisitions](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [int] NOT NULL,
	[Client_Id] [uniqueidentifier] NOT NULL,
	[ProspectVehicle_Id] [uniqueidentifier] NOT NULL,
	[Salesman_Id] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[Rejection_Reason] [int] NULL,
	[Creation_Date] [datetime] NOT NULL,
	[Accepted_Appraisal_Id] [uniqueidentifier] NULL,
	[Agreed_Price] [float] NULL,
	[Owner_CUIL_CUIT] [varchar](13) NULL,
	[Contract_Signed_Date] [datetime] NULL,
	[Precharge_Code_08] [varchar](50) NULL,
	[Transfer_Appointment_Date] [datetime] NULL,
	[Vehicle_Id] [uniqueidentifier] NULL,
	[Closing_Date] [datetime] NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Acquisitions] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [UQ_Acquisitions_Number] UNIQUE ([Number]),
 CONSTRAINT [UQ_Acquisitions_ProspectVehicle] UNIQUE ([ProspectVehicle_Id]),
 CONSTRAINT [FK_Acquisitions_Clients] FOREIGN KEY ([Client_Id]) REFERENCES [dbo].[Clients] ([Id]),
 CONSTRAINT [FK_Acquisitions_ProspectVehicles] FOREIGN KEY ([ProspectVehicle_Id]) REFERENCES [dbo].[ProspectVehicles] ([Id]),
 CONSTRAINT [FK_Acquisitions_Users] FOREIGN KEY ([Salesman_Id]) REFERENCES [dbo].[Users] ([Id]),
 CONSTRAINT [FK_Acquisitions_Vehicles] FOREIGN KEY ([Vehicle_Id]) REFERENCES [dbo].[Vehicles] ([Id]),
 CONSTRAINT [CK_Acquisitions_Status] CHECK ([Status]>=(1) AND [Status]<=(10)),
 CONSTRAINT [CK_Acquisitions_Rejection_Reason] CHECK ([Rejection_Reason] IS NULL OR [Rejection_Reason]>=(1) AND [Rejection_Reason]<=(3)),
 CONSTRAINT [CK_Acquisitions_Agreed_Price] CHECK ([Agreed_Price] IS NULL OR [Agreed_Price]>(0)),
 CONSTRAINT [CK_Acquisitions_DVH] CHECK (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
)
GO
CREATE INDEX [IX_Acquisitions_Client] ON [dbo].[Acquisitions] ([Client_Id])
GO
CREATE INDEX [IX_Acquisitions_Status] ON [dbo].[Acquisitions] ([Status])
GO
-- Un vehiculo del inventario sale de una sola adquisicion.
CREATE UNIQUE INDEX [UX_Acquisitions_Vehicle] ON [dbo].[Acquisitions] ([Vehicle_Id]) WHERE [Vehicle_Id] IS NOT NULL
GO

CREATE TABLE [dbo].[Inspections](
	[Id] [uniqueidentifier] NOT NULL,
	[Acquisition_Id] [uniqueidentifier] NOT NULL,
	[Mechanic_Id] [uniqueidentifier] NOT NULL,
	[Scheduled_Date] [datetime] NOT NULL,
	[Performed_Date] [datetime] NULL,
	[Verified_Kilometers] [int] NULL,
	[Result] [int] NULL,
	[Observations] [varchar](max) NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Inspections] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [UQ_Inspections_Acquisition] UNIQUE ([Acquisition_Id]),
 CONSTRAINT [FK_Inspections_Acquisitions] FOREIGN KEY ([Acquisition_Id]) REFERENCES [dbo].[Acquisitions] ([Id]),
 CONSTRAINT [FK_Inspections_Users] FOREIGN KEY ([Mechanic_Id]) REFERENCES [dbo].[Users] ([Id]),
 CONSTRAINT [CK_Inspections_Result] CHECK ([Result] IS NULL OR [Result]>=(1) AND [Result]<=(3)),
 CONSTRAINT [CK_Inspections_Verified_Kilometers] CHECK ([Verified_Kilometers] IS NULL OR [Verified_Kilometers]>=(0)),
 CONSTRAINT [CK_Inspections_DVH] CHECK (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
)
GO
-- Agenda del mecanico: el SP lo usa para validar que la franja este libre (CUF07 paso 6).
CREATE INDEX [IX_Inspections_Mechanic_Schedule] ON [dbo].[Inspections] ([Mechanic_Id], [Scheduled_Date])
GO

CREATE TABLE [dbo].[InspectionItems](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Display_Order] [int] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_InspectionItems] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [CK_InspectionItems_DVH] CHECK (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
)
GO

CREATE TABLE [dbo].[InspectionScores](
	[Inspection_Id] [uniqueidentifier] NOT NULL,
	[InspectionItem_Id] [uniqueidentifier] NOT NULL,
	[Score] [int] NOT NULL,
	[Observations] [varchar](max) NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_InspectionScores] PRIMARY KEY CLUSTERED ([Inspection_Id] ASC, [InspectionItem_Id] ASC),
 CONSTRAINT [FK_InspectionScores_Inspections] FOREIGN KEY ([Inspection_Id]) REFERENCES [dbo].[Inspections] ([Id]),
 CONSTRAINT [FK_InspectionScores_InspectionItems] FOREIGN KEY ([InspectionItem_Id]) REFERENCES [dbo].[InspectionItems] ([Id]),
 CONSTRAINT [CK_InspectionScores_Score] CHECK ([Score]>=(1) AND [Score]<=(3)),
 CONSTRAINT [CK_InspectionScores_DVH] CHECK (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
)
GO

CREATE TABLE [dbo].[InspectionPhotos](
	[Id] [uniqueidentifier] NOT NULL CONSTRAINT [DF_InspectionPhotos_Id] DEFAULT (newid()),
	[Inspection_Id] [uniqueidentifier] NOT NULL,
	[FileName] [varchar](255) NOT NULL,
	[FileContent] [varbinary](max) NOT NULL,
	[UploadedDate] [datetime] NOT NULL CONSTRAINT [DF_InspectionPhotos_UploadedDate] DEFAULT (getdate()),
 CONSTRAINT [PK_InspectionPhotos] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_InspectionPhotos_Inspections] FOREIGN KEY ([Inspection_Id]) REFERENCES [dbo].[Inspections] ([Id])
)
GO
CREATE INDEX [IX_InspectionPhotos_Inspection] ON [dbo].[InspectionPhotos] ([Inspection_Id])
GO

-- Una adquisicion puede re-tasarse cuando vence la vigencia (CUF09 2.1): la vigente es la ultima.
CREATE TABLE [dbo].[Appraisals](
	[Id] [uniqueidentifier] NOT NULL,
	[Acquisition_Id] [uniqueidentifier] NOT NULL,
	[Inspection_Id] [uniqueidentifier] NOT NULL,
	[User_Id] [uniqueidentifier] NOT NULL,
	[Amount] [float] NOT NULL,
	[Justification] [varchar](max) NOT NULL,
	[Creation_Date] [datetime] NOT NULL,
	[Expiration_Date] [datetime] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_Appraisals] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_Appraisals_Acquisitions] FOREIGN KEY ([Acquisition_Id]) REFERENCES [dbo].[Acquisitions] ([Id]),
 CONSTRAINT [FK_Appraisals_Inspections] FOREIGN KEY ([Inspection_Id]) REFERENCES [dbo].[Inspections] ([Id]),
 CONSTRAINT [FK_Appraisals_Users] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id]),
 CONSTRAINT [CK_Appraisals_Amount] CHECK ([Amount]>(0)),
 CONSTRAINT [CK_Appraisals_Expiration_Date] CHECK ([Expiration_Date]>[Creation_Date]),
 CONSTRAINT [CK_Appraisals_DVH] CHECK (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
)
GO
CREATE INDEX [IX_Appraisals_Acquisition] ON [dbo].[Appraisals] ([Acquisition_Id], [Creation_Date])
GO
-- Tasacion que el cliente acepto. Va despues de crear Appraisals porque la referencia es circular.
ALTER TABLE [dbo].[Acquisitions] ADD CONSTRAINT [FK_Acquisitions_Appraisals]
	FOREIGN KEY ([Accepted_Appraisal_Id]) REFERENCES [dbo].[Appraisals] ([Id])
GO

-- Forma de pago pactada en CUF10 etapa A: un monto por medio. La suma debe igualar Agreed_Price (lo valida la BLL).
CREATE TABLE [dbo].[AcquisitionPaymentTerms](
	[Acquisition_Id] [uniqueidentifier] NOT NULL,
	[PaymentType_Id] [uniqueidentifier] NOT NULL,
	[Amount] [float] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_AcquisitionPaymentTerms] PRIMARY KEY CLUSTERED ([Acquisition_Id] ASC, [PaymentType_Id] ASC),
 CONSTRAINT [FK_AcquisitionPaymentTerms_Acquisitions] FOREIGN KEY ([Acquisition_Id]) REFERENCES [dbo].[Acquisitions] ([Id]),
 CONSTRAINT [FK_AcquisitionPaymentTerms_PaymentTypes] FOREIGN KEY ([PaymentType_Id]) REFERENCES [dbo].[PaymentTypes] ([Id]),
 CONSTRAINT [CK_AcquisitionPaymentTerms_Amount] CHECK ([Amount]>(0)),
 CONSTRAINT [CK_AcquisitionPaymentTerms_DVH] CHECK (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
)
GO

-- Pagos del cajero al cliente (CUF11). La FK compuesta contra AcquisitionPaymentTerms garantiza que
-- solo se pague con un medio pactado; el saldo por medio es la diferencia de las sumas.
CREATE TABLE [dbo].[AcquisitionPayments](
	[Id] [uniqueidentifier] NOT NULL,
	[Acquisition_Id] [uniqueidentifier] NOT NULL,
	[PaymentType_Id] [uniqueidentifier] NOT NULL,
	[Cashier_Id] [uniqueidentifier] NOT NULL,
	[Amount] [float] NOT NULL,
	[Bank] [varchar](100) NULL,
	[Cbu_Alias] [varchar](50) NULL,
	[Operation_Number] [varchar](50) NULL,
	[Check_Number] [varchar](30) NULL,
	[Payment_Date] [datetime] NULL,
	[Creation_Date] [datetime] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_AcquisitionPayments] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_AcquisitionPayments_AcquisitionPaymentTerms] FOREIGN KEY ([Acquisition_Id], [PaymentType_Id])
	REFERENCES [dbo].[AcquisitionPaymentTerms] ([Acquisition_Id], [PaymentType_Id]),
 CONSTRAINT [FK_AcquisitionPayments_Users] FOREIGN KEY ([Cashier_Id]) REFERENCES [dbo].[Users] ([Id]),
 CONSTRAINT [CK_AcquisitionPayments_Amount] CHECK ([Amount]>(0)),
 CONSTRAINT [CK_AcquisitionPayments_DVH] CHECK (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
)
GO
CREATE INDEX [IX_AcquisitionPayments_Acquisition] ON [dbo].[AcquisitionPayments] ([Acquisition_Id], [PaymentType_Id])
GO

-- Comprobante de compra (CUF11 paso 7). Guarda una foto de los datos impresos, como Invoice.
CREATE TABLE [dbo].[PurchaseReceipts](
	[Id] [uniqueidentifier] NOT NULL,
	[Number] [int] NOT NULL,
	[Acquisition_Id] [uniqueidentifier] NOT NULL,
	[Issue_Date] [datetime] NOT NULL,
	[Client_Name] [varchar](255) NOT NULL,
	[Client_CUIL_CUIT] [varchar](13) NOT NULL,
	[Detail] [varchar](max) NOT NULL,
	[Net_Amount] [float] NOT NULL,
	[Tax_Amount] [float] NOT NULL,
	[Total_Amount] [float] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_PurchaseReceipts] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [UQ_PurchaseReceipts_Number] UNIQUE ([Number]),
 CONSTRAINT [UQ_PurchaseReceipts_Acquisition] UNIQUE ([Acquisition_Id]),
 CONSTRAINT [FK_PurchaseReceipts_Acquisitions] FOREIGN KEY ([Acquisition_Id]) REFERENCES [dbo].[Acquisitions] ([Id]),
 CONSTRAINT [CK_PurchaseReceipts_Amounts] CHECK ([Net_Amount]>(0) AND [Tax_Amount]>=(0) AND [Total_Amount]>(0)),
 CONSTRAINT [CK_PurchaseReceipts_DVH] CHECK (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
)
GO

CREATE TABLE [dbo].[AcquisitionDocumentTypes](
	[Id] [uniqueidentifier] NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[Stage] [int] NOT NULL,
	[Validity_Days] [int] NULL,
	[Display_Order] [int] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_AcquisitionDocumentTypes] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [CK_AcquisitionDocumentTypes_Stage] CHECK ([Stage]>=(1) AND [Stage]<=(2)),
 CONSTRAINT [CK_AcquisitionDocumentTypes_Validity_Days] CHECK ([Validity_Days] IS NULL OR [Validity_Days]>(0)),
 CONSTRAINT [CK_AcquisitionDocumentTypes_DVH] CHECK (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
)
GO

-- Reemplazar un documento (CUF10 11.2) es borrar la fila y cargar otra: hay una sola por tipo.
CREATE TABLE [dbo].[AcquisitionDocuments](
	[Id] [uniqueidentifier] NOT NULL,
	[Acquisition_Id] [uniqueidentifier] NOT NULL,
	[DocumentType_Id] [uniqueidentifier] NOT NULL,
	[Status] [int] NOT NULL,
	[Issue_Date] [datetime] NOT NULL,
	[Observation] [varchar](max) NULL,
	[User_Id] [uniqueidentifier] NOT NULL,
	[Uploaded_Date] [datetime] NOT NULL,
	[DVH] [char](64) COLLATE Latin1_General_100_BIN2 NOT NULL,
 CONSTRAINT [PK_AcquisitionDocuments] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [UQ_AcquisitionDocuments_Type] UNIQUE ([Acquisition_Id], [DocumentType_Id]),
 CONSTRAINT [FK_AcquisitionDocuments_Acquisitions] FOREIGN KEY ([Acquisition_Id]) REFERENCES [dbo].[Acquisitions] ([Id]),
 CONSTRAINT [FK_AcquisitionDocuments_AcquisitionDocumentTypes] FOREIGN KEY ([DocumentType_Id]) REFERENCES [dbo].[AcquisitionDocumentTypes] ([Id]),
 CONSTRAINT [FK_AcquisitionDocuments_Users] FOREIGN KEY ([User_Id]) REFERENCES [dbo].[Users] ([Id]),
 CONSTRAINT [CK_AcquisitionDocuments_Status] CHECK ([Status]>=(1) AND [Status]<=(2)),
 CONSTRAINT [CK_AcquisitionDocuments_DVH] CHECK (([DVH] IS NULL OR len([DVH])=(64) AND NOT ([DVH]) collate Latin1_General_100_BIN2 like '%[^0-9A-F]%'))
)
GO

-- Escaneo del documento. Comparte la clave con AcquisitionDocuments (1 a 1) y se borra con el.
CREATE TABLE [dbo].[AcquisitionDocumentFiles](
	[Document_Id] [uniqueidentifier] NOT NULL,
	[FileName] [varchar](255) NOT NULL,
	[FileContent] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_AcquisitionDocumentFiles] PRIMARY KEY CLUSTERED ([Document_Id] ASC),
 CONSTRAINT [FK_AcquisitionDocumentFiles_AcquisitionDocuments] FOREIGN KEY ([Document_Id])
	REFERENCES [dbo].[AcquisitionDocuments] ([Id]) ON DELETE CASCADE
)
GO

-- Catalogos. El DVH queda en cero hasta correr "Recalcular DV", que tambien crea las filas de dbo.DV.
DECLARE @PendingDvh char(64) = REPLICATE('0', 64)

INSERT [dbo].[InspectionItems] ([Id], [Description], [Display_Order], [DVH]) VALUES
	(NEWID(), 'Engine', 1, @PendingDvh),
	(NEWID(), 'Transmission', 2, @PendingDvh),
	(NEWID(), 'Brakes', 3, @PendingDvh),
	(NEWID(), 'Suspension', 4, @PendingDvh),
	(NEWID(), 'Bodywork', 5, @PendingDvh),
	(NEWID(), 'Interior', 6, @PendingDvh),
	(NEWID(), 'Electrical System', 7, @PendingDvh),
	(NEWID(), 'Tires', 8, @PendingDvh)

INSERT [dbo].[AcquisitionDocumentTypes] ([Id], [Description], [Stage], [Validity_Days], [Display_Order], [DVH]) VALUES
	(NEWID(), 'Vehicle Title', 1, NULL, 1, @PendingDvh),
	(NEWID(), 'Form 12', 1, 150, 2, @PendingDvh),
	(NEWID(), 'Vehicle Registration Card', 1, NULL, 3, @PendingDvh),
	(NEWID(), 'Ownership Certificate', 1, 15, 4, @PendingDvh),
	(NEWID(), 'Vehicle Tax Clearance', 1, 30, 5, @PendingDvh),
	(NEWID(), 'Traffic Fines Clearance', 1, 30, 6, @PendingDvh),
	(NEWID(), 'Signed Purchase Contract', 1, NULL, 7, @PendingDvh),
	(NEWID(), 'Signed Form 08', 2, NULL, 8, @PendingDvh),
	(NEWID(), 'Municipal Deregistration', 2, NULL, 9, @PendingDvh)
GO

-- Alcance de digitos verificadores. Tiene que coincidir con BE/Integrity/DVTables.cs.
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
