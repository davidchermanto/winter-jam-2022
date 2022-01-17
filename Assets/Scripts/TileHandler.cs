using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileHandler : MonoBehaviour
{
    [Header("Visuals")]
    [SerializeField] private SpriteRenderer tilePart_top;
    [SerializeField] private SpriteRenderer tilePart_left;
    [SerializeField] private SpriteRenderer tilePart_right;

    [Header("Link to the tileboard")]
    [SerializeField] private TileHandler nextTile;
    [SerializeField] private string correctDirection;

    [SerializeField] private DirectionBias directionBias;

    [Header("Identification")]
    [SerializeField] private int tileNumber;

    public void Setup(string correctDirection, int tileNumber, DirectionBias directionBias)
    {
        this.correctDirection = correctDirection;
        this.directionBias = directionBias;
        this.tileNumber = tileNumber;
    }

    public void SetNextTile(TileHandler nextTile)
    {
        this.nextTile = nextTile;
    }

    public void SetColors(Color top, Color left, Color right)
    {
        tilePart_top.color = top;
        tilePart_left.color = left;
        tilePart_right.color = right;
    }

    public void SetLayer(int layer)
    {
        tilePart_top.sortingLayerID = layer + Constants.tileTopOffset;
        tilePart_left.sortingLayerID = layer + Constants.tileLeftOffset;
        tilePart_right.sortingLayerID = layer + Constants.tileRightOffset;
    }

    /// <summary>
    /// Moves this tile out of sight, down, and deletes this after.
    /// </summary>
    public void OnDie()
    {
        StartCoroutine(DieAnimation());
    }

    private IEnumerator DieAnimation()
    {
        yield return new WaitForSeconds(0.016f);
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
}
