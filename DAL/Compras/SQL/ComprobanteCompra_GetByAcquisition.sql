CREATE or ALTER proc ComprobanteCompra_GetByAcquisition @Compra_Id uniqueidentifier as

-- El PDF del comprobante se arma siempre con lo que se guardo al emitirlo.
select	a.ComprobanteCompra_Id,
        a.Numero,
        a.Compra_Id,
        a.Fecha,
        a.NombrePersona,
        a.CuilCuit,
        a.Detalle,
        a.MontoNeto,
        a.MontoImpuesto,
        a.MontoTotal
from	ComprobanteCompra a
where	a.Compra_Id = @Compra_Id

go
