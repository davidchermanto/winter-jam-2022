using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoardManager : MonoBehaviour
{
    [Header("Dependency")]
    [SerializeField] private PlayerManager playerManager;

    [Header("Game Settings")]
    [SerializeField] private Difficulty difficulty;

    [Header("Prefab")]
    [SerializeField] private GameObject tile;

    [Header("Dynamic Variables")]
    [SerializeField] private int tileNumber;

    // The tile where the player is on
    [SerializeField] private TileHandler playerTile;

    // Will contain the last tile generated
    [SerializeField] private TileHandler previousTile;

    [SerializeField] private List<TileHandler> activeTiles;
    [SerializeField] private List<TileHandler> deadTiles;

    [Header("Constants")]
    [SerializeField] private GameObject tilesFolder;

    public void Setup()
    {

    }

    public void StartGenerate()
    {
        SpawnFirstTiles();
    }

    /// <summary>
    /// Spawns tiles with the assumption that the lastTenTiles list is empty
    /// </summary>
    public void SpawnFirstTiles()
    {
        StartCoroutine(SpawnTileDelay(Constants.initialTileSpawnCount));
    }

    /// <summary>
    /// Spawns multiple tiles consecutively.
    /// </summary>
    /// <param name="count">How many tiles?</param>
    /// <param name="delay">How long between each tiles? 1 Frame is the minimum.</param>
    /// <returns></returns>
    private IEnumerator SpawnTileDelay(int count = 1, float delay = 0.5f)
    {
        float timer = 0;

        for(int i = 0; i < count; i++)
        {
            while(timer < 1)
            {
                timer += Time.deltaTime / delay;

                yield return new WaitForEndOfFrame();
            }

            SpawnNextTile();
            timer -= delay;
        }
    }

    /// <summary>
    /// Extends the level by 1 tile
    /// </summary>
    public void SpawnNextTile()
    {
        int initialTileLayer = Constants.initialTileLayer;
        int tileLayerSize = Constants.tileLayerSize;

        tileNumber++;

        GameObject tile = Instantiate(this.tile);
        tile.name = "IsometricTile_" + tileNumber;

        TileHandler tileHandler = tile.GetComponent<TileHandler>();

        activeTiles.Add(tileHandler);

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
        if (newDirection.Equals("up") || newDirection.Equals("left"))
        {
            tileHandler.SetLayer(previousTile.GetLayer() - tileLayerSize);
        }
        else
        {
            tileHandler.SetLayer(previousTile.GetLayer() + tileLayerSize);
        }

        tile.transform.SetParent(tilesFolder.transform);

        Vector3 oldPosition = previousTile.GetCorrectPosition();

        tileHandler.Setup(newDirection, tileNumber, newDirectionBias, new Vector3(oldPosition.x + distanceX, oldPosition.y + distanceY));

        ColorPack colorPack = ColorThemeManager.Instance.GetColorPack();
        tileHandler.SetColors(colorPack.brightOne, colorPack.brightTwo, colorPack.darkOne);

        // Links the previous tile to this one like a LinkedList
        previousTile.SetNextTile(tileHandler);

        //Debug.Log("Generated tile number " + tileNumber + ", facing direction "+newDirection);

        previousTile = tileHandler;
    }

    /// <summary>
    /// When player moves to a new tile, do this to the board
    /// </summary>
    public void OnPlayerMove(string direction)
    {
        

        SpawnNextTile();
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        this.difficulty = difficulty;
    }
}
