using iNVENTARIO.COMMON.entidades;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iNVENTARIO.COMMON.interfaces
{
   public interface IManejadorDeVales:IManejadorGenerico<Vale>
    {
        List<Vale> valesPorLiquidar();
        List<Vale> valesEnIntervalos(DateTime inicio, DateTime final);
        IEnumerable BuscarNoEntregadosPorEmpleado(Empleado empleado);
    }
}
