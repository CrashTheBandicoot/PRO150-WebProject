using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PRO150.Models;

namespace PRO150.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //Validation of input will be here
        private static List<Game> games = new List<Game>();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult NewGame(string color, int? gameId)
        {
            Color colorEnum = convertColor(color);
            JsonResult result = new JsonResult();
            result.ContentType = "application/json";
            if (gameId == null)
            {
                Game game;
                if (colorEnum == Color.NOT_VALID)
                {
                    result.Data = "{\"error\":" + "Invalid Color" + "\"Invalid Color\":" + color + "}";
                }
                else
                {
                    Player p1 = new Player(colorEnum);
                    game = new Game(p1, newGameId());
                    games.Add(game);
                    result.Data = "{\"gameId\":" + game.gameId +"}";
                }
            }
            else
            {
                Game game;
                if (gameExists(gameId, out game))
                {
                    if (colorEnum == Color.NOT_VALID)
                    {
                        result.Data = "{\"error\":" + "Invalid Color" + "\"Invalid Color\":" + color + "}";
                    }
                    else
                    {
                        Player p2 = new Player(colorEnum);
                        game.addPlayer(p2);
                        result.Data = "{\"gameId\":" + game.gameId + "}";
                    }
                }
                else
                {
                    result.Data = "{\"error\":" + "Game Does Not Exist" + "}";
                }
            }
            return result;
        }

        private bool gameExists(int? gameId, out Game gameToFind)
        {
            if (gameId != null)
            {
                foreach (Game game in games)
                {
                    if (gameId == game.gameId)
                    {
                        gameToFind = game;
                        return true;
                    }
                }
            }
            gameToFind = null;
            return false;
        }

        private Color convertColor(string color)
        {
            color = color.ToLower();
            Color colorEnum;
            switch (color)
            {
                case "red": colorEnum = Color.Red;break;
                case "black": colorEnum = Color.Black; break;
                default: colorEnum = Color.NOT_VALID; break;
            }
            return colorEnum;
        }
        private int newGameId()
        {
            Random rand = new Random();
            int id = rand.Next();
            if (games.Count != 0)
            {
                Game game;
                while (gameExists(id, out game))
                {
                    id = rand.Next();
                }
            }
            return id;
        }
    }
}