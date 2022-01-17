using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleSqueeze : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    [SerializeField] private float maxSqueezeValueY;
    [SerializeField] private float maxSqueezeValueX;

    [SerializeField] private float squeezeDurationUp;
    [SerializeField] private float squeezeDurationDown;

    private bool goingUp;

    private Vector3 normalSize;
    private Vector3 squeezedSize;

    private float timer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        goingUp = false;

        Vector3 currentScale = spriteRenderer.transform.localScale;

        normalSize = new Vector3(currentScale.x, currentScale.y, currentScale.z);
        squeezedSize = new Vector3(maxSqueezeValueX * currentScale.x, maxSqueezeValueY * currentScale.y, currentScale.z);
    }

    void Update()
    {
        if (goingUp)
        {
            timer += Time.deltaTime / squeezeDurationUp;

            spriteRenderer.transform.localScale = Vector3.Lerp(squeezedSize, normalSize, timer);

            if (timer >= 1)
            {
                timer = 0;
                goingUp = false;
            }
        }
        else
        {
            timer += Time.deltaTime / squeezeDurationDown;

            spriteRenderer.transform.localScale = Vector3.Lerp(normalSize, squeezedSize, timer);

            if(timer >= 1)
            {
                timer = 0;
                goingUp = true;
            }
        }
    }
}
