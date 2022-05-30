using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Difficulty
{
    DUMB,
    NOTDUMB
}
public enum NumPlayers
{
    PVE,
    PVP
}
public class MiniMaxBrain
{
    private Players[,] playerBase, playerTemp;
    private Players thisPlay;
    private Colour aiColour, humanColour;
    private int score, topScore, topPosX, topPosY;
    private int[,] scores;
    public Colour Colour
    {
        get { return aiColour; }
    }
    public int TopPosX
    {
        get { return topPosX; }
    }
    public int TopPosY
    {
        get { return topPosY; }
    }
    public int[,] Scores
    {
        get { return scores; }
    }
    public MiniMaxBrain(Players[,] play, Colour c, int w, int h)
    {
        playerBase = play;
        playerTemp = play;
        aiColour = c;
        if (aiColour == Colour.RED)
            humanColour = Colour.BLUE;
        else
            humanColour = Colour.RED;
        score = 0;
        topScore = int.MinValue;
        scores = new int[w,h];
    }
    

    public void doMove(int x, int y, Players[,] p, int ap)
    {
        playerTemp = p;
        thisPlay = p[x, y];
        for(int i = 0; i < scores.GetLength(0); i++)
        {
            for(int j = 0; j < scores.GetLength(1); j++)
            {
                score = MiniMax(x,y,1,playerTemp, ap);
                scores[i,j] = score;
                if(score > topScore)
                    topScore = score;
            }
        }
        List<int> TopX = new List<int>();
        List<int> TopY = new List<int>();
        int numTops = 0;
        for(int i = 0; i < scores.GetLength(0); i++)
        {
            for(int j = 0; j < scores.GetLength(1); j++)
            {
                if(scores[i,j] == topScore)
                {
                    numTops++;
                    TopX.Add(i);
                    TopY.Add(j);
                }
            }
        }
        int topFinder = Random.Range(0,numTops);
        topPosX = TopX[topFinder];
        topPosY = TopY[topFinder];
    }

    private int MiniMax(int posX, int posY, int depth, Players[,] plays, int ap)
    {
        depth++;
        bool myTurn = false;
        if (depth % 2 == 0)
            myTurn = true;
        int tempScore = 0; int topPosX = 0; int topPosY = 0; int botPosX = 0; int botPosY = 0; int topScore = 0; int botScore = 20; int count = 0;
        List<int> hPosX = new List<int>();
        List<int> hPosY = new List<int>();
        int[,] scores = new int[plays.GetLength(0),plays.GetLength(1)];
        for (int i = 0; i < plays.GetLength(0); i++)
        {
            for (int j = 0; j < plays.GetLength(1); j++)
            {
                scores[i, j] = 0;
            }
        }
        int minMovX, maxMovX, minMovY, maxMovY;
        minMovX = Mathf.Clamp(posX - ap, 0, scores.GetLength(0));
        maxMovX = Mathf.Clamp(posX + ap + 1, 0, scores.GetLength(0));
        minMovY = Mathf.Clamp(posY - ap, 0, scores.GetLength(1));
        maxMovY = Mathf.Clamp(posY + ap + 1, 0, scores.GetLength(1));
        int randX = Random.Range(minMovX, maxMovX);
        int randY = Random.Range(minMovY, maxMovY);
        plays[posX, posY].PosX = randX;
        plays[posX, posY].PosY = randY;
        plays[randX, randY] = plays[posX, posY];
        plays[posX, posY] = null;
        for (int i = 0; i < plays.GetLength(0); i++)
        {
            for (int j = 0; j < plays.GetLength(1); j++)
            {
                if (plays[i, j] != null && plays[i, j].TeamColour == humanColour)
                {
                    hPosX.Add(i);
                    hPosY.Add(j);
                    count++;
                }
                if (depth > 20)
                {
                    if (count > 0)
                    {
                        for (int k = 0; k < count; k++)
                        {
                            int distance = Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(i - hPosX[k], 2) + Mathf.Pow(j - hPosY[k], 2)));
                            tempScore = 15 - distance;
                            if (tempScore > scores[i, j])
                                scores[i, j] = tempScore;
                            if (tempScore > topScore)
                            {
                                topScore = tempScore;
                                topPosX = i;
                                topPosY = j;
                            }
                            if (tempScore < botScore)
                            {
                                botScore = tempScore;
                                botPosX = i;
                                botPosY = j;
                            }
                        }
                        return myTurn ? scores[topPosX, topPosY] : scores[botPosX, botPosY];
                    }
                    else return 100;
                }
                tempScore = MiniMax(randX, randY, depth, plays, ap);
                scores[i, j] = tempScore;
            }
        }
        for (int i = 0; i < plays.GetLength(0); i++)
        {
            for(int j = 0; j < plays.GetLength(1); j++)
            {
                int playDistance = Mathf.RoundToInt(Mathf.Sqrt(Mathf.Pow(i - randX, 2) + Mathf.Pow(j - randY, 2)));
                tempScore = scores[i, j];
                if (tempScore > topScore && playDistance < ap)
                {
                    topScore = tempScore;
                    topPosX = i;
                    topPosY = j;
                }
                if (tempScore < botScore && playDistance < ap)
                {
                    botScore = tempScore;
                    botPosX = i;
                    botPosY = j;
                }
                plays[posX, posY].PosX = topPosX;
                plays[posX, posY].PosY = topPosY;
                plays[topPosX, topPosY] = plays[posX, posY];
                plays[posX, posY] = null;
            }
        }
        return myTurn ? scores[topPosX, topPosY] : scores[botPosX, botPosY];
    }
}
