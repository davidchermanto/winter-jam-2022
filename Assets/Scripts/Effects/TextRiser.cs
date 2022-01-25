using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class TextRiser : MonoBehaviour
{
    private TextMeshProUGUI text;
    private float timer;
    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private Color initialColor;
    private Color targetColor;

    [SerializeField] private float duration;
    [SerializeField] private float distanceY;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();

        initialPosition = transform.localPosition;
        targetPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + distanceY, 0);

        initialColor = text.color;
        targetColor = new Color(text.color.r, text.color.g, text.color.b, 0f);
    }

    private void Update()
    {
        timer += Time.deltaTime / duration;

        if(timer >= 1)
        {
            Destroy(gameObject);
        }

        text.color = Color.Lerp(initialColor, targetColor, timer);
        transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, timer);
    }
}
