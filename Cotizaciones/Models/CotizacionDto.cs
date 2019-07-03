using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cotizaciones.Models
{
    public class CotizacionDto
    {
        [JsonProperty("result")]
        public Resultado Resultado { get; set; }
        [JsonProperty("status")]
        public string Estado { get; set; }
    }

    public class Resultado
    {
        [JsonProperty("updated")]
        public DateTime Fecha { get; set; }
        [JsonProperty("source")]
        public string MonedaOrigen { get; set; }
        [JsonProperty("target")]
        public string MonedaDestino { get; set; }
        [JsonProperty("value")]
        private decimal Value { get; set; }

        public decimal Precio { get => decimal.Round(Value, 2); }
    }
}
