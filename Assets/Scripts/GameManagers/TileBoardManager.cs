using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoardManager : MonoBehaviour
{
    [Header("Prefab")]
    [SerializeField] private GameObject tile;

    [Header("Dynamic Variables")]
    [SerializeField] private int tileNumber;

    // Will contain the last tile generated
    [SerializeField] private TileHandler previousTile;

    [SerializeField] private List<TileHandler> activeTiles;
    [SerializeField] private List<TileHandler> deadTiles;

    public void Setup()
    {

    }

    /// <summary>
    /// Spawns tiles with the assumption that the lastTenTiles list is empty
    /// </summary>
    public void SpawnFirstTiles()
    {
        int initialTileLayer = Constants.initialTileLayer;
        int tileLayerSize = Constants.tileLayerSize;

        for(int i = 0; i < Constants.initialTileSpawnCount; i++)
        {
            GameObject tile = Instantiate(this.tile);
            TileHandler tileHandler = tile.GetComponent<TileHandler>();

            activeTiles.Add(tileHandler);

            // TODO: Set the color of the tile

            // Picks a direction for the tile to connect to the previous tile
            // TODO: Give bias to previous travelled direction (If before = front, then more likely next to be front)
            string direction = Directions.Instance.GetRandomDirectionWeighed(new DirectionBias(100, 0, 20, 20));

            float distanceX = Constants.tileDistanceX;
            float distanceY = Constants.tileDistanceY;

            switch (direction)
            {
                case "up":
                    break;
                case "down":
                    distanceX *= -1;
                    distanceY *= -1;
                    break;
                case "left":
                    distanceX *= -1;
                    break;
                case "right":
                    distanceY *= -1;
                    break;
                default:
                    break;
            }

            // Going up or left means that the new generated tile will be behind the old tile visually
            if(direction.Equals("up") || direction.Equals("left"))
            {
                tileHandler.SetLayer(initialTileLayer - i * tileLayerSize);
            }
            else
            {
                tileHandler.SetLayer(initialTileLayer + i * tileLayerSize);
            }

            Vector3 oldPosition = previousTile.transform.position;
            tile.transform.position = new Vector3(oldPosition.x + distanceX, oldPosition.y + distanceY);

            tileHandler = previousTile;
        }
    }

    /// <summary>
    /// Extends the level
    /// </summary>
    public void SpawnNextTile()
    {
        

        tileNumber++;
    }
}
