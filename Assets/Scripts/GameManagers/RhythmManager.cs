using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmManager : MonoBehaviour
{
    public static RhythmManager Instance;

    /// <summary>
    /// Timer
    /// Get this value to determine how far in a beat are you
    /// If you call
    /// </summary>
    private float timer;
    private int beatCount;

    private float secondsPerBeat;

    private void Awake()
    {
        Instance = this;
    }

    public void Setup()
    {

    }

    /// <summary>
    /// Starts the process of counting beats within the coroutine
    /// </summary>
    /// <param name="bps">Beats per second</param>
    /// <param name="beatDelay">How many beats before the timer actually starts</param>
    public void StartCount(float bps, int beatDelay)
    {
        // 60 in music tempo equals to 1 second, use this to normalize BPS
        secondsPerBeat = 60 / bps;

        StartCoroutine(Count(beatDelay));
    }

    private IEnumerator Count(int beatDelay)
    {
        yield return new WaitForSeconds(secondsPerBeat * beatDelay);

        while (GameState.Instance.IsPlaying())
        {
            if (!GameState.Instance.IsPaused())
            {
                timer += Time.deltaTime / secondsPerBeat;

                if(timer >= 1)
                {
                    beatCount++;

                    timer -= 1;
                }

                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void ResetCount()
    {
        timer = 0;
        beatCount = 0;
    }

    public float GetTimer()
    {
        return timer;
    }

    public int GetBeatCount()
    {
        return beatCount;
    }
}
