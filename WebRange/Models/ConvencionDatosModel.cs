using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.WebRange.Models {
    public class ConvencionDatosModel {
        public List<List<int>> Datos { get; set; }
        public String Id { get; set; }
        public List<cls.Filtro> Filtros { get; set; }
        public List<String> Headers { get; set; }
        public List<cls.HOTColConfig> ColsConfig { get; set; }


        public ConvencionDatosModel(String id, String nombreFiltro) {
            this.Filtros = cls.FiltrosConvenciones.GetFiltros();
            cls.Filtro filtroBasico = this.Filtros.FirstOrDefault(m => m.IsBasic);
            this.Headers = new List<string>();
            this.ColsConfig = new List<cls.HOTColConfig>();

            foreach (var field in filtroBasico.Fields) {
                this.Headers.Add(field.Name);
                this.ColsConfig.Add(field.HOT_ColConfig);
            }

            if (!String.IsNullOrEmpty(nombreFiltro)) {
                if (nombreFiltro == "ALL") {
                    foreach (var filtro in this.Filtros) {
                        if (!filtro.IsBasic) {
                            foreach (var field in filtro.Fields) {
                                this.Headers.Add(field.Name);
                                this.ColsConfig.Add(field.HOT_ColConfig);
                            }
                        }
                    }
                } else {
                    cls.Filtro filtroSeleccionado = this.Filtros.FirstOrDefault(m => m.Name.ToLower() == nombreFiltro.ToLower());
                    if (filtroSeleccionado != null) {
                        foreach (var field in filtroSeleccionado.Fields) {
                            this.Headers.Add(field.Name);
                            this.ColsConfig.Add(field.HOT_ColConfig);
                        }
                    }
                }
            }
        }


        public void LoadData(int count) {
            //Creamos datos aleatorios para que se vea algo en los mismos.
            Datos = new List<List<int>>();
            List<int> rowAux = null;
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < count; i++) {
                rowAux = new List<int>();
                for (int i2 = 0; i2 < this.Headers.Count; i2++) {
                    rowAux.Add(r.Next(1000, 99999999));
                }
                Datos.Add(rowAux);
            }


        }

    }//Class Finish
}//Namespace Finish