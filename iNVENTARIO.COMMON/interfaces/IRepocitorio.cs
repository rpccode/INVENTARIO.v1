using iNVENTARIO.COMMON.entidades;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iNVENTARIO.COMMON.interfaces
{
   public interface IRepocitorio<T> where T:Base 
    {
        bool Create(T entidad);
        List<T> Read { get; }
        bool Update(T entidadModificada);
        bool Delete(ObjectId id);
       
    }
}
