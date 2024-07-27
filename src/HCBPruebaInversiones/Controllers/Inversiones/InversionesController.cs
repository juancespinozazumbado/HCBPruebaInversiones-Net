using HCBPruebaInversiones.EntidadesODB.Entidades;
using HCBPruebaInversiones.EntidadesODB.Request;
using HCBPruebaInversiones.EntidadesODB.Response;
using HCBPruebaInversiones.Negocio.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HCBPruebaInversiones.Controllers.Inversiones
{
    public class InversionesController : Controller
    {

        private readonly IservicioDeInversiones _servicioDeInversiones;
        private ILogger<InversionesController> _logger;
     
        public InversionesController(IservicioDeInversiones servicioDeInversiones, ILogger<InversionesController> logger)
        {
            _servicioDeInversiones = servicioDeInversiones;
            _logger = logger;
        }



        // GET: InversionesController
        public async  Task<ActionResult> Index()
        {
            try
            {
                var inversiones = await _servicioDeInversiones.ObtenerInversionesConDetalles();

                
                return View(inversiones);

            }
            catch(Exception ex) {
            
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: InversionesController/agregar
        public ActionResult Agregar(int id)
        {
          
            return View(new AgregarInversionRequest());
        }

        // GET: InversionesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AgregarInversion([FromForm] AgregarInversionRequest request)
        {

            try
            {
               var resultado =  await _servicioDeInversiones.AgregarInversion(request);

                if (resultado)
                {
                    return RedirectToAction(nameof(Index));
                }
            

            }catch(Exception ex)
            {

            }
            return View(new AgregarInversionRequest());
        }

        // POST: InversionesController/Create
        [HttpGet]
     
        public ActionResult Agregar()
        {
            
                var modelo = new AgregarInversionRequest { };
                return View(modelo);
            
        }

        // GET: InversionesController/Edit/5
        public async Task<ActionResult> Inversion(int id)
        {
            var inversioes = await _servicioDeInversiones.ListraInversiones();
            var inversion = inversioes.First(inv => inv.ID_INVERSION == id);

            var modelo = new InversioneConDetallesVM
            {
                Inversion = inversion,
                Detalles = new (), 
                Encabezado = new  ()

            };
            return View(modelo);
        }



        [HttpPost]

        public async Task<ActionResult> Cargar ([FromForm] InversioneConDetallesVM request)
        {


            var modelos = await _servicioDeInversiones.ObtenerInversionesConDetalles();
            var modelo = modelos.First(i => i.Inversion.ID_INVERSION == request.Inversion.ID_INVERSION);

            if(modelo.Detalles.Count() == 0)
            {

                var isCorrecto = await _servicioDeInversiones.CalcularCupones(new CalcularCuponesRequest { IdInversion = request.Inversion.ID_INVERSION });
                if (!isCorrecto)
                {
                    return View("Inversion");
                }


                var encabezado = await _servicioDeInversiones.ListarEncabezados(request.Inversion.ID_INVERSION);
                var detalles = await _servicioDeInversiones.ListarDetalles(request.Inversion.ID_INVERSION);

                //formar el objecto

                if (encabezado == null)
                {
                    return View("Inversion");

                }

                var respuesta = new InversioneConDetallesVM
                {

                    Encabezado = new EncabezadoResponse
                    {

                        SaldoCapitalizado = encabezado.SALDO_CAPITALIZADO,
                        InteresTotalc = encabezado.TOTAL_INTERES
                    },
                    Inversion = request.Inversion,
                    Detalles = detalles.ToList()


                };

                respuesta.Detalles = detalles.ToList();





                return View("Inversion", respuesta);

            }
            else return View("Inversion", modelo);




        }


        [HttpPost]
        public async Task<JsonResult> CalcularCuotas([FromBody] CalcularCuponesRequest requet)
        {

            try
            {
                var inversiones = await _servicioDeInversiones.ObtenerInversionesConDetalles();
                var inversion = inversiones.First(i => i.Inversion.ID_INVERSION == requet.IdInversion);
                var isCorrecto= await _servicioDeInversiones.CalcularCupones(requet);

                if (isCorrecto)
                {
                    return Json(new { resultado = true });
                }else return Json(new { resultado = false });

            }catch(Exception ex)
            {
                return Json(new { requet = requet });

            }
            

        }


        [HttpGet]
        //[Route("/api/{id}/detalles")]
        public async Task<JsonResult> ObetenerDetalles(int id)
        {

            try
            {

                var encabezado = await _servicioDeInversiones.ListarEncabezados(id);
                var detalles = await _servicioDeInversiones.ListarDetalles(id);

                //formar el objecto

                if(encabezado != null)
                {
                    var respuesta = new InversioneConDetallesVM
                    {

                        Encabezado = new EncabezadoResponse
                        {

                            SaldoCapitalizado = encabezado.SALDO_CAPITALIZADO,
                            InteresTotalc = encabezado.TOTAL_INTERES
                        }

                    };

                    respuesta.Detalles = detalles.ToList();
                    return Json(respuesta);


                }


                return Json(new { });



            }
            catch (Exception ex)
            {
                return Json(new { requet = "bad request" });

            }


        }
    }
}
