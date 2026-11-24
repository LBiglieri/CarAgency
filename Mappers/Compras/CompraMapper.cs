using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using CarAgency.BE;
using CarAgency.DAL.Compras;
using CarAgency.Security.Integrity;
using CarAgency.Security;

namespace CarAgency.Mappers
{
    public class CompraMapper
    {
        private readonly CompraDataAccess data = new CompraDataAccess();

        public int GetNextNumber()
        {
            using (DataTable table = data.GetNextNumber())
                return (int)table.Rows[0]["Numero"];
        }

        // Sin coincidencias devuelve null.
        public List<DuplicateVehiculo> GetDuplicates(VehiculoProspecto vehicle)
        {
            List<DuplicateVehiculo> duplicates = MappingHandler.MapTableToEntities<DuplicateVehiculo>(data.GetDuplicates(vehicle.Patente,
                vehicle.NumeroChasis, vehicle.NumeroMotor));
            return duplicates.Count == 0 ? null : duplicates;
        }

        public SQLUpdateResult AddProspectVehicle(VehiculoProspecto vehicle)
        {
            return DigitVerifierWriteMapper.Save("VehiculoProspecto", dvh => data.AddProspectVehicle(vehicle.VehiculoProspecto_Id, vehicle.Patente,
                vehicle.NumeroChasis, vehicle.NumeroMotor, vehicle.Marca_Id, vehicle.Modelo_Id, vehicle.Version_Id, vehicle.Color_Id,
                vehicle.Anio, vehicle.Puertas, vehicle.Kilometraje, dvh));
        }

        public SQLUpdateResult AddAcquisition(Compra acquisition)
        {
            return DigitVerifierWriteMapper.Save("Compra", dvh => data.Add(acquisition.Compra_Id, acquisition.Numero, acquisition.Persona_Id,
                acquisition.VehiculoProspecto_Id, acquisition.Vendedor_Id, (int)acquisition.Estado, (int?)acquisition.MotivoRechazo,
                acquisition.Fecha, acquisition.TasacionAceptada_Id, acquisition.PrecioAcordado, acquisition.CuilCuitTitular,
                acquisition.FechaFirmaContrato, acquisition.CodigoPrecarga08, acquisition.FechaCitaTransferencia,
                acquisition.Vehiculo_Id, acquisition.FechaCierre, dvh));
        }

        // El MappingHandler no convierte NULL ni enums: la adquisicion se arma a mano.
        public Compra GetById(Guid id)
        {
            using (DataTable table = data.GetById(id))
            {
                if (table.Rows.Count == 0) return null;
                DataRow row = table.Rows[0];
                Compra acquisition = new Compra();
                acquisition.Compra_Id = (Guid)row["Compra_Id"];
                acquisition.Numero = (int)row["Numero"];
                acquisition.Persona_Id = (Guid)row["Persona_Id"];
                acquisition.VehiculoProspecto_Id = (Guid)row["VehiculoProspecto_Id"];
                acquisition.Vendedor_Id = (Guid)row["Vendedor_Id"];
                acquisition.Estado = (CompraStatus)(int)row["Estado"];
                acquisition.MotivoRechazo = (RejectionReason?)(row["MotivoRechazo"] as int?);
                acquisition.Fecha = (DateTime)row["Fecha"];
                acquisition.TasacionAceptada_Id = row["TasacionAceptada_Id"] as Guid?;
                acquisition.PrecioAcordado = row["PrecioAcordado"] as double?;
                acquisition.CuilCuitTitular = row["CuilCuitTitular"] as string;
                acquisition.FechaFirmaContrato = row["FechaFirmaContrato"] as DateTime?;
                acquisition.CodigoPrecarga08 = row["CodigoPrecarga08"] as string;
                acquisition.FechaCitaTransferencia = row["FechaCitaTransferencia"] as DateTime?;
                acquisition.Vehiculo_Id = row["Vehiculo_Id"] as Guid?;
                acquisition.FechaCierre = row["FechaCierre"] as DateTime?;
                return acquisition;
            }
        }

        public SQLUpdateResult UpdateAcquisition(Compra acquisition)
        {
            return DigitVerifierWriteMapper.Save("Compra", dvh => data.Update(acquisition.Compra_Id, acquisition.Numero, acquisition.Persona_Id,
                acquisition.VehiculoProspecto_Id, acquisition.Vendedor_Id, (int)acquisition.Estado, (int?)acquisition.MotivoRechazo,
                acquisition.Fecha, acquisition.TasacionAceptada_Id, acquisition.PrecioAcordado, acquisition.CuilCuitTitular,
                acquisition.FechaFirmaContrato, acquisition.CodigoPrecarga08, acquisition.FechaCitaTransferencia,
                acquisition.Vehiculo_Id, acquisition.FechaCierre, dvh));
        }

        // Sin compras en gestion devuelve null.
        public List<PendingCompra> GetPendingManagement(string filter)
        {
            List<PendingCompra> acquisitions = new List<PendingCompra>();
            using (DataTable table = data.GetPendingManagement(filter))
            {
                foreach (DataRow row in table.Rows)
                {
                    PendingCompra acquisition = new PendingCompra();
                    acquisition.Compra_Id = (Guid)row["Compra_Id"];
                    acquisition.Numero = (int)row["Numero"];
                    acquisition.Inspeccion_Id = (Guid)row["Inspeccion_Id"];
                    acquisition.Persona_Id = (Guid)row["Persona_Id"];
                    acquisition.DNI = (int)row["DNI"];
                    acquisition.Patente = (string)row["Patente"];
                    acquisition.Vehiculo_Descripcion = (string)row["Vehiculo_Descripcion"];
                    acquisition.Estado = (CompraStatus)(int)row["Estado"];
                    acquisitions.Add(acquisition);
                }
            }
            return acquisitions.Count == 0 ? null : acquisitions;
        }

        // Como la adquisicion, el resumen tiene NULL segun la etapa: se arma a mano.
        public CompraSummary GetSummary(Guid id)
        {
            using (DataTable table = data.GetSummary(id))
            {
                if (table.Rows.Count == 0) return null;
                DataRow row = table.Rows[0];
                CompraSummary summary = new CompraSummary();
                summary.Compra_Id = (Guid)row["Compra_Id"];
                summary.Numero = (int)row["Numero"];
                summary.Persona_Id = (Guid)row["Persona_Id"];
                summary.DNI = (int)row["DNI"];
                summary.Patente = (string)row["Patente"];
                summary.NumeroChasis = (string)row["NumeroChasis"];
                summary.NumeroMotor = (string)row["NumeroMotor"];
                summary.Vehiculo_Descripcion = (string)row["Vehiculo_Descripcion"];
                summary.Marca_Id = (Guid)row["Marca_Id"];
                summary.Modelo_Id = (Guid)row["Modelo_Id"];
                summary.Version_Id = (Guid)row["Version_Id"];
                summary.Color_Id = (Guid)row["Color_Id"];
                summary.Anio = (int)row["Anio"];
                summary.Puertas = (int)row["Puertas"];
                summary.Kilometraje = (int)row["Kilometraje"];
                summary.Inspeccion_Id = (Guid)row["Inspeccion_Id"];
                summary.FechaRealizada = row["FechaRealizada"] as DateTime?;
                summary.KilometrajeVerificado = row["KilometrajeVerificado"] as int?;
                summary.Tasacion_Valor = row["Tasacion_Valor"] as double?;
                summary.Tasacion_Fecha = row["Tasacion_Fecha"] as DateTime?;
                summary.PrecioAcordado = row["PrecioAcordado"] as double?;
                summary.CuilCuitTitular = row["CuilCuitTitular"] as string;
                summary.FechaFirmaContrato = row["FechaFirmaContrato"] as DateTime?;
                summary.MontoPagado = (double)row["MontoPagado"];
                summary.CodigoPrecarga08 = row["CodigoPrecarga08"] as string;
                summary.FechaCitaTransferencia = row["FechaCitaTransferencia"] as DateTime?;
                summary.FechaCierre = row["FechaCierre"] as DateTime?;
                summary.ComprobanteCompra_Numero = row["ComprobanteCompra_Numero"] as int?;
                summary.ComprobanteCompra_Fecha = row["ComprobanteCompra_Fecha"] as DateTime?;
                return summary;
            }
        }

        // Vehiculo y adquisicion se graban en una sola transaccion y despues se rehacen sus DVV.
        public SQLUpdateResult FinishAcquisition(Vehiculo vehicle, Compra acquisition)
        {
            return DigitVerifierWriteMapper.Save(digitVerifier => data.Finish(VehicleRow(vehicle), AcquisitionRow(acquisition),
                digitVerifier.Dvh("Vehiculo"), digitVerifier.Dvh("Compra")), "Vehiculo", "Compra");
        }

        // Fila completa de la adquisicion, con el nombre de columna como clave. La usan tambien las transacciones
        // de la inspeccion y del pago, que cambian el estado de la compra junto con su propia tabla.
        internal static IDictionary<string, object> AcquisitionRow(Compra acquisition)
        {
            return new Dictionary<string, object>
            {
                { "Compra_Id", acquisition.Compra_Id },
                { "Numero", acquisition.Numero },
                { "Persona_Id", acquisition.Persona_Id },
                { "VehiculoProspecto_Id", acquisition.VehiculoProspecto_Id },
                { "Vendedor_Id", acquisition.Vendedor_Id },
                { "Estado", (int)acquisition.Estado },
                { "MotivoRechazo", (int?)acquisition.MotivoRechazo },
                { "Fecha", acquisition.Fecha },
                { "TasacionAceptada_Id", acquisition.TasacionAceptada_Id },
                { "PrecioAcordado", acquisition.PrecioAcordado },
                { "CuilCuitTitular", acquisition.CuilCuitTitular },
                { "FechaFirmaContrato", acquisition.FechaFirmaContrato },
                { "CodigoPrecarga08", acquisition.CodigoPrecarga08 },
                { "FechaCitaTransferencia", acquisition.FechaCitaTransferencia },
                { "Vehiculo_Id", acquisition.Vehiculo_Id },
                { "FechaCierre", acquisition.FechaCierre }
            };
        }

        private static IDictionary<string, object> VehicleRow(Vehiculo vehicle)
        {
            return new Dictionary<string, object>
            {
                { "Vehiculo_Id", vehicle.Vehiculo_Id },
                { "Patente", vehicle.Patente },
                { "Marca_Id", vehicle.Marca_Id },
                { "Modelo_Id", vehicle.Modelo_Id },
                { "Version_Id", vehicle.Version_Id },
                { "Color_Id", vehicle.Color_Id },
                { "Precio", vehicle.Precio },
                { "Opcionales", vehicle.Opcionales },
                { "Observaciones", vehicle.Observaciones },
                { "Puertas", vehicle.Puertas },
                { "Anio", vehicle.Anio },
                { "Kilometraje", vehicle.Kilometraje },
                { "LinkImagen", vehicle.LinkImagen }
            };
        }
    }
}
