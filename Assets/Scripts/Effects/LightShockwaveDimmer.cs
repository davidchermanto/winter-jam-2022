using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Experimental.Rendering.Universal;

public class LightShockwaveDimmer : MonoBehaviour
{
    private Light2D activeLight;
    private float timer = 0;
    private float initialIntensity;
    private Vector3 initialScale;

    [SerializeField] private Vector3 zoomOutMax;
    [SerializeField] private float duration;

    private void Start()
    {
        activeLight = GetComponent<Light2D>();
        initialIntensity = activeLight.intensity;
        initialScale = transform.localScale;
    }

    private void Update()
    {
        timer += Time.deltaTime / duration;

        activeLight.intensity = initialIntensity * (1 - timer);

        if (timer < 0.5f)
        {
            transform.localScale = Vector3.Lerp(initialScale, zoomOutMax, timer * 1.5f);
        }
        else
        {
            transform.localScale = Vector3.Lerp(initialScale, zoomOutMax, timer * 0.5f + 0.5f);
        }


        if(timer >= 1)
        {
            Destroy(gameObject);
        }
    }
}
