using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.WebRange.DTO {
    public class DtoUpdate {
        public String Id { get; set; }
        public String Campo { get; set; }
        public String Valor { get; set; }


        public DtoUpdate(String id, String campo, String valor) {
            this.Id = id;
            this.Campo = campo;
            this.Valor = valor;
        }






    }//Class Finish
}//Namespace Finish