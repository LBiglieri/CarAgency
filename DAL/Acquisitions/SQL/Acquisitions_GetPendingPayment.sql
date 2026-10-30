CREATE or ALTER proc Acquisitions_GetPendingPayment @Client_Id uniqueidentifier as

-- Compras del cliente para el cajero (CUF11 paso 2): las de Documentacion completa (5), con saldo o con el
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
where	a.Client_Id = @Client_Id
        and (a.Status = 5 or r.Id is not null)
order by a.Number asc

go
