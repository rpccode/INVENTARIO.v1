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
    public class RepositorioDeVales //: IRepocitorio<Vale>
    {
        private string DBName = @"C:\InventarioDB\\Inventario.db";
        private string TableName = "Vale";

        public List<Vale> Read
        {
            get
            {
                List<Vale> datos = new List<Vale>();
                using (var db = new LiteDatabase(DBName))
                {
                    datos = db.GetCollection<Vale>(TableName).FindAll().ToList();
                }
                return datos;
            }
        }

        public bool Create(Vale entidad)
        {
           // entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Vale>(TableName);
                    coleccion.Insert(entidad);
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
                
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Vale>(TableName);
                    coleccion.Delete(id);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Vale entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBName))
                {
                    var coleccion = db.GetCollection<Vale>(TableName);
                    coleccion.Update(entidadModificada);
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
