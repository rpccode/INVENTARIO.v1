﻿using iNVENTARIO.COMMON.entidades;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iNVENTARIO.COMMON.interfaces
{
   public interface IManejadorGenerico<T> where T:Base
    {
        bool Agregar(T entidad);
        List<T> Listar { get; }
        bool Eliminar(ObjectId id);
        bool Modificar(T entidad);
        T BuscarPorId(ObjectId id);
    }
}
