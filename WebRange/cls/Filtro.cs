using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.WebRange.cls {

    public static class FiltrosConvenciones {

        public static List<Filtro> GetFiltros() {
            List<Filtro> res;
            string path = HttpContext.Current.Server.MapPath("~/App_Data/filtros.json");
            String sJson = System.IO.File.ReadAllText(path);
            res = Newtonsoft.Json.JsonConvert.DeserializeObject<List<cls.Filtro>>(sJson);
            return res;
        }

    }//Class Finish


    public class Filtro {
        public string Name { get; set; }
        public bool IsBasic { get; set; }
        public List<FieldFilter> Fields { get; set; }
    }//Class Finish

    public class FieldFilter {
        public string FieldkeyJDE { get; set; }
        public string Name { get; set; }
        public string KeyIdioma { get; set; }
    }//Class Finish





}//Namespace Finish