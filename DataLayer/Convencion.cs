using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace ALC.IES.WebRange.DataLayer {
    public class Convencion {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DataLayer.Convencion));

        public static EntitiesLayer.Convencion Get(String id) {
            String select = String.Format("SELECT DCZON, DCDSE, DCDEE from F55V126A where DCZON = '{0}'", id);
            EntitiesLayer.Convencion res = null;
            DataSet ds = Consultas.Execute(select);
            if (ds!=null&&ds.Tables!=null&&ds.Tables[0].Rows.Count>0) {
                res = Parse(ds.Tables[0].Rows[0]);
            }            
            return res;
        }


        internal static EntitiesLayer.Convencion Parse(DataRow dr) {
            EntitiesLayer.Convencion res = new EntitiesLayer.Convencion();
            foreach (DataColumn col in dr.Table.Columns) {
                switch (col.ColumnName) {
                    case "DCZON":
                        res.Id = dr["DCZON"].ToString();
                        break;
                    case "DCDSE":
                        res.Start = DataLayer.Utils.UnixTimeStampToDateTime(double.Parse(dr["DCDSE"].ToString()));
                        break;
                    case "DCDEE":
                        res.End = DataLayer.Utils.UnixTimeStampToDateTime(double.Parse(dr["DCDEE"].ToString()));
                        break;
                    default:
                        _log.WarnFormat("Columna '{0}' no reconocida para parsear la entidad {1}", col.ColumnName, "EntitiesLayer.Convencion");
                        break;
                }
            }
            return res;
        }


    }//Class Finish


    public class Convenciones {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DataLayer.Convenciones));

        public static EntitiesLayer.Convenciones GetAll() {
            EntitiesLayer.Convenciones res = new EntitiesLayer.Convenciones();
            EntitiesLayer.Convencion cAux = null;
            String sSelect = "SELECT DCZON, DCDSE, DCDEE from F55V126A ORDER BY DCDSE DESC";
            DataSet ds = DataLayer.Consultas.Execute(sSelect);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0) {
                foreach (DataRow r in ds.Tables[0].Rows) {
                    cAux = Convencion.Parse(r);
                    res.Add(cAux);
                }
            } else {
                String message = String.Format("No se ha devuelto ningún elemento, por lo que suponemos que ha fallado la consulta '{0}'", sSelect);
                _log.Error(message);
                throw new Exception(message);
            }
            return res;
        }


    }//Class Finish
}//Namespace Finish
