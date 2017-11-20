using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.EntitiesLayer {
    public class Convencion {
        public String Id { get; set; }
        public String Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }       



        public Convencion() {

        }




    }//Class Finish

    public class Convenciones : List<Convencion> {


        public Convenciones() {

        }
    }
}//Namespace Finish
