-- PN2 - Verificacion y Adquisicion de Vehiculos: eventos de bitacora, patentes y familia Mechanic.
-- Requiere PN2_Acquisitions.sql. Debe coincidir con Security/Audit/AuditCatalog.cs y BE/Permissions/PermissionType.cs.
-- Permissions y Permission_Permission tienen DVH: entran con el digito en cero y hay que recalcular
-- DVH y DVV de esas dos tablas despues de ejecutar este script.
USE [CarAgency]
GO
SET XACT_ABORT ON
BEGIN TRAN

INSERT [dbo].[EventTypes] ([EventType], [Module], [Criticality]) VALUES
	('AcquisitionCreated', 'Acquisitions', 4),
	('InspectionRecorded', 'Acquisitions', 4),
	('AppraisalCreated', 'Acquisitions', 4),
	('AcquisitionFormalized', 'Acquisitions', 4),
	('AcquisitionDocumentCreated', 'Acquisitions', 4),
	('AcquisitionDocumentObserved', 'Acquisitions', 4),
	('AcquisitionPaymentCreated', 'Acquisitions', 4),
	('PurchaseReceiptIssued', 'Acquisitions', 4),
	('AcquisitionTransferScheduled', 'Acquisitions', 4),
	('AcquisitionTransferred', 'Acquisitions', 4),
	('AcquisitionFinished', 'Acquisitions', 4),
	('AcquisitionCancelled', 'Acquisitions', 3),
	('AcquisitionRejected', 'Acquisitions', 3),
	('AcquisitionDocumentDeleted', 'Acquisitions', 3)

DECLARE @PendingDvh char(64) = REPLICATE('0', 64)

-- Una patente por caso de uso; Type es el nombre del formulario.
INSERT [dbo].[Permissions] ([Id], [Name], [Type], [DVH]) VALUES
	('dacf376a-77cf-4970-ae9b-10c14a3187d3', 'Register Vehicle for Appraisal', 'RegisterAcquisitionForm', @PendingDvh),
	('f14efe30-5c10-4fb1-9c46-1e6a27ec8f5c', 'Register Mechanical Inspection', 'RegisterInspectionForm', @PendingDvh),
	('9f70629a-7d78-400b-bd94-f13f5f356e24', 'Appraise Vehicle', 'AppraiseVehicleForm', @PendingDvh),
	('521ab5ef-abf2-4fd6-89bd-aebdfdba560c', 'Manage Vehicle Purchase', 'ManageAcquisitionForm', @PendingDvh),
	('b5a35e91-41d7-4a37-9fde-187c019a3caa', 'Pay Vehicle Purchase', 'PayAcquisitionForm', @PendingDvh),
	('6e01dd6e-5164-484c-b576-aa83bd26c1ac', 'Mechanic', NULL, @PendingDvh)

-- Cada actor del PN2 recibe la patente de su caso de uso. Mechanic hereda Base User como las demas familias.
INSERT [dbo].[Permission_Permission] ([Father_Id], [Child_Id], [DVH]) VALUES
	('998a4eae-4bc9-4078-803b-6e2f90d53f46', 'dacf376a-77cf-4970-ae9b-10c14a3187d3', @PendingDvh), -- Salesman: CUF07
	('6e01dd6e-5164-484c-b576-aa83bd26c1ac', '4293e0e8-4a85-486f-927c-c4999a8f4038', @PendingDvh), -- Mechanic: Base User
	('6e01dd6e-5164-484c-b576-aa83bd26c1ac', 'f14efe30-5c10-4fb1-9c46-1e6a27ec8f5c', @PendingDvh), -- Mechanic: CUF08
	('5fda21db-c888-4d63-9890-2dca815d19c5', '9f70629a-7d78-400b-bd94-f13f5f356e24', @PendingDvh), -- Administrative Staff: CUF09
	('5fda21db-c888-4d63-9890-2dca815d19c5', '521ab5ef-abf2-4fd6-89bd-aebdfdba560c', @PendingDvh), -- Administrative Staff: CUF10
	('f53282e8-496e-413d-afa7-f04fe6ab3a26', 'b5a35e91-41d7-4a37-9fde-187c019a3caa', @PendingDvh)  -- Cashier: CUF11

COMMIT
GO
