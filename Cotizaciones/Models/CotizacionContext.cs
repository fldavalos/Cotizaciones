using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cotizaciones.Models
{
    /// <summary>
    /// Interfaz para la implementacion de patron Strategy en las Cotizaciones
    /// </summary>
    public interface ICotizacion
    {
        Task<CotizacionDto> GetCotizacion();
    }

    /// <summary>
    /// Clase concreta donde se implementa la búsqueda de la cotizacion en moneda USD
    /// </summary>
    public class CotizacionDolar : ICotizacion
    {
        private const string DOLAR = "USD";

        private readonly string url;
        private readonly HttpClient httpClient;

        public CotizacionDolar(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.url = string.Format(httpClient.BaseAddress.ToString(), DOLAR);
        }

        public async Task<CotizacionDto> GetCotizacion()
        {
            var result = await this.httpClient.GetStringAsync(this.url).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<CotizacionDto>(result);
        }
    }

    /// <summary>
    /// Clase concreta donde se implementa la búsqueda de la cotizacion en moneda EUR
    /// </summary>
    public class CotizacionEuro : ICotizacion
    {
        private const string EURO = "EUR";

        private readonly string url;
        private readonly HttpClient httpClient;

        public CotizacionEuro(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.url = string.Format(httpClient.BaseAddress.ToString(), EURO);
        }

        public async Task<CotizacionDto> GetCotizacion()
        {
            var result = await this.httpClient.GetStringAsync(this.url).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<CotizacionDto>(result);
        }
    }

    /// <summary>
    /// Clase concreta donde se implementa la búsqueda de la cotizacion en moneda BRL
    /// </summary>
    public class CotizacionReal : ICotizacion
    {
        private const string REAL = "BRL";

        private readonly string url;
        private readonly HttpClient httpClient;

        public CotizacionReal(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.url = string.Format(httpClient.BaseAddress.ToString(), REAL);
        }

        public async Task<CotizacionDto> GetCotizacion()
        {
            var result = await this.httpClient.GetStringAsync(this.url).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<CotizacionDto>(result);
        }
    }

    /// <summary>
    /// Clase donde según el contexto se selecciona la Strategy
    /// </summary>
    public class CotizacionContext
    {
        private const string DOLAR = "dolar";
        private const string EURO = "euro";
        private const string REAL = "real";

        private readonly HttpClient httpClient;
        private ICotizacion cotizacion;

        public CotizacionContext(HttpClient _httpClient)
        {
            this.httpClient = _httpClient ?? throw new ArgumentNullException(nameof(_httpClient));
        }

        /// <summary>
        /// Metodo que selecciona la Strategy y obtiene la cotizacion
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CotizacionDto> GetResult(string id)
        {
            switch (id)
            {
                case DOLAR:
                    this.cotizacion = new CotizacionDolar(this.httpClient); break;
                case EURO:
                    this.cotizacion = new CotizacionEuro(this.httpClient); break;
                case REAL:
                    this.cotizacion = new CotizacionReal(this.httpClient); break;
                default:
                    throw new NotImplementedException("Moneda no implementada");
            }

            return await this.cotizacion?.GetCotizacion();
        }
    }
}
