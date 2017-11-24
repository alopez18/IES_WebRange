using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.BusinessLayer {
    public class Folleto {



    }//Class Finish

    public class Folletos {
        public static EntitiesLayer.Folletos Get(String IdConvencion) {
            return DataLayer.Folletos.Get(IdConvencion);
        }
    }
}//Namespace Finish
