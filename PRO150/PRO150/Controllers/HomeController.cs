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
        public JsonRequestBehavior behavior = JsonRequestBehavior.AllowGet;
        private static List<Game> games = new List<Game>();
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult NewGame(string color, int? gameId, int? playerId)
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
                    Player p1 = new Player(colorEnum, newPlayerId());
                    game = new Game(p1, newGameId());
                    games.Add(game);
                    result.Data = "{\"gameId\":" + game.gameId + ",\"playerId\":" + game.p1.playerId + "}";
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
                        int id = newPlayerId();
                        while (game.p1.playerId == id)
                        {
                            id = newPlayerId();
                        }
                        Player p2 = new Player(colorEnum, newPlayerId());
                        game.addPlayer(p2);
                        result.Data = "{\"gameId\":" + game.gameId + ",\"playerId\":" + game.p2.playerId + "}";
                    }
                }
                else
                {
                    result.Data = "{\"error\":" + "Game Does Not Exist" + "}";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult Move(int gameId, int playerId, string move)
        {
            JsonResult result = new JsonResult();
            result.ContentType = "application/json";
            Game game;
            if (gameExists(gameId, out game))
            {
                if (game.p1.playerId == playerId || game.p2.playerId == playerId)
                {
                    Player p;
                    if (game.p1.playerId == playerId) { p = game.p1; }
                    else { p = game.p2; }
                    if(game.playerTurnId == p.playerId)
                    {
                        string[] moves = move.Split(' ');
                        if (moves.Count() > 1)
                        {
                            string possibleError = "";
                            bool validMoves = true;
                            for (int i = 0; i < moves.Count(); i++)
                            {
                                int startingX;
                                int startingY;
                                int endingX;
                                int endingY;
                                convertMove(moves[i], out startingX, out startingY);
                                convertMove(moves[i+1], out endingX, out endingY);
                                if (!game.gameBoard.validMove(p, startingX, startingY, endingX, endingY, out possibleError))
                                {
                                    validMoves = false;
                                    break;
                                }
                            }
                            if (validMoves)
                            {
                                for (int i = 0; i < moves.Count(); i++)
                                {
                                    int startingX;
                                    int startingY;
                                    int endingX;
                                    int endingY;
                                    convertMove(moves[i], out startingX, out startingY);
                                    convertMove(moves[i + 1], out endingX, out endingY);
                                    game.gameBoard.movePiece(startingX, startingY, endingX, endingY);
                                }
                                result.Data = "{\"success\":" + "move successful" + "}";
                            }
                            result.Data = "{\"error\":" + possibleError + "}";                       
                        }
                        result.Data = "{\"error\":" + "Invalid move" + ",\"move\":" + move + "}";
                    }
                    result.Data = "{\"error\":" + "It's Not Your Turn" + "}";
                }
                result.Data = "{\"error\":" + "Invalid playerId" + ",\"playerId\":" + playerId + "}";
            }
            result.Data = "{\"error\":" + "Game Does Not Exist" + "}";
            return Json(result, JsonRequestBehavior.AllowGet);
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
        private int newPlayerId()
        {
            Random rand = new Random();
            int id = rand.Next();
            return id;
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
        private void convertMove(string move, out int x, out int y)
        {
            char[] temp = move.ToCharArray();
            char alpha = temp[0];
            int numaric = char.Parse(temp[1].ToString());
            switch (alpha.ToString().ToLower())
            {
                case "a": x = 0; break;
                case "b": x = 1; break;
                case "c": x = 2; break;
                case "d": x = 3; break;
                case "e": x = 4; break;
                case "f": x = 5; break;
                case "g": x = 6; break;
                case "h": x = 7; break;
                default: x = -1; break;
            }
            y = numaric - 1;
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