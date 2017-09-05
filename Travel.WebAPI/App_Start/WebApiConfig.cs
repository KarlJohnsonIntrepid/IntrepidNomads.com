using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.OData.Builder;
using System.Web.Http.OData.Extensions;
using Travel.WebAPI.Models;
using Travel.WebAPI.Providers;


namespace Travel.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            //config.SuppressDefaultHostAuthentication();
            //config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            //config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Author>("Authors");
            builder.EntitySet<Blog>("Blogs");
            builder.EntitySet<Category>("Categories");
            builder.EntitySet<Country>("Countries");
            builder.EntitySet<vBlog>("vBlogs");
            builder.EntitySet<Continent>("Continents");
            builder.EntitySet<Location>("Locations");
            builder.EntitySet<UploadedImage>("UploadedImages");
            builder.EntitySet<vCategory>("vCategories");
            builder.EntitySet<vCountry>("vCountries");
            builder.EntitySet<vUploadedImage>("vUploadedImages");
            config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
        }

    }
}
