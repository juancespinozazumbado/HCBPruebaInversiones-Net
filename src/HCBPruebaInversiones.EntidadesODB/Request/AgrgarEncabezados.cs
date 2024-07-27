using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HCBPruebaInversiones.EntidadesODB.Request
{
    public class AgregarEncabezadosRequest
    {
        [JsonPropertyName("IdInversion")]
        public int IdInversion { get; set; }
    }
}
