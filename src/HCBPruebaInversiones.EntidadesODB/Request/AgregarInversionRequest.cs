using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HCBPruebaInversiones.EntidadesODB.Request
{
    public class AgregarInversionRequest
    {
        [JsonPropertyName("MontoInversion")]
        public int MontoInversion { get; set; }

        [JsonPropertyName("TasaInteres")]
        public double TasaInteres { get; set; }

        [JsonPropertyName("PlazoMeses")]
        public int PlazoMeses { get; set; }

        [JsonPropertyName("CuponesAnuales")]
        public int CuponesAnuales { get; set; } 
    }
}
