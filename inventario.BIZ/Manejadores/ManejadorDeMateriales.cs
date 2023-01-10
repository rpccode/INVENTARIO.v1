using System;
using iNVENTARIO.COMMON.entidades;
using iNVENTARIO.COMMON.interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace inventario.BIZ.Manejadores
{
    public class ManejadorDeMateriales : IManejadorDeMateriales
    {
        IRepocitorio<Material> repocitorio;

        public ManejadorDeMateriales(IRepocitorio<Material> repocitorio)
        {
            this.repocitorio = repocitorio;
        }

        public List<Material> Listar => repocitorio.Read ;

        public bool Agregar(Material entidad)
        {
            return repocitorio.Create(entidad);
        }

        public Material BuscarPorId(ObjectId id)
        {
            return Listar.Where(e => e.Id == id).SingleOrDefault();
        }

        public bool Eliminar(ObjectId id)
        {
            return repocitorio.Delete(id);
        }

        public List<Material> materialsDeCategoria(string caregoria)
        {
            return Listar.Where(e => e.Categoria == caregoria).ToList();
        }

        public bool Modificar(Material entidad)
        {
            return repocitorio.Update(entidad);
        }
    }
}
