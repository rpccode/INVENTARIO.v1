using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iNVENTARIO.COMMON.entidades
{
     public class Empleado:Base
    {
        
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Area { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1}", Nombre, Apellidos);
        }
    }
}
