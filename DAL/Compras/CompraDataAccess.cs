using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using CarAgency.DAL.Integrity;
using CarAgency.DAL.Persistence;

namespace CarAgency.DAL.Compras
{
    // Compra de vehiculos a personas (PN2): vehiculo ofrecido, adquisicion y cierre. La inspeccion, la tasacion,
    // los documentos y los pagos tienen su propio acceso a datos.
    public sealed class CompraDataAccess : DataAccessBase
    {
        public DataTable GetNextNumber() { return Read("Compra_GetNextNumber"); }

        public DataTable GetDuplicates(string licensePlate, string chassisNumber, string engineNumber)
        {
            return Read("Compra_GetDuplicates",
                new SqlParameter("@Patente", licensePlate),
                new SqlParameter("@NumeroChasis", chassisNumber),
                new SqlParameter("@NumeroMotor", engineNumber));
        }

        public DataTable AddProspectVehicle(Guid id, string licensePlate, string chassisNumber, string engineNumber, Guid makeId, Guid modelId,
            Guid versionId, Guid colourId, int year, int doors, int declaredKilometers, DvhCalculator dvh)
        {
            return Read("VehiculoProspecto_Add", dvh,
                new SqlParameter("@VehiculoProspecto_Id", id),
                new SqlParameter("@Patente", licensePlate),
                new SqlParameter("@NumeroChasis", chassisNumber),
                new SqlParameter("@NumeroMotor", engineNumber),
                new SqlParameter("@Marca_Id", makeId),
                new SqlParameter("@Modelo_Id", modelId),
                new SqlParameter("@Version_Id", versionId),
                new SqlParameter("@Color_Id", colourId),
                new SqlParameter("@Anio", year),
                new SqlParameter("@Puertas", doors),
                new SqlParameter("@Kilometraje", declaredKilometers));
        }

        // Todas las columnas viajan aunque esten vacias: el DVH se calcula con la fila completa.
        public DataTable Add(Guid id, int number, Guid personId, Guid prospectVehicleId, Guid salesmanId, int status, int? rejectionReason,
            DateTime creationDate, Guid? acceptedAppraisalId, double? agreedPrice, string ownerCuilCuit, DateTime? contractSignedDate,
            string prechargeCode08, DateTime? transferAppointmentDate, Guid? vehicleId, DateTime? closingDate, DvhCalculator dvh)
        {
            return Read("Compra_Add", dvh,
                new SqlParameter("@Compra_Id", id),
                new SqlParameter("@Numero", number),
                new SqlParameter("@Persona_Id", personId),
                new SqlParameter("@VehiculoProspecto_Id", prospectVehicleId),
                new SqlParameter("@Vendedor_Id", salesmanId),
                new SqlParameter("@Estado", status),
                new SqlParameter("@MotivoRechazo", (object)rejectionReason ?? DBNull.Value),
                new SqlParameter("@Fecha", creationDate),
                new SqlParameter("@TasacionAceptada_Id", (object)acceptedAppraisalId ?? DBNull.Value),
                new SqlParameter("@PrecioAcordado", (object)agreedPrice ?? DBNull.Value),
                new SqlParameter("@CuilCuitTitular", (object)ownerCuilCuit ?? DBNull.Value),
                new SqlParameter("@FechaFirmaContrato", (object)contractSignedDate ?? DBNull.Value),
                new SqlParameter("@CodigoPrecarga08", (object)prechargeCode08 ?? DBNull.Value),
                new SqlParameter("@FechaCitaTransferencia", (object)transferAppointmentDate ?? DBNull.Value),
                new SqlParameter("@Vehiculo_Id", (object)vehicleId ?? DBNull.Value),
                new SqlParameter("@FechaCierre", (object)closingDate ?? DBNull.Value));
        }

        public DataTable GetById(Guid id) { return Read("Compra_GetById", new SqlParameter("@Compra_Id", id)); }

        // Igual que el alta: todas las columnas viajan para que el DVH se calcule con la fila completa.
        public DataTable Update(Guid id, int number, Guid personId, Guid prospectVehicleId, Guid salesmanId, int status, int? rejectionReason,
            DateTime creationDate, Guid? acceptedAppraisalId, double? agreedPrice, string ownerCuilCuit, DateTime? contractSignedDate,
            string prechargeCode08, DateTime? transferAppointmentDate, Guid? vehicleId, DateTime? closingDate, DvhCalculator dvh)
        {
            return Read("Compra_Update", dvh,
                new SqlParameter("@Compra_Id", id),
                new SqlParameter("@Numero", number),
                new SqlParameter("@Persona_Id", personId),
                new SqlParameter("@VehiculoProspecto_Id", prospectVehicleId),
                new SqlParameter("@Vendedor_Id", salesmanId),
                new SqlParameter("@Estado", status),
                new SqlParameter("@MotivoRechazo", (object)rejectionReason ?? DBNull.Value),
                new SqlParameter("@Fecha", creationDate),
                new SqlParameter("@TasacionAceptada_Id", (object)acceptedAppraisalId ?? DBNull.Value),
                new SqlParameter("@PrecioAcordado", (object)agreedPrice ?? DBNull.Value),
                new SqlParameter("@CuilCuitTitular", (object)ownerCuilCuit ?? DBNull.Value),
                new SqlParameter("@FechaFirmaContrato", (object)contractSignedDate ?? DBNull.Value),
                new SqlParameter("@CodigoPrecarga08", (object)prechargeCode08 ?? DBNull.Value),
                new SqlParameter("@FechaCitaTransferencia", (object)transferAppointmentDate ?? DBNull.Value),
                new SqlParameter("@Vehiculo_Id", (object)vehicleId ?? DBNull.Value),
                new SqlParameter("@FechaCierre", (object)closingDate ?? DBNull.Value));
        }

        public DataTable GetPendingManagement(string filter)
        {
            return Read("Compra_GetPendingManagement", new SqlParameter("@Filter", (object)filter ?? DBNull.Value));
        }

        public DataTable GetSummary(Guid id) { return Read("Compra_GetSummary", new SqlParameter("@Compra_Id", id)); }

        // Alta en inventario y cierre de la compra en un solo SP, dentro de una transaccion. Las filas llegan
        // completas para el DVH y al SP solo viaja lo que cambia de la adquisicion.
        public DataTable Finish(IDictionary<string, object> vehicle, IDictionary<string, object> acquisition, DvhCalculator vehicleDvh,
            DvhCalculator acquisitionDvh)
        {
            DvhResult vehicleRow = vehicleDvh(vehicle);
            DvhResult acquisitionRow = acquisitionDvh(acquisition);
            return Read("Compra_Finish",
                new SqlParameter("@Vehiculo_Id", Value(vehicleRow, "Vehiculo_Id")),
                new SqlParameter("@Patente", SqlDbType.VarChar, 255) { Value = Value(vehicleRow, "Patente") },
                new SqlParameter("@Marca_Id", Value(vehicleRow, "Marca_Id")),
                new SqlParameter("@Modelo_Id", Value(vehicleRow, "Modelo_Id")),
                new SqlParameter("@Version_Id", Value(vehicleRow, "Version_Id")),
                new SqlParameter("@Color_Id", Value(vehicleRow, "Color_Id")),
                new SqlParameter("@Precio", SqlDbType.Float) { Value = Value(vehicleRow, "Precio") },
                new SqlParameter("@Opcionales", SqlDbType.VarChar, 255) { Value = Value(vehicleRow, "Opcionales") },
                new SqlParameter("@Observaciones", SqlDbType.VarChar, -1) { Value = Value(vehicleRow, "Observaciones") },
                new SqlParameter("@Puertas", SqlDbType.Int) { Value = Value(vehicleRow, "Puertas") },
                new SqlParameter("@Anio", SqlDbType.Int) { Value = Value(vehicleRow, "Anio") },
                new SqlParameter("@Kilometraje", SqlDbType.Int) { Value = Value(vehicleRow, "Kilometraje") },
                new SqlParameter("@LinkImagen", SqlDbType.NVarChar, -1) { Value = Value(vehicleRow, "LinkImagen") },
                new SqlParameter("@DVH", SqlDbType.Char, 64) { Value = vehicleRow.Dvh },
                new SqlParameter("@Compra_Id", Value(acquisitionRow, "Compra_Id")),
                new SqlParameter("@Estado", SqlDbType.Int) { Value = Value(acquisitionRow, "Estado") },
                new SqlParameter("@FechaCierre", SqlDbType.DateTime) { Value = Value(acquisitionRow, "FechaCierre") },
                new SqlParameter("@Acquisition_DVH", SqlDbType.Char, 64) { Value = acquisitionRow.Dvh });
        }
    }
}
