using System.Web.Mvc;
using System.Web.Routing;

namespace fashion_shop_group32
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
                 "ProductList",
                 "{controller}/{action}/{s1}/{s2}/{s3}/{s4}/{s5}/{s6}/{s7}",
                 new { controller = "Product", action = "ProductList", s1 = UrlParameter.Optional, s2 = UrlParameter.Optional, s3 = UrlParameter.Optional, s4 = UrlParameter.Optional, s5 = UrlParameter.Optional, s6 = UrlParameter.Optional, s7 = UrlParameter.Optional }
            );



        }
    }
}
