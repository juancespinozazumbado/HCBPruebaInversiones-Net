using System.Threading.Tasks;
using HCBPruebaInversiones.EntidadesODB.Entidades;
using HCBPruebaInversiones.EntidadesODB.Request;

namespace HCBPruebaInversiones.Negocio.Inversiones
{

    public interface IReporitorioDeInnversiones
    {
        public IEnumerable<Inversion> ListarInversiones();
        public IEnumerable<Encabezado> ListarEncabezados(Inversion inversion);
        public IEnumerable<DetalleInversion> ListarDetalles(Inversion inversion);

        public int AgregarInversion(AgregarInversionRequest inversion);

        public int AgregarEncabezado(AgregarEncabezadosRequest inversion);
        public int CalcularCupones(Inversion inversion);
    }


}
