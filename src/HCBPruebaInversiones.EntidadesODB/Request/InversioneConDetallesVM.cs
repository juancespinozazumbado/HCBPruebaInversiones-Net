using HCBPruebaInversiones.EntidadesODB.Entidades;
using HCBPruebaInversiones.EntidadesODB.Response;

namespace HCBPruebaInversiones.EntidadesODB.Request
{
    public class InversioneConDetallesVM
    {
        public Inversion Inversion { get; set; }
        public EncabezadoResponse? Encabezado { get; set; }
        public List<ListarDetallesResponse>? Detalles { get; set; }

    }
}
