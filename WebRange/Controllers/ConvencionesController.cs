using System;
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

        public String Datos(String id, String filtroId, int? nivel, int? page) {
            String sPageSize = System.Configuration.ConfigurationManager.AppSettings.Get("ARTICULOS_CONVENCIONES_PAGESIZE");
            int? pageSize = null;
            if (!String.IsNullOrWhiteSpace(sPageSize)) {
                pageSize = int.Parse(sPageSize);
            }


            Models.ConvencionDatosModel model = new Models.ConvencionDatosModel(id, filtroId);
            model.LoadData(nivel, page, pageSize);

            Newtonsoft.Json.JsonSerializerSettings sett = new Newtonsoft.Json.JsonSerializerSettings();
            sett.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            String res = Newtonsoft.Json.JsonConvert.SerializeObject(model, sett);
            Response.ContentType = "Application/Json";
            return res;
        }




    }//Class Finish



}//Namespace Finish