using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [SerializeField] private int currentScore;
    [SerializeField] private int highScoreEasy;
    [SerializeField] private int highScoreNormal;
    [SerializeField] private int highScoreHard;
   

    [SerializeField] private int currentCombo;
    [SerializeField] private int maxCombo;

    [SerializeField] private int furthestDistance;
    [SerializeField] private int currentDistance;


    /// <summary>
    /// Checks if the player has a current highscore in any difficulty, maximum combos and its furthest distance
    /// </summary>
    public void Awake()
    {
        instance = this;

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
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public int HighScoreEasy
    {
        get
        {
            return highScoreEasy;
        }

        set
        {
            highScoreEasy = currentScore;
            PlayerPrefs.SetInt("highScoreEasy", highScoreEasy);
        }
    }

    public int HighScoreNormal
    {
        get
        {
            return highScoreNormal;
        }

        set
        {
            highScoreNormal = currentScore;
            PlayerPrefs.SetInt("highScoreNormal", highScoreNormal);
        }
    }

    public int HighScoreHard
    {
        get
        {
            return highScoreHard;
        }

        set
        {
            highScoreHard = currentScore;
            PlayerPrefs.SetInt("highScoreHard", highScoreHard);
        }
    }


    public int MaxCombo
    {
        get
        {
            return maxCombo;
        }

        set
        {
            maxCombo = currentCombo;
            PlayerPrefs.SetInt("maxCombo", maxCombo);
        }
    }


    public int FurthestDistance
    {
        get
        {
            return furthestDistance;
        }

        set
        {
            furthestDistance = currentDistance;
            PlayerPrefs.SetInt("furthestDistance", furthestDistance);
        }
    }

    public bool CheckHighScore(int score, int highscore)
    {
        if (score > highscore)
        {
            highscore = score;
            return true;
        }
        else
        {
            return false;
        }
        
    }

    public int AddScores(int currentScore, int addAmount)
    {
        int newScore = currentScore + addAmount;

        return newScore;
    }
}
