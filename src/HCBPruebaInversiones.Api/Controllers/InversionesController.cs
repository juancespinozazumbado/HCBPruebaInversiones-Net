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
        public ActionResult<IEnumerable<ListarInversionesResponse>> Get()
        {
            try
            {
                var inversiones = _repositorio.ListarInversiones();
               
                return Ok(inversiones.Select( inversion => new ListarInversionesResponse
                {
                    IdInversion = inversion.ID_INVERSION, 
                    MontoInversion = (int)inversion.MONTO_INVERSION,
                    TasaInteres = inversion.TAS_INT_ANUAL,
                    PlazoMeses = inversion.PLAZO_MESES, 
                    CuponesAnuales = inversion.CUPONES_POR_AÑO

                }));

            }
            catch (Exception ex)
            {
                _logger.LogError("Se produjo una excepcion no controlada : {ex}", ex);
                return BadRequest(ex.Message);  
            }
               
        }

        [HttpGet("{id}/detalles")]
        public ActionResult<IEnumerable<ListarDetallesResponse>> ListarDetalles(int id)
        {
            try
            {
                var detalles = _repositorio.ListarDetalles( new Inversion { ID_INVERSION = id});

                return Ok(detalles.Select(detalle => new ListarDetallesResponse
                {
                    Id = detalle.ID_DETALLE,
                    IdInversion = detalle.ID_INVERSION, 
                    Año = detalle.AÑO, 
                    Cupon = detalle.CUPON, 
                    Saldo = (int)detalle.SALDO_INVERSION,
                    InteresesGanados = detalle.INTERES_GANADO,
                    SaldoCapitalizado = (int )detalle.SALDO_CAPITALIZADO

                }));

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

                return Ok(detalles.Select( e => new EncabezadoResponse
                {
                    IdEncabezado = e.ID_ENCABEZADO, 
                    IdInversion = e.ID_INVERSION, 
                    InteresTotalc = e.TOTAL_INTERES, 
                    SaldoCapitalizado = e.SALDO_CAPITALIZADO
                }));

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



        [HttpPost("encabezados")]
        public ActionResult<string> IniciarEncabezados([FromBody] AgregarEncabezadosRequest encabezado)
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
