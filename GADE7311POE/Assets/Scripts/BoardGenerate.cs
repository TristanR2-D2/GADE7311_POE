using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BoardPiece
{
    CLOUD,
    HOLE,
    ROCK,
    SELECTR,
    SELECTB
}

public class Boards
{
    private BoardPiece[,] floors;
    private float[,] vals;
    private int width;
    private int height;
    private int health;

    public BoardPiece[,] Floors
    {
        get { return floors; }
        set { floors = value; }
    }
    public float[,] Vals
    {
        get { return vals; }
        set { vals = value; }
    }
    public int Width
    {
        get { return width; }
    }
    public int Height
    {
        get { return height; }
    }
    public int Health
    {
        get { return health; }
    }

    public Boards(int w, int h)
    {
        width = w;
        height = h;
        floors = new BoardPiece[width, height];
        CreateBoard();
    }

    public Boards(int w, int h, string[] s)
    {
        width = w;
        height = h;
        floors = new BoardPiece[width, height];
        CreateBoard(s);
    }
    public Boards(int w, int h, float r, int off)
    {
        width = w;
        height = h;
        floors = new BoardPiece[width, height];
        vals = new float[width, height];
        CreateBoard(r, off);
    }

    public void CreateBoard()
    {
        int rand;
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                rand = Random.Range(0, 9);
                if (rand < 8)
                    floors[x, y] = BoardPiece.CLOUD;
                else
                    floors[x, y] = BoardPiece.HOLE;
                switch (y)
                {
                    case 0: case 1: if (floors[x, y] != BoardPiece.HOLE && floors[x, y] != BoardPiece.ROCK) floors[x, y] = BoardPiece.SELECTB; break;
                    default: if (floors[x, y] != BoardPiece.HOLE && y >= Height - 2 && floors[x, y] != BoardPiece.ROCK) floors[x, y] = BoardPiece.SELECTR; break;
                }
            }
        }
    }
    public void CreateBoard(string[] s)
    {
        char[] map;
        for (int x = 0; x < Width; x++)
        {
            map = s[x].ToCharArray();
            for (int y = 0; y < Height; y++)
            {
                switch (map[y])
                {
                    case 'C': floors[x, y] = BoardPiece.CLOUD; break;
                    case 'H': floors[x, y] = BoardPiece.HOLE; break;
                    case 'R': floors[x, y] = BoardPiece.SELECTB; break;
                    case 'B': floors[x, y] = BoardPiece.SELECTR; break;
                }
            }
        }
    }
    public void CreateBoard(float refine, int offset)
    {
        float noise, clamp;
        int id;
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                noise = Mathf.PerlinNoise((x - offset) / refine, (y - offset) / refine);
                clamp = Mathf.Clamp01(noise);
                id = Mathf.FloorToInt(clamp * 4);
                switch (id)
                {
                    case 0: floors[x, y] = BoardPiece.HOLE; break;
                    case 1: floors[x, y] = BoardPiece.CLOUD; break;
                    case 2: floors[x, y] = BoardPiece.ROCK; break;
                    default: floors[x, y] = BoardPiece.CLOUD; break;
                }
                switch (y)
                {
                    case 0: case 1: if (floors[x, y] != BoardPiece.HOLE && floors[x, y] != BoardPiece.ROCK) floors[x, y] = BoardPiece.SELECTB; break;
                    default: if (floors[x, y] != BoardPiece.HOLE && y >= Height - 2 && floors[x, y] != BoardPiece.ROCK) floors[x, y] = BoardPiece.SELECTR; break;
                }
            }
        }
    }
    public void ChangeTile(int x, int y)
    {
        floors[x, y] = BoardPiece.HOLE;
    }
}