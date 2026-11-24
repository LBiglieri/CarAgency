using System;
using CarAgency.BE;
using CarAgency.Mappers;
using BE;

namespace CarAgency.BLL
{
    // Regla comun de PN2: cada paso trabaja sobre una compra en un estado puntual. Es una hoja: la usan CompraBLL
    // y las BLL de la compra sin depender entre si.
    internal static class CompraStatusGuard
    {
        internal static Compra GetAcquisitionInStatus(CompraMapper mapper, Guid Acquisition_Id, CompraStatus status)
        {
            Compra acquisition = mapper.GetById(Acquisition_Id);
            if (acquisition == null || acquisition.Estado != status)
                throw new TranslatableException("AcquisitionNotInStage", "La adquisicion ya no esta en esta etapa. Vuelva a buscarla.");
            return acquisition;
        }
    }
}
