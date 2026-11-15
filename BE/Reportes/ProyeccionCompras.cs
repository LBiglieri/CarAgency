using System;
using System.Collections.Generic;

namespace CarAgency.BE
{
    // Resultado del reporte inteligente de PN2: cuantos vehiculos van a entrar al inventario por compras en los
    // proximos dias, cuanto valen y cuanto falta pagarles, con el embudo por etapa que explica el numero.
    // Lo arma ProyeccionComprasCalculator; la UI solo lo dibuja en el PDF.
    public sealed class ProyeccionCompras
    {
        // Se usaron los datos anteriores a FechaCorte. El horizonte es [FechaCorte, HorizonteHasta).
        public DateTime FechaCorte { get; set; }
        public DateTime HorizonteHasta { get; set; }

        // Vehiculos que se espera finalizar (entran al inventario) y su precio de compra.
        public double Unidades { get; set; }
        public double UnidadesMinimo { get; set; }
        public double UnidadesMaximo { get; set; }
        public double Monto { get; set; }
        public double MontoMinimo { get; set; }
        public double MontoMaximo { get; set; }
        // De ese monto, lo que todavia no se pago: la caja que hay que tener disponible.
        public double CajaPendiente { get; set; }

        // Las dos partes de la proyeccion: las compras en curso y las que todavia no se iniciaron.
        public double UnidadesEnCurso { get; set; }
        public double MontoEnCurso { get; set; }
        public double UnidadesFlujoNuevo { get; set; }
        public double MontoFlujoNuevo { get; set; }

        // De cada 100 compras resueltas en la ventana, cuantas terminaron en el inventario.
        public double TasaFinalizacion { get; set; }
        public int ComprasResueltas { get; set; }
        // Dias promedio desde el alta hasta el cierre de las compras finalizadas.
        public double DuracionPromedio { get; set; }

        // Flujo nuevo: compras iniciadas por periodo y que parte de una compra nueva llega a cerrarse dentro
        // del horizonte (depende de cuanto tarda una compra en recorrer todas las etapas).
        public double ComprasPorPeriodo { get; set; }
        public double FactorTiempoNuevas { get; set; }
        public double TicketPromedio { get; set; }

        public bool HistoriaSuficiente { get; set; }
        public double? ErrorValidacion { get; set; }

        // Hallazgos del embudo: la etapa donde las compras pasan mas tiempo y donde mas se pierden.
        public int? EtapaMasLenta { get; set; }
        public int? EtapaConMasPerdidas { get; set; }

        public List<CompraEnCurso> ComprasEnCurso { get; set; } = new List<CompraEnCurso>();
        public List<CompraMensual> Historial { get; set; } = new List<CompraMensual>();
        public List<EtapaEmbudoCompra> Etapas { get; set; } = new List<EtapaEmbudoCompra>();
        public List<PerdidaCompra> Perdidas { get; set; } = new List<PerdidaCompra>();
        public List<ValidacionProyeccion> Validaciones { get; set; } = new List<ValidacionProyeccion>();
    }

    // Una compra abierta al corte, con la probabilidad de cerrarla dentro del horizonte.
    public sealed class CompraEnCurso
    {
        public Guid Compra_Id { get; set; }
        public int Numero { get; set; }
        public string Vehiculo_Descripcion { get; set; }
        public string Persona_Descripcion { get; set; }
        public string Marca { get; set; }
        public CompraStatus Etapa { get; set; }
        public int DiasEnEtapa { get; set; }
        // Lleva en la etapa mas que cualquier compra finalizada de la ventana.
        public bool Demorada { get; set; }
        // Precio acordado; si todavia no hay acuerdo, la ultima tasacion o el ticket promedio.
        public double Precio { get; set; }
        public bool PrecioEstimado { get; set; }
        public double MontoPagado { get; set; }
        // P(finalizar | etapa) x P(cerrarse dentro del horizonte | ya lleva DiasEnEtapa).
        public double ProbabilidadFinalizar { get; set; }
        public double ProbabilidadEnHorizonte { get; set; }
        public double Probabilidad { get; set; }
        public double MontoEsperado { get; set; }
    }

    public sealed class CompraMensual
    {
        public int Anio { get; set; }
        public int Mes { get; set; }
        public int Unidades { get; set; }
        public double Monto { get; set; }
    }

    // Embudo de la ventana: de las compras resueltas que llegaron a la etapa, cuantas pasaron a la siguiente
    // y cuantos dias tardaron en promedio.
    public sealed class EtapaEmbudoCompra
    {
        public CompraStatus Etapa { get; set; }
        public int Llegaron { get; set; }
        public int Avanzaron { get; set; }
        public double TasaPaso { get; set; }
        public double? DiasPromedio { get; set; }
    }

    public enum MotivoPerdidaCompra
    {
        NoSePresento = 1,
        InspeccionNoApta = 2,
        TasacionRechazada = 3,
        ContratoNoFirmado = 4
    }

    public sealed class PerdidaCompra
    {
        public MotivoPerdidaCompra Motivo { get; set; }
        // Etapa en la que se pierde: la inspeccion para los dos primeros motivos, la tasada para los otros dos.
        public CompraStatus Etapa { get; set; }
        public int Cantidad { get; set; }
        public double Porcentaje { get; set; }
    }
}
