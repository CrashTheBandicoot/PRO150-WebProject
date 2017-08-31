using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRO150.Models
{
    public class Game
    {
        public Player p1;
        public Player p2;
        public Board gameBoard;
        public int playerTurnId;
        public int gameId;
        public GameState state;
        public Game(Player player, int id)
        {
            p1 = player;
            gameId = id;
            state = GameState.WaitingForSecond;
        }
        public void addPlayer(Player player)
        {
            p2 = player;
            gameBoard = new Board(p1.playerPieces, p2.playerPieces);
            state = GameState.InProgress;
        }
    }
}