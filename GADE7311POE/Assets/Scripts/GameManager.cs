using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private string mapPath;
    private BoardGenerate board;

    public GameObject grass, hill1, hill2, hill3, rock, ruinF, ruinW; 
    // Start is called before the first frame update
    void Start()
    {
        mapPath = Application.dataPath + "/DefaultMap.txt";
        string[] map = File.ReadAllLines(mapPath);
        Debug.Log(mapPath);
        board = new BoardGenerate(map[0].Length, map.Length, map);
        //GameObject.Find("CameraPivot").transform.position = new Vector3(board.X, 0, board.Y);
        Board();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PassOn(int x, int y, string s)
    {

    }

    private void Board()
    {
        //GameObject[] clouds = GameObject.FindGameObjectsWithTag("Tile");
        //foreach (GameObject obj in clouds) { Destroy(obj); }
        for (int x = 0; x < board.Width; x++)
        {
            for (int z = 0; z < board.Height; z++)
            {
                switch (board.Tiles[x, z])
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
    }
}
