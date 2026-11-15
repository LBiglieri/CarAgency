-- Renombre de Clients a People (la persona que compra o vende un vehiculo, CUF02 Registrar Persona).
-- Correr completo con: sqlcmd -S . -E -d CarAgency -I -b -i Rename_Clients_To_People.sql
-- El DVH se calcula con el nombre de la tabla y de cada columna, asi que despues de correrlo hay que
-- recalcular DVH y DVV de People, Acquisitions, Invoice, Paperwork, Quotations, Reservation,
-- PurchaseReceipts y Permissions con Recalcular DV (la pantalla de integridad aparece sola al iniciar sesion).
SET XACT_ABORT ON
GO
BEGIN TRANSACTION

EXEC sp_rename 'dbo.Clients', 'People'
EXEC sp_rename 'dbo.PK_Clients', 'PK_People', 'OBJECT'
EXEC sp_rename 'dbo.CK_Clients_DVH', 'CK_People_DVH', 'OBJECT'

EXEC sp_rename 'dbo.Acquisitions.Client_Id', 'Person_Id', 'COLUMN'
EXEC sp_rename 'dbo.Invoice.Client_Id', 'Person_Id', 'COLUMN'
EXEC sp_rename 'dbo.Invoice.CUIL_CUIT_Client', 'CUIL_CUIT_Person', 'COLUMN'
EXEC sp_rename 'dbo.Paperwork.Client_Id', 'Person_Id', 'COLUMN'
EXEC sp_rename 'dbo.Quotations.Client_Id', 'Person_Id', 'COLUMN'
EXEC sp_rename 'dbo.Reservation.Client_Id', 'Person_Id', 'COLUMN'
EXEC sp_rename 'dbo.PurchaseReceipts.Client_Name', 'Person_Name', 'COLUMN'
EXEC sp_rename 'dbo.PurchaseReceipts.Client_CUIL_CUIT', 'Person_CUIL_CUIT', 'COLUMN'

EXEC sp_rename 'dbo.FK_Acquisitions_Clients', 'FK_Acquisitions_People', 'OBJECT'
EXEC sp_rename 'dbo.FK_Invoice_Clients', 'FK_Invoice_People', 'OBJECT'
EXEC sp_rename 'dbo.FK_Quotation_Clients', 'FK_Quotation_People', 'OBJECT'
EXEC sp_rename 'dbo.FK_Reservation_Clients', 'FK_Reservation_People', 'OBJECT'
EXEC sp_rename 'dbo.Acquisitions.IX_Acquisitions_Client', 'IX_Acquisitions_Person', 'INDEX'

UPDATE dbo.DV SET TableName = 'People' WHERE TableName = 'Clients'

-- Events apunta a EventTypes con una FK compuesta sin cascada: se suspende mientras se renombra el tipo.
ALTER TABLE dbo.Events NOCHECK CONSTRAINT FK_Events_Type
UPDATE dbo.EventTypes SET EventType = 'PersonCreated', Module = 'People' WHERE EventType = 'ClientCreated'
UPDATE dbo.Events SET EventType = 'PersonCreated', Module = 'People' WHERE EventType = 'ClientCreated'
ALTER TABLE dbo.Events WITH CHECK CHECK CONSTRAINT FK_Events_Type

UPDATE dbo.Permissions SET Name = 'Create Person', Type = 'CreatePersonForm' WHERE Type = 'CreateClientForm'

COMMIT TRANSACTION
GO

DROP PROCEDURE IF EXISTS dbo.Clients_Add, dbo.Clients_GetByDni, dbo.Clients_GetById, dbo.Invoice_GetAllPendingOfPaperworkByClient, dbo.Paperwork_GetAllActiveByClient, dbo.Paperwork_GetAllByClient, dbo.Quotations_GetAllActiveByClient, dbo.Reservation_GetAllActiveByClient
GO

CREATE or ALTER proc Acquisitions_Add	@Id uniqueidentifier, @Number int, @Person_Id uniqueidentifier, @ProspectVehicle_Id uniqueidentifier, @Salesman_Id uniqueidentifier,
									@Status int, @Rejection_Reason int, @Creation_Date datetime, @Accepted_Appraisal_Id uniqueidentifier, @Agreed_Price float,
									@Owner_CUIL_CUIT varchar(13), @Contract_Signed_Date datetime, @Precharge_Code_08 varchar(50), @Transfer_Appointment_Date datetime,
									@Vehicle_Id uniqueidentifier, @Closing_Date datetime, @DVH char(64) as

insert into Acquisitions(Id, Number, Person_Id, ProspectVehicle_Id, Salesman_Id, Status, Rejection_Reason, Creation_Date, Accepted_Appraisal_Id, Agreed_Price,
						 Owner_CUIL_CUIT, Contract_Signed_Date, Precharge_Code_08, Transfer_Appointment_Date, Vehicle_Id, Closing_Date, DVH)
values (@Id, @Number, @Person_Id, @ProspectVehicle_Id, @Salesman_Id, @Status, @Rejection_Reason, @Creation_Date, @Accepted_Appraisal_Id, @Agreed_Price,
		@Owner_CUIL_CUIT, @Contract_Signed_Date, @Precharge_Code_08, @Transfer_Appointment_Date, @Vehicle_Id, @Closing_Date, @DVH)

select 'success' as SQLResultType,'Acquisition added succesfully!' as message
GO

CREATE or ALTER proc Acquisitions_GetById @Id uniqueidentifier as

-- Fila completa: cualquier cambio de estado recalcula el DVH con todas las columnas.
select	a.Id,
        a.Number,
        a.Person_Id,
        a.ProspectVehicle_Id,
        a.Salesman_Id,
        a.Status,
        a.Rejection_Reason,
        a.Creation_Date,
        a.Accepted_Appraisal_Id,
        a.Agreed_Price,
        a.Owner_CUIL_CUIT,
        a.Contract_Signed_Date,
        a.Precharge_Code_08,
        a.Transfer_Appointment_Date,
        a.Vehicle_Id,
        a.Closing_Date
from	Acquisitions a
where	a.Id = @Id
GO

CREATE or ALTER proc Acquisitions_GetPendingAppraisal @Filter varchar(20), @Now datetime as

-- Adquisiciones a tasar: Inspeccionado (2), o Tasada (3) con la ultima tasacion vencida.
-- El filtro busca por DNI exacto o por el comienzo de la matricula; vacio trae todas.
select	a.Id,
        a.Number,
        b.Id as Inspection_Id,
        a.Person_Id,
        h.Dni,
        g.License_Plate,
        (cast(g.Year as varchar) + ' ' + e.Description + ' ' + c.Description + ' ' + d.Description + ' ' + f.Description) as Vehicle_Description,
        b.Performed_Date,
        (select count(*) from Appraisals x where x.Acquisition_Id = a.Id) as Previous_Appraisals
from	Acquisitions a
        join Inspections b on (b.Acquisition_Id = a.Id)
        join People h on (h.Id = a.Person_Id)
        join ProspectVehicles g on (g.Id = a.ProspectVehicle_Id)
        join Makes c on (c.Id = g.Make_Id)
		join Models d on (d.Id = g.Model_Id)
		join Versions f on (f.Id = g.Version_Id)
		join Colours e on (e.Id = g.Colour_Id)
where	(a.Status = 2
            or (a.Status = 3 and (select top 1 x.Expiration_Date from Appraisals x where x.Acquisition_Id = a.Id order by x.Creation_Date desc) < @Now))
        and (isnull(@Filter, '') = ''
            or h.Dni = try_cast(@Filter as int)
            or g.License_Plate like @Filter + '%')
order by b.Performed_Date asc
GO

CREATE or ALTER proc Acquisitions_GetPendingManagement @Filter varchar(20) as

-- Compras en gestion (CUF10): Tasada (3), Compra formalizada (4), Documentacion completa (5), Pagada (6) y Transferida (7).
-- El filtro busca por DNI exacto o por el comienzo de la matricula; vacio trae todas.
select	a.Id,
        a.Number,
        b.Id as Inspection_Id,
        a.Person_Id,
        h.Dni,
        g.License_Plate,
        (cast(g.Year as varchar) + ' ' + e.Description + ' ' + c.Description + ' ' + d.Description + ' ' + f.Description) as Vehicle_Description,
        a.Status
from	Acquisitions a
        join Inspections b on (b.Acquisition_Id = a.Id)
        join People h on (h.Id = a.Person_Id)
        join ProspectVehicles g on (g.Id = a.ProspectVehicle_Id)
        join Makes c on (c.Id = g.Make_Id)
		join Models d on (d.Id = g.Model_Id)
		join Versions f on (f.Id = g.Version_Id)
		join Colours e on (e.Id = g.Colour_Id)
where	a.Status between 3 and 7
        and (isnull(@Filter, '') = ''
            or h.Dni = try_cast(@Filter as int)
            or g.License_Plate like @Filter + '%')
order by a.Number asc
GO

CREATE or ALTER proc Acquisitions_GetPendingPayment @Person_Id uniqueidentifier as

-- Compras de la persona para el cajero (CUF11 paso 2): las de Documentacion completa (5), con saldo o con el
-- saldo cancelado y el comprobante sin emitir, y las que ya tienen comprobante para volver a imprimirlo.
select	a.Id,
        a.Number,
        g.License_Plate,
        (cast(g.Year as varchar) + ' ' + e.Description + ' ' + c.Description + ' ' + d.Description + ' ' + f.Description) as Vehicle_Description,
        a.Agreed_Price,
        isnull((select sum(y.Amount) from AcquisitionPayments y where y.Acquisition_Id = a.Id), 0) as Paid_Amount,
        r.Number as Receipt_Number,
        a.Status
from	Acquisitions a
        join ProspectVehicles g on (g.Id = a.ProspectVehicle_Id)
        join Makes c on (c.Id = g.Make_Id)
		join Models d on (d.Id = g.Model_Id)
		join Versions f on (f.Id = g.Version_Id)
		join Colours e on (e.Id = g.Colour_Id)
        left join PurchaseReceipts r on (r.Acquisition_Id = a.Id)
where	a.Person_Id = @Person_Id
        and (a.Status = 5 or r.Id is not null)
order by a.Number asc
GO

CREATE or ALTER proc Acquisitions_GetSummary @Id uniqueidentifier as

-- Resumen completo de la compra (CUF10 paso 20): vehiculo, inspeccion, tasacion aceptada, acuerdo,
-- lo pagado y el comprobante de CUF11 y la transferencia. Los datos de la persona se leen aparte porque estan encriptados.
select	a.Id,
        a.Number,
        a.Person_Id,
        h.Dni,
        g.License_Plate,
        g.Chassis_Number,
        g.Engine_Number,
        (cast(g.Year as varchar) + ' ' + e.Description + ' ' + c.Description + ' ' + d.Description + ' ' + f.Description) as Vehicle_Description,
        g.Make_Id,
        g.Model_Id,
        g.Version_Id,
        g.Colour_Id,
        g.Year,
        g.Doors,
        g.Declared_Kilometers,
        b.Id as Inspection_Id,
        b.Performed_Date,
        b.Verified_Kilometers,
        x.Amount as Appraisal_Amount,
        x.Creation_Date as Appraisal_Date,
        a.Agreed_Price,
        a.Owner_CUIL_CUIT,
        a.Contract_Signed_Date,
        isnull((select sum(y.Amount) from AcquisitionPayments y where y.Acquisition_Id = a.Id), 0) as Paid_Amount,
        r.Number as Receipt_Number,
        r.Issue_Date as Receipt_Issue_Date,
        a.Precharge_Code_08,
        a.Transfer_Appointment_Date,
        a.Closing_Date
from	Acquisitions a
        join Inspections b on (b.Acquisition_Id = a.Id)
        join People h on (h.Id = a.Person_Id)
        join ProspectVehicles g on (g.Id = a.ProspectVehicle_Id)
        join Makes c on (c.Id = g.Make_Id)
		join Models d on (d.Id = g.Model_Id)
		join Versions f on (f.Id = g.Version_Id)
		join Colours e on (e.Id = g.Colour_Id)
        left join Appraisals x on (x.Id = a.Accepted_Appraisal_Id)
        left join PurchaseReceipts r on (r.Acquisition_Id = a.Id)
where	a.Id = @Id
GO

CREATE or ALTER proc Acquisitions_Pay	@Id uniqueidentifier, @Number int, @Acquisition_Id uniqueidentifier, @Issue_Date datetime, @Person_Name varchar(255),
									@Person_CUIL_CUIT varchar(13), @Detail varchar(max), @Net_Amount float, @Tax_Amount float, @Total_Amount float,
									@DVH char(64), @Status int, @Acquisition_DVH char(64) as

-- CUF11 pasos 7 y 9: el comprobante y el pase a Pagada van juntos o no se graba nada. Un comprobante con la
-- compra sin pagar no se podria volver a emitir, y una compra pagada sin comprobante no se podria reimprimir.
-- Los pagos ya estan grabados: si se cae antes de este paso, la compra queda con saldo cero y se reintenta.
set xact_abort on
begin try
	begin tran

	if not exists (select * from Acquisitions where Id = @Acquisition_Id and Status = 5)
		throw 51012, 'La adquisicion ya no esta en estado Documentacion completa.', 1;

	if exists (select * from AcquisitionPaymentTerms t
				where t.Acquisition_Id = @Acquisition_Id
				and round(t.Amount - isnull((select sum(x.Amount) from AcquisitionPayments x
										where x.Acquisition_Id = t.Acquisition_Id and x.PaymentType_Id = t.PaymentType_Id), 0), 2) <> 0)
		throw 51013, 'La compra todavia tiene saldo pendiente.', 1;

	insert into PurchaseReceipts(Id, Number, Acquisition_Id, Issue_Date, Person_Name, Person_CUIL_CUIT, Detail, Net_Amount, Tax_Amount, Total_Amount, DVH)
	values (@Id, @Number, @Acquisition_Id, @Issue_Date, @Person_Name, @Person_CUIL_CUIT, @Detail, @Net_Amount, @Tax_Amount, @Total_Amount, @DVH)

	update Acquisitions
	set DVH = @Acquisition_DVH,
		Status = @Status
	where Id = @Acquisition_Id

	commit tran
end try
begin catch
	if @@trancount > 0 rollback tran;
	throw;
end catch

select 'success' as SQLResultType,'Acquisition paid succesfully!' as message
GO

CREATE or ALTER proc Acquisitions_Update	@Id uniqueidentifier, @Number int, @Person_Id uniqueidentifier, @ProspectVehicle_Id uniqueidentifier, @Salesman_Id uniqueidentifier,
											@Status int, @Rejection_Reason int, @Creation_Date datetime, @Accepted_Appraisal_Id uniqueidentifier, @Agreed_Price float,
											@Owner_CUIL_CUIT varchar(13), @Contract_Signed_Date datetime, @Precharge_Code_08 varchar(50), @Transfer_Appointment_Date datetime,
											@Vehicle_Id uniqueidentifier, @Closing_Date datetime, @DVH char(64) as

-- Fila completa: el DVH se recalcula con todas las columnas aunque solo cambie el estado.
update Acquisitions
set DVH = @DVH,
	Number = @Number,
	Person_Id = @Person_Id,
	ProspectVehicle_Id = @ProspectVehicle_Id,
	Salesman_Id = @Salesman_Id,
	Status = @Status,
	Rejection_Reason = @Rejection_Reason,
	Creation_Date = @Creation_Date,
	Accepted_Appraisal_Id = @Accepted_Appraisal_Id,
	Agreed_Price = @Agreed_Price,
	Owner_CUIL_CUIT = @Owner_CUIL_CUIT,
	Contract_Signed_Date = @Contract_Signed_Date,
	Precharge_Code_08 = @Precharge_Code_08,
	Transfer_Appointment_Date = @Transfer_Appointment_Date,
	Vehicle_Id = @Vehicle_Id,
	Closing_Date = @Closing_Date
where Id = @Id

select 'success' as SQLResultType,'Acquisition updated succesfully!' as message
GO

CREATE or ALTER proc People_Add	@Id uniqueidentifier, @Dni int, @Name varchar(max), @Surname varchar(max), @Address varchar(max),
									@Phone_Number_Personal varchar(max), @Phone_Number_House varchar(max), @Email varchar(max), @Date_Of_Birth datetime , @DVH char(64) as

insert into People(Id,Dni,Name,Surname,Address,Phone_Number_Personal,Phone_Number_House,Email,Date_Of_Birth,DVH)
values (@Id,@Dni,@Name,@Surname,@Address,@Phone_Number_Personal,@Phone_Number_House,@Email,@Date_Of_Birth,@DVH)

select 'success' as SQLResultType,'Person added succesfully!' as message
GO

CREATE or ALTER proc People_GetByDni @Dni int as

select	u.Id,u.Dni,u.Name,u.Surname,u.Address,u.Phone_Number_Personal,u.Phone_Number_House,u.Email,u.Date_Of_Birth
from	People  u
where	u.Dni = @Dni
GO

CREATE or ALTER proc People_GetById @Id uniqueidentifier as

select	Id,Dni,Name,Surname,Address,Phone_Number_Personal,Phone_Number_House,Email,Date_Of_Birth
from	People  
where	Id = @Id
GO

CREATE OR ALTER PROCEDURE dbo.DV_ValidateTable @TableName nvarchar(128)
AS
BEGIN
    SET NOCOUNT ON;
    IF @TableName IS NULL OR @TableName COLLATE Latin1_General_100_BIN2 NOT IN
        ('AcquisitionDocuments','AcquisitionDocumentTypes','AcquisitionPayments','AcquisitionPaymentTerms','Acquisitions',
         'Appraisals','People','Colours','Events','InspectionItems','Inspections','InspectionScores','Invoice','Makes',
         'Models','Paperwork','Payments','PaymentTypes','Permission_Permission','Permissions','ProspectVehicles',
         'PurchaseReceipts','Quotations','Reservation','Users','Vehicles','Versions')
        THROW 51003, 'Tabla fuera del alcance de digitos verificadores.', 1;
END
GO

CREATE or ALTER proc Invoice_Add	@Id uniqueidentifier, @Vehicle_Id uniqueidentifier, @Person_Id uniqueidentifier, @Reservation_Id uniqueidentifier, @Detail varchar(max),
									@CUIL_CUIT_Person varchar(max), @Razon_Social varchar(max), @Amount float, @Payment_Status bit, @Creation_Date datetime , @DVH char(64) as

insert into Invoice(Id, Vehicle_Id, Person_Id, Reservation_Id, Detail, CUIL_CUIT_Person, Razon_Social, Amount, Payment_Status, Creation_Date,DVH)
values (@Id, @Vehicle_Id, @Person_Id, @Reservation_Id, @Detail, @CUIL_CUIT_Person, @Razon_Social, @Amount, @Payment_Status, @Creation_Date,@DVH)

select 'success' as SQLResultType,'Invoice added succesfully!' as message
GO

CREATE or ALTER proc Invoice_GetAllPendingOfPaperworkByPerson @Person_Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id,  (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Person_Id,
        a.Reservation_Id, 
        a.Detail, 
        a.CUIL_CUIT_Person,
        a.Razon_Social, 
        a.Amount,
        a.Payment_Status, 
        a.Creation_Date
from	Invoice a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
where   a.Payment_Status = 1
        and a.Person_Id = @Person_Id
        and not exists 
        (
            select * from Paperwork x where x.Invoice_id = a.Id
        )
GO

CREATE or ALTER proc Invoice_GetById @Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id,  (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Person_Id,
        a.Reservation_Id, 
        a.Detail, 
        a.CUIL_CUIT_Person,
        a.Razon_Social, 
        a.Amount,
        a.Payment_Status, 
        a.Creation_Date
from	Invoice a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
where	a.Id = @Id
GO

CREATE or ALTER proc Invoice_Update	@Id uniqueidentifier, @Vehicle_Id uniqueidentifier, @Person_Id uniqueidentifier, @Reservation_Id uniqueidentifier, @Detail varchar(max),
										@CUIL_CUIT_Person varchar(max), @Razon_Social varchar(max), @Amount float, @Payment_Status bit, @Creation_Date datetime , @DVH char(64) as

update Invoice
set DVH = @DVH,
	Vehicle_Id = @Vehicle_Id,
	Person_Id = @Person_Id,
	Reservation_Id = @Reservation_Id,
	Detail = @Detail,
	CUIL_CUIT_Person = @CUIL_CUIT_Person,
	Razon_Social = @Razon_Social,
	Amount = @Amount,
	Payment_Status = @Payment_Status,
	Creation_Date = @Creation_Date
where Id = @Id

select 'success' as SQLResultType,'Invoice updated succesfully!' as message
GO

CREATE or ALTER proc Paperwork_Add	@Id uniqueidentifier, @Vehicle_Id uniqueidentifier, @Person_Id uniqueidentifier, @Invoice_Id uniqueidentifier, @Paperwork_Precharge_Code varchar(50),
									@Transfer_Date datetime, @Observations varchar(max), @IsFinished bit , @DVH char(64) as

insert into Paperwork(Id, Vehicle_Id, Person_Id, Invoice_Id, Paperwork_Precharge_Code, Transfer_Date, Observations, IsFinished,DVH)
values (@Id, @Vehicle_Id, @Person_Id, @Invoice_Id, @Paperwork_Precharge_Code, @Transfer_Date, @Observations, @IsFinished,@DVH)

select 'success' as SQLResultType,'Paperwork added succesfully!' as message
GO

CREATE or ALTER proc Paperwork_GetAllActiveByPerson @Person_Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id,  (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Person_Id,
        a.Invoice_Id, 
        a.Paperwork_Precharge_Code, 
        a.Transfer_Date,
        a.Observations, 
        a.IsFinished
from	Paperwork a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
where	(a.Person_Id = @Person_Id or (@Person_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
		and a.IsFinished = 0
GO

CREATE OR ALTER proc Paperwork_GetAllByPerson @Person_Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id,  (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Person_Id,
        a.Invoice_Id, 
        a.Paperwork_Precharge_Code, 
        a.Transfer_Date,
        a.Observations, 
        a.IsFinished
from	Paperwork a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
where	(a.Person_Id = @Person_Id or (@Person_Id = CAST(0x0 AS UNIQUEIDENTIFIER)))
GO

CREATE or ALTER proc Paperwork_GetById @Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id,  (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Person_Id,
        a.Invoice_Id, 
        a.Paperwork_Precharge_Code, 
        a.Transfer_Date,
        a.Observations, 
        a.IsFinished
from	Paperwork a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
where	a.Id = @Id
GO

CREATE or ALTER proc Paperwork_Update	@Id uniqueidentifier, @Vehicle_Id uniqueidentifier, @Person_Id uniqueidentifier, @Invoice_Id uniqueidentifier, @Paperwork_Precharge_Code varchar(50),
									@Transfer_Date datetime, @Observations varchar(max), @IsFinished bit , @DVH char(64) as

update Paperwork
set DVH = @DVH,
	Vehicle_Id = @Vehicle_Id,
	Person_Id = @Person_Id,
	Invoice_Id = @Invoice_Id,
	Paperwork_Precharge_Code = @Paperwork_Precharge_Code,
	Transfer_Date = @Transfer_Date,
	Observations = @Observations,
	IsFinished = @IsFinished
where Id = @Id

select 'success' as SQLResultType,'Paperwork updated succesfully!' as message
GO

CREATE or ALTER proc PurchaseReceipts_GetByAcquisition @Acquisition_Id uniqueidentifier as

-- El PDF del comprobante se arma siempre con lo que se guardo al emitirlo.
select	a.Id,
        a.Number,
        a.Acquisition_Id,
        a.Issue_Date,
        a.Person_Name,
        a.Person_CUIL_CUIT,
        a.Detail,
        a.Net_Amount,
        a.Tax_Amount,
        a.Total_Amount
from	PurchaseReceipts a
where	a.Acquisition_Id = @Acquisition_Id
GO

CREATE or ALTER proc Quotations_Add	@Id uniqueidentifier, @Vehicle_Id uniqueidentifier, @Person_Id uniqueidentifier, @Price float, @Creation_Date datetime , @DVH char(64) as

insert into Quotations(Id, Vehicle_Id, Person_Id, Price, Creation_Date,DVH)
values (@Id, @Vehicle_Id, @Person_Id, @Price, @Creation_Date,@DVH)

select 'success' as SQLResultType,'Quotations added succesfully!' as message
GO

CREATE or ALTER proc Quotations_GetAll @Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id, (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Person_Id, g.Name as Person_Name, g.Surname as Person_Surname, g.Dni as Person_Dni, 
        a.Price,
        a.Creation_Date
from	Quotations a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
        join People g on (g.Id = a.Person_Id)
where	a.Id = @Id
GO

CREATE or ALTER proc Quotations_GetAllActiveByPerson @Person_Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id, (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Person_Id, g.Name as Person_Name, g.Surname as Person_Surname, g.Dni as Person_Dni, 
        a.Price,
        a.Creation_Date
from	Quotations a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
        join People g on (g.Id = a.Person_Id)
where	a.Person_Id = @Person_Id
        and cast(a.Creation_Date as date) = cast(GETDATE() as date)
        and not exists 
        (
            select * 
            from Reservation r 
            left join Invoice i on (i.Reservation_Id = r.Id and i.Payment_Status = 1)
            where r.Vehicle_Id = a.Vehicle_Id 
            and 
            (
                i.Reservation_Id is not null
                or 
                (cast(getdate() as date) >= cast(r.Creation_Date as date)) and (cast(getdate() as date) <= cast(r.Expiration_Date as date))
            )
        )
GO

CREATE or ALTER proc Quotations_GetById @Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id, (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Person_Id, g.Name as Person_Name, g.Surname as Person_Surname, g.Dni as Person_Dni, 
        a.Price,
        a.Creation_Date
from	Quotations a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
        join People g on (g.Id = a.Person_Id)
where	a.Id = @Id
GO

CREATE or ALTER proc Reservation_Add	@Id uniqueidentifier, @Vehicle_Id uniqueidentifier, @Person_Id uniqueidentifier, @Price float, @Creation_Date datetime, @Expiration_Date datetime , @DVH char(64) as

insert into Reservation(Id, Vehicle_Id, Person_Id, Price, Creation_Date,Expiration_Date,DVH)
values (@Id, @Vehicle_Id, @Person_Id, @Price, @Creation_Date,@Expiration_Date,@DVH)

select 'success' as SQLResultType,'Quotations added succesfully!' as message
GO

CREATE or ALTER proc Reservation_GetAll @Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id, (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Person_Id, g.Name as Person_Name, g.Surname as Person_Surname, g.Dni as Person_Dni, 
        a.Price,
        a.Creation_Date,
        a.Expiration_Date
from	Reservation a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
        join People g on (g.Id = a.Person_Id)
where	a.Id = @Id
GO

CREATE or ALTER proc Reservation_GetAllActiveByPerson @Person_Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id, (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Person_Id, g.Name as Person_Name, g.Surname as Person_Surname, g.Dni as Person_Dni, 
        a.Price,
        a.Creation_Date,
        a.Expiration_Date
from	Reservation a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
        join People g on (g.Id = a.Person_Id)
        left join Invoice i on (i.Reservation_Id = a.Id and i.Payment_Status = 1)
where	a.Person_Id = @Person_Id
        and (cast(getdate() as date) >= cast(a.Creation_Date as date)) and (cast(getdate() as date) <= cast(a.Expiration_Date as date))
        and i.Id is null
GO

CREATE or ALTER proc Reservation_GetById @Id uniqueidentifier as

select	a.Id,
        a.Vehicle_Id, (cast(b.Year as varchar) + ' ' + e.Description + ' ' + f.Description + ' ' + c.Description + ' ' + d.Description + ' ' + b.License_Plate) as Vehicle_Description,
        a.Person_Id, g.Name as Person_Name, g.Surname as Person_Surname, g.Dni as Person_Dni, 
        a.Price,
        a.Creation_Date,
        a.Expiration_Date
from	Reservation a
        join Vehicles b on (b.Id = a.Vehicle_Id)
		join Models c on (c.Id = b.Model_Id)
		join Versions d on (d.Id = b.Version_Id)
		join Colours e on (e.Id = b.Colour_Id)
        join Makes f on (f.Id = b.Make_Id)
        join People g on (g.Id = a.Person_Id)
where	a.Id = @Id
GO
