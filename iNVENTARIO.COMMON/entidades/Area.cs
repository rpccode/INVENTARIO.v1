using iNVENTARIO.COMMON.entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iNVENTARIO.COMMON.entidades
{
   public class Area:Base
    {
        public string Nombre { get; set; }

        public override string ToString()
        {
            return string.Format("{0}", Nombre);
        }
    }
}
