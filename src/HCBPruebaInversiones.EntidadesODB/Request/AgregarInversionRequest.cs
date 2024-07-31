using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        //[Required]
       //  [Range(0.0, Double.MaxValue, ErrorMessage = "Please enter a valid double value.")]
        [JsonPropertyName("PlazoMeses")]
        public int PlazoMeses { get; set; }

        [JsonPropertyName("CuponesAnuales")]
        public int CuponesAnuales { get; set; } 
    }
}
