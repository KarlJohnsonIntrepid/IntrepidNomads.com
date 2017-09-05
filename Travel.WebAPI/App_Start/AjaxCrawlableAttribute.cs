using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Travel.WebAPI
{
    public class AjaxCrawlableAttribute : ActionFilterAttribute
    {
        private string[] FBAgentstrings = new string[] { "facebookexternalhit/", "Facebot" };
        private const string test = "snapshottest";
     
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpRequestBase request = filterContext.RequestContext.HttpContext.Request;
           
            foreach (string s in FBAgentstrings)
            {
                if (request.UserAgent.Contains(s))
                {
                    redirectToSnapShot(filterContext);
                    break;
                }
             }

            if (request.UserAgent.Contains("Google"))
            {
                //Google Plus
                redirectToSnapShot(filterContext);
            }

            if (request.UserAgent.Contains("Twitterbot"))
            {
                //Twitter Cads
                redirectToSnapShot(filterContext);
            }

            if (request.QueryString[test] != null)
            {
                redirectToSnapShot(filterContext);
            }
            return;
        }

        private void redirectToSnapShot(ActionExecutingContext filterContext)
        {  
            HttpRequestBase request = filterContext.RequestContext.HttpContext.Request;
            string paramURL = filterContext.RequestContext.HttpContext.Server.UrlEncode(request.Url.GetLeftPart(UriPartial.Path));
            string url = "/HtmlSnapshot/returnHTML/";

            if (request.CurrentExecutionFilePath.Equals(url)) {
                //Dont do anything this is the correct page,  else it will be in a loop
            } else
            {
                String FullUrl = "~" + url + "?url=" + paramURL;
                filterContext.RequestContext.HttpContext.Server.TransferRequest(FullUrl, true);
            }
        }

    }
}