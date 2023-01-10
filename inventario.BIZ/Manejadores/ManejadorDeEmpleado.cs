using System;
using iNVENTARIO.COMMON.entidades;
using iNVENTARIO.COMMON.interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using MongoDB.Bson;

namespace inventario.BIZ.Manejadores
{
    public class ManejadorDeEmpleado : IManejadorDeEmpleados
    {
        IRepocitorio<Empleado> repocitorio;

        public ManejadorDeEmpleado(IRepocitorio<Empleado> repocitorio)
        {
            this.repocitorio = repocitorio;
        }
        public List<Empleado> Listar => repocitorio.Read.OrderBy(e=>e.Nombre).ToList() ;

        public bool Agregar(Empleado entidad)
        {
            return repocitorio.Create(entidad);
        }

       

        public Empleado BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
          return  repocitorio.Delete(id);
        }

        public List<Empleado> EmpleadoPorArea(string area)
        {
            return Listar.Where(e => e.Area == area).ToList();
        }

        public bool Modificar(Empleado entidad)
        {
            return repocitorio.Update(entidad);
        }
    }
}
