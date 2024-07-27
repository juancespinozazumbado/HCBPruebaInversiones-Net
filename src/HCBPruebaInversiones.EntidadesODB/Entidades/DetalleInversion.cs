using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCBPruebaInversiones.EntidadesODB.Entidades
{
    public class DetalleInversion
    {
        public int ID_DETALLE { get; set; }
        public int ID_INVERSION { get; set; }
        public int AÑO { get; set; }
        public int CUPON { get; set; }
        public decimal SALDO_INVERSION { get; set; }
        public decimal INTERES_GANADO { get; set; }
        public decimal SALDO_CAPITALIZADO { get; set; }

    }
}
