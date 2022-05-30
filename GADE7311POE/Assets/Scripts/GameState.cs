using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerType
{
    AI,
    HUMAN
}
public class GameState
{
    public Players[,] players;
    public BoardState[,] boards;
    private Colour colour;
    public PlayerType playerType;
    public GameObject playerSelected;
    private MiniMaxBrain mini;
    private int width, height, ap;
    private bool primed;
    public Colour Colour
    {
        get { return colour; }
    }
    public int AP
    {
        get { return ap; }
    }
    public PlayerType PlayerType
    {
        get { return playerType; }
    }
    public GameState(BoardGenerate board, Colour c)
    {
        colour = c;
        primed = false;
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
        setupPlayers();
    }
    public GameState(BoardGenerate board, Colour c, PlayerType pt, Difficulty d)
    {
        colour = c;
        primed = false;
        width = board.Width;
        height = board.Height;
        playerType = pt;
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
        setupPlayers();
        if (pt == PlayerType.AI)
        {
            mini = new MiniMaxBrain(players, colour, width, height, d);
        }
        else
        {
            if (colour == Colour.RED)
                mini = new MiniMaxBrain(players, Colour.BLUE, width, height, d);
            else
                mini = new MiniMaxBrain(players, Colour.RED, width, height, d);
        }
    }

    void setupPlayers()
    {
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
            ap = 5;
            if (players[x, y].Types == colour)
            {
                if (check == 0)
                {
                    players[x, y].Selected = true;
                    buildMoves(x, y, 0, 0);
                }
                else if (check > 0 && players[x, y].Selected)
                {
                    players[x, y].Selected = false;
                    clearBoard();
                }
            }
        }
        if (s == "Player" && primed)
        {
            if (players[x, y].Types != colour)
            {
                players[tempX, tempY].Selected = false;
                players[x, y].Health = 0;
                players[x, y].Alive = false;
                players[tempX, tempY].Spent = true;
                players[x, y].Selected = false;
                clearBoard();
                GameObject.Find("GameManager").GetComponent<GameManager>().ButtonControl("E");
                primed = false;
            }
        }

        if (s == "TIle" && boards[x, y].Selected && !primed)
        {
            int distance = Mathf.FloorToInt(Mathf.Sqrt(Mathf.Pow(tempX - x, 2) + Mathf.Pow(tempY - y, 2)));
            ap -= distance;
            players[x, y] = players[tempX, tempY];
            players[x, y].PosX = x;
            players[x, y].PosY = y;
            players[x, y].Moved = true;
            players[tempX, tempY] = null;
            clearBoard();
            if (ap <= 0)
            {
                players[x, y].Spent = true;
                players[x, y].Selected = false;
            }
            else
            {
                buildMoves(x, y, 0, 0);
                if(players[x, y].Piece == PieceType.ARCHER)
                    GameObject.Find("GameManager").GetComponent<GameManager>().ButtonControl("A");
                else
                    GameObject.Find("GameManager").GetComponent<GameManager>().ButtonControl("K");
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

    void clearBoard()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                boards[i, j].Selected = false;
                boards[i, j].Select = Selection.NONE;
            }
        }
    }

    public void ButtonClick(string s)
    {
        if(s == "Attack")
        {
            int checker = 0; int x = 0; int y = 0;
            for (int i = 0; i < width; i++)
            {
                for(int j = 0; j < height; j++)
                {
                    if (players[i, j] != null)
                    {
                        if (players[i, j].Selected)
                        {
                            x = i;
                            y = j;
                        }
                        else
                        {
                            /*int minMovX, minMovY, maxMovX, maxMovY;
                            minMovX = Mathf.Clamp(x - ap, 0, width);
                            maxMovX = Mathf.Clamp(x + ap + 1, 0, width);
                            minMovY = Mathf.Clamp(y - ap, 0, height);
                            maxMovY = Mathf.Clamp(y + ap + 1, 0, height);*/
                            float distance = Mathf.Sqrt(Mathf.Pow(i - x, 2) + Mathf.Pow(j - y, 2));
                            if (distance < ap)
                            {
                                if (colour == Colour.RED)
                                    if (players[i, j].Types == Colour.BLUE)
                                        checker++;
                                    else
                                    if (players[i, j].Types == Colour.RED)
                                        checker++;
                            }
                        }
                    }
                }
            }
            Debug.Log(x + " " + y);
            if (checker > 0)
            {
                primed = true;
                buildMoves(x, y, players[x, y].MinRange, players[x, y].MaxRange);
            }
            else
            {
                players[x, y].Spent = true;
                players[x, y].Selected = false;
                clearBoard();
                GameObject.Find("GameManager").GetComponent<GameManager>().ButtonControl("E");
            }
        }
    }

    void buildMoves(int x, int y, int nr, int xr)
    {
        if (nr == 0)
        {
            int minMovX, maxMovX, minMovY, maxMovY;
            minMovX = Mathf.Clamp(x - ap, 0, width);
            maxMovX = Mathf.Clamp(x + ap + 1, 0, width);
            minMovY = Mathf.Clamp(y - ap, 0, height);
            maxMovY = Mathf.Clamp(y + ap + 1, 0, height);
            for (int i = minMovX; i < maxMovX; i++)
            {
                for (int j = minMovY; j < maxMovY; j++)
                {
                    boards[i, j].Selected = true;
                    float distance = Mathf.Sqrt(Mathf.Pow(i - x, 2) + Mathf.Pow(j - y, 2));
                    if (distance <= ap)
                    {
                        if (colour == Colour.RED)
                            boards[i, j].Select = Selection.SELECTRED;
                        else
                            boards[i, j].Select = Selection.SELECTBLUE;
                    }
                }
            }
        }
        else
        {
            int minMovX, maxMovX, minMovY, maxMovY;
            minMovX = Mathf.Clamp(x - xr, 0, width);
            maxMovX = Mathf.Clamp(x + xr + 1, 0, width);
            minMovY = Mathf.Clamp(y - xr, 0, height);
            maxMovY = Mathf.Clamp(y + xr + 1, 0, height);
            for (int i = minMovX; i < maxMovX; i++)
            {
                for (int j = minMovY; j < maxMovY; j++)
                {
                    boards[i, j].Selected = true;
                    float distance = Mathf.Sqrt(Mathf.Pow(i - x, 2) + Mathf.Pow(j - y, 2));
                    if (distance <= xr && distance >= nr)
                    {
                        if (colour == Colour.RED)
                            boards[i, j].Select = Selection.SELECTRED;
                        else
                            boards[i, j].Select = Selection.SELECTBLUE;
                    }
                }
            }
        }
    }
}
