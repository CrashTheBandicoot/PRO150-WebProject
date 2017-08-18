using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRO150.Models
{
    public class BoardSquare
    {
        public Color color;
        public Piece playerPiece;
        public BoardSquare(Color newColor)
        {
            color = newColor;
        }
        public void addPiece(Piece piece)
        {
            playerPiece = piece;
        }
        public void removePiece()
        {
            playerPiece = null;
        }
        public bool hasPiece()
        {
            if(playerPiece == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}