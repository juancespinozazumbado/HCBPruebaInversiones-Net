using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Oracle;
using HCBPruebaInversiones.AccesoDatos.Configuracion;
using HCBPruebaInversiones.EntidadesODB.Entidades;
using HCBPruebaInversiones.Negocio.Inversiones;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using Oracle.ManagedDataAccess.Client;

namespace HCBPruebaInversiones.AccesoDatos.Repositorio
{
    public class ReporitorioDeInversiones : IReporitorioDeInnversiones
    {
        private readonly IDbConnection _connecion;
        private readonly string _cadenaConeccion;
        private readonly IConfiguration _configuration;

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

                parametros.Add(name: "inversiones", dbType: OracleMappingType.RefCursor, direction: ParameterDirection.Output);

                var inversiones = _connecion.Query<Inversion>(sql: Constantes.SpListarInversiones, param: parametros, commandType: CommandType.StoredProcedure);
                return inversiones;
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        public int AgregarInversion(Inversion inversion)
        {
            try
            {
                return _connecion.Execute(sql: Constantes.SpAgrearInversion, param:
                new { monto = inversion.MONTO_INVERSION, tasa_interes = inversion.TASA_INTERES_ANUAL, plazo = inversion.PLAZO_MESES, cupones_anual = inversion.CANT_CUPONES_ANUAL }
                , commandType: CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                return 0;
            }


        }

    }
}
