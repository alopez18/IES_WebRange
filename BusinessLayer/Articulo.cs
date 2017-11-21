using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.BusinessLayer {
    public class Articulo {
        public EntitiesLayer.Articulo ArticuloEntidad { get; set; }

        public Articulo() {

        }

    }//Class Finish


    public class Articulos {
        public EntitiesLayer.Articulos ArticulosEntidad { get; set; }

        public Articulos() {

        }


        public static BusinessLayer.Articulos Get(String idConvencion, List<String> campos, int? pageNumber = null, int? pageSize = null) {
            BusinessLayer.Articulos res = new Articulos() {
                ArticulosEntidad = DataLayer.Articulos.Get(idConvencion, campos, pageNumber, pageSize)
            };
            return res;
        }

    }
}//Namespace Finish
