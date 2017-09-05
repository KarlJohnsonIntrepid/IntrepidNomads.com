using System.Web;
using System.Web.Optimization;
using System.Reflection;


namespace Travel.WebAPI
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {

            //BundleTable.EnableOptimizations = true;

            string versionNumber = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        

            Styles.DefaultTagFormat = string.Format("<link href='{{0}}?v={0}' rel='stylesheet'/>", versionNumber);
            Scripts.DefaultTagFormat = string.Format("<script src='{{0}}?v={0}'></script>", versionNumber);

            //This Loads before angular
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.dotdotdot.min.js",
                        "~/Content/tinymce/tinymce.min.js",
                         "~/Scripts/toastr.js",
                         "~/Client/library.js"));

       
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/toastr.css",
                      "~/Content/fancybox/source/jquery.fancybox.css",
                       "~/Content/fancybox/source/helpers/jquery.fancybox-buttons.css",
                       "~/Content/fancybox/source/helpers/jquery.fancybox-thumbs.css"));

               bundles.Add(new StyleBundle("~/Content/slick/style").Include(
                     "~/Content/slick/slick.css",
                     "~/Content/slick/slick-theme.css"));

            bundles.Add(new ScriptBundle("~/bundles/paralax").Include(
                       "~/Scripts/paralax.js"));

            bundles.Add(new ScriptBundle("~/bundles/fancybox").Include(
                        "~/Content/fancybox/lib/jquery.mousewheel-3.0.6.pack.js",
                        "~/Content/fancybox/source/jquery.fancybox.js",
                        "~/Content/fancybox/source/helpers/jquery.fancybox-buttons.js",
                        "~/Content/fancybox/source/helpers/jquery.fancybox-media.js",
                        "~/Content/fancybox/source/helpers/jquery.fancybox-thumbs.js"));

            bundles.Add(new ScriptBundle("~/bundles/slick").Include(
                   "~/Content/slick/slick.min.js"));

            //Load Modules here before app start
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-route.js",
                        "~/Scripts/angular-sanitize.js",
                       // "~/Scripts/angular-resource.js",
                        "~/Scripts/angular-ui/ui-bootstrap.js",
                        "~/Scripts/angular-ui/ui-bootstrap-tpls.js",
                        "~/Client/Scripts/tinymce.min.js",
                        "~/Client/app-start.js",
                        "~/Client/constants.js,",
                        "~/Client/directives.js",
                        "~/Client/models.js").IncludeDirectory("~/Client/Services", "*.js", true));

             bundles.Add(new ScriptBundle("~/blog/controllers").Include(
                        "~/Client/Controllers/pageController.js")
                        .IncludeDirectory("~/Client/Controllers/blog", "*.js", true));

            bundles.Add(new ScriptBundle("~/bundles/admin")
                       .IncludeDirectory("~/Client/Controllers/admin", "*.js", true)
                       .Include("~/Client/scripts/geolocations.js"));

        }
    }
}
