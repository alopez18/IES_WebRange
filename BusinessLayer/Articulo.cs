using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.BusinessLayer {
    public class Articulo {
        public EntitiesLayer.Articulo ArticuloEntidad { get; set; }

        public Articulo() {

        }

    }//Class Finish


    public class Articulos {
        public EntitiesLayer.Articulos ArticulosEntidad { get; set; }

        public Articulos() {

        }


        public static BusinessLayer.Articulos Get(String idConvencion, List<String> campos, int? nivel, int? pageNumber = null, int? pageSize = null) {
            BusinessLayer.Articulos res = new Articulos() {
                ArticulosEntidad = DataLayer.Articulos.Get(idConvencion, campos, nivel, pageNumber, pageSize)
            };
            return res;
        }


    }

    public class ArticuloFieldSaver {
        public String Field { get; set; }
        public Object Value { get; set; }

    }

    public class ArticuloSaver {
        public String Id { get; set; }
        public List<ArticuloFieldSaver> Fields { get; set; }

        public static void Save(Dictionary<String, List<ArticuloFieldSaver>> saverList) {
            EntitiesLayer.Articulo artBSAux;
            //BusinessLayer.Articulos arts = new BusinessLayer.Articulos() {
            //    ArticulosEntidad = new EntitiesLayer.Articulos()
            //};
            foreach (KeyValuePair<String, List<ArticuloFieldSaver>> svr in saverList) {
                artBSAux = new EntitiesLayer.Articulo();
                artBSAux.Id = svr.Key;
                foreach (ArticuloFieldSaver afs in svr.Value) {
                    /*
                 * Establecemos el valor de la propiedades especificadas
                 * con reflection
                 */
                    Type myType = typeof(EntitiesLayer.Articulo);
                    System.Reflection.PropertyInfo myPropInfo = myType.GetProperty(afs.Field);
                    if (myPropInfo.PropertyType.ToString().Contains("DateTime")) {
                        myPropInfo.SetValue(artBSAux, DateTime.Parse(afs.Value.ToString()), null);
                    } else if (myPropInfo.PropertyType.ToString().Contains("Int32")) {
                        myPropInfo.SetValue(artBSAux, int.Parse(afs.Value.ToString()), null);
                    } else if (myPropInfo.PropertyType.ToString().Contains("Boolean") || myPropInfo.PropertyType.ToString().Contains("bool")) {
                        myPropInfo.SetValue(artBSAux, Boolean.Parse(afs.Value.ToString()), null);
                    } else if (myPropInfo.PropertyType.ToString().Contains("TipoServicioItem")) {
                        myPropInfo.SetValue(artBSAux, Enum.Parse(typeof(EntitiesLayer.TipoServicioItem), afs.Value.ToString(), true), null);
                    } else if (myPropInfo.PropertyType.ToString().Contains("CompraSeguraItem")) {
                        myPropInfo.SetValue(artBSAux, Enum.Parse(typeof(EntitiesLayer.CompraSeguraItem), afs.Value.ToString(), true), null);
                    } else if (myPropInfo.PropertyType.ToString().Contains("BaseCalculoCargoItem")) {
                        myPropInfo.SetValue(artBSAux, Enum.Parse(typeof(EntitiesLayer.BaseCalculoCargoItem), afs.Value.ToString(), true), null);
                    } else {
                        myPropInfo.SetValue(artBSAux, afs.Value, null);
                    }
                }
                //arts.ArticulosEntidad.Add(artBSAux);
                List<String> lista = svr.Value.Select(m => m.Field).ToList();
                DataLayer.Articulo.Save(artBSAux, lista);
            }
        }
    }
}//Namespace Finish
