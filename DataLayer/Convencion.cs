using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ALC.IES.WebRange.DataLayer {
    public class Convencion {


        public static EntitiesLayer.Convencion Get(String Id) {
            String select = "select top 10 * from F55DS53;";
            EntitiesLayer.Convencion res = new EntitiesLayer.Convencion();
            DataSet ds = Consultas.Execute(select);

            return res;
        }



    }//Class Finish


    public class Convenciones {


        public Convenciones() {

        }

    }
}//Namespace Finish
