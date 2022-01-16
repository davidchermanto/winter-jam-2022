using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Constants
{
    // The first tile is at layer X
    public static int initialTileLayer = 500;

    public static int tileTopOffset = 5;
    public static int tileLeftOffset = 3;
    public static int tileRightOffset = 4;

    // How many layers should a tile occupy?
    public static int tileLayerSize = 10;

    // The distance between tiles, to the direction "Front"
    public static float tileDistanceX = 1.7f;
    public static float tileDistanceY = 1f;

    // Initial game settings
    // Please make sure this is below 10
    public static int initialTileSpawnCount = 5;
}
