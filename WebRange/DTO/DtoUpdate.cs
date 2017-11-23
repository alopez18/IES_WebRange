using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.WebRange.DTO {
    public class DtoUpdate {
        public String Id { get; set; }
        public String Campo { get; set; }
        public Object Valor { get; set; }


        public DtoUpdate() {

        }

        public DtoUpdate(String id) {
            this.Id = id;
        }
    }//Class Finish

    //public class DtoUpdateField {
    //    public String Campo { get; set; }
    //    public Object Valor { get; set; }

    //    public DtoUpdateField(String campo, Object valor) {
    //        this.Campo = campo;
    //        this.Valor = valor;
    //    }
    //}

}//Namespace Finish