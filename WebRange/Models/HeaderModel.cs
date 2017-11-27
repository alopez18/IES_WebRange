using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.WebRange.Models {
    public class HeaderModel {
        //Falta el objeto usuario que se enviará para establecer.
        public String Titulo { get; set; }
        public Boolean FiltroNivel { get; set; }
        public List<cls.Filtro> Filtros { get; set; }
        public List<String> FiltroMarca { get; set; }


        public HeaderModel(String titulo, Boolean filtroNivel = false) {
            this.Titulo = titulo;
            this.FiltroNivel = filtroNivel;
        }


    }//Class Finish
}//Namespace Finish