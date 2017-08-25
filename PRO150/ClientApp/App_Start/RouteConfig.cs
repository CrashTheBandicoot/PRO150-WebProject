﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ClientApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Play",
                url: "playgame",
                defaults: new { controller = "Home", action = "PlayGame" }
            );
            routes.MapRoute(
                name: "MakeTheNewGame",
                url: "makenewgame",
                defaults: new { controller = "Home", action = "MakeNewGame" }
            );
            routes.MapRoute(
                name: "Move",
                url: "move/{gameId}/{playerId}/{move}",
                defaults: new { controller = "Home", action = "Move" }
            );
            routes.MapRoute(
                name: "NewGame",
                url: "newgame/{color}/{gameId}/{playerId}",
                defaults: new { controller = "Home", action = "NewGame", color = "", gameId = UrlParameter.Optional, playerId = UrlParameter.Optional }
            );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
