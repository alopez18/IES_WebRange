﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALC.IES.WebRange.Controllers {
    public class ConvencionesController : Controller {
        // GET: Convencions
        public ActionResult Index() {
            Models.ConvencionesListModel model = new Models.ConvencionesListModel();
            return View(model);
        }


    }//Class Finish


    public class ConvencionController : Controller {
        public ActionResult Editar(String id) {
            Models.ConvencionEditarModel model = new Models.ConvencionEditarModel(id);
            return View(model);
        }

        public String Datos(String id, String filtroId, int? nivel, int? page, int? pageSize, List<DTO.DtoFiltrosCollectionItem> filtros) {
            if (!pageSize.HasValue) {
                String sPageSizeDef = System.Configuration.ConfigurationManager.AppSettings.Get("ARTICULOS_CONVENCIONES_PAGESIZE_DEF");
                if (!String.IsNullOrWhiteSpace(sPageSizeDef)) {
                    pageSize = int.Parse(sPageSizeDef);
                }
            } else {
                if (pageSize.Value < 0) {
                    pageSize = null;
                }
            }

            Models.ConvencionDatosModel model = new Models.ConvencionDatosModel(id, filtroId);
            model.LoadData(nivel, page, pageSize, filtros);

            Newtonsoft.Json.JsonSerializerSettings sett = new Newtonsoft.Json.JsonSerializerSettings();
            sett.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            sett.Converters = new List<Newtonsoft.Json.JsonConverter>() {
                new Newtonsoft.Json.Converters.StringEnumConverter() { CamelCaseText = false, AllowIntegerValues = false },
                new EntitiesLayer.ShortDateConverter()
            };

            String res = Newtonsoft.Json.JsonConvert.SerializeObject(model, sett);
            Response.ContentType = "Application/Json";
            return res;
        }




    }//Class Finish



}//Namespace Finish