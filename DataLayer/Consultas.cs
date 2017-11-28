using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALC.IES.WebRange.DataLayer {
    public static class Consultas {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(DataLayer.Consultas));


        public static System.Data.DataSet Execute(String sSelect) {
            try {
                JDEdwardsService.Service1SoapClient client = new JDEdwardsService.Service1SoapClient("Service1Soap12");
                DateTime now = DateTime.Now;
                System.Data.DataSet ds = client.EjecutarConsulta(sSelect);
                TimeSpan ts = new TimeSpan(now.Ticks - DateTime.Now.Ticks);
                _log.DebugFormat("Consulta ejecutada: '{0}'. Tiempo:  {1:dd\\.hh\\:mm\\:ss\\:fff}", sSelect, ts);
                return ds;
            } catch (Exception ex) {
                _log.Error(String.Format("Ha fallado la consulta al web service '{0}'. la consulta es: '{1}' ", "EjecutarConsulta", sSelect), ex);
                throw;
            }
        }



        public static bool Update(String sSelect) {
            try {
                JDEdwardsService.Service1SoapClient client = new JDEdwardsService.Service1SoapClient("Service1Soap12");
                DateTime now = DateTime.Now;
                bool res = client.EjecutarActualizacion(sSelect);
                TimeSpan ts = new TimeSpan(now.Ticks - DateTime.Now.Ticks);
                _log.DebugFormat("Update ejecutado: '{0}'. Tiempo:  {1:dd\\.hh\\:mm\\:ss\\:fff}", sSelect, ts);
                return res;
            } catch (Exception ex) {
                _log.Error(String.Format("Ha fallado la consulta al web service '{0}'. la consulta es: '{1}' ", "EjecutarConsulta", sSelect), ex);
                throw;
            }
        }


    }//Class Finish
}//Namespace Finish
