using iNVENTARIO.COMMON.entidades;
using iNVENTARIO.COMMON.interfaces;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.DAL
{
    public class RepositorioDeMateriales //: IRepocitorio<Material>
    {
        private string DBNAME = @"C:\InventarioDB\\Inventario.db";
        public string TABLENAME = "Material";

        public List<Material> Read {

            get
            {
                List<Material> datos = new List<Material>();
                using(var db = new LiteDatabase(DBNAME))
                {
                    datos = db.GetCollection<Material>(TABLENAME).FindAll().ToList();
                }
                return datos;
            }
        
        }

        public bool Create(Material entidad)
        {
            //entidad.Id = Guid.NewGuid().ToString();

            try
            {
                using (var db = new LiteDatabase(DBNAME))
                {
                    var colleccion = db.GetCollection<Material>(TABLENAME);
                    colleccion.Insert(entidad);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
               // int r;
                using(var db = new LiteDatabase(DBNAME))
                {
                    var colleccion = db.GetCollection<Material>(TABLENAME);
                    colleccion.Delete(id);

                }
                return true;
            }
            catch (Exception)
            {
                return false;   
            }
            
        }

        public bool Update(Material entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBNAME))
                {
                    var colleccion = db.GetCollection<Material>(TABLENAME);
                    colleccion.Update(entidadModificada);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
