using System.Web.Http;

namespace fashion_shop_group32
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            //   config.Routes.MapHttpRoute(
            //    name: "SizeApi",
            //    routeTemplate: "api/{controller}/{action}",
            //    defaults: new { controller = "SizeController", action = RouteParameter.Optional }
            //);
        }
    }
}
