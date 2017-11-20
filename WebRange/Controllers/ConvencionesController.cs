using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALC.IES.WebRange.Controllers {
    public class ConvencionesController : Controller {
        // GET: Convencions
        public ActionResult Index() {
            return View();
        }

        
    }//Class Finish


    public class ConvencionController : Controller {
        public ActionResult Editar(String id) {
            Models.ConvencionEditarModel model = new Models.ConvencionEditarModel(id);
            return View(model);
        }

        public String Datos(String id, String filtroId) {
            Models.ConvencionDatosModel model = new Models.ConvencionDatosModel(id, filtroId);
            model.LoadData(1500);

            Newtonsoft.Json.JsonSerializerSettings sett = new Newtonsoft.Json.JsonSerializerSettings();
            sett.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            String res = Newtonsoft.Json.JsonConvert.SerializeObject(model, sett);
            Response.ContentType = "Application/Json";
            return res;            
        }


        

    }//Class Finish



    }//Namespace Finish