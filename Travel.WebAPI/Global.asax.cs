using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Elmah;

namespace Travel.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMappings();
        }

        void ErrorLog_Filtering(object sender, ExceptionFilterEventArgs e)
        {
            //We dont want to log errors caused by Spam bots etc so try and filter them out here

            //Anthing with bot in the user agent
            if (HttpContext.Current.Request.UserAgent != null && HttpContext.Current.Request.UserAgent.Contains("bot"))
            {
                e.Dismiss();
            }

            //404
            var httpException = e.Exception as HttpException;
            if (httpException != null && httpException.GetHttpCode() == 404)
            {
                e.Dismiss();
            }

            //Bots trying to access files that dont exists
            if (e.Exception.Message.Contains("controller") &&
                e.Exception.Message.Contains("path") &&
                e.Exception.Message.Contains("not") &&
                e.Exception.Message.Contains("implement") &&
                e.Exception.Message.Contains("IController"))
            {
                e.Dismiss();
            }

            if (e.Exception.Message.Contains("potentially ") &&
                e.Exception.Message.Contains("dangerous ") &&
                e.Exception.Message.Contains("value ") &&
                e.Exception.Message.Contains("detected") &&
                e.Exception.Message.Contains("client "))
            {
                e.Dismiss();
            }
        }
    }
}
