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
    [SerializeField] private float currentAccuracy;
    [SerializeField] private int beatCount;

    [SerializeField] private float secondsPerBeat;

    [Header("Dependencies")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private TileBoardManager tileBoardManager;
    [SerializeField] private UIManager uiManager;

    [Header("Helpers")]
    // If a beat is marked, the player cannot move again on this beat.
    //[SerializeField] private bool beatMarked;
    //[SerializeField] private bool earlyMark;

    [SerializeField] private bool previousBeatHit;
    [SerializeField] private bool currentBeatHit;

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

        float previousHitTimingInBeat = 0;
        bool sentRhythmHit = false;

        while (beatsDelay > 0)
        {
            timer = ((float)AudioSettings.dspTime - AudioManager.Instance.GetSoundtrackStartTime() - secondsPerBeat * gameManager.GetDifficulty().offset) / secondsPerBeat % 1;

            if (timer < previousHitTimingInBeat)
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

            previousHitTimingInBeat = timer;

            yield return new WaitForEndOfFrame();
        }

        // The game starts here
        // Putting this code here is pretty yuck but eh its a game jam
        GameState.Instance.SetPlaying(true);
        float prevTimer = 0;

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
                currentAccuracy = SetCurrentAccuracy();

                // Timer is a value 1 going down to 0.
                // If timer < 0.4, then check if previous beat has been hit. If it hasnt, previousBeatHit = true. If hit fails, substract life.
                // If timer > 0.6, check next beat, currentBeatHit = true
                // when timer reaches 1, if currentBeatHit = true, previousBeatHit becomes true, else it becomes false.
                if(timer < Constants.perfectThreshold + Constants.goodThreshold + Constants.badThreshold)
                { 

                }
                else if (timer > 1 - (Constants.perfectThreshold + Constants.goodThreshold + Constants.badThreshold))
                {

                }

                if(timer < prevTimer)
                {
                    if (previousBeatHit)
                    {
                        previousBeatHit = false;
                    }
                    
                    if(currentBeatHit)
                    {
                        previousBeatHit = true;
                        currentBeatHit = false;
                    }

                    beatCount++;
                    sentRhythmHit = false;
                }

                prevTimer = timer;

                // If the next bar is reached...
                //if(timer < previousHitTimingInBeat)
                //{
                //    // If score is 0, spare them from losing lifes
                //    if (!earlyMark && !beatMarked && DataManager.Instance.GetScore() != 0)
                //    {
                //        gameManager.SubstractLife(true);
                //    }

                //    if (earlyMark)
                //    {
                //        beatMarked = true;
                //    }
                //    else
                //    {
                //        beatMarked = false;
                //    }

                //    beatCount++;

                //    sentRhythmHit = false;
                //}

                //previousHitTimingInBeat = timer;

                yield return new WaitForEndOfFrame();
            }
        }
    }

    public void ResetCount()
    {
        timer = 0;
        beatCount = 0;
    }

    public float GetAccuracy()
    {
        return currentAccuracy;
    }

    public float GetLiteral()
    {
        return timer;
    }

    public float SetCurrentAccuracy()
    {
        if (timer < (Constants.perfectThreshold + Constants.goodThreshold + Constants.badThreshold))
        {
            return ((Constants.perfectThreshold + Constants.goodThreshold + Constants.badThreshold) - timer) 
                / (Constants.perfectThreshold + Constants.goodThreshold + Constants.badThreshold);
        }
        else if (timer > (1 - (Constants.perfectThreshold + Constants.goodThreshold + Constants.badThreshold)))
        {
            return (timer - (Constants.perfectThreshold + Constants.goodThreshold + Constants.badThreshold))
                / (Constants.perfectThreshold + Constants.goodThreshold + Constants.badThreshold);
        }
        else
        {
            return 0;
        }
    }

    public int GetBeatCount()
    {
        return beatCount;
    }

    public bool CanMove()
    {
        if (timer < Constants.perfectThreshold + Constants.goodThreshold + Constants.badThreshold && !previousBeatHit)
        {
            return true;
        }
        else if (timer > 1 - (Constants.perfectThreshold + Constants.goodThreshold + Constants.badThreshold) && !currentBeatHit)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void MoveNow()
    {
        if (timer < Constants.perfectThreshold + Constants.goodThreshold + Constants.badThreshold)
        {
            if (previousBeatHit)
            {
                gameManager.SubstractLife(true);
            }
            else
            {
                previousBeatHit = true;
            }
        }
        else if (timer > 1 - (Constants.perfectThreshold + Constants.goodThreshold + Constants.badThreshold) && !currentBeatHit)
        {
            if (currentBeatHit)
            {
                gameManager.SubstractLife(true);
            }
            else
            {
                currentBeatHit = true;
            }
        }
    }
}
