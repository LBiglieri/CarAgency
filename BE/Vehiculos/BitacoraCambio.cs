using System;

namespace CarAgency.BE
{
    // Valores de dbo.BitacoraCambios.Operacion.
    public enum BitacoraCambiosOperation { Created = 1, Updated = 2, Sold = 3 }

    // Fila de dbo.BitacoraCambios. La escriben los triggers de Vehiculo y Tramite; la aplicacion solo la lee.
    // Sin [TableColumnAttribute]: el MappingHandler no convierte int a enum ni NULL.
    public sealed class BitacoraCambio
    {
        public Guid BitacoraCambios_Id { get; set; }
        public Guid Vehiculo_Id { get; set; }
        public string Patente { get; set; }
        public DateTime Fecha { get; set; }
        public BitacoraCambiosOperation Operacion { get; set; }
        // Solo en las modificaciones: el campo del vehiculo que cambio y sus dos valores.
        public string Campo { get; set; }
        public string ValorAnterior { get; set; }
        public string ValorNuevo { get; set; }
        public Guid? Usuario_Id { get; set; }

        // Proyeccion del join con Usuario. Vacio si el cambio se hizo fuera de la aplicacion.
        public string Login { get; set; }
    }

    public sealed class BitacoraCambiosFilter
    {
        public Guid? Vehiculo_Id { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public BitacoraCambiosOperation? Operacion { get; set; }

        public static BitacoraCambiosFilter LastMonth()
        {
            return new BitacoraCambiosFilter { From = DateTime.Today.AddMonths(-1), To = DateTime.Today };
        }

        // Desde el ABM se consulta un vehiculo puntual: el periodo arranca un anio atras.
        public static BitacoraCambiosFilter ForVehicle(Guid vehicleId)
        {
            return new BitacoraCambiosFilter { Vehiculo_Id = vehicleId, From = DateTime.Today.AddYears(-1), To = DateTime.Today };
        }
    }
}
