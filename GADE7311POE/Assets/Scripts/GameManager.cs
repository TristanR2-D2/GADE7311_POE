using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string mapPath;
    private BoardGenerate board;
    GameState state;
    private Colour currentTeam;
    [Header("Base Ground")]
    public GameObject grass; public GameObject hill1, hill2, hill3, rock, ruinF, ruinW;
    [Header("Red Highlights")]
    public GameObject redGrass; public GameObject redHill1, redHill2, redHill3, redRock, redRuinF;
    [Header("Blue Highlights")]
    public GameObject bluGrass; public GameObject bluHill1, bluHill2, bluHill3, bluRock, bluRuinF;
    [Header("Pieces")]
    public GameObject archerRed; public GameObject archerBlue, knightRed, knightBlue;
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, 99);
        mapPath = Application.dataPath + "/DefaultMap.txt";
        string[] map = File.ReadAllLines(mapPath);
        Debug.Log(mapPath);
        board = new BoardGenerate(map[0].Length, map.Length, map);
        if (rand < 50)
            currentTeam = Colour.RED;
        else
            currentTeam = Colour.BLUE;
        //GameObject.Find("CameraPivot").transform.position = new Vector3(board.X, 0, board.Y);
        state = new GameState(board, currentTeam);
        Board();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PassOn(int x, int y, string s)
    {
        state.PassOn(x, y, s);
    }

    private void Board()
    {
        //GameObject[] tiles = GameObject.FindGameObjectsWithTag("Tile");
        //foreach (GameObject obj in tiles) { Destroy(obj); }
        for (int x = 0; x < board.Width; x++)
        {
            for (int z = 0; z < board.Height; z++)
            {
                switch (state.boards[x,z].Space)
                {
                    case BoardSpace.GRASS: Instantiate(grass, new Vector3(x - board.X, 0f, z - board.Y), Quaternion.identity); break;
                    case BoardSpace.HILL1: Instantiate(hill1, new Vector3(x - board.X, 0.25f, z - board.Y), Quaternion.identity); break;
                    case BoardSpace.HILL2: Instantiate(hill2, new Vector3(x - board.X, 0.5f, z - board.Y), Quaternion.identity); break;
                    case BoardSpace.HILL3: Instantiate(hill3, new Vector3(x - board.X, 0.75f, z - board.Y), Quaternion.identity); break;
                    case BoardSpace.ROCKY: Instantiate(rock, new Vector3(x - board.X, 0f, z - board.Y), Quaternion.identity); break;
                    case BoardSpace.RUINFLOOR: Instantiate(ruinF, new Vector3(x - board.X, 0f, z - board.Y), Quaternion.identity); break;
                    case BoardSpace.RUINWALL: Instantiate(ruinW, new Vector3(x - board.X, 0.5f, z - board.Y), Quaternion.identity); break;
                }
            }
        }
        foreach (Players player in state.players)
        {
            if (player.Types == Colour.RED)
            {
                if (player.Piece == PieceType.ARCHER)
                    Instantiate(archerRed, new Vector3(player.PosX, 1.5f, player.PosY), Quaternion.identity);
                else
                    Instantiate(knightRed, new Vector3(player.PosX, 1.5f, player.PosY), Quaternion.identity);
            }
            else
            {
                if (player.Piece == PieceType.ARCHER)
                    Instantiate(archerBlue, new Vector3(player.PosX, 1.5f, player.PosY), Quaternion.identity);
                else
                    Instantiate(knightBlue, new Vector3(player.PosX, 1.5f, player.PosY), Quaternion.identity);
            }
        }
    }
}
