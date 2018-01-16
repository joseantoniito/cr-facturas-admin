using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp
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

            /*routes.MapRoute(
                name: "UserGenerarCodigoRuta",
                url: "{controller}/{action}/{str_rutaId}",
                defaults: new { controller = "User", action = "GenerarCodigoRuta", 
                    str_rutaId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "UserObtenerMovimientos",
                url: "{controller}/{action}/{str_initial_date}/{str_final_date}",
                defaults: new { controller = "User", action = "ObtenerMovimientos", 
                    str_initial_date = UrlParameter.Optional,
                    str_final_date = UrlParameter.Optional
                }
            );*/
        }
    }
}
