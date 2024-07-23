using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HCBPruebaInversiones.AccesoDatos.Repositorio;
using HCBPruebaInversiones.Negocio.Inversiones;
using Microsoft.Extensions.DependencyInjection;

namespace HCBPruebaInversiones.AccesoDatos.Extenciones
{
    public static class AgegarDependencias
    {
        public static IServiceCollection AgregarDependenciasDeDatos(this IServiceCollection servcios)
        {
            servcios.AddTransient(typeof(IReporitorioDeInnversiones), typeof(ReporitorioDeInversiones));

            return servcios;
        }
    }
}
