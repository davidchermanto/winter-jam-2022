using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataManager : MonoBehaviour
{
    public static DataManager instance;

    [SerializeField] private int HighScoreEasy;
    [SerializeField] private int HighScoreNormal;
    [SerializeField] private int HighScoreHard;
    [SerializeField] private int CurrentScore;

    [SerializeField] private int CurrentCombo;
    [SerializeField] private int MaxCombo;

    [SerializeField] private int FurthestDistance;
    [SerializeField] private int CurrentDistance;


    /// <summary>
    /// Checks if the player has a current highscore in any difficulty, maximum combos and its furthest distance
    /// </summary>
    public void Awake()
    {
        instance = this;

        if (PlayerPrefs.HasKey("HighScoreEasy"))
        {
            PlayerPrefs.GetInt("HighScoreEasy", HighScoreEasy);
        }
        else if (PlayerPrefs.HasKey("HighScoreNormal"))
        {
            PlayerPrefs.GetInt("HighScoreNormal", HighScoreNormal);
        }
        else if (PlayerPrefs.HasKey("HighScoreHard"))
        {
            PlayerPrefs.GetInt("HighScoreHard", HighScoreHard);
        }


        if (PlayerPrefs.HasKey("MaxCombo"))
        {
            PlayerPrefs.GetInt("MaxCombo", MaxCombo);
        }

        if (PlayerPrefs.HasKey("FurthestDistance"))
        {
            PlayerPrefs.GetInt("FurthestDistance", FurthestDistance);
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

    public int HighScore_Easy
    {
        get
        {
            return HighScoreEasy;
        }

        set
        {
            HighScoreEasy = CurrentScore;
            PlayerPrefs.SetInt("HighScoreEasy", HighScoreEasy);
        }
    }

    public int HighScore_Normal
    {
        get
        {
            return HighScoreNormal;
        }

        set
        {
            HighScoreNormal = CurrentScore;
            PlayerPrefs.SetInt("HighScoreNormal", HighScoreNormal);
        }
    }

    public int HighScore_Hard
    {
        get
        {
            return HighScoreHard;
        }

        set
        {
            HighScoreHard = CurrentScore;
            PlayerPrefs.SetInt("HighScoreHard", HighScoreHard);
        }
    }


    public int Max_Combo
    {
        get
        {
            return MaxCombo;
        }

        set
        {
            MaxCombo = CurrentCombo;
            PlayerPrefs.SetInt("MaxCombo", MaxCombo);
        }
    }


    public int Furthest_Distance
    {
        get
        {
            return FurthestDistance;
        }

        set
        {
            FurthestDistance = CurrentDistance;
            PlayerPrefs.SetInt("FurthestDistance", FurthestDistance);
        }
    }



}
