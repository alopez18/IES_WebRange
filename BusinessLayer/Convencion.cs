using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.BusinessLayer {
    public class Convencion {



        public Convencion() {

        }

        public static EntitiesLayer.Convencion Get(string id) {
            return DataLayer.Convencion.Get(id);
        }
    }//Class Finish


    public class Convenciones {



        public static EntitiesLayer.Convenciones GetAll() {
            return DataLayer.Convenciones.GetAll();
        }








    }//Class Finish
}//Namespace Finish
