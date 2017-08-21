using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientApp.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult newGame(string color, int? gameId, int? playerId)
        {

            //PRO150.Controllers.HomeController.NewGame(color, gameId, playerId);
            return View();
        }
    }
}