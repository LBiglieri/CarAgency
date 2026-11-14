CREATE or ALTER proc Compra_GetSummary @Compra_Id uniqueidentifier as

-- Resumen completo de la compra (CUF10 paso 20): vehiculo, inspeccion, tasacion aceptada, acuerdo,
-- lo pagado y el comprobante de CUF11 y la transferencia. Los datos de la persona se leen aparte porque estan encriptados.
select	a.Compra_Id,
        a.Numero,
        a.Persona_Id,
        h.DNI,
        g.Patente,
        g.NumeroChasis,
        g.NumeroMotor,
        (cast(g.Anio as varchar) + ' ' + e.Descripcion + ' ' + c.Descripcion + ' ' + d.Descripcion + ' ' + f.Descripcion) as Vehiculo_Descripcion,
        g.Marca_Id,
        g.Modelo_Id,
        g.Version_Id,
        g.Color_Id,
        g.Anio,
        g.Puertas,
        g.Kilometraje,
        b.Inspeccion_Id as Inspeccion_Id,
        b.FechaRealizada,
        b.KilometrajeVerificado,
        x.Valor as Tasacion_Valor,
        x.Fecha as Tasacion_Fecha,
        a.PrecioAcordado,
        a.CuilCuitTitular,
        a.FechaFirmaContrato,
        isnull((select sum(y.Monto) from PagoCompra y where y.Compra_Id = a.Compra_Id), 0) as MontoPagado,
        r.Numero as ComprobanteCompra_Numero,
        r.Fecha as ComprobanteCompra_Fecha,
        a.CodigoPrecarga08,
        a.FechaCitaTransferencia,
        a.FechaCierre
from	Compra a
        join Inspeccion b on (b.Compra_Id = a.Compra_Id)
        join Persona h on (h.Persona_Id = a.Persona_Id)
        join VehiculoProspecto g on (g.VehiculoProspecto_Id = a.VehiculoProspecto_Id)
        join Marca c on (c.Marca_Id = g.Marca_Id)
		join Modelo d on (d.Modelo_Id = g.Modelo_Id)
		join Version f on (f.Version_Id = g.Version_Id)
		join Color e on (e.Color_Id = g.Color_Id)
        left join Tasacion x on (x.Tasacion_Id = a.TasacionAceptada_Id)
        left join ComprobanteCompra r on (r.Compra_Id = a.Compra_Id)
where	a.Compra_Id = @Compra_Id

go
