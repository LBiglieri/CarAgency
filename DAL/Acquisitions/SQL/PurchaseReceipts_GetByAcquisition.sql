CREATE or ALTER proc PurchaseReceipts_GetByAcquisition @Acquisition_Id uniqueidentifier as

-- El PDF del comprobante se arma siempre con lo que se guardo al emitirlo.
select	a.Id,
        a.Number,
        a.Acquisition_Id,
        a.Issue_Date,
        a.Client_Name,
        a.Client_CUIL_CUIT,
        a.Detail,
        a.Net_Amount,
        a.Tax_Amount,
        a.Total_Amount
from	PurchaseReceipts a
where	a.Acquisition_Id = @Acquisition_Id

go
