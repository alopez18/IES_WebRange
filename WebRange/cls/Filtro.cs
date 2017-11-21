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
        public HOTColConfig HOT_ColConfig { get; set; }
    }//Class Finish


    public class HOTColConfig {
        public string data { get; set; }
        public string type { get; set; }
        public string dateFormat { get; set; }
        public bool? correctFormat { get; set; }
        public bool? allowEmpty { get; set; }        
        public string defaultDate { get; set; }
        public DatePickerConfig datePickerConfig { get; set; }
        public int? width { get; set; }
        public string renderer { get; set; }
        public Boolean editor { get; set; }
        public ChosenOptions chosenOptions { get; set; }
        public string format { get; set; }
        public string language { get; set; }
        public List<String> source { get; set; }
    }


    public class DatePickerConfig {
        public int firstDay { get; set; }
        public int numberOfMonths { get; set; }
    }

    public class ChosenOption {
        public string id { get; set; }
        public string label { get; set; }
    }

    public class ChosenOptions {
        public List<ChosenOption> data { get; set; }
    }

}//Namespace Finish