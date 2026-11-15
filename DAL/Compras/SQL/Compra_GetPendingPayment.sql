CREATE or ALTER proc Compra_GetPendingPayment @Persona_Id uniqueidentifier as

-- Compras de la persona para el cajero (CUF11 paso 2): las de Documentacion completa (5), con saldo o con el
-- saldo cancelado y el comprobante sin emitir, y las que ya tienen comprobante para volver a imprimirlo.
select	a.Compra_Id,
        a.Numero,
        g.Patente,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        a.PrecioAcordado,
        isnull((select sum(y.Monto) from PagoCompra y where y.Compra_Id = a.Compra_Id), 0) as MontoPagado,
        r.Numero as ComprobanteCompra_Numero,
        a.Estado
from	Compra a
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = a.VehiculoProspecto_Id)
        join Marca c on (c.Marca_Id = g.Marca_Id)
		join Modelo d on (d.Modelo_Id = g.Modelo_Id)
		join Version f on (f.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
        left join ComprobanteCompra r on (r.Compra_Id = a.Compra_Id)
where	a.Persona_Id = @Persona_Id
        and (a.Estado = 5 or r.ComprobanteCompra_Id is not null)
order by a.Numero asc

go
