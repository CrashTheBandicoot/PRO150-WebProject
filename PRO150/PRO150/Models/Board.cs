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
        //does NOT include multiple jumps
        public bool validMove(Player player, int startingX, int startingY, int endingX, int endingY, out string errorMessage)
        {
            if(!boardSquares[startingX, startingY].hasPiece())
            {
                errorMessage = "There is not a piece on that square";
                return false;
            }
            else if(boardSquares[endingX, endingY].hasPiece())
            {
                errorMessage = "That square has a piece on it";
                return false;
            }
            else if(boardSquares[endingX, endingY].color == Color.Red || boardSquares[startingX, startingY].color == Color.Red)
            {
                errorMessage = "Pieces can only be placed on Black squares";
                return false;
            }
            Piece piece = boardSquares[startingX, startingY].playerPiece;
            if(player.playerPieces.Contains(piece))
            {
                if(player.pieceColor == Color.Black && !piece.isKing)
                {
                    if ((startingX + 1 == endingX || startingX - 1 == endingX) && startingY + 1 == endingY)
                    {
                        errorMessage = "";
                        return true;
                    }
                    else if (startingX - 2 == endingX && startingY + 2 == endingY)
                    {
                        if (boardSquares[startingX - 1, startingY + 1].hasPiece() && boardSquares[startingX - 1, startingY + 1].playerPiece.color != player.pieceColor)
                        {
                            errorMessage = "";
                            return true;
                        }
                    }
                    else if (startingX + 2 == endingX && startingY + 2 == endingY)
                    {
                        if (boardSquares[startingX + 1, startingY + 1].hasPiece() && boardSquares[startingX + 1, startingY + 1].playerPiece.color != player.pieceColor)
                        {
                            errorMessage = "";
                            return true;
                        }
                    }
                }
                else if(player.pieceColor == Color.Red && !piece.isKing)
                {
                    if ((startingX + 1 == endingX || startingX - 1 == endingX) && startingY - 1 == endingY)
                    {
                        errorMessage = "";
                        return true;
                    }
                    else if (startingX - 2 == endingX && startingY - 2 == endingY)
                    {
                        if (boardSquares[startingX - 1, startingY - 1].hasPiece() && boardSquares[startingX - 1, startingY - 1].playerPiece.color != player.pieceColor)
                        {
                            errorMessage = "";
                            return true;
                        }
                    }
                    else if (startingX + 2 == endingX && startingY - 2 == endingY)
                    {
                        if (boardSquares[startingX + 1, startingY - 1].hasPiece() && boardSquares[startingX + 1, startingY - 1].playerPiece.color != player.pieceColor)
                        {
                            errorMessage = "";
                            return true;
                        }
                    }
                }
                else if (piece.isKing)
                {
                    if ((startingX + 1 == endingX || startingX - 1 == endingX) && (startingY + 1 == endingY || startingY -1 == endingY))
                    {
                        errorMessage = "";
                        return true;
                    }
                    else if (startingX - 2 == endingX && startingY + 2 == endingY)
                    {
                        if (boardSquares[startingX - 1, startingY + 1].hasPiece() && boardSquares[startingX - 1, startingY + 1].playerPiece.color != player.pieceColor)
                        {
                            errorMessage = "";
                            return true;
                        }
                    }
                    else if (startingX + 2 == endingX && startingY + 2 == endingY)
                    {
                        if (boardSquares[startingX + 1, startingY + 1].hasPiece() && boardSquares[startingX + 1, startingY + 1].playerPiece.color != player.pieceColor)
                        {
                            errorMessage = "";
                            return true;
                        }
                    }
                    else if (startingX - 2 == endingX && startingY - 2 == endingY)
                    {
                        if (boardSquares[startingX - 1, startingY - 1].hasPiece() && boardSquares[startingX - 1, startingY - 1].playerPiece.color != player.pieceColor)
                        {
                            errorMessage = "";
                            return true;
                        }
                    }
                    else if (startingX + 2 == endingX && startingY - 2 == endingY)
                    {
                        if (boardSquares[startingX + 1, startingY - 1].hasPiece() && boardSquares[startingX + 1, startingY - 1].playerPiece.color != player.pieceColor)
                        {
                            errorMessage = "";
                            return true;
                        }
                    }
                }
                errorMessage = "You can't move to that square";
                return false;
            }
            else
            {
                errorMessage = "You can't move your opponent's piece";
                return false;
            }
        }
    }
}