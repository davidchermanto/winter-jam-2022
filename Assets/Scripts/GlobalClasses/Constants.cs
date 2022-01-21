using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    // The first tile is at layer X
    public static int initialTileLayer = 500;

    public static int tileLayerLimit = 1000;

    public static int tileTopOffset = 5;
    public static int tileLeftOffset = 3;
    public static int tileRightOffset = 4;

    // How many layers should a tile occupy?
    public static int tileLayerSize = 10;

    // The distance between tiles, to the direction "Front"
    public static float tileDistanceX = 1.7f;
    public static float tileDistanceY = 1f;

    // Initial game settings
    public static int initialTileSpawnCount = 5;

    public static int normalBias = 200;
    public static int normalTurnBias = 20;

    public static int normalBiasReduction = 5;
    public static int normalTurnBiasIncrease = 5;

    public static int maxLives = 3;

    // How combo works
    // If a move is perfect or good, the combo stays
    // If a move is bad or miss, the combo disappears
    // Miss = lose 1 life?
    //
    // Accuracy threshold for each move
    public static string perfect = "PERFECT";
    public static string good = "GOOD";
    public static string bad = "BAD";
    public static string miss = "MISS";

    public static float perfectThreshold = 0.85f;
    public static float goodThreshold = 0.7f;
    public static float badThreshold = 0.5f;

    public static int baseScore = 100;
    
    public static float perfectScoreMultiplier = 1.5f;
    public static float goodScoreMultiplier = 1f;
    public static float badScoreMultiplier = 0.5f;

    public static float bonusComboMultiplier = 0.01f;

    // Animation settings of the tile
    public static float enterDistance = 3f;
    public static float exitDistance = 2f;

    // 
    public static int tileLimit = 10000000;
    public static int rangeLimit = 20;
}
