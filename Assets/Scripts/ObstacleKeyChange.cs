using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class ObstacleKeyChange : MonoBehaviour
{
    [SerializeField] private char key;
    [SerializeField] private string direction;

    [SerializeField] private TextMeshPro text;

    [SerializeField] private GameObject upArrow;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;
    [SerializeField] private GameObject downArrow;

    public void Setup(string direction, char key)
    {
        this.direction = direction;

        upArrow.SetActive(false);
        downArrow.SetActive(false);
        leftArrow.SetActive(false);
        rightArrow.SetActive(false);

        switch (direction)
        {
            case "up":
                upArrow.SetActive(true);
                break;
            case "down":
                downArrow.SetActive(true);
                break;
            case "left":
                leftArrow.SetActive(true);
                break;
            case "right":
                rightArrow.SetActive(true);
                break;
            default:
                Debug.LogError("Weird direction on obstacle generation");
                break;
        }

        this.key = key;

        string keyString = "" + key;
        text.SetText(keyString);
    }

    public void SetLayer(int newLayer)
    {
        text.sortingOrder = newLayer + Constants.playerSpriteOffset;

        upArrow.GetComponent<SpriteRenderer>().sortingOrder = newLayer + Constants.playerSpriteOffset;
        leftArrow.GetComponent<SpriteRenderer>().sortingOrder = newLayer + Constants.playerSpriteOffset;
        rightArrow.GetComponent<SpriteRenderer>().sortingOrder = newLayer + Constants.playerSpriteOffset;
        downArrow.GetComponent<SpriteRenderer>().sortingOrder = newLayer + Constants.playerSpriteOffset;
    }

    public void SyncPosition(TileHandler tileHandler)
    {
        Vector3 pos = tileHandler.GetCorrectPosition();

        transform.position = new Vector3(pos.x, pos.y + 1.7f, pos.z);
    }

    public void Take()
    {
        // disappear animation
    }

    public char GetKey()
    {
        return key;
    }

    public string GetDirection()
    {
        return direction;
    }

    private IEnumerator Appear()
    {

        yield return new WaitForEndOfFrame();
    }

    private IEnumerator Disappear()
    {

        yield return new WaitForEndOfFrame();
    }
}
