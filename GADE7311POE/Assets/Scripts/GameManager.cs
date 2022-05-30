using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.UI;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string mapPath;
    private BoardGenerate board;
    GameState state;
    private Colour currentTeam;
    private PlayerType playerType;
    [Header("Game Setup")]
    public NumPlayers numPlayers; public Difficulty difficulty;
    [Header("UI")]
    public Text playTurn; public Text playAP, txtEnd, txtSpell1, txtSpell2; public GameObject btnAttack, btnSpell1, btnSpell2, btnEnd;
    [Header("Base Ground")]
    public GameObject grass; public GameObject hill1, hill2, hill3, rock, ruinF, ruinW;
    [Header("Red Highlights")]
    public GameObject redGrass; public GameObject redHill1, redHill2, redHill3, redRock, redRuinF;
    [Header("Blue Highlights")]
    public GameObject bluGrass; public GameObject bluHill1, bluHill2, bluHill3, bluRock, bluRuinF;
    [Header("Pieces")]
    public GameObject archerRed; public GameObject archerBlue, knightRed, knightBlue, archerSpent, knightSpent,archerSelect,knightSelect;
    // Start is called before the first frame update
    void Start()
    {
        btnAttack.SetActive(false);
        btnSpell1.SetActive(false);
        btnSpell2.SetActive(false);
        btnEnd.SetActive(false);
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
        if (numPlayers == NumPlayers.PVP)
        {
            state = new GameState(board, currentTeam);
        }
        Board();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

     public void playerVAI()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void playerVPlayer()
    {
        SceneManager.LoadScene("GameScene"); 
    }

    public void Quit()
    {
        Application.Quit();
    }


    public void ButtonControl(string s)
    {
        if(s == "E")
        {
            btnAttack.SetActive(false);
            btnSpell1.SetActive(false);
            btnSpell2.SetActive(false);
            btnEnd.SetActive(false);
        }
        else
        {
            btnAttack.SetActive(true);
            //btnSpell1.SetActive(true);
            //btnSpell2.SetActive(true);
            //btnEnd.SetActive(true);
        }
    }

    public void PassOn(int x, int y, string s)
    {
        state.PassOn(x + board.X, y + board.Y, s);
        Board();
    }

    public void ButtonClick(string s)
    {
        state.ButtonClick(s);
        Board();
    }

    private void Board()
    {
        GameObject[] tiles = GameObject.FindGameObjectsWithTag("TIle");
        foreach (GameObject obj in tiles) { Destroy(obj); }
        GameObject[] play = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject obj in play) { Destroy(obj); }
        if (state.Colour == Colour.RED)
            playTurn.color = Color.red;
        else
            playTurn.color = Color.blue;
        playAP.text = "AP: " + state.AP.ToString();
        playTurn.text = "Current Player: " + state.Colour;
        for (int x = 0; x < board.Width; x++)
        {
            for (int z = 0; z < board.Height; z++)
            {
                switch (state.boards[x, z].Select)
                {
                    case Selection.NONE:
                        switch (state.boards[x, z].Space)
                        {
                            case BoardSpace.GRASS: Instantiate(grass, new Vector3(x - board.X, 0f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.HILL1: Instantiate(hill1, new Vector3(x - board.X, 0.25f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.HILL2: Instantiate(hill2, new Vector3(x - board.X, 0.5f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.HILL3: Instantiate(hill3, new Vector3(x - board.X, 0.75f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.ROCKY: Instantiate(rock, new Vector3(x - board.X, 0f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.RUINFLOOR: Instantiate(ruinF, new Vector3(x - board.X, 0f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.RUINWALL: Instantiate(ruinW, new Vector3(x - board.X, 0.5f, z - board.Y), Quaternion.identity); break;
                        }
                        break;
                    case Selection.SELECTRED:
                        switch (state.boards[x, z].Space)
                        {
                            case BoardSpace.GRASS: Instantiate(redGrass, new Vector3(x - board.X, 0f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.HILL1: Instantiate(redHill1, new Vector3(x - board.X, 0.25f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.HILL2: Instantiate(redHill2, new Vector3(x - board.X, 0.5f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.HILL3: Instantiate(redHill3, new Vector3(x - board.X, 0.75f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.ROCKY: Instantiate(redRock, new Vector3(x - board.X, 0f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.RUINFLOOR: Instantiate(redRuinF, new Vector3(x - board.X, 0f, z - board.Y), Quaternion.identity); break;
                        }
                        break;
                    case Selection.SELECTBLUE:
                        switch (state.boards[x, z].Space)
                        {
                            case BoardSpace.GRASS: Instantiate(bluGrass, new Vector3(x - board.X, 0f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.HILL1: Instantiate(bluHill1, new Vector3(x - board.X, 0.25f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.HILL2: Instantiate(bluHill2, new Vector3(x - board.X, 0.5f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.HILL3: Instantiate(bluHill3, new Vector3(x - board.X, 0.75f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.ROCKY: Instantiate(bluRock, new Vector3(x - board.X, 0f, z - board.Y), Quaternion.identity); break;
                            case BoardSpace.RUINFLOOR: Instantiate(bluRuinF, new Vector3(x - board.X, 0f, z - board.Y), Quaternion.identity); break;
                        }
                        break;

                }
                if (state.players[x, z] != null)
                {
                    float temp = 1.5f;
                    if (state.boards[x, z].Space == BoardSpace.HILL1)
                        temp = 2f;
                    else if (state.boards[x, z].Space == BoardSpace.HILL2)
                        temp = 2.5f;
                    else if (state.boards[x, z].Space == BoardSpace.HILL3)
                        temp = 3f;
                    if (state.players[x, z].Alive)
                    {
                        if (!state.players[x, z].Spent)
                        {
                            if (!state.players[x, z].Selected)
                            {
                                if (state.players[x, z].Types == Colour.RED)
                                {
                                    if (state.players[x, z].Piece == PieceType.ARCHER)
                                        Instantiate(archerRed, new Vector3(state.players[x, z].PosX - board.X, temp, state.players[x, z].PosY - board.Y), Quaternion.identity);
                                    else
                                        Instantiate(knightRed, new Vector3(state.players[x, z].PosX - board.X, temp, state.players[x, z].PosY - board.Y), Quaternion.identity);
                                }
                                else
                                {
                                    if (state.players[x, z].Piece == PieceType.ARCHER)
                                        Instantiate(archerBlue, new Vector3(state.players[x, z].PosX - board.X, temp, state.players[x, z].PosY - board.Y), Quaternion.identity);
                                    else
                                        Instantiate(knightBlue, new Vector3(state.players[x, z].PosX - board.X, temp, state.players[x, z].PosY - board.Y), Quaternion.identity);
                                }
                            }
                            else
                            {
                                if (state.players[x, z].Piece == PieceType.ARCHER)
                                    Instantiate(archerSelect, new Vector3(state.players[x, z].PosX - board.X, temp, state.players[x, z].PosY - board.Y), Quaternion.identity);
                                else
                                    Instantiate(knightSelect, new Vector3(state.players[x, z].PosX - board.X, temp, state.players[x, z].PosY - board.Y), Quaternion.identity);
                            }
                        }
                        else
                        {
                            if (state.players[x, z].Piece == PieceType.ARCHER)
                                Instantiate(archerSpent, new Vector3(state.players[x, z].PosX - board.X, temp, state.players[x, z].PosY - board.Y), Quaternion.identity);
                            else
                                Instantiate(knightSpent, new Vector3(state.players[x, z].PosX - board.X, temp, state.players[x, z].PosY - board.Y), Quaternion.identity);
                        }
                    }
                }
            }
        }
    }
}
