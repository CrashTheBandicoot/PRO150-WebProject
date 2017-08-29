using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ClientApp.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ClientApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        string BaseUrl = "localHost:24182/";
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Game(string color, int? gameId)
        {
            //NEED TO CALL SERVER TO GET GAME ID TO PASS TO PlayGame
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync(string.Concat("localHost:24182/newgame/", color));
                if (res.IsSuccessStatusCode)
                {

                }
            }
            //Need to go to PlayGame with parameters
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
            List<SelectListItem> items = new List<SelectListItem>();
            //foreach ()
            //{
            //    items.Add(new SelectListItem { Text = gameId, Value = });
            //}
            ViewBag.Games = items;
            return View();
        }
        public ActionResult PlayGame(int gameId, int playerId)
        {
            return View();
        }
    }
}