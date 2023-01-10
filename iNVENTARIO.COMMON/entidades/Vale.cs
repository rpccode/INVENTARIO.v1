using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iNVENTARIO.COMMON.entidades
{
     public class Vale:Base
    {
      
        public DateTime FechaHoraSolicitud { get; set; }
        public DateTime FechaEntrega { get; set; }
        public DateTime? FechaEntregaReal { get; set; }

        public List<Material> MaterialesPrestado { get; set; }

        public Empleado Solicitante { get; set; }

        public Empleado EncargadoDeAlmacen { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} debe {2} articulos y los deberia de entregar el {3}", Solicitante.Nombre, Solicitante.Apellidos, MaterialesPrestado.Count, FechaEntrega.ToShortDateString());
        }
    }
}
