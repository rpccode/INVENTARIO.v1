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
    public class RepocitorioAreas // : IRepocitorio<Area>
    {
        private string DBNAME = @"C:\InventarioDB\\Inventario.db";
        public string TABLENAME = "Area";
        public List<Area> Read
        {

            get
            {
                List<Area> datos = new List<Area>();
                using (var db = new LiteDatabase(DBNAME))
                {
                    datos = db.GetCollection<Area>(TABLENAME).FindAll().ToList();
                }
                return datos;
            }

        }

        public bool Create(Area entidad)
        {
           // entidad.Id = Guid.NewGuid().ToString();

            try
            {
                using (var db = new LiteDatabase(DBNAME))
                {
                    var colleccion = db.GetCollection<Area>(TABLENAME);
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
                
                using (var db = new LiteDatabase(DBNAME))
                {
                    var colleccion = db.GetCollection<Area>(TABLENAME);
                    colleccion.Delete(id);

                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool Update(Area entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBNAME))
                {
                    var colleccion = db.GetCollection<Area>(TABLENAME);
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
