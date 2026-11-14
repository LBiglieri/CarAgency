CREATE or ALTER proc FormaPagoCompra_GetByAcquisition @Compra_Id uniqueidentifier as

-- Forma de pago pactada y lo que el cajero ya pago por cada medio (CUF11).
select	a.Compra_Id,
        a.TipoPago_Id,
        b.Descripcion as TipoPago_Descripcion,
        a.Monto,
        isnull((select sum(x.Monto) from PagoCompra x
                where x.Compra_Id = a.Compra_Id and x.TipoPago_Id = a.TipoPago_Id), 0) as MontoPagado
from	FormaPagoCompra a
        join TipoPago b on (b.TipoPago_Id = a.TipoPago_Id)
where	a.Compra_Id = @Compra_Id
order by a.Monto desc

go
