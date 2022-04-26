using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Colour
{
    RED,
    BLUE
}
public enum CurrentState
{
    SPENT,
    FULL,
    DEAD
}
public enum PieceType
{
    KNIGHT,
    ARCHER
}
public class Players
{
    private Colour teamColour;
    private CurrentState state;
    private PieceType piece;
    private bool alive, spent, selected;
    private int health, posX, posY, minRange, maxRange, damage;

    public Colour Types
    {
        get { return teamColour; }
        set { teamColour = value; }
    }
    public CurrentState State
    {
        get { return state; }
        set { state = value; }
    }
    public bool Alive
    {
        get { return alive; }
        set { alive = value; }
    }
    public bool Spent
    {
        get { return spent; }
        set { spent = value; }
    }
    public bool Selected
    {
        get { return selected; }
        set { selected = value; }
    }
    public int Health
    {
        set { health = value; }
        get { return health; }
    }
    public int PosX
    {
        get { return posX; }
    }
    public int PosY
    {
        get { return posY; }
    }
    public int Damage
    {
        get { return damage; }
    }
    public PieceType Piece
    {
        get { return piece; }
    }
    public int MinRange
    {
        get { return minRange; }
    }
    public int MaxRange
    {
        get { return maxRange; }
    }
    public Players(PieceType p, Colour c, int x, int y)
    {
        if (p == PieceType.KNIGHT)
        {
            piece = PieceType.KNIGHT;
            state = CurrentState.FULL;
            alive = true;
            teamColour = c;
            health = 10;
            damage = 6;
            minRange = 1;
            maxRange = 2;
            posX = x;
            posY = y;
            selected = false;
            spent = false;
        }
        else if (p == PieceType.ARCHER)
        { 
            piece = PieceType.ARCHER;
            state = CurrentState.FULL;
            alive = true;
            teamColour = c;
            health = 6;
            damage = 4;
            minRange = 3;
            maxRange = 5;
            posX = x;
            posY = y;
            selected = false;
            spent = false;
        }
    }
}
