using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Travel.WebAPI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
             name: "HomePage",
             url: "",
             defaults: new
             {
                 controller = "Home",
                 action = "Home",
                 id = UrlParameter.Optional
             }
         );



            routes.MapRoute(
              name: "AboutCatchAllRoute",
              url: "About/",
              defaults: new
              {
                  controller = "Home",
                  action = "Index",
                  id = UrlParameter.Optional
              }
          );


            routes.MapRoute(
              name: "ContactCatchAllRoute",
              url: "Contact/",
              defaults: new
              {
                  controller = "Home",
                  action = "Index",
                  id = UrlParameter.Optional
              }
          );

            routes.MapRoute(
                name: "BlogCatchAllRoute",
                url: "Blog/{*.}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
            name: "DestinationsCatchAllRoute",
            url: "Destinations/{*.}",
            defaults: new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            }
        );

            routes.MapRoute(
            name: "DiariesCatchAllRoute",
            url: "Diaries/{*.}",
            defaults: new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            }
        );


            routes.MapRoute(
            name: "DrinksCatchAllRoute",
            url: "around-the-world-in-80-drinks/{*.}",
            defaults: new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            }
        );

            routes.MapRoute(
            name: "FindUsCatchAllRoute",
            url: "findus/{*.}",
            defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
            name: "CountryCatchAllRoute",
                url: "Country/{*.}",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
                       name: "CategoryCatchAllRoute",
                       url: "Category/{*.}",
                       defaults: new
                       {
                           controller = "Home",
                           action = "Index",
                           id = UrlParameter.Optional
                       }
             );

            routes.MapRoute(
              name: "client",
              url: "Client/{*.}",
              defaults: new
              {
                  controller = "Home",
                  action = "Index",
                  id = UrlParameter.Optional
              }
       );

            routes.MapRoute(
                name: "Admin",
                url: "admin/{*.}",
                defaults: new
                {
                    controller = "Admin",
                    action = "Admin",
                    id = UrlParameter.Optional
                }
            );

            routes.MapRoute(
              name: "UploadImages/{blogID}",
              url: "UploadImages",
              defaults: new
              {
                  controller = "Admin",
                  action = "UploadImages",
                  blogID = UrlParameter.Optional
              }
          );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
