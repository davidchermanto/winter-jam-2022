using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct DirectionBias
{
    public int up;
    public int down;
    public int left;
    public int right;

    public DirectionBias(int up, int down, int left, int right)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
    }
}

[System.Serializable]
public struct Difficulty
{
    public string name;
    public float tempo;

    // How many beats before an item appears
    public int obstacleSpawnDelay;
    
    // Higher means more turning around 
    public int biasModifier;

    public float scoreMultiplier;

    public float offset;
}

[System.Serializable]
public struct HSVColor
{
    public float hue;
    public float saturation;
    public float value;
}

[System.Serializable]
public struct ColorPack
{
    public Color brightOne;
    public Color brightTwo;
    public Color darkOne;
    public Color darkTwo;
    public Color antaOne;
    public Color antaTwo;

    public ColorPack(Color brightOne, Color brightTwo, Color darkOne, Color darkTwo, Color antaOne, Color antaTwo)
    {
        this.brightOne = brightOne;
        this.brightTwo = brightTwo;
        this.darkOne = darkOne;
        this.darkTwo = darkTwo;
        this.antaOne = antaOne;
        this.antaTwo = antaTwo;
    }
}

[System.Serializable]
public struct TileTrace
{
    public int x;
    public int y;
    public int id;
    public bool permanent;

    public TileTrace(int x, int y, int id)
    {
        this.x = x;
        this.y = y;
        this.id = id;

        permanent = false;
    }
}