using System.Data;
using Dapper;
using Dapper.Oracle;
using HCBPruebaInversiones.AccesoDatos.Configuracion;
using HCBPruebaInversiones.EntidadesODB.Entidades;
using HCBPruebaInversiones.EntidadesODB.Request;
using HCBPruebaInversiones.Negocio.Inversiones;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using Oracle.ManagedDataAccess.Client;

namespace HCBPruebaInversiones.AccesoDatos.Repositorio
{
    public class ReporitorioDeInversiones : IReporitorioDeInnversiones
    {
        private readonly IDbConnection _connecion;
        private readonly string _cadenaConeccion;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ReporitorioDeInversiones> _logger;

        public ReporitorioDeInversiones(IConfiguration config)
        {
            _configuration = config;
            _cadenaConeccion = _configuration.GetConnectionString("BaseDatosOracle") ?? "";
            _connecion = new OracleConnection(_cadenaConeccion);
        }


        public IEnumerable<Inversion> ListarInversiones()
        {
            try
            {

                var parametros = new OracleDynamicParameters();

                parametros.Add(name: "Inversiones", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var inversiones = _connecion.Query<Inversion>(sql: Constantes.SpListarInversiones, param: parametros, commandType: CommandType.StoredProcedure);
                return inversiones;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public IEnumerable<Encabezado> ListarEncabezados(Inversion inversion)
        {
            try
            {

                var parametros = new OracleDynamicParameters();

                parametros.Add(name: "IdInversion", value: inversion.ID_INVERSION, dbType: OracleMappingType.Int64, direction: ParameterDirection.Input);
                parametros.Add(name: "Encabezados", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var inversiones = _connecion.Query<Encabezado>(sql: Constantes.SpListarEncabezados, param: parametros, commandType: CommandType.StoredProcedure);
                return inversiones;
            }
            catch (Exception ex)
            {
                return null;
            }

        }



        public IEnumerable<DetalleInversion> ListarDetalles(Inversion inversion)
        {
            try
            {

                var parametros = new OracleDynamicParameters();

                parametros.Add(name: "IdInversion", value: inversion.ID_INVERSION,  dbType: OracleMappingType.Int64, direction: ParameterDirection.Input);
                parametros.Add(name: "Detalles", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var inversiones = _connecion.Query<DetalleInversion>(sql: Constantes.SpListarDetalles, param: parametros, commandType: CommandType.StoredProcedure);
                return inversiones;
            }
            catch (Exception ex)
            {
                return null;
            }

        }



        public int AgregarInversion(AgregarInversionRequest inversion)
        {
            try
            {
                return _connecion.Execute(sql: Constantes.SpAgrearInversion, param:
                new { MontoInversion = inversion.MontoInversion, TasaInteres = inversion.TasaInteres, PlazoMeses = inversion.PlazoMeses, CuponesAnuales = inversion.CuponesAnuales }
                , commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                return 0;
            }


        }



        public int AgregarEncabezado(AgregarEncabezadosRequest inversion)
        {
            try
            {
                return _connecion.Execute(sql: Constantes.SpAgregaEncabezados, param:
                new { IdInversion = inversion.IdInversion, InteresTotal = 0, SaldoCapitalizado = 0 }
                , commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public int CalcularCupones(Inversion inversion)
        {
            try
            {

                var parametros = new OracleDynamicParameters();

                parametros.Add(name: "IdInversion", value: inversion.ID_INVERSION, dbType: OracleMappingType.Int64, direction: ParameterDirection.Input);
                parametros.Add(name: "MensageError", dbType: OracleMappingType.NVarchar2, direction: ParameterDirection.Output);


                return _connecion.Execute(sql: Constantes.SpLCalculaCupones, param: parametros, commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                return 0;
            }

        }




        public Inversion ObtenerUnaIversion(int id)
        {
            try
            {

                var parametros = new OracleDynamicParameters();
                parametros.Add(name: "IdInversion", dbType: OracleMappingType.Int64, direction: ParameterDirection.Input);

                parametros.Add(name: "Inversion", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var inversiones = _connecion.QuerySingle<Inversion>(sql: Constantes.SpObtieneUnaInversion, param: parametros, commandType: CommandType.StoredProcedure);
                return inversiones;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


    }
}
