using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCBPruebaInversiones.EntidadesODB.Entidades
{
    public class Encabezado
    {
        public int ID_ENCABEZADO { get; set; }

        public int ID_INVERSION { get; set; }
        public decimal TOTAL_INTERES { get; set; }
        public double SALDO_CAPITALIZADO { get; set; }

    }
}
