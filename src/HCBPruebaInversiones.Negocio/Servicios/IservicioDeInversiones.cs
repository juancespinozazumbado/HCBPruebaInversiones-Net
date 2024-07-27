using HCBPruebaInversiones.EntidadesODB.Entidades;
using HCBPruebaInversiones.EntidadesODB.Request;
using HCBPruebaInversiones.EntidadesODB.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCBPruebaInversiones.Negocio.Servicios
{
    public interface IservicioDeInversiones
    {
        public  Task<IEnumerable<ListarDetallesResponse>> ListarDetalles(int id);
        public  Task<Encabezado> ListarEncabezados(int id);
        public Task<IEnumerable<Inversion>> ListraInversiones();
        public Task<List<InversioneConDetallesVM>> ObtenerInversionesConDetalles();

        public Task<bool> AgregarInversion(AgregarInversionRequest request);
        public Task<bool> AgregarEncabezados(AgregarEncabezadosRequest request);

        public Task<bool> CalcularCupones(CalcularCuponesRequest request);

    }
}
