using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.WebRange.Models {
    public class ConvencionEditarModel {
        public List<List<int>> Datos { get; set; }
        public String Id { get; set; }

        public List<cls.Filtro> Filtros { get; set; }
        public Models.HeaderModel Header { get; set; }

        public BusinessLayer.Convencion ConvencionCurrent { get; set; }

        public ConvencionEditarModel(String id) {
            this.Id = id;
            this.ConvencionCurrent = BusinessLayer.Convencion.Get(id);


            this.Filtros = cls.FiltrosConvenciones.GetFiltros();
            String titulo = String.Format("Editar el catálogo de la convención <b>{0}</b>", this.Id);
            this.Header = new HeaderModel(titulo);
            this.Header.Filtros = this.Filtros;



        }




        //public String GetJSON() {
        //    String json = null;
        //    json = Newtonsoft.Json.JsonConvert.SerializeObject(this.Datos);
        //    return json;
        //}



    }//Class Finish
}//Namespace Finish