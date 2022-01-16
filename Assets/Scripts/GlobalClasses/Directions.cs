using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Directions
{
    public static Directions Instance;

    public string GetRandomDirection()
    {
        int random = Random.Range(0, 3);

        switch (random)
        {
            case 0:
                return "up";
            case 1:
                return "down";
            case 2:
                return "left";
            case 3:
                return "right";
            default:
                Debug.LogError("Something terrible has happened, check Directions class");
                return null;
        }
    }

    /// <summary>
    /// Returns a random direction, but the higher the weight given, the more likely it is to appear.
    /// </summary>
    /// <param name="up"></param>
    /// <param name="down"></param>
    /// <param name="left"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public string GetRandomDirectionWeighed(DirectionBias directionBias)
    {
        // Example
        // up down left right
        // 20  5    10   10
        //
        // Total : 45
        //
        // Picks a random number between 0 and 45
        // Gets 34
        //
        // 20 < 34, its not up
        // 20 + 5 < 34, its not down
        // 20 + 5 + 10 > 35, its left

        int up = directionBias.up;
        int down = directionBias.down;
        int left = directionBias.left;
        int right = directionBias.right;

        int totalWeight = up + down + left + right;

        int random = Random.Range(0, totalWeight);

        // There are better solutions but its 1 AM and I'm tired
        if(up > random)
        {
            return "up";
        }
        else if(up + down > random)
        {
            return "down";
        }
        else if(up + down + left > random)
        {
            return "left";
        }
        else
        {
            return "right";
        }
    }
}
