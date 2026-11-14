CREATE or ALTER proc Compra_GetById @Compra_Id uniqueidentifier as

-- Fila completa: cualquier cambio de estado recalcula el DVH con todas las columnas.
select	a.Compra_Id,
        a.Numero,
        a.Persona_Id,
        a.VehiculoProspecto_Id,
        a.Vendedor_Id,
        a.Estado,
        a.MotivoRechazo,
        a.Fecha,
        a.TasacionAceptada_Id,
        a.PrecioAcordado,
        a.CuilCuitTitular,
        a.FechaFirmaContrato,
        a.CodigoPrecarga08,
        a.FechaCitaTransferencia,
        a.Vehiculo_Id,
        a.FechaCierre
from	Compra a
where	a.Compra_Id = @Compra_Id

go
