using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DataManager : MonoBehaviour
{
    public static DataManager instance;
    public int score, highscore;
    public TextMeshProUGUI scoreText, highscoreText;

    // <summary>
    // </summary>
    public void Awake()
    {
        instance = this;

        if (PlayerPrefs.HasKey("EasyHighscore"))
        {
            highscore = PlayerPrefs.GetInt("EasyHighscore");
            highscoreText.text = "HIGHSCORE - " + highscore.ToString();
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

    // <summary>
    // </summary>
    
    public void AddScore()
    {
        score++;

        UpdateEasyHighScore();

        scoreText.text = score.ToString();
    }

    // <summary>
    // Updates the Highscore in Easy Mode
    // </summary>

    public void UpdateEasyHighScore()
    {
        if (score > highscore)
        {
            highscore = score;
            highscoreText.text = "HIGHSCORE - " + highscore.ToString();

            PlayerPrefs.SetInt("EasyHighscore", highscore);
        }
    }

    // <summary>
    // resets the score back to 0
    // </summary>
    public void ResetScore()
    {
        score = 0;
        scoreText.text = score.ToString();
    }
}
