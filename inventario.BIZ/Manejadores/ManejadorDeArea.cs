using iNVENTARIO.COMMON.entidades;
using iNVENTARIO.COMMON.interfaces;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace inventario.BIZ.Manejadores
{
    public class ManejadorDeArea : IManejadorDeAreas
    {
        IRepocitorio<Area> repocitorio;
        public List<Area> Listar => repocitorio.Read;

        public ManejadorDeArea(IRepocitorio<Area> repocitorio)
        {
            this.repocitorio = repocitorio;
        }

        public bool Agregar(Area entidad)
        {
            return repocitorio.Create(entidad);
        }

        public Area BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repocitorio.Delete(id);
        }

        public bool Modificar(Area entidad)
        {
            return repocitorio.Update(entidad);
        }
    }
}
