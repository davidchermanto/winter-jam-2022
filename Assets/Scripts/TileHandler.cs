using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private SpriteRenderer tilePart_top;
    [SerializeField] private SpriteRenderer tilePart_left;
    [SerializeField] private SpriteRenderer tilePart_right;

    [SerializeField] private int baseLayer;
    [SerializeField] private Vector3 correctPosition;

    [Header("Link to the tileboard")]
    [SerializeField] private TileHandler nextTile;
    [SerializeField] private string correctDirection;

    [SerializeField] private DirectionBias directionBias;

    [Header("Identification")]
    [SerializeField] private int tileNumber;
    [SerializeField] private TileTrace tileTrace;

    public void Setup(string correctDirection, int tileNumber, DirectionBias directionBias, Vector3 correctPosition, TileTrace tileTrace)
    {
        this.correctDirection = correctDirection;
        this.directionBias = directionBias;
        this.tileNumber = tileNumber;

        this.correctPosition = correctPosition;
        transform.position = correctPosition;

        this.tileTrace = tileTrace;

        StartCoroutine(SpawnAnimation());
    }

    private IEnumerator SpawnAnimation(float duration = 0.4f)
    {
        float timer = 0;

        Vector3 initialPosition = transform.position;
        Vector3 spawnPosition = new Vector3(initialPosition.x, initialPosition.y - Constants.enterDistance, initialPosition.z);

        transform.position = spawnPosition;

        while(timer < 1)
        {
            timer += Time.deltaTime / duration;

            transform.position = Vector3.Lerp(spawnPosition, initialPosition, timer);

            tilePart_top.color = new Color(tilePart_top.color.r, tilePart_top.color.g, tilePart_top.color.b, timer);
            tilePart_left.color = new Color(tilePart_left.color.r, tilePart_left.color.g, tilePart_left.color.b, timer);
            tilePart_right.color = new Color(tilePart_right.color.r, tilePart_right.color.g, tilePart_right.color.b, timer);

            yield return new WaitForEndOfFrame();
        }

        
    }

    /// <summary>
    /// Moves this tile out of sight, down, and deletes this after.
    /// </summary>
    public void OnDie()
    {
        StartCoroutine(DieAnimation());
    }

    private IEnumerator DieAnimation(float duration = 0.5f)
    {
        yield return new WaitForSeconds(0.016f);
    }

    public TileHandler GetNextTile()
    {
        return nextTile;
    }

    public void SetNextTile(TileHandler nextTile)
    {
        this.nextTile = nextTile;
    }

    public TileTrace GetTrace()
    {
        return tileTrace;
    }

    public void SetColors(Color top, Color left, Color right)
    {
        tilePart_top.color = top;
        tilePart_left.color = left;
        tilePart_right.color = right;
    }

    public void SetLayer(int layer)
    {
        baseLayer = layer;

        tilePart_top.sortingOrder = layer + Constants.tileTopOffset;
        tilePart_left.sortingOrder = layer + Constants.tileLeftOffset;
        tilePart_right.sortingOrder = layer + Constants.tileRightOffset;
    }

    public int GetLayer()
    {
        return baseLayer;
    }

    public DirectionBias GetDirectionBias()
    {
        return directionBias;
    }

    public string GetCorrectDirection()
    {
        return correctDirection;
    }

    public int GetTileNumber()
    {
        return tileNumber;
    }

    public Vector3 GetCorrectPosition()
    {
        return correctPosition;
    }
}
