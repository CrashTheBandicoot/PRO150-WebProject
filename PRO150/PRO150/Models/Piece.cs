using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRO150.Models
{
    public class Piece
    {
        public Color color;
        public bool isKing;
        public Piece(Color newColor)
        {
            color = newColor;
            isKing = false;
        }
        public void MakdKing()
        {
            isKing = true;
        }
    }
}