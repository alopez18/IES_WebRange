using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.EntitiesLayer {
    public class Folleto {
        public String IdConvencion { get; set; }
        public String Descripcion { get; set; }

        public Folleto() {

        }

    }//Class Finish

    public class Folletos : List<Folleto> {
        public Folletos() {

        }
    }//Class Finish
}//Namespace Finish
