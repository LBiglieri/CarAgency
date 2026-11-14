CREATE or ALTER proc Pago_GetAllByInvoice @Factura_Id uniqueidentifier as

select	a.Pago_Id,
        a.Factura_Id,
        a.TipoPago_Id, b.Descripcion as TipoPago_Descripcion,
        a.Monto,
        a.Detalle
from	Pago a
		join TipoPago b on (b.TipoPago_Id = a.TipoPago_Id)
where	a.Factura_Id = @Factura_Id

go


