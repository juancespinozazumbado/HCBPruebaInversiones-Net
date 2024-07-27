using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HCBPruebaInversiones.EntidadesODB.Response
{
    public class EncabezadoResponse
    {

        [JsonPropertyName("IdEncabezado")]
        public int IdEncabezado { get; set; }

        [JsonPropertyName("IdInversion")]
        public int IdInversion { get; set; }


        [JsonPropertyName("InteresTotalc")]
        public decimal InteresTotalc { get; set; }


        [JsonPropertyName("SaldoCapitalizado")]
        public decimal SaldoCapitalizado { get; set; }
    }
}
