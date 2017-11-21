using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALC.IES.WebRange.Controllers {
    public class ArticuloController : Controller {
        
        public JsonResult Update(List<DTO.DtoUpdate> cambios) {
            DTO.DtoAjaxReturn res = new DTO.DtoAjaxReturn();


            return Json(res);
        }





    }//Class Finish
}//Namespace Finish