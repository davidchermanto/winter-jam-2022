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

    public AudioClip theme;
    public float timing;

    // How many beats before an item appears
    public int obstacleSpawnDelay;
    
    // Higher means more turning around 
    public int biasModifier;
}