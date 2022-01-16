using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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