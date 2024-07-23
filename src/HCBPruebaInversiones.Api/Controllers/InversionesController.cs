using HCBPruebaInversiones.EntidadesODB.Entidades;
using HCBPruebaInversiones.Negocio.Inversiones;
using Microsoft.AspNetCore.Mvc;

namespace HCBPruebaInversiones.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InversionesController : ControllerBase
    {
        private readonly IReporitorioDeInnversiones _repositorio;

        private readonly ILogger<WeatherForecastController> _logger;

        public InversionesController(ILogger<WeatherForecastController> logger, IReporitorioDeInnversiones repositorio)
        {
            _logger = logger;
            _repositorio = repositorio;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Inversion>> Get()
        {
            var inversiones = _repositorio.ListarInversiones();
            _logger.LogInformation("inversiones{inversiones}", inversiones);
            return Ok(inversiones);
        }
    }
}
