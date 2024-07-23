using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCBPruebaInversiones.EntidadesODB.Entidades
{
    public class DetalleInversion
    {
        public int Id { get; set; }
        public int Año { get; set; }
        public decimal SaldoInversion { get; set; }
        public decimal InteresesGanados { get; set; }
        public decimal SaldoCapitalizado { get; set; }

    }
}
