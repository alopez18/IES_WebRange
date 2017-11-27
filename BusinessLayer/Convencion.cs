using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.BusinessLayer {
    public class Convencion {
        EntitiesLayer.Convencion ConvencionEntidad { get; set; }


        public Convencion() {

        }

        public static BusinessLayer.Convencion Get(string id) {
            BusinessLayer.Convencion res = new Convencion() {
                ConvencionEntidad = DataLayer.Convencion.Get(id)
            };
            return res;
        }

        public BusinessLayer.Articulos GetArticulos(List<String> campos, int? nivel, int pageNumber, int pageSize, List<KeyValuePair<String, String>> filtros) {
            BusinessLayer.Articulos res = new Articulos() {
                ArticulosEntidad = DataLayer.Articulos.Get(this.ConvencionEntidad.Id, campos, nivel, pageNumber, pageSize, filtros)
            };
            return res;
        }
    }//Class Finish


    public class Convenciones {



        public static EntitiesLayer.Convenciones GetAll() {
            return DataLayer.Convenciones.GetAll();
        }








    }//Class Finish
}//Namespace Finish
