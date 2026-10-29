CREATE or ALTER proc Acquisitions_GetSummary @Id uniqueidentifier as

-- Resumen completo de la compra (CUF10 paso 20): vehiculo, inspeccion, tasacion aceptada, acuerdo,
-- lo pagado en CUF11 y la transferencia. Los datos del cliente se leen aparte porque estan encriptados.
select	a.Id,
        a.Number,
        a.Client_Id,
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
        a.Precharge_Code_08,
        a.Transfer_Appointment_Date,
        a.Closing_Date
from	Acquisitions a
        join Inspections b on (b.Acquisition_Id = a.Id)
        join Clients h on (h.Id = a.Client_Id)
        join ProspectVehicles g on (g.Id = a.ProspectVehicle_Id)
        join Makes c on (c.Id = g.Make_Id)
		join Models d on (d.Id = g.Model_Id)
		join Versions f on (f.Id = g.Version_Id)
		join Colours e on (e.Id = g.Colour_Id)
        left join Appraisals x on (x.Id = a.Accepted_Appraisal_Id)
where	a.Id = @Id

go
