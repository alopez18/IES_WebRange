using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.WebRange.Models {
    public class ConvencionDatosModel {
        public List<List<Object>> Datos { get; set; }
        public String Id { get; set; }
        public List<cls.Filtro> Filtros { get; set; }
        public List<cls.Filtro> FiltrosSelected { get; set; }
        public List<String> Headers { get; set; }
        public List<cls.HOTColConfig> ColsConfig { get; set; }


        public ConvencionDatosModel(String id, String nombreFiltro) {
            this.Filtros = cls.FiltrosConvenciones.GetFiltros();
            this.FiltrosSelected = new List<cls.Filtro>();
            cls.Filtro filtroBasico = this.Filtros.FirstOrDefault(m => m.IsBasic);
            this.Headers = new List<string>();
            this.ColsConfig = new List<cls.HOTColConfig>();
            this.FiltrosSelected.Add(filtroBasico);
            foreach (var field in filtroBasico.Fields) {
                this.Headers.Add(field.Name);
                this.ColsConfig.Add(field.HOT_ColConfig);
                
            }

            if (!String.IsNullOrEmpty(nombreFiltro)) {
                if (nombreFiltro == "ALL") {
                    foreach (var filtro in this.Filtros) {
                        this.FiltrosSelected.Add(filtro);
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
                        this.FiltrosSelected.Add(filtroSeleccionado);
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
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            Datos = new List<List<Object>>();
            List<Object> rowAux = null;
            Random r = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < count; i++) {
                rowAux = new List<Object>();
                foreach (var filtro in this.FiltrosSelected) {
                    foreach (var field in filtro.Fields) {
                        if (field.HOT_ColConfig != null) {
                            switch (field.HOT_ColConfig.type) {
                                case "date":
                                    DateTime start = new DateTime(1995, 1, 1);
                                    int range = (DateTime.Today - start).Days;
                                    DateTime dRes = start.AddDays(r.Next(range));
                                    rowAux.Add(dRes.ToShortDateString());
                                    break;
                                case "checkbox":
                                    rowAux.Add((r.Next(1000, 99999999) % 2 == 0));
                                    break;
                                case "numeric":
                                    rowAux.Add(r.Next(1000, 99999999));
                                    break;
                                default:
                                    if (field.HOT_ColConfig.editor != null && field.HOT_ColConfig.editor == "chosen") {
                                        rowAux.Add((r.Next(1000, 99999999) % 2 == 0) ? "N" : "S");
                                    } else {
                                        rowAux.Add(new string(Enumerable.Repeat(chars, r.Next(5, 15)).Select(s => s[r.Next(s.Length)]).ToArray()));
                                    }
                                    
                                    break;
                            }
                        } else {
                            rowAux.Add(new string(Enumerable.Repeat(chars, r.Next(5, 15)).Select(s => s[r.Next(s.Length)]).ToArray()));
                        }
                    }
                }
                Datos.Add(rowAux);
            }


        }

    }//Class Finish
}//Namespace Finish