using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState
{
    public Players[,] players;
    public BoardState[,] boards;
    private Colour colour;
    public GameObject playerSelected;
    private int width, height, ap;
    public Colour Colour
    {
        get { return colour; }
    }
    public int AP
    {
        get { return ap; }
    }
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
                tempBoard = new BoardState(i, j, board.Tiles[i, j]);
                boards[i, j] = tempBoard;
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
        int check, tempX, tempY, count, alive, turn, turnCount;
        alive = 0;
        tempY = 0;
        count = 0;
        turn = 0;
        turnCount = 0;
        tempX = 0;
        check = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (players[i, j] != null && players[i,j].Types == colour)
                {
                    if (players[i, j].Selected)
                    {
                        check++;
                        tempX = i;
                        tempY = j;
                    }
                    alive++;
                    if (players[i, j].Spent)
                        turnCount++;
                }
                if (players[i, j] != null && players[i, j].Spent)
                    count++;

                if (players[i, j] != null && players[i, j].Alive)
                    turn++;
            }
        }
        if (s == "Player" && !players[x, y].Spent)
        {
            ap = 6;
            int minMovX, maxMovX, minMovY, maxMovY;
            if (players[x, y].Types == colour)
            {
                if (check == 0)
                {
                    players[x, y].Selected = true;
                    minMovX = Mathf.Clamp(x - 6, 0, width);
                    maxMovX = Mathf.Clamp(x + 6, 0, width);
                    minMovY = Mathf.Clamp(y - 6, 0, height);
                    maxMovY = Mathf.Clamp(y + 6, 0, height);
                    for (int i = minMovX; i < maxMovX; i++)
                    {
                        for (int j = minMovY; j < maxMovY; j++)
                        {
                            boards[i, j].Selected = true;
                            float distance = Mathf.Sqrt(Mathf.Pow(i - x, 2) + Mathf.Pow(j - y, 2));
                            if (distance < 6)
                            {
                                if (colour == Colour.RED)
                                    boards[i, j].Select = Selection.SELECTRED;
                                else
                                    boards[i, j].Select = Selection.SELECTBLUE;
                            }
                        }
                    }
                }
                else if(check > 0 && players[x, y].Selected)
                {
                    players[x, y].Selected = false;
                    for (int i = 0; i < width; i++)
                    {
                        for (int j = 0; j < height; j++)
                        {
                            boards[i, j].Selected = false;
                            boards[i, j].Select = Selection.NONE;

                        }
                    }
                }
            }
        }

        if(s == "TIle" && boards[x, y].Selected)
        {
            int distance = Mathf.FloorToInt(Mathf.Sqrt(Mathf.Pow(tempX - x, 2) + Mathf.Pow(tempY - y, 2)));
            ap -= distance;
            players[x, y] = players[tempX, tempY];
            players[x, y].PosX = x;
            players[x, y].PosY = y;
            players[x, y].Selected = false;
            players[x, y].Spent = true;
            players[tempX, tempY] = null;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    boards[i, j].Selected = false;
                    boards[i, j].Select = Selection.NONE;
                }
            }
            //Debug.Log(count);
            //Debug.Log(turn);
            if (alive - count == 1)
            {
                if (colour == Colour.RED)
                    colour = Colour.BLUE;
                else
                    colour = Colour.RED;
            }
            if(turn - count == 1)
            {
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        if (players[i, j] != null)
                        {
                            players[i, j].Spent = false;
                        }
                    }
                }
                if (colour == Colour.RED)
                    colour = Colour.BLUE;
                else
                    colour = Colour.RED;
            }
        }
    }
}
