using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public Players[,] players;
    public BoardState[,] boards;
    private Colour colour;
    public GameObject playerSelected;
    private int width, height;
    public GameState(BoardGenerate board, Colour c)
    {
        colour = c;
        width = board.Width;
        height = board.Height;
        players = new Players[width, height];
        boards = new BoardState[width, height];
        BoardState tempBoard;
        for (int i = 0; i < board.Width; i++)
        {
            for (int j = 0; j < board.Height; j++)
            {
                tempBoard = new BoardState(i,j,board.Tiles[i,j]);
                boards[i,j] = tempBoard;
                players[i, j] = null;
            }
        }
        for (int i = 0; i <= 1; i++)
        {
            Players tempPlayer = new Players(PieceType.KNIGHT, colour, 2, 12);
            players[tempPlayer.PosX, tempPlayer.PosY] = tempPlayer;
            tempPlayer = new Players(PieceType.KNIGHT, colour, 2, 14);
            players[tempPlayer.PosX, tempPlayer.PosY] = tempPlayer;
            tempPlayer = new Players(PieceType.KNIGHT, colour, 0, 12);
            players[tempPlayer.PosX, tempPlayer.PosY] = tempPlayer;
            tempPlayer = new Players(PieceType.ARCHER, colour, 1, 13);
            players[tempPlayer.PosX, tempPlayer.PosY] = tempPlayer;
            tempPlayer = new Players(PieceType.ARCHER, colour, 0, 14);
            players[tempPlayer.PosX, tempPlayer.PosY] = tempPlayer;
            if (colour == Colour.RED)
                colour = Colour.BLUE;
            else
                colour = Colour.RED;
            tempPlayer = new Players(PieceType.KNIGHT, colour, 12, 2);
            players[tempPlayer.PosX, tempPlayer.PosY] = tempPlayer;
            tempPlayer = new Players(PieceType.KNIGHT, colour, 12, 0);
            players[tempPlayer.PosX, tempPlayer.PosY] = tempPlayer;
            tempPlayer = new Players(PieceType.KNIGHT, colour, 14, 2);
            players[tempPlayer.PosX, tempPlayer.PosY] = tempPlayer;
            tempPlayer = new Players(PieceType.ARCHER, colour, 13, 1);
            players[tempPlayer.PosX, tempPlayer.PosY] = tempPlayer;
            tempPlayer = new Players(PieceType.ARCHER, colour, 14, 0);
            players[tempPlayer.PosX, tempPlayer.PosY] = tempPlayer;
            if (colour == Colour.BLUE)
                colour = Colour.RED;
            else
                colour = Colour.BLUE;
        }
    }
    public void PassOn(int x, int y, string s)
    {
        int check = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (players[i, j] != null)
                    if (players[i, j].Selected)
                        check++;
            }
        }
        if (s == "Player")
        {
            if (check == 0)
                players[x, y].Selected = true; 
            else
                players[x, y].Selected = false;
        }
    }
}
