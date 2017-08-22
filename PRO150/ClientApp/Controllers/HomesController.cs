using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ClientApp.Controllers
{
    public class HomesController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult newGame(string color, int? gameId, int? playerId)
        {
            var result = Redirect("localHost:24182/newgame/"+color+"/"+gameId+"/"+playerId);
            ViewBag.json = result;
            return View();
        }
    }
}