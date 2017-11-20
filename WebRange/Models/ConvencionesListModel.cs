using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.WebRange.Models {
    public class ConvencionesListModel {

        public EntitiesLayer.Convenciones ConvencionesCurrent { get; set; }

        public ConvencionesListModel() {
            this.ConvencionesCurrent = BusinessLayer.Convenciones.GetAll();
        }


    }//Class Finish
}//Namespace Finish