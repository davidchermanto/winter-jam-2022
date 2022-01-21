using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [Header("Dynamic Data")]
    [SerializeField] private int currentScore;
    [SerializeField] private int currentDistance;
    [SerializeField] private int currentCombo;

    [Header("Saved Data")]
    [SerializeField] private int highScoreEasy;
    [SerializeField] private int highScoreNormal;
    [SerializeField] private int highScoreHard;

    [SerializeField] private int maxCombo;
    [SerializeField] private int furthestDistance;


    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// Checks if the player has a current highscore in any difficulty, maximum combos and its furthest distance
    /// </summary>
    public void Setup()
    {
        if (PlayerPrefs.HasKey("highScoreEasy"))
        {
            highScoreEasy = PlayerPrefs.GetInt("highScoreEasy", 0);
        }
        
        if (PlayerPrefs.HasKey("highScoreNormal"))
        {
            highScoreNormal = PlayerPrefs.GetInt("highScoreNormal", 0);
        }
        
        if (PlayerPrefs.HasKey("highScoreHard"))
        {
            highScoreHard = PlayerPrefs.GetInt("highScoreHard", 0);
        }

        if (PlayerPrefs.HasKey("maxCombo"))
        {
            maxCombo = PlayerPrefs.GetInt("maxCombo", 0);
        }

        if (PlayerPrefs.HasKey("furthestDistance"))
        {
            furthestDistance = PlayerPrefs.GetInt("furthestDistance", 0);
        }

    }

    public int HighScoreEasy
    {
        get
        {
            return highScoreEasy;
        }
    }

    public int HighScoreNormal
    {
        get
        {
            return highScoreNormal;
        }
    }

    public int HighScoreHard
    {
        get
        {
            return highScoreHard;
        }
    }


    public int MaxCombo
    {
        get
        {
            return maxCombo;
        }
    }


    public int FurthestDistance
    {
        get
        {
            return furthestDistance;
        }
    }

    /// <summary>
    /// Evaluates whether the current score is the high score. If it is, saves it.
    /// </summary>
    /// <param name="difficulty">The current difficulty</param>
    /// <returns></returns>
    public bool EvaluateScore(Difficulty difficulty)
    {
        switch (difficulty.name)
        {
            case "EASY":
                if(currentScore > highScoreEasy)
                {
                    highScoreEasy = currentScore;
                    PlayerPrefs.SetInt("highScoreEasy", highScoreEasy);

                    return true;
                }
                else
                {
                    return false;
                }
            case "NORMAL":
                if (currentScore > highScoreNormal)
                {
                    highScoreNormal = currentScore;
                    PlayerPrefs.SetInt("highScoreNormal", highScoreNormal);

                    return true;
                }
                else
                {
                    return false;
                }
            case "HARD":
                if (currentScore > highScoreHard)
                {
                    highScoreHard = currentScore;
                    PlayerPrefs.SetInt("highScoreHard", highScoreHard);

                    return true;
                }
                else
                {
                    return false;
                }
            default:
                Debug.LogError("No difficulty found of the name: " + difficulty.name);

                return false;
        }
    }

    public void EvaluateMiscellaneous()
    {
        if(currentDistance > furthestDistance)
        {
            furthestDistance = currentDistance;
        }

        if(currentCombo > maxCombo)
        {
            maxCombo = currentCombo;
        }
    }

    public void AddScore(int addAmount)
    {
        currentScore += addAmount;
    }

    public void ResetStageVariables()
    {
        currentScore = 0;
        currentCombo = 0;
        currentDistance = 0;
    }
}
