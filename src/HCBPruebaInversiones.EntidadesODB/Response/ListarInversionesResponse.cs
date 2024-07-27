using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCBPruebaInversiones.EntidadesODB.Response
{
    public  class ListarInversionesResponse
    {
        public int IdInversion { get; set; }
        public int MontoInversion { get; set; }
        public double TasaInteres { get; set; }
        public int PlazoMeses { get; set; }
        public int CuponesAnuales { get; set; }
    }
}
