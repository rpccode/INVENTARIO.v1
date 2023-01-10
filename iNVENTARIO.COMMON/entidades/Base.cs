using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iNVENTARIO.COMMON.entidades
{
   public abstract class Base
    {
        public ObjectId Id { get; set; }
    }
}
