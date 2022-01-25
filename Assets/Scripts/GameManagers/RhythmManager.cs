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
    [SerializeField] private float timer;
    [SerializeField] private int beatCount;

    [SerializeField] private float secondsPerBeat;

    [Header("Dependencies")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TileBoardManager tileBoardManager;

    [Header("Helpers")]
    // If a beat is marked, the player cannot move again on this beat.
    [SerializeField] private bool beatMarked;

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
    /// <param name="initialDelay">How long  before the timer actually starts</param>
    public void StartCount(float bps, float initialDelay)
    {
        // 60 in music tempo equals to 1 second, use this to normalize BPS
        secondsPerBeat = 60 / bps;

        StartCoroutine(Count(initialDelay));
    }

    private IEnumerator Count(float initialDelay)
    {
        yield return new WaitForSeconds(initialDelay);

        float previousValue = 0;

        // The game starts here
        // Putting this code here is pretty yuck but eh its a game jam
        GameState.Instance.SetPlaying(true);

        while (GameState.Instance.IsPlaying())
        {
            if (!GameState.Instance.IsPaused())
            {
                // Timer is song in beat x where x is timer
                timer = ((float)AudioSettings.dspTime - AudioManager.Instance.GetSoundtrackStartTime() - secondsPerBeat * gameManager.GetDifficulty().offset) / secondsPerBeat % 1;

                if(timer < previousValue)
                {
                    if (!beatMarked)
                    {
                        gameManager.SubstractLife(true);
                    }
                    else
                    {
                        beatMarked = false;
                    }

                    beatCount++;

                    timer -= 1;
                }

                previousValue = timer;

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
        //return 1;
        return timer;
    }

    public int GetBeatCount()
    {
        return beatCount;
    }

    public bool GetBeatMarked()
    {
        return beatMarked;
    }

    public void MarkBeat()
    {
        beatMarked = true;
    }
}
