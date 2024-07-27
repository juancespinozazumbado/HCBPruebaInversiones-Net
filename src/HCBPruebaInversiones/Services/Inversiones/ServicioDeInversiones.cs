using HCBPruebaInversiones.EntidadesODB.Entidades;
using HCBPruebaInversiones.EntidadesODB.Request;
using HCBPruebaInversiones.EntidadesODB.Response;
using HCBPruebaInversiones.Negocio.Servicios;
using System.Text;
using System.Text.Json;

namespace HCBPruebaInversiones.Services.Inversiones
{
    public class ServicioDeInversiones : IservicioDeInversiones
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private ILogger<ServicioDeInversiones> _logger; 
        private readonly IConfiguration _configuracion;

        public ServicioDeInversiones(IHttpClientFactory httpClientFactory, ILogger<ServicioDeInversiones> logger, IConfiguration configuracion)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _configuracion = configuracion; 
        }

        public async Task<bool> AgregarEncabezados(AgregarEncabezadosRequest request)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                var uri = _configuracion.GetSection("BaseURL")["Local"] ?? "";

                var respuesta = await cliente.GetAsync(uri);
                respuesta.EnsureSuccessStatusCode();
                var json = await respuesta.Content.ReadAsStringAsync();

                var datos = JsonSerializer.Deserialize<string>(json);
                return true;

            }catch (Exception ex)
            {
                _logger.LogError("Susecio un error al consulta el servicio : {ex}", ex.Message); 
                return false;

            }
        }


        public async Task<bool> AgregarInversion(AgregarInversionRequest request)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                var uri = _configuracion.GetSection("BaseURL")["Local"] ?? "";

                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var respuesta = await cliente.PostAsync(uri, content );
                respuesta.EnsureSuccessStatusCode();
                
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("Susecio un error al consulta el servicio : {ex}", ex.Message);
                return false;

            }
        }


        public async Task<bool> CalcularCupones(CalcularCuponesRequest request)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                var uri = _configuracion.GetSection("BaseURL")["Local"] ?? "";

                var json = JsonSerializer.Serialize(request);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var respuesta = await cliente.PostAsync($"{uri}/calculo-inversion", content);
                respuesta.EnsureSuccessStatusCode();
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError("Susecio un error al consulta el servicio : {ex}", ex.Message);
                return false;

            }
        }

        public async  Task<IEnumerable<ListarDetallesResponse>> ListarDetalles(int id)
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                var uri = _configuracion.GetSection("BaseURL")["Local"] ?? "";

                var respuesta = await cliente.GetAsync($"{uri}/{id}/detalles");
                respuesta.EnsureSuccessStatusCode();
                var json = await respuesta.Content.ReadAsStringAsync();

                var inveriosnes =  JsonSerializer.Deserialize<IEnumerable<ListarDetallesResponse>>(json);
                return inveriosnes;

            }
            catch (Exception ex)
            {
                _logger.LogError("Susecio un error al consulta el servicio : {ex}", ex.Message);
                return null;

            }
        }

        public async Task<Encabezado> ListarEncabezados(int id)
        {
            try
            {
                if(id == 0)
                {
                    return new Encabezado();
                }
                var cliente = _httpClientFactory.CreateClient();
                var uri = _configuracion.GetSection("BaseURL")["Local"] ?? "";
                
                var respuesta = await cliente.GetAsync($"{uri}/{id}/encabezados");
                respuesta.EnsureSuccessStatusCode();
                var json = await respuesta.Content.ReadAsStringAsync();

                var encabezados = JsonSerializer.Deserialize<IEnumerable<EncabezadoResponse>>(json);

                if(encabezados.Count() ==0)
                {
                    return new Encabezado { };
                }
                var encavezado = new Encabezado
                {
                    ID_ENCABEZADO = encabezados.First().IdEncabezado,
                    SALDO_CAPITALIZADO = encabezados.First().SaldoCapitalizado, 
                    TOTAL_INTERES = encabezados.First().InteresTotalc
                };
                return encavezado; 
                

            }
            catch (Exception ex)
            {
                _logger.LogError("Susecio un error al consulta el servicio : {ex}", ex.Message);
                return null;

            }
        }

        public async Task<IEnumerable<Inversion>> ListraInversiones()
        {
            try
            {
                var cliente = _httpClientFactory.CreateClient();
                var uri = _configuracion.GetSection("BaseURL")["Local"] ?? "";

                var respuesta = await cliente.GetAsync($"{uri}");
                respuesta.EnsureSuccessStatusCode();
                var json = await respuesta.Content.ReadAsStringAsync();

                var inversiones = JsonSerializer.Deserialize<IEnumerable<ListarInversionesResponse>>(json);
                return inversiones.Select(inversion => new Inversion
                {
                    ID_INVERSION = inversion.IdInversion, 
                    MONTO_INVERSION = inversion.MontoInversion, 
                    PLAZO_MESES = inversion.PlazoMeses, 
                    TAS_INT_ANUAL = inversion.TasaInteres,
                    CUPONES_POR_AÑO = inversion.CuponesAnuales
                });

            }
            catch (Exception ex)
            {
                _logger.LogError("Susecio un error al consulta el servicio : {ex}", ex.Message);
                return null;

            }
        }





        public async Task<List<InversioneConDetallesVM>> ObtenerInversionesConDetalles()
        {
            try
            {

                var inversiones = await ListraInversiones();
                var inversionesModelo = new List<InversioneConDetallesVM>();


                foreach (var inversion in inversiones)
                {
                    var encavezado = await ListarEncabezados(inversion.ID_INVERSION);
                    var detalles = await ListarDetalles(inversion.ID_INVERSION);
                    inversionesModelo.Add(new InversioneConDetallesVM
                    {
                        Inversion = inversion,
                        Detalles = detalles.ToList(),
                        Encabezado = new EncabezadoResponse
                        {
                            IdEncabezado = encavezado.ID_ENCABEZADO,
                            IdInversion = encavezado.ID_INVERSION,
                            SaldoCapitalizado = encavezado.SALDO_CAPITALIZADO,
                            InteresTotalc = encavezado.TOTAL_INTERES


                        }

                    });

                }

                return inversionesModelo;

            }
            catch(Exception ex)
            {
                return null;
            }

           

        }


    }
}
