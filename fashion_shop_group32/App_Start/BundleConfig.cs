using System.Web;
using System.Web.Optimization;

namespace fashion_shop_group32
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*custom*/
            /*css bundles*/
            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/css/bootstrap.css",
                 "~/Content/css/jquery.smartmenus.bootstrap.css",
                 "~/Content/css/jquery.simpleLens.css",
                 "~/Content/css/slick.css",
                 "~/Content/css/nouislider.css",
                 "~/Content/css/theme-color/default-theme.css",
                 "~/Content/css/sequence-theme.modern-slide-in.css",
                 "~/Content/css/style.css"
                 ));

            /*js bundles*/
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/js/jquery.min.js",
                 "~/Scripts/js/bootstrap.min.js",
                "~/Scripts/js/jquery.smartmenus.js",
                "~/Scripts/js/jquery.smartmenus.bootstrap.js",
                "~/Scripts/js/sequence.js",
                "~/Scripts/js/sequence-theme.modern-slide-in.js",
                "~/Scripts/js/jquery.simpleGallery.js",
                "~/Scripts/js/jquery.simpleLens.js",
                "~/Scripts/js/slick.js",
                "~/Scripts/js/nouislider.js",
                "~/Scripts/js/custom.js"
                ));
        }
    }
}
