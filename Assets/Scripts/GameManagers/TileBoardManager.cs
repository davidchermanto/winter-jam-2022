using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoardManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject tile;

    [Header("Dynamic Variables")]
    [SerializeField] private int tileNumber;

    // Will contain the last 10 tiles to calculate where to generate tiles next
    // Analyze this list's positioning so that the next tiles don't snake back to itself
    private List<TileHandler> lastTenTiles;

    public void Setup()
    {

    }

    /// <summary>
    /// Spawns tiles with the assumption that the lastTenTiles list is empty
    /// </summary>
    public void SpawnFirstTiles(int count = 10)
    {

    }

    /// <summary>
    /// Extends the level
    /// </summary>
    public void SpawnNextTile()
    {
        

        tileNumber++;
    }
}
