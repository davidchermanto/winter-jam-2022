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
    [SerializeField] private UIManager uiManager;

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
    /// <param name="beatsDelay">How long  before the timer actually starts</param>
    public void StartCount(float bps, int beatsDelay)
    {
        // 60 in music tempo equals to 1 second, use this to normalize BPS
        secondsPerBeat = 60 / bps;

        StartCoroutine(Count(beatsDelay));
    }

    private IEnumerator Count(int beatsDelay)
    {

        float previousValue = 0;
        bool sentRhythmHit = false;

        while (beatsDelay > 0)
        {
            timer = ((float)AudioSettings.dspTime - AudioManager.Instance.GetSoundtrackStartTime() - secondsPerBeat * gameManager.GetDifficulty().offset) / secondsPerBeat % 1;

            if (timer < previousValue)
            {
                if (beatsDelay == 8)
                {
                    uiManager.CountDown(3);
                }
                else if (beatsDelay == 6)
                {
                    uiManager.CountDown(2);
                }
                else if (beatsDelay == 4)
                {
                    uiManager.CountDown(1);
                }
                else if (beatsDelay == 2)
                {
                    uiManager.CountDown(0);
                }
                else if(beatsDelay == 0)
                {
                    uiManager.SendRhythmHit(secondsPerBeat);
                }

                beatsDelay--;
            }

            previousValue = timer;

            yield return new WaitForEndOfFrame();
        }

        // The game starts here
        // Putting this code here is pretty yuck but eh its a game jam
        GameState.Instance.SetPlaying(true);

        while (GameState.Instance.IsPlaying())
        {
            if (!GameState.Instance.IsPaused())
            {
                if (!sentRhythmHit)
                {
                    uiManager.SendRhythmHit(secondsPerBeat);
                    sentRhythmHit = true;
                }

                // Timer is song in beat x where x is timer
                timer = ((float)AudioSettings.dspTime - AudioManager.Instance.GetSoundtrackStartTime() - secondsPerBeat * gameManager.GetDifficulty().offset) / secondsPerBeat % 1;

                if(timer < previousValue)
                {
                    // If score is 0, spare them from losing lifes
                    if (!beatMarked && DataManager.Instance.GetScore() != 0)
                    {
                        gameManager.SubstractLife(true);
                    }
                    else
                    {
                        beatMarked = false;
                    }

                    beatCount++;

                    timer -= 1;
                    sentRhythmHit = false;
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
