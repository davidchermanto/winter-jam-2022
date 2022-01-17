using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoardManager : MonoBehaviour
{
    [Header("Game Settings")]
    [SerializeField] private Difficulty difficulty;

    [Header("Prefab")]
    [SerializeField] private GameObject tile;

    [Header("Dynamic Variables")]
    [SerializeField] private int tileNumber;

    // Will contain the last tile generated
    [SerializeField] private TileHandler previousTile;

    [SerializeField] private List<TileHandler> activeTiles;
    [SerializeField] private List<TileHandler> deadTiles;

    public void Setup(Difficulty difficulty)
    {
        this.difficulty = difficulty;
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
            tileNumber++;

            GameObject tile = Instantiate(this.tile);
            TileHandler tileHandler = tile.GetComponent<TileHandler>();

            activeTiles.Add(tileHandler);

            // TODO: Set the color of the tile

            // Picks a direction for the tile to connect to the previous tile
            string newDirection = Directions.Instance.GetRandomDirectionWeighed(previousTile.GetDirectionBias());

            DirectionBias newDirectionBias = Directions.Instance.ReduceDirectionBias(previousTile, newDirection, difficulty);

            float distanceX = Constants.tileDistanceX;
            float distanceY = Constants.tileDistanceY;

            switch (newDirection)
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
            if(newDirection.Equals("up") || newDirection.Equals("left"))
            {
                tileHandler.SetLayer(initialTileLayer - i * tileLayerSize);
            }
            else
            {
                tileHandler.SetLayer(initialTileLayer + i * tileLayerSize);
            }

            Vector3 oldPosition = previousTile.transform.position;
            tile.transform.position = new Vector3(oldPosition.x + distanceX, oldPosition.y + distanceY);

            tileHandler.Setup(newDirection, tileNumber, newDirectionBias);

            // Links the previous tile to this one like a LinkedList
            previousTile.SetNextTile(tileHandler);

            previousTile = tileHandler;
        }
    }

    /// <summary>
    /// Extends the level
    /// </summary>
    public void SpawnNextTile()
    {
        tileNumber++;

    }

    /// <summary>
    /// When player moves to a new tile, do this to the board
    /// </summary>
    public void OnPlayerMove(string direction)
    {


        SpawnNextTile();
    }
}
