using System.Web;
using System.Web.Optimization;
using WebHelpers.Mvc5;

namespace WebApplication1
{
    public class BundleConfig
    {
        private static string RegisterTablesCss()
        {
            return "~/Content/js/plugins/datatables/css/dataTables.bootstrap.css";
        }

        private static string RegisterCategoriesScripts()
        {
            return "~/Scripts/Modules/Categories/Data.js";
        }

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
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            var initBundleStyle = new StyleBundle("~/Bundles/css")
                .Include("~/Content/css/select2.min.css")
                .Include("~/Content/css/bootstrap-datepicker3.min.css")
                .Include("~/Content/css/font-awesome.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/icheck/blue.min.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/AdminLTE.css", new CssRewriteUrlTransformAbsolute())
                .Include("~/Content/css/skins/skin-blue.css");

            var initBundleScript = new ScriptBundle("~/Bundles/js")
                .Include("~/Content/js/plugins/fastclick/fastclick.js")
                .Include("~/Content/js/plugins/slimscroll/jquery.slimscroll.js")
                .Include("~/Content/js/plugins/select2/select2.full.js")
                .Include("~/Content/js/plugins/moment/moment.js")
                .Include("~/Content/js/plugins/datepicker/bootstrap-datepicker.js")
                .Include("~/Content/js/plugins/icheck/icheck.js")
                .Include("~/Content/js/plugins/validator.js")
                .Include("~/Content/js/plugins/inputmask/jquery.inputmask.bundle.js")
                .Include("~/Content/js/app.js")
                .Include("~/Content/js/init.js")
                /* Tables plugins */
                .Include("~/Content/js/plugins/datatables/js/jquery.dataTables.min.js")
                .Include("~/Content/js/plugins/datatables/js/dataTables.bootstrap.min.js");

            var newBundle = new ScriptBundle("~/bundles/table").Include(RegisterCategoriesScripts());

            initBundleStyle.Include(RegisterTablesCss());

            bundles.Add(initBundleStyle);
            bundles.Add(initBundleScript);
            bundles.Add(newBundle);
        }
    }
}
