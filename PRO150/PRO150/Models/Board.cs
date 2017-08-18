using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PRO150.Models;

namespace PRO150.Models
{
    public class Board
    {
        public BoardSquare[,] boardSquares;
        public Board(List<Piece> p1Pieces, List<Piece> p2Pieces)
        {
            List<Piece> p1 = p1Pieces;
            List<Piece> p2 = p2Pieces;
            boardSquares = new BoardSquare[8,8];
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Color color;
                    BoardSquare square;
                    if (((i==0||i==2||i==4||i==6) && (j==1||j==3||j==5||j==7)) || ((i==1||i==3||i==5||i==7) && (j==0||j==2||j==4||j==6)))
                    {
                        color = Color.Black;
                        square = new BoardSquare(color);
                        if (i<3)
                        {
                            square.addPiece(p1.Last());
                            p1.Remove(p1.Last());
                        }
                        else if (i > 4)
                        {
                            square.addPiece(p2.Last());
                            p2.Remove(p2.Last());
                        }
                    }
                    else
                    {
                        color = Color.Red;
                        square = new BoardSquare(color);
                    }
                    boardSquares[i,j] = square;
                }
            }
        }
        public void movePiece(int statingX, int startingY, int endingX,  int endingY)
        {
            Piece piece = boardSquares[statingX, startingY].playerPiece;
            boardSquares[statingX, startingY].removePiece();
            boardSquares[endingX, endingY].addPiece(piece);
        }

        private bool validMove(Player player, int statingX, int startingY, int endingX, int endingY, out string errorMessage)
        {
            if(!boardSquares[statingX, startingY].hasPiece())
            {
                errorMessage = "There is not a piece on that square";
                return false;
            }
            else if(boardSquares[endingX, endingY].hasPiece())
            {
                errorMessage = "That square has a piece on it";
                return false;
            }
            else if(boardSquares[endingX, endingY].color == Color.Red || boardSquares[statingX, startingY].color == Color.Red)
            {
                errorMessage = "Pieces can only be placed on Black squares";
                return false;
            }
            Piece piece = boardSquares[statingX, startingY].playerPiece;
            if(!player.playerPieces.Contains(piece))
            {
                errorMessage = "You can't move your opponent's piece";
                return false;
            }
            else
            {
                if(player.pieceColor)
            }
        }
    }
}