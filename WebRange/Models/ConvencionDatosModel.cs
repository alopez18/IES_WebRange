using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ALC.IES.WebRange.Models {
    public class ConvencionDatosModel {
        //public EntitiesLayer.Articulos Datos { get; set; }
        public List<List<Object>> Datos { get; set; }
        public String Id { get; set; }
        private List<cls.Filtro> Filtros { get; set; }
        public List<cls.Filtro> FiltrosSelected { get; set; }
        public List<String> Headers { get; set; }
        public List<cls.HOTColConfig> ColsConfig { get; set; }
        public List<String> MapeoCampos { get; set; }
        public int Total { get; set; }

        private List<String> Campos { get; set; }


        public ConvencionDatosModel(String id, String idFiltro) {
            this.Id = id;
            this.Filtros = cls.FiltrosConvenciones.GetFiltros();
            this.FiltrosSelected = new List<cls.Filtro>();
            cls.Filtro filtroBasico = this.Filtros.FirstOrDefault(m => m.IsBasic);
            this.Headers = new List<string>();
            this.Campos = new List<string>();
            this.ColsConfig = new List<cls.HOTColConfig>();
            this.MapeoCampos = new List<string>();
            this.FiltrosSelected.Add(filtroBasico);

            foreach (var field in filtroBasico.Fields) {
                this.Headers.Add(field.Name);
                this.ColsConfig.Add(field.HOT_ColConfig);
                this.MapeoCampos.Add(field.data);
                if (!String.IsNullOrWhiteSpace(field.FieldkeyJDE)) {
                    this.Campos.Add(field.FieldkeyJDE);
                }

            }

            if (!String.IsNullOrEmpty(idFiltro) && idFiltro != "BSC") {
                if (idFiltro == "ALL") {
                    foreach (var filtro in this.Filtros) {
                        this.FiltrosSelected.Add(filtro);
                        if (!filtro.IsBasic) {
                            foreach (var field in filtro.Fields) {
                                this.Headers.Add(field.Name);
                                this.ColsConfig.Add(field.HOT_ColConfig);
                                this.MapeoCampos.Add(field.data);
                                if (!String.IsNullOrWhiteSpace(field.FieldkeyJDE)) {
                                    this.Campos.Add(field.FieldkeyJDE);
                                }
                            }
                        }
                    }
                } else {
                    cls.Filtro filtroSeleccionado = this.Filtros.FirstOrDefault(m => m.Id.ToLower().Trim() == idFiltro.ToLower().Trim());
                    if (filtroSeleccionado != null) {
                        this.FiltrosSelected.Add(filtroSeleccionado);
                        foreach (var field in filtroSeleccionado.Fields) {
                            this.Headers.Add(field.Name);
                            this.ColsConfig.Add(field.HOT_ColConfig);
                            this.MapeoCampos.Add(field.data);
                            if (!String.IsNullOrWhiteSpace(field.FieldkeyJDE)) {
                                this.Campos.Add(field.FieldkeyJDE);
                            }
                        }
                    }
                }
            }



            //Controlamos los sourcesExternos            
            foreach (var config in this.ColsConfig) {
                if (!String.IsNullOrWhiteSpace(config.sourceOut)) {
                    switch (config.sourceOut.ToLower()) {
                        case "folleto":
                        case "folletos":
                            EntitiesLayer.Folletos fs = BusinessLayer.Folletos.Get(this.Id);
                            config.source = fs.Select(m => m.Descripcion).ToList();
                            config.sourceOut = null;
                            break;
                        default:
                            break;
                    }
                }

            }


        }


        public void LoadData(int? nivel, int? pageNumber, int? pageSize, List<DTO.DtoFiltrosCollectionItem> filtros) {
            List<KeyValuePair<String, String>> filtrosAux = new List<KeyValuePair<string, string>>();
            if (filtros != null && filtros.Count > 0) {//Transformamos a tipo standard para pasar a capa de negocio.
                foreach (var item in filtros) {
                    if (!String.IsNullOrWhiteSpace(item.campo) && !String.IsNullOrWhiteSpace(item.valor)) {
                        filtrosAux.Add(new KeyValuePair<string, string>(item.campo, item.valor));
                    }
                }
            }
            BusinessLayer.Articulos artsBS = BusinessLayer.Articulos.Get(this.Id, this.Campos, nivel, pageNumber, pageSize, filtrosAux);
            this.Datos = new List<List<object>>();// artsBS.ArticulosEntidad;
            this.Total = artsBS.ArticulosEntidad.Total;
            List<Object> articuloLista = null;
            foreach (var art in artsBS.ArticulosEntidad) {
                articuloLista = new List<object>();
                Type myType = typeof(EntitiesLayer.Articulo);


                foreach (var filtro in this.FiltrosSelected) {
                    foreach (var field in filtro.Fields) {
                        System.Reflection.PropertyInfo myPropInfo = myType.GetProperty(field.data);
                        articuloLista.Add(myPropInfo.GetValue(art));
                    }
                }
                this.Datos.Add(articuloLista);
            }




            //Creamos datos aleatorios para que se vea algo en los mismos.
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            //Datos = new List<List<Object>>();
            //List<Object> rowAux = null;
            //Random r = new Random(DateTime.Now.Millisecond);
            //for (int i = 0; i < count; i++) {
            //    rowAux = new List<Object>();
            //    foreach (var filtro in this.FiltrosSelected) {
            //        foreach (var field in filtro.Fields) {
            //            if (field.HOT_ColConfig != null) {
            //                switch (field.HOT_ColConfig.type) {
            //                    case "date":
            //                        DateTime start = new DateTime(1995, 1, 1);
            //                        int range = (DateTime.Today - start).Days;
            //                        DateTime dRes = start.AddDays(r.Next(range));
            //                        rowAux.Add(dRes.ToShortDateString());
            //                        break;
            //                    case "checkbox":
            //                        rowAux.Add((r.Next(1000, 99999999) % 2 == 0));
            //                        break;
            //                    case "numeric":
            //                        rowAux.Add(r.Next(1000, 99999999));
            //                        break;
            //                    default:
            //                        if (field.HOT_ColConfig.editor != null && field.HOT_ColConfig.editor == "chosen") {
            //                            rowAux.Add((r.Next(1000, 99999999) % 2 == 0) ? "N" : "S");
            //                        } else {
            //                            rowAux.Add(new string(Enumerable.Repeat(chars, r.Next(5, 15)).Select(s => s[r.Next(s.Length)]).ToArray()));
            //                        }

            //                        break;
            //                }
            //            } else {
            //                rowAux.Add(new string(Enumerable.Repeat(chars, r.Next(5, 15)).Select(s => s[r.Next(s.Length)]).ToArray()));
            //            }
            //        }
            //    }
            //    Datos.Add(rowAux);
            //}


        }

    }//Class Finish
}//Namespace Finish