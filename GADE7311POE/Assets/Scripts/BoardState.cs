using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Selection
{
    NONE,
    SELECTRED,
    SELECTBLUE
}

public class BoardState
{
    private int posx, posy;
    private bool selected;
    private Selection select;
    private BoardSpace space;

    public int PosX
    { 
        get { return posx; } 
    }
    public int PosY
    {
        get { return posy; }
    }
    public bool Selected
    {
        get { return selected; }
        set { selected = value; }
    }
    public Selection Select
    {
        get { return select; }
        set { select = value; }
    }
    public BoardSpace Space
    {
        get { return space; }
    }
    public BoardState(int x, int y, BoardSpace s)
    {
        posx = x;
        posy = y;
        selected = false;
        select = Selection.NONE;
        space = s;
    }
}
