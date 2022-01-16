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
    private GameObject nextTile;
    private GameObject previousTile;

    private string directionFromPreviousTile;
    private string directionToNextTile;

    [Header("Identification")]
    [SerializeField] private int tileNumber;

    public void Setup()
    {

    }

    public void SetColors(Color top, Color left, Color right)
    {

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
    public void MoveDown()
    {

    }
}
