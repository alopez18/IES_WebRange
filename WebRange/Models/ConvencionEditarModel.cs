using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.WebRange.Models {
    public class ConvencionEditarModel {
        public List<List<int>> Datos { get; set; }
        public String Id { get; set; }

        public Boolean MostrarFiltro { get; set; }

        public List<cls.Filtro> Filtros { get; set; }
        public Models.HeaderModel Header { get; set; }

        public String JsonHOT_Config { get; set; }
        public String JsonHOT_Headers { get; set; }
        public int PageSize { get; set; }


        //public BusinessLayer.Convencion ConvencionCurrent { get; set; }

        public ConvencionEditarModel(String id) {
            this.Id = id;
            //this.ConvencionCurrent = BusinessLayer.Convencion.Get(id);
            this.MostrarFiltro = false;
            String sPageSizeDef = System.Configuration.ConfigurationManager.AppSettings.Get("ARTICULOS_CONVENCIONES_PAGESIZE_DEF");
            if (!String.IsNullOrWhiteSpace(sPageSizeDef)) {
                this.PageSize = int.Parse(sPageSizeDef);
            }

            this.Filtros = cls.FiltrosConvenciones.GetFiltros();
            String titulo = String.Format("Editar el catálogo de la convención <b>{0}</b>", this.Id);
            this.Header = new HeaderModel(titulo, true);
            this.Header.Filtros = this.Filtros;
            this.Header.FiltroMarca = BusinessLayer.Articulos.GetDistinctField(this.Id, "LCSRP8");
            this.Header.MenuLeft = false;

            cls.Filtro f = this.Filtros.FirstOrDefault(m => m.IsBasic);
            if (f != null) {
                List<cls.HOTColConfig> configs = f.Fields.Select(m => m.HOT_ColConfig).ToList();
                List<String> headers = f.Fields.Select(m => m.Name).ToList();
                Newtonsoft.Json.JsonSerializerSettings sett = new Newtonsoft.Json.JsonSerializerSettings();
                sett.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                this.JsonHOT_Config = Newtonsoft.Json.JsonConvert.SerializeObject(configs, sett);
                this.JsonHOT_Headers = Newtonsoft.Json.JsonConvert.SerializeObject(headers, sett);
            }

        }




        //public String GetJSON() {
        //    String json = null;
        //    json = Newtonsoft.Json.JsonConvert.SerializeObject(this.Datos);
        //    return json;
        //}



    }//Class Finish
}//Namespace Finish