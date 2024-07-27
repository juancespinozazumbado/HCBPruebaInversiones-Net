using System;
using System.Collections.Generic;


namespace HCBPruebaInversiones.EntidadesODB.Entidades
{
    public class Inversion
    {
        public int ID_INVERSION { get; set; }
        public decimal MONTO_INVERSION { get; set; }
        public double TAS_INT_ANUAL { get; set; }
        public int PLAZO_MESES { get; set; }
        public int CUPONES_POR_AÑO {  get; set; }   
    }
}
