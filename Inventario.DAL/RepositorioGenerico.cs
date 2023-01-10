using iNVENTARIO.COMMON.entidades;
using iNVENTARIO.COMMON.interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventario.DAL
{
    public class RepositorioGenerico<T> : IRepocitorio<T> where T : Base
    {
        private MongoClient client;
        private IMongoDatabase database;

        public RepositorioGenerico()
        {
            client = new MongoClient(new MongoUrl("mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&ssl=false"));
            database = client.GetDatabase("RudyInventario");
        }
        private IMongoCollection<T> Collection()
        {
            return database.GetCollection<T>(typeof(T).Name);
        }

        public List<T> Read => Collection().AsQueryable().ToList();

        public bool Create(T entidad)
        {
            try
            {
                Collection().InsertOne(entidad);
                return true;
            }
            catch(Exception)
            {
                return false;
            }
        }

        public bool Delete(ObjectId id)
        {
            try
            {
                return  Collection().DeleteOne(e => e.Id == id).DeletedCount == 1;
               
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(T entidadModificada)
        {
            try
            {
               return Collection().ReplaceOne(e=>e.Id == entidadModificada.Id,entidadModificada).ModifiedCount == 1;
                
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
