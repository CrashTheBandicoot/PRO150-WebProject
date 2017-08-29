using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PRO150
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Games",
                url: "availablegames",
                defaults: new { controller = "Home", action = "AvailableGames" }
            );
            routes.MapRoute(
               name: "NewGame",
               url: "newgame/{color}/{gameId}/",
               defaults: new { controller = "Home", action = "NewGame", gameId = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Move",
                url: "move/{gameId}/{playerId}/{move}",
                defaults: new { controller = "Home", action = "Move" }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
