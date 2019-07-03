using System;
using System.Net.Http;
using System.Threading.Tasks;
using Cotizaciones.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Cotizaciones.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CotizacionController : ControllerBase
    {
        private const string STATUSOK = "OK";
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptions<CotizacionConfig> _config;

        public CotizacionController(IHttpClientFactory httpClientFactory, IOptions<CotizacionConfig> config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        // GET: api/Cotizacion/dolar
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetById(string id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(string.Format(_config.Value.Url, "{0}", _config.Value.Target));
            var cotizacionContext = new CotizacionContext(httpClient);
            var result = await cotizacionContext.GetResult(id);
            if (result.Estado.Equals(STATUSOK))
            {
                return Ok(new { moneda = result.Resultado.MonedaOrigen, precio = result.Resultado.Precio });
            }
            return BadRequest();
        }
    }
}
