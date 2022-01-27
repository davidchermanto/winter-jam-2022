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

    [SerializeField] private ParticleSystem black;
    [SerializeField] private ParticleSystem gray;

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

        StartCoroutine(Shift(true));
    }

    private void Update()
    {
        
    }

    public void SetLayer(int newLayer)
    {
        text.sortingOrder = newLayer + Constants.playerSpriteOffset;

        upArrow.GetComponent<SpriteRenderer>().sortingOrder = newLayer + Constants.playerSpriteOffset;
        leftArrow.GetComponent<SpriteRenderer>().sortingOrder = newLayer + Constants.playerSpriteOffset;
        rightArrow.GetComponent<SpriteRenderer>().sortingOrder = newLayer + Constants.playerSpriteOffset;
        downArrow.GetComponent<SpriteRenderer>().sortingOrder = newLayer + Constants.playerSpriteOffset;

        black.GetComponent<ParticleSystemRenderer>().sortingOrder = newLayer + Constants.playerShadowOffset;
        gray.GetComponent<ParticleSystemRenderer>().sortingOrder = newLayer + Constants.playerShadowOffset;
    }

    public void SyncPosition(TileHandler tileHandler)
    {
        Vector3 pos = tileHandler.GetCorrectPosition();

        transform.position = new Vector3(pos.x, pos.y + 1.7f, pos.z);
    }

    public void Take()
    {
        StartCoroutine(Shift(false));
        
    }

    public char GetKey()
    {
        return key;
    }

    public string GetDirection()
    {
        return direction;
    }

    private IEnumerator Shift(bool appear, float appearTime = 1f)
    {
        float timer = 0;

        Color target;
        Color initial;

        if (appear)
        {
            target = text.color;
            initial = new Color(target.r, target.g, target.b, 0);
        }
        else
        {
            initial = text.color;
            target = new Color(initial.r, initial.g, initial.b, 0);
        }

        while (timer < 1)
        {
            timer += Time.deltaTime / appearTime;

            text.color = Color.Lerp(initial, target, timer);

            if (upArrow.activeInHierarchy)
            {
                upArrow.GetComponent<SpriteRenderer>().color = Color.Lerp(initial, target, timer);
            }
            else if (downArrow.activeInHierarchy)
            {
                downArrow.GetComponent<SpriteRenderer>().color = Color.Lerp(initial, target, timer);
            }
            else if (leftArrow.activeInHierarchy)
            {
                leftArrow.GetComponent<SpriteRenderer>().color = Color.Lerp(initial, target, timer);
            }
            else if (rightArrow.activeInHierarchy)
            {
                rightArrow.GetComponent<SpriteRenderer>().color = Color.Lerp(initial, target, timer);
            }
            else
            {
                Debug.LogError("How did we get here? No arrow found active");
            }

            yield return new WaitForEndOfFrame();
        }

        if (!appear)
        {
            Destroy(gameObject);
        }
    }
}
