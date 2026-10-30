CREATE or ALTER proc AcquisitionPayments_GetByAcquisition @Acquisition_Id uniqueidentifier as

-- Pagos registrados por el cajero (CUF11 paso 5), con el medio y quien pago.
select	a.Id,
        a.Acquisition_Id,
        a.PaymentType_Id,
        b.Description as PaymentType_Description,
        a.Cashier_Id,
        (u.Name + ' ' + u.Surname) as Cashier,
        a.Amount,
        a.Bank,
        a.Cbu_Alias,
        a.Operation_Number,
        a.Check_Number,
        a.Payment_Date,
        a.Creation_Date
from	AcquisitionPayments a
        join PaymentTypes b on (b.Id = a.PaymentType_Id)
        join Users u on (u.Id = a.Cashier_Id)
where	a.Acquisition_Id = @Acquisition_Id
order by a.Creation_Date asc

go
