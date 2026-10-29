CREATE or ALTER proc Acquisitions_GetById @Id uniqueidentifier as

-- Fila completa: cualquier cambio de estado recalcula el DVH con todas las columnas.
select	a.Id,
        a.Number,
        a.Client_Id,
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

go
