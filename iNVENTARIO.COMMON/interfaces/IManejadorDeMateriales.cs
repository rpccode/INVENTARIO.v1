using iNVENTARIO.COMMON.entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iNVENTARIO.COMMON.interfaces
{
   public interface IManejadorDeMateriales:IManejadorGenerico<Material>    
    {
        List<Material> materialsDeCategoria(string caregoria);
    }
}
