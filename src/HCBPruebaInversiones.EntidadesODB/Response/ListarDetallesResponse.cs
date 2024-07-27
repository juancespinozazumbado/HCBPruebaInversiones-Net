using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HCBPruebaInversiones.EntidadesODB.Response
{
    public class ListarDetallesResponse
    {
        [JsonPropertyName("Id")]
        public int Id { get; set; }


        [JsonPropertyName("IdInversion")]
        public int IdInversion { get; set; }
        [JsonPropertyName("Año")]
        public int Año { get; set; }
        [JsonPropertyName("TasaInteres")]
        public double Cupon { get; set; }
        [JsonPropertyName("Saldo")]
        public int Saldo { get; set; }

        [JsonPropertyName("InteresesGanados")]

        public decimal  InteresesGanados { get; set; }
    


    [JsonPropertyName("SaldoCapitalizado")]

        public int SaldoCapitalizado { get; set; }
    }
}
