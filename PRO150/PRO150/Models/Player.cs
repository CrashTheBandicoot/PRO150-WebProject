using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRO150.Models
{
    public class Player
    {
        public Color pieceColor;
        public List<Piece> playerPieces;
        public Player(Color newColor)
        {
            playerPieces = new List<Piece>();
            pieceColor = newColor;
            for(int i = 0; i < 12; i++)
            {
                playerPieces.Add(new Piece(pieceColor));
            }
        }
    }
}