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
    public class RespEmpleados //: IRepocitorio<Empleado>
    {


        private readonly string DBname = @"C:\InventarioDB\\Inventario.db";
        public string tableName = "Empleado";
       

        public List<Empleado> Read
        {

            get
            {
                List<Empleado> Datos = new();
                using (var db = new LiteDatabase(DBname))
                {
                    Datos = db.GetCollection<Empleado>(tableName).FindAll().ToList();
                }

                return Datos;
            }
        }

        public bool Create(Empleado entidad)
        {
           // entidad.Id = Guid.NewGuid().ToString();
            try
            {
                using(var db =  new LiteDatabase(DBname))
                {
                    var coleccion = db.GetCollection<Empleado>(tableName);
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
               // int r;
                using (var db = new LiteDatabase(DBname))
                {
                    var coleccion = db.GetCollection<Empleado>(tableName);
                    
                   
                    
                    coleccion.Delete(id);


                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }



        }

        public bool Update(Empleado entidadModificada)
        {
            try
            {
                using (var db = new LiteDatabase(DBname))
                {
                    var coleccion = db.GetCollection<Empleado>(tableName);
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
