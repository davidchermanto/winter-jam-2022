using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Weather());
    }

    private IEnumerator Weather()
    {
        yield return new WaitForSeconds(5f);

        AudioManager.Instance.PlayWeather("rain", true);
    }
}
