using iNVENTARIO.COMMON.entidades;
using iNVENTARIO.COMMON.interfaces;
using MongoDB.Bson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventario.BIZ
{
    public class ManejadorDeVales : IManejadorDeVales
    {
        IRepocitorio<Vale> repocitorio;

        public ManejadorDeVales(IRepocitorio<Vale> repocitorio)
        {
            this.repocitorio = repocitorio;
        }


        public List<Vale> Listar =>  repocitorio.Read;

        public bool Agregar(Vale entidad)
        {
            return repocitorio.Create(entidad);
        }

        public IEnumerable BuscarNoEntregadosPorEmpleado(Empleado empleado)
        {
            return repocitorio.Read.Where(p => p.Solicitante.Id == empleado.Id && p.FechaEntregaReal == null).OrderBy(e=>e.FechaEntrega);
        }

        public Vale BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repocitorio.Delete(id);
        }

        public bool Modificar(Vale entidad)
        {
            return repocitorio.Update(entidad);
        }

        public List<Vale> valesEnIntervalos(DateTime inicio, DateTime final)
        {
            throw new NotImplementedException();
        }

        public List<Vale> valesPorLiquidar()
        {
            throw new NotImplementedException();
        }
    }
}
