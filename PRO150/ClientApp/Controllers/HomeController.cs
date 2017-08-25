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
            var result = Redirect("localHost:24182/newgame/" + color + "/" + gameId + "/" + playerId);
            ViewBag.json = result;
            return RedirectToAction("PlayGame");
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
        public ActionResult JoinGame()
        {
            //THIS IS THE URL TO GO TO -> "localHost:24182/availablegames/"
            //RETURNS A LIST OF GAMES
            //NEED TO PASS IT IN TO THE VIEW TO DISPLAY
            List<SelectListItem> items = new List<SelectListItem>()
            {

            };
            ViewBag.Games = items;
            return View();
        }
        public ActionResult PlayGame()
        {
            return View();
        }
    }
}