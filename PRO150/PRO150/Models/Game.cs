using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRO150.Models
{
    public class Game
    {
        Player p1;
        Player p2;
        Board gameBoard;
        public int gameId;
        public Game(Player player, int id)
        {
            p1 = player;
            gameId = id;
        }
        public void addPlayer(Player player)
        {
            p2 = player;
            gameBoard = new Board(p1.playerPieces, p2.playerPieces);
        }
    }
}