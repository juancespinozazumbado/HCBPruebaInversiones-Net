using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCBPruebaInversiones.EntidadesODB.Entidades
{
    public class Inversion
    {
        public int ID_INVERSION { get; set; }
        public decimal MONTO_INVERSION { get; set; }
        public double TASA_INTERES_ANUAL { get; set; }
        public int PLAZO_MESES { get; set; }

        public int CANT_CUPONES_ANUAL;
    }
}
