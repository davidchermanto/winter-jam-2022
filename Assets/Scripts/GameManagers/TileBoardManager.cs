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

    // Contains all list of X, Y where a tile once spawned and prevents them from spawning there again
    [SerializeField] private List<TileTrace> tileTraces;

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

        string newDirection = "";
        int generationAttempt = 0;

        TileTrace tileTrace = new TileTrace();

        bool isValid;

        do
        {
            // Picks a direction for the tile to connect to the previous tile
            newDirection = Directions.Instance.GetRandomDirectionWeighed(previousTile.GetDirectionBias());

            TileTrace previousTrace = previousTile.GetTrace();

            TileTrace twoTilesForward = previousTrace;
            tileTrace = previousTrace;

            switch (newDirection)
            {
                case "up":
                    tileTrace.y++;
                    twoTilesForward.y += 2;
                    break;
                case "down":
                    tileTrace.y--;
                    twoTilesForward.y -= 2;
                    break;
                case "left":
                    tileTrace.x--;
                    twoTilesForward.x -= 2;
                    break;
                case "right":
                    tileTrace.x++;
                    twoTilesForward.x += 2;
                    break;
                default:
                    break;
            }

            // Check 1: Has there been a tile here before?
            // Check 2: Has there been a tile here 2 tiles in front before?
            isValid = CheckTraceValidity(tileTrace) && CheckTraceValidity(twoTilesForward);

            generationAttempt++;

            if(generationAttempt > 30)
            {
                // Give up
                break;
            }
        }
        while (!isValid);

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

        tileHandler.Setup(newDirection, tileNumber, newDirectionBias, new Vector3(oldPosition.x + distanceX, oldPosition.y + distanceY), tileTrace);

        activeTiles.Add(tileHandler);
        tileTraces.Add(tileTrace);

        ColorPack colorPack = ColorThemeManager.Instance.GetColorPack();
        tileHandler.SetColors(colorPack.brightOne, colorPack.brightTwo, colorPack.darkOne);

        // Links the previous tile to this one like a LinkedList
        previousTile.SetNextTile(tileHandler);

        //Debug.Log("Generated tile number " + tileNumber + ", facing direction "+newDirection);

        previousTile = tileHandler;
    }

    // If a tile has been there before, refuse to generate
    private bool CheckTraceValidity(TileTrace tileTrace)
    {
        foreach(TileTrace existingTrace in tileTraces)
        {
            if(tileTrace.x == existingTrace.x && tileTrace.y == existingTrace.y)
            {
                return false;
            }
        }

        return true;
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
