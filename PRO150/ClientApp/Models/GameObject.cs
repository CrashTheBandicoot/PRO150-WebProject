using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientApp.Models
{
    public class GameObject
    {
        public int gameId { get; set; }
        public int playerId { get; set; }
        public GameObject(int game, int player)
        {
            gameId = game;
            playerId = player;
        }
        public GameObject()
        {

        }
    }
}