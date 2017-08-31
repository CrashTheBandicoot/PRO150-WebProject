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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ClientApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        string BaseUrl = "http://localHost:24182/";
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Game(PlayerModel player)
        {
            //NEED TO CALL SERVER TO GET GAME ID TO PASS TO PlayGame
            int gameId = 0;
            int playerId = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync(string.Concat("http://localHost:24182/newgame/", player.Color));
                if (res.IsSuccessStatusCode)
                {
                    String content = await res.Content.ReadAsStringAsync();
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    JsonReader jsonReader = new JsonTextReader(new StringReader(content));
                    JObject jsonObject = (JObject)jsonSerializer.Deserialize(jsonReader);
                    String data = (String)jsonObject.GetValue("Data");
                    jsonReader = new JsonTextReader(new StringReader(data));
                    JObject dataObject = (JObject)jsonSerializer.Deserialize(jsonReader);
                    gameId = Int32.Parse((string)dataObject.GetValue("gameId"));
                    playerId = Int32.Parse((string)dataObject.GetValue("playerId"));
                }
            }
            GameObject game = new GameObject(gameId, playerId);
            //Need to go to PlayGame with parameters
            return RedirectToAction("PlayGame", game);
        }
        [HttpGet]
        public ActionResult MakeNewGame()
        {
            List<SelectListItem> items = new List<SelectListItem>()
            {
                new SelectListItem{Text="Black", Value="Black"},
                new SelectListItem{Text="Red", Value="Red"}
            };
            PlayerModel player = new PlayerModel();
            player.ColorChoice = items;
            return View(player);
        }
        [HttpPost]
        public ActionResult MakeNewGame(PlayerModel player)
        {
            return RedirectToAction("Game", player);
        }
        [HttpGet]
        public async Task<ActionResult> JoinGame()
        {
            //THIS IS THE URL TO GO TO -> "localHost:24182/availablegames/"
            //RETURNS A LIST OF GAMES
            //NEED TO PASS IT IN TO THE VIEW TO DISPLAY
            List<SelectListItem> items = new List<SelectListItem>();
            List<int> ids = new List<int>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync(string.Concat(BaseUrl, "availablegames"));
                if (res.IsSuccessStatusCode)
                {
                    String content = await res.Content.ReadAsStringAsync();
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    JsonReader jsonReader = new JsonTextReader(new StringReader(content));
                    JArray jsonArray = JArray.Parse(jsonSerializer.Deserialize(jsonReader).ToString());                 
                    for(int i = 0; i < jsonArray.Count; i++)
                    {
                        int id = Int32.Parse(jsonArray[i].ToString());
                        ids.Add(id);
                    }
                }
            }
            foreach (int gameId in ids)
            {
                items.Add(new SelectListItem { Text = "game: " + gameId, Value = gameId.ToString()});
            }
            GameIdModel model = new GameIdModel();
            model.GameChoices = items;
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> JoinGame(GameIdModel gameModel)
        {
            int gameId = 0;
            int playerId = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync(string.Concat("http://localHost:24182/newgame/null/", gameModel.gameId));
                if (res.IsSuccessStatusCode)
                {
                    String content = await res.Content.ReadAsStringAsync();
                    JsonSerializer jsonSerializer = new JsonSerializer();
                    JsonReader jsonReader = new JsonTextReader(new StringReader(content));
                    JObject jsonObject = (JObject)jsonSerializer.Deserialize(jsonReader);
                    String data = (String)jsonObject.GetValue("Data");
                    jsonReader = new JsonTextReader(new StringReader(data));
                    JObject dataObject = (JObject)jsonSerializer.Deserialize(jsonReader);
                    gameId = Int32.Parse((string)dataObject.GetValue("gameId"));
                    playerId = Int32.Parse((string)dataObject.GetValue("playerId"));
                }
            }
            GameObject game = new GameObject(gameId, playerId);
            return RedirectToAction("PlayGame", game);
        }
        // unsure yet [HttpGet]
        public async Task<ActionResult> PlayGame(GameObject game)
        {
            //call service to find turn
            string gamestate = "";
            int playerturnid = 0;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage res = await client.GetAsync(string.Concat("http://localhost:24182/gamestate/", game.gameId));
                if (res.IsSuccessStatusCode)
                {
                    string content = await res.Content.ReadAsStringAsync();
                    JsonSerializer jsonserializer = new JsonSerializer();
                    JsonReader jsonreader = new JsonTextReader(new StringReader(content));
                    JObject jsonobject = (JObject)jsonserializer.Deserialize(jsonreader);
                    string data = (String)jsonobject.GetValue("Data");
                    jsonreader = new JsonTextReader(new StringReader(data));
                    JObject dataobject = (JObject)jsonserializer.Deserialize(jsonreader);
                    gamestate = (String)dataobject.GetValue("gamestate");
                    playerturnid = Int32.Parse((string)dataobject.GetValue("playerturnid"));
                }
            }
            if (gamestate.Equals("inprogress"))
            {
                if (game.playerId == playerturnid)
                {
                    ViewBag.player = "your";
                }
                else
                {
                    ViewBag.player = "opponent's";
                }
            }
            else if (gamestate.Equals("waitingforsecond"))
            {
                ViewBag.parts = "waiting";
            }
            return View();
        }
    }
}