using System;
using System.Collections.Generic;
using System.Linq;

namespace CarAgency.BE.Integrity
{
    public static class DVTables
    {
        private static readonly string[] tables = {
            "DocumentoCompra", "TipoDocumentoCompra", "PagoCompra", "FormaPagoCompra",
            "Compra", "Tasacion", "Persona", "Color", "BitacoraEventos", "ItemInspeccion", "Inspeccion",
            "PuntajeInspeccion", "Factura", "Marca", "Modelo", "Tramite", "Pago", "TipoPago",
            "Permiso_Permiso", "Permiso", "VehiculoProspecto", "ComprobanteCompra", "Cotizacion",
            "Reserva", "Usuario", "Vehiculo", "Version"
        };

        public static IEnumerable<string> All { get { return tables.ToArray(); } }

        public static void RequireProtected(string table)
        {
            if (!tables.Contains(table, StringComparer.Ordinal))
                throw new ArgumentException("Tabla fuera del alcance de digitos verificadores.", nameof(table));
        }
    }
}
