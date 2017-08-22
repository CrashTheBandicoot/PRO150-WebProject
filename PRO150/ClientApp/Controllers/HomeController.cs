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
        public ActionResult NewGame(string color, int? gameId, int? playerId)
        {
            var result = Redirect("localHost:24182/newgame/"+color+"/"+gameId+"/"+playerId);
            ViewBag.json = result;
            return View();
        }
        public ActionResult MakeNewGame()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem{Text="Black", Value="0"},
                new SelectListItem{Text="Red", Value="1"}
            };
            ViewBag.ColorChoice = items;
            return View();
        }
        public ActionResult PlayGame()
        {
            return View();
        }

    }
}