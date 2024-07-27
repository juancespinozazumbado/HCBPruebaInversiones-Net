using HCBPruebaInversiones.EntidadesODB.Entidades;
using HCBPruebaInversiones.EntidadesODB.Request;
using HCBPruebaInversiones.EntidadesODB.Response;
using HCBPruebaInversiones.Negocio.Inversiones;
using Microsoft.AspNetCore.Mvc;

namespace HCBPruebaInversiones.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class InversionesController : ControllerBase
    {
        private readonly IReporitorioDeInnversiones _repositorio;

        private readonly ILogger<InversionesController> _logger;

        public InversionesController(ILogger<InversionesController> logger, IReporitorioDeInnversiones repositorio)
        {
            _logger = logger;
            _repositorio = repositorio;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Inversion>> Get()
        {
            try
            {
                var inversiones = _repositorio.ListarInversiones();
               
                return Ok(inversiones);

            }
            catch (Exception ex)
            {
                _logger.LogError("Se produjo una excepcion no controlada : {ex}", ex);
                return BadRequest(ex.Message);  
            }
               
        }

        [HttpGet("{id}/detalles")]
        public ActionResult<IEnumerable<DetalleInversion>> ListarDetalles(int id)
        {
            try
            {
                var detalles = _repositorio.ListarDetalles( new Inversion { ID_INVERSION = id});

                return Ok(detalles);

            }
            catch (Exception ex)
            {
                _logger.LogError("Se produjo una excepcion no controlada : {ex}", ex);
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}/encabezados")]
        public ActionResult<IEnumerable<EncabezadoResponse>> ListarEncabezados(int id)
        {
            try
            {
                var detalles = _repositorio.ListarEncabezados(new Inversion { ID_INVERSION = id });

                return Ok(detalles);

            }
            catch (Exception ex)
            {
                _logger.LogError("Se produjo una excepcion no controlada : {ex}", ex);
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public ActionResult Agregar([FromBody] AgregarInversionRequest inversion)
        {
            try
            {
                var resultado = _repositorio.AgregarInversion(inversion);
                //Inisializamos el encabezado en 0

             

                return Ok(new { Resultado = "Inversion agregada exitosamente" });

            }
            catch (Exception ex)
            {
                _logger.LogError("Se produjo una excepcion no controlada : {ex}", ex);
                return BadRequest(ex.Message);
            }

        }



        [HttpPost("/encabezados")]
        public ActionResult<IEnumerable<Encabezado>> IniciarEncabezados([FromBody] AgregarEncabezadosRequest encabezado)
        {
            try
            {
                var resultado = _repositorio.AgregarEncabezado(encabezado);
                //Inisializamos el encabezado en 0

                var url_back = "";
                return Ok(new { Resultado = $"Consulta los saldos en {url_back}" });

            }
            catch (Exception ex)
            {
                _logger.LogError("Se produjo una excepcion no controlada : {ex}", ex);
                return BadRequest(ex.Message);
            }

        }


        [HttpPost("calculo-inversion")]
        public ActionResult<IEnumerable<Encabezado>> CalcularCupones([FromBody] CalcularCuponesRequest request)
        {
            try
            {
                var resultado = _repositorio.CalcularCupones(new Inversion { ID_INVERSION = request.IdInversion});
                //Inisializamos el encabezado en 0

                var url_back = "";
                return Ok(new { Resultado = $"Consulta los saldos en {url_back}" });

            }
            catch (Exception ex)
            {
                _logger.LogError("Se produjo una excepcion no controlada : {ex}", ex);
                return BadRequest(ex.Message);
            }

        }

    }
}
