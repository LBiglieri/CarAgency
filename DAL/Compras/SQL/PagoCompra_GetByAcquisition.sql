CREATE or ALTER proc PagoCompra_GetByAcquisition @Compra_Id uniqueidentifier as

-- Pagos registrados por el cajero (CUF11 paso 5), con el medio y quien pago.
select	a.PagoCompra_Id,
        a.Compra_Id,
        a.TipoPago_Id,
        b.Descripcion as TipoPago_Descripcion,
        a.Cajero_Id,
        (u.Nombre + ' ' + u.Apellido) as Cajero,
        a.Monto,
        a.Banco,
        a.CbuAlias,
        a.NumeroOperacion,
        a.NumeroCheque,
        a.FechaPago,
        a.FechaCreacion
from	PagoCompra a
        join TipoPago b on (b.TipoPago_Id = a.TipoPago_Id)
        join Usuario u on (u.Usuario_Id = a.Cajero_Id)
where	a.Compra_Id = @Compra_Id
order by a.FechaCreacion asc

go
