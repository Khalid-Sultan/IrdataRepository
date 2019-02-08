using System.Web;
using System.Web.Optimization;

namespace Irdata
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/siteScripts").Include(
                        "~/SiteFiles/plugins/jQuery/jquery.min.js",
                        "~/SiteFiles/plugins/bootstrap/bootstrap.min.js",
                        "~/SiteFiles/plugins/slick/slick.min.js",
                        "~/SiteFiles/plugins/aos/aos.js",
                        "~/SiteFiles/plugins/venobox/venobox.min.js",
                        "~/SiteFiles/plugins/mixitup/mixitup.min.js",
                        "~/SiteFiles/plugins/google-map/gmap.js",
                        "~/SiteFiles/js/script.js"
                )); 
            bundles.Add(new StyleBundle("~/Content/siteStyles").Include(  
                        "~/SiteFiles/plugins/bootstrap/bootstrap.min.css",
                        "~/SiteFiles/plugins/slick/slick.css",
                        "~/SiteFiles/plugins/themify-icons/themify-icons.css",
                        "~/SiteFiles/plugins/animate/animate.css",
                        "~/SiteFiles/plugins/aos/aos.css",
                        "~/SiteFiles/plugins/venobox/venobox.css",
                        "~/SiteFiles/css/style.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/canvasScripts").Include("~/SiteFiles/js/canvasjs.min.js"));

        }
    }
}
