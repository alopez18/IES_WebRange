using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.DataLayer {
    public class Folleto {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DataLayer.Folleto));


        internal static EntitiesLayer.Folleto Parse(DataRow dr) {
            EntitiesLayer.Folleto res = new EntitiesLayer.Folleto();
            foreach (DataColumn col in dr.Table.Columns) {
                switch (col.ColumnName) {
                    case "VFZON":
                        res.IdConvencion = dr["VFZON"].ToString();
                        break;
                    case "VFCCD07DES":
                        res.Descripcion = dr["VFCCD07DES"].ToString();
                        break;
                    default:
                        _log.WarnFormat("Columna '{0}' no reconocida para parsear la entidad {1}", col.ColumnName, "EntitiesLayer.Convencion");
                        break;
                }
            }
            return res;
        }


    }//Class Finish
    public class Folletos {
        public static EntitiesLayer.Folletos Get(String IdConvencion) {
            EntitiesLayer.Folletos fRes = new EntitiesLayer.Folletos();
            String sSelect = String.Format("select VFZON, VFCCD07DES from F55V137C where VFZON = '{0}' ", IdConvencion);
            EntitiesLayer.Folleto fAux = null;
            DataSet ds = DataLayer.Consultas.Execute(sSelect);
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0) {
                foreach (DataRow r in ds.Tables[0].Rows) {
                    fAux = new EntitiesLayer.Folleto() {
                        IdConvencion = r["VFZON"].ToString(),
                        Descripcion = r["VFCCD07DES"].ToString()
                    };
                    fRes.Add(fAux);
                }
            }
            return fRes;
        }


    }//Class Finish
}//namespace Finish
