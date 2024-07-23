using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCBPruebaInversiones.EntidadesODB.Entidades;

namespace HCBPruebaInversiones.Negocio.Inversiones
{

    public interface IReporitorioDeInnversiones
    {
        IEnumerable<Inversion> ListarInversiones();

        int AgregarInversion(Inversion inversion);
    }
}
