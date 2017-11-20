using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ALC.IES.WebRange {
    public class MvcApplication : System.Web.HttpApplication {
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(typeof(MvcApplication));

        protected void Application_Start() {
            // Configuramos el log4net
            log4net.Config.XmlConfigurator.Configure();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }//Class Finish
}//Namespace Finish
