using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public List<Players> players;
    public BoardState[,] boards;
    private Colour colour;
    public GameObject playerSelected;
    public GameState(BoardGenerate board, Colour c)
    {
        colour = c;
        players = new List<Players>();
        boards = new BoardState[board.Width,board.Height];
        BoardState tempBoard;
        for (int i = 0; i < board.Width; i++)
        {
            for (int j = 0; j < board.Height; j++)
            {
                tempBoard = new BoardState(i,j,board.Tiles[i,j]);
                boards[i,j] = tempBoard;
            }
        }
        for (int i = 0; i <= 1; i++)
        {
            Players tempPlayer = new Players(PieceType.KNIGHT, colour, -3, 3);
            players.Add(tempPlayer);
            tempPlayer = new Players(PieceType.KNIGHT, colour, -5, 7);
            players.Add(tempPlayer);
            tempPlayer = new Players(PieceType.KNIGHT, colour, -7, 5);
            players.Add(tempPlayer);
            tempPlayer = new Players(PieceType.ARCHER, colour, -5, 5);
            players.Add(tempPlayer);
            tempPlayer = new Players(PieceType.ARCHER, colour, -7, 7);
            players.Add(tempPlayer);
            if (c == Colour.RED)
                colour = Colour.BLUE;
            else
                colour = Colour.RED;
            tempPlayer = new Players(PieceType.KNIGHT, colour, 3, -3);
            players.Add(tempPlayer);
            tempPlayer = new Players(PieceType.KNIGHT, colour, 5, -7);
            players.Add(tempPlayer);
            tempPlayer = new Players(PieceType.KNIGHT, colour, 7, -5);
            players.Add(tempPlayer);
            tempPlayer = new Players(PieceType.ARCHER, colour, 5, -5);
            players.Add(tempPlayer);
            tempPlayer = new Players(PieceType.ARCHER, colour, 7, -7);
            players.Add(tempPlayer);
            if (c == Colour.RED)
                colour = Colour.BLUE;
            else
                colour = Colour.RED;
        }
    }
    public void PassOn(int x, int y, string s)
    {

    }
}
