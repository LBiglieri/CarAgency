CREATE or ALTER proc AcquisitionPaymentTerms_GetByAcquisition @Acquisition_Id uniqueidentifier as

-- Forma de pago pactada y lo que el cajero ya pago por cada medio (CUF11).
select	a.Acquisition_Id,
        a.PaymentType_Id,
        b.Description as PaymentType_Description,
        a.Amount,
        isnull((select sum(x.Amount) from AcquisitionPayments x
                where x.Acquisition_Id = a.Acquisition_Id and x.PaymentType_Id = a.PaymentType_Id), 0) as Paid_Amount
from	AcquisitionPaymentTerms a
        join PaymentTypes b on (b.Id = a.PaymentType_Id)
where	a.Acquisition_Id = @Acquisition_Id
order by a.Amount desc

go
