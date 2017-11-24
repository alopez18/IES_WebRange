using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.WebRange.Models {
    public class HeaderModel {
        //Falta el objeto usuario que se enviará para establecer.
        public String Titulo { get; set; }

        public List<cls.Filtro> Filtros { get; set; }

        public HeaderModel(String titulo) {
            this.Titulo = titulo;
        }


    }//Class Finish
}//Namespace Finish