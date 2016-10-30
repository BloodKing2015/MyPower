using MyPower.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MyPower
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_BeginRequest()
        {

        }

        protected void Application_EndRequest()
        {

        }

        protected void Session_Start()
        {
            //DBFactory df = new DBFactory();
            //Session[DBFactory.MyPowerConStrName] = df;
            //df.CreateMyPowerConStr();
        }

        protected void Session_End()
        {
            //DBFactory df = Session[DBFactory.MyPowerConStrName] as DBFactory;
            //if (df != null)
            //{
            //    df.CreateMyPowerConStr();
            //}
        }
    }
}
