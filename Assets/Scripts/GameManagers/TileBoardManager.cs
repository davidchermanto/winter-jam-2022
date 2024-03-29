using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBoardManager : MonoBehaviour
{
    [Header("Dependency")]
    [SerializeField] private PlayerVisualManager playerManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private UIManager uiManager;

    [Header("Game Settings")]
    [SerializeField] private Difficulty difficulty;

    [Header("Prefab")]
    [SerializeField] private GameObject tile;
    [SerializeField] private GameObject treeTile;

    [SerializeField] private GameObject obstacleKeyChange;

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

    private Vector3 initialPos;

    private char nextChar;
    private string nextDirection;

    [Header("Debug")]
    private int upCount;
    private int downCount;
    private int leftCount;
    private int rightCount;

    public void Setup()
    {
        initialPos = activeTiles[0].transform.position;
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
        TileTrace previousTrace = previousTile.GetTrace();

        bool isValid = true;

        List<string> directions = new List<string>
        {
            "up",
            "down",
            "left",
            "right"
        };

        do
        {
            // Picks a direction for the tile to connect to the previous tile
            newDirection = Directions.Instance.GetRandomDirectionWeighed(previousTile.GetDirectionBias());

            while (!(directions.Contains(newDirection)))
            {
                newDirection = Directions.Instance.GetRandomDirectionWeighed(previousTile.GetDirectionBias());
            }
            directions.Remove(newDirection);

            if(directions.Count == 0)
            {
                break;
            }
            //Debug.Log(newDirection + "("+ previousTile.GetDirectionBias().up+","+previousTile.GetDirectionBias().down+","
            //+ previousTile.GetDirectionBias().left+","+ previousTile.GetDirectionBias().right);

            tileTrace = previousTrace;
            tileTrace.id = tileNumber;

            switch (newDirection)
            {
                case "up":
                    tileTrace.y++;
                    break;
                case "down":
                    tileTrace.y--;
                    break;
                case "left":
                    tileTrace.x--;
                    break;
                case "right":
                    tileTrace.x++;
                    break;
                default:
                    break;
            }

            // Check 1: Has there been a tile here before?
            isValid = CheckTraceValidity(tileTrace);
        }
        while (!isValid);

        DirectionBias newDirectionBias = Directions.Instance.ReduceDirectionBias(previousTile, newDirection, difficulty);

        float distanceX = Constants.tileDistanceX;
        float distanceY = Constants.tileDistanceY;

        switch (newDirection)
        {
            case "up":
                upCount++;
                break;
            case "down":
                downCount++;
                distanceX *= -1;
                distanceY *= -1;
                break;
            case "left":
                leftCount++;
                distanceX *= -1;
                break;
            case "right":
                rightCount++;
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

        // So that it doesn't loop back in on itself
        switch (newDirection)
        {
            case "up":
                tileTraces.Add(new TileTrace(tileTrace.x + 1, tileTrace.y - 1, tileNumber));
                tileTraces.Add(new TileTrace(tileTrace.x - 1, tileTrace.y - 1, tileNumber));
                break;
            case "down":
                tileTraces.Add(new TileTrace(tileTrace.x + 1, tileTrace.y + 1, tileNumber));
                tileTraces.Add(new TileTrace(tileTrace.x - 1, tileTrace.y + 1, tileNumber));
                break;
            case "left":
                tileTraces.Add(new TileTrace(tileTrace.x + 1, tileTrace.y - 1, tileNumber));
                tileTraces.Add(new TileTrace(tileTrace.x + 1, tileTrace.y + 1, tileNumber));
                break;
            case "right":
                tileTraces.Add(new TileTrace(tileTrace.x - 1, tileTrace.y - 1, tileNumber));
                tileTraces.Add(new TileTrace(tileTrace.x - 1, tileTrace.y + 1, tileNumber));
                break;
            default:
                break;
        }

        ColorPack colorPack = ColorThemeManager.Instance.GetColorPack();
        tileHandler.SetColors(colorPack.brightOne, colorPack.brightTwo, colorPack.darkOne);
        tileHandler.Invisible();

        // Links the previous tile to this one like a LinkedList
        previousTile.SetNextTile(tileHandler);

        // Try to scan the list of tiletraces to find the closest x and y to this tile that is blocked, and block all tiles then
        int closestTileDistanceX = Constants.tileLimit;
        int closestTileDistanceY = Constants.tileLimit;

        TileTrace closestTraceX = new TileTrace();
        TileTrace closestTraceY = new TileTrace();

        foreach (TileTrace existingTrace in tileTraces)
        {
            if(existingTrace.x == tileTrace.x && existingTrace.y != tileTrace.y)
            {
                int range = Mathf.Abs(existingTrace.y - tileTrace.y);

                if(range < closestTileDistanceY && range < Constants.rangeLimit)
                {
                    closestTileDistanceY = range;
                    closestTraceY = existingTrace;
                }
            }

            if (existingTrace.y == tileTrace.y && existingTrace.x != tileTrace.x)
            {
                int range = Mathf.Abs(existingTrace.x - tileTrace.x);

                if (range < closestTileDistanceX && range < Constants.rangeLimit)
                {
                    closestTileDistanceX = range;
                    closestTraceX = existingTrace;
                }
            }
        }

        //Debug.Log("Y: " + closestTileY);
        //Debug.Log("X: " + closestTileX);

        if (closestTileDistanceX != Constants.tileLimit)
        {
            for(int i = 0; i < closestTileDistanceX; i++)
            {
                TileTrace newTrace = new TileTrace
                {
                    y = tileTrace.y,
                    id = tileNumber
                };

                if (tileTrace.x > closestTraceX.x)
                {
                    newTrace.x = closestTraceX.x + (i + 1);
                }
                else
                {
                    newTrace.x = closestTraceX.x - (i + 1);
                }

                tileTraces.Add(newTrace);
                //Debug.Log("Added trace: " + newTrace.x + " / " + newTrace.y);
            }
        }

        if (closestTileDistanceY != Constants.tileLimit)
        {
            for (int i = 0; i < closestTileDistanceY; i++)
            {
                TileTrace newTrace = new TileTrace
                {
                    x = tileTrace.x,
                    id = tileNumber
                };

                if (tileTrace.y > closestTraceY.y)
                {
                    newTrace.y = closestTraceY.y + (i + 1);
                }
                else
                {
                    newTrace.y = closestTraceY.y - (i + 1);
                }

                tileTraces.Add(newTrace);
                //Debug.Log("Added trace: " + newTrace.x + " / " + newTrace.y);
            }
        }

        //Debug.Log("UP: " + upCount + "  DOWN: " + downCount + "  LEFT: " + leftCount + "  RIGHT: " + rightCount);

        //Debug.Log("Generated tile number " + tileNumber + ", facing direction "+newDirection);

        
        if(tileNumber % difficulty.obstacleSpawnDelay == 0)
        {
            char randomKey = inputManager.GetRandomKey();
            string randomDirection = inputManager.GetRandomDirection();

            GameObject newObstacle = Instantiate(obstacleKeyChange);
            ObstacleKeyChange obstacleKey = newObstacle.GetComponent<ObstacleKeyChange>();

            obstacleKey.Setup(randomDirection, randomKey);
            obstacleKey.SetLayer(tileHandler.GetLayer());
            obstacleKey.SyncPosition(tileHandler);

            tileHandler.AddObstacle(newObstacle);
        }

        if(tileNumber % Constants.treeSpawnDelay - Mathf.Max(Mathf.FloorToInt(tileNumber / Constants.treeSpawnDelayReductionDelay), 1) == 0 && tileNumber > 5)
        {
            for(int i = 0; i < Random.Range(1, 3); i++)
            {
                SpawnTreeTile();
            }
        }

        previousTile = tileHandler;
    }

    // If a tile has been there before, refuse to generate
    private bool CheckTraceValidity(TileTrace tileTrace)
    {
        foreach(TileTrace existingTrace in tileTraces)
        {
            if(tileTrace.x == existingTrace.x && tileTrace.y == existingTrace.y)
            {
                //Debug.Log(tileTrace.x + "/" + tileTrace.y + " VS " + existingTrace.x + "/" + existingTrace.y);
                return false;
            }
        }

        return true;
    }

    // Checks but based on active tiles and dead tiles instead
    private bool CheckTileValidity(TileTrace tileTrace)
    {
        foreach(TileHandler tileHandler in activeTiles)
        {
            if(tileTrace.x == tileHandler.GetTrace().x && tileTrace.x == tileHandler.GetTrace().y)
            {
                return false;
            }
        }

        foreach (TileHandler tileHandler in deadTiles)
        {
            if (tileTrace.x == tileHandler.GetTrace().x && tileTrace.x == tileHandler.GetTrace().y)
            {
                return false;
            }
        }

        return true;
    }

    public void SpawnTreeTile()
    {
        // get current position
        TileTrace tileTrace = playerTile.GetTrace();

        // select a random tile within 20 tile
        tileTrace.x = Random.Range(tileTrace.x - Constants.treeSpawnDistance, tileTrace.x + Constants.treeSpawnDistance);
        tileTrace.y = Random.Range(tileTrace.y - Constants.treeSpawnDistance, tileTrace.y + Constants.treeSpawnDistance);

        // check traces
        if (CheckTraceValidity(tileTrace))
        {
            // generate a tile with a tree on top of it
            GameObject treeTile = Instantiate(this.treeTile);

            treeTile.transform.SetParent(tilesFolder.transform);

            // Calculate layer where it should be
            // I hate vectors, the next two lines was harder than the procedural algorithm
            int layer = Constants.initialTileLayer - (tileTrace.y + -tileTrace.x) * Constants.tileLayerSize;

            Vector3 correctPos = new Vector3((tileTrace.y + tileTrace.x) * Constants.tileDistanceX, ((tileTrace.y - tileTrace.x) * Constants.tileDistanceY) - 5);

            TileHandler newTileHandler = treeTile.GetComponent<TileHandler>();
            newTileHandler.Setup(null, tileNumber, new DirectionBias(), correctPos, tileTrace, true);

            ColorPack colorPack = ColorThemeManager.Instance.GetColorPack();
            newTileHandler.SetColors(colorPack.brightOne, colorPack.brightTwo, colorPack.darkOne);
            newTileHandler.Invisible();

            newTileHandler.SetLayer(layer);

            tileTraces.Add(tileTrace);

            activeTiles.Add(newTileHandler);

            // tree module?
            TreeModule treeModule = treeTile.GetComponent<TreeModule>();
            treeModule.Setup(layer);
        }
    }

    /// <summary>
    /// When player moves to a new tile, do this to the board
    /// </summary>
    public void OnPlayerMove(string direction)
    {
        if(nextDirection != null)
        {

            nextDirection = null;
        }

        deadTiles.Add(playerTile);

        playerTile = playerTile.GetNextTile();

        // If the dead tiles reach a certain amount, destroy them.
        if (deadTiles.Count > Constants.deadTilesLimit)
        {
            activeTiles.Remove(deadTiles[0]);

            RemoveTraces(deadTiles[0].GetTileNumber());

            deadTiles.RemoveAt(0);
            deadTiles[0].OnDie();
        }

        GameObject obstacle = playerTile.GetObstacle();

        if (obstacle != null)
        {
            ObstacleKeyChange obstacleKeyChange = obstacle.GetComponent<ObstacleKeyChange>();
            obstacleKeyChange.Take();

            char newKey = obstacleKeyChange.GetKey();
            string newDirection = obstacleKeyChange.GetDirection();

            nextChar = newKey;
            nextDirection = newDirection;

            inputManager.SetKeyCode(nextChar, nextDirection);
            uiManager.UpdateKey(nextDirection, nextChar);

            ColorThemeManager.Instance.GenerateColorForDifficulty(difficulty);
            uiManager.TweenColors();
        }

        SpawnNextTile();
    }

    public Vector3 CalculateRealPosition(TileHandler tileHandler, string direction = null)
    {
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
                distanceX = 0;
                distanceY = 0;
                break;
        }

        float vectorX = tileHandler.GetCorrectPosition().x;
        float vectorY = tileHandler.GetCorrectPosition().y;

        return new Vector3(vectorX  + distanceX, vectorY + distanceY);
    }

    private void RemoveTraces(int tracesID)
    {
        List<TileTrace> removeableTraces = new List<TileTrace>();

        foreach(TileTrace tileTrace in tileTraces)
        {
            if(tileTrace.id == tracesID && !tileTrace.permanent)
            {
                removeableTraces.Add(tileTrace);
            }
        }

        foreach(TileTrace tileTrace in removeableTraces)
        {
            if (tileTraces.Contains(tileTrace))
            {
                tileTraces.Remove(tileTrace);
            }
        }
    }

    public void ResetVariables()
    {
        // destroy active tiles
        foreach(TileHandler tileHandler in activeTiles)
        {
            if(tileHandler != null)
            {
                tileHandler.OnDie();
            }
        }

        activeTiles = new List<TileHandler>();

        // destroy dead tiles
        foreach (TileHandler tileHandler in deadTiles)
        {
            if (tileHandler != null)
            {
                tileHandler.OnDie();
            }
        }

        deadTiles = new List<TileHandler>();

        // destroy tile traces
        tileTraces = new List<TileTrace>();

        // reset tile count
        tileNumber = 0;

        // generate initial player tile;
        GameObject newTile = Instantiate(tile);
        newTile.transform.position = initialPos;
        newTile.transform.SetParent(tilesFolder.transform);

        TileHandler newTileHandler = newTile.GetComponent<TileHandler>();
        newTileHandler.Setup("up", tileNumber, new DirectionBias(200, 200, 200, 200), initialPos, new TileTrace(0, 0, tileNumber), false);
        newTileHandler.SetLayer(Constants.initialTileLayer);

        playerTile = newTileHandler;
        previousTile = newTileHandler;

        activeTiles.Add(newTileHandler);
        deadTiles.Add(newTileHandler);
    }

    public void SetDifficulty(Difficulty difficulty)
    {
        this.difficulty = difficulty;
    }

    public void SetPlayerTile(TileHandler tileHandler)
    {
        playerTile = tileHandler;
    }

    public TileHandler GetPlayerTile()
    {
        return playerTile;
    }

    public TileHandler GetPreviousTile()
    {
        return previousTile;
    }

    public List<TileHandler> GetActiveTiles()
    {
        return activeTiles;
    }
}
