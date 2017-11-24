using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ALC.IES.WebRange.Controllers {
    public class ArticuloController : Controller {

        public JsonResult Update(List<DTO.DtoUpdate> cambios) {
            DTO.DtoAjaxReturn res = new DTO.DtoAjaxReturn();

            Dictionary<String, List<BusinessLayer.ArticuloFieldSaver>> saverList = new Dictionary<string, List<BusinessLayer.ArticuloFieldSaver>>();


            foreach (var cambio in cambios) {
                if (saverList.ContainsKey(cambio.Id)) {
                    saverList[cambio.Id].Add(new BusinessLayer.ArticuloFieldSaver() {
                        Field = cambio.Campo,
                        Value = cambio.Valor
                    });

                } else {
                    saverList.Add(cambio.Id, new List<BusinessLayer.ArticuloFieldSaver>() {
                        new BusinessLayer.ArticuloFieldSaver() {
                            Field = cambio.Campo,
                            Value = cambio.Valor
                        }
                    });
                }

                //saverAux = new BusinessLayer.ArticuloSaver() {
                //    Id = cambio.Id,
                //    Fields = new List<BusinessLayer.ArticuloFieldSaver>()
                //};
                //foreach (var campo in cambio.Campos) {
                //    saverAux.Fields.Add(new BusinessLayer.ArticuloFieldSaver() {
                //        Field = campo.Campo,
                //        Value = campo.Valor
                //    });
                //}
            }

            bool? efectuado = BusinessLayer.ArticuloSaver.Save(saverList);
            res.Success = (efectuado.HasValue == false || efectuado.Value == true);
            res.ErrorTecnico = "El servicio ha devuelto false";
            return Json(res);
        }





    }//Class Finish
}//Namespace Finish