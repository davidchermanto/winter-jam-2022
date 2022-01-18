using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationManager : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private TileBoardManager tileBoardManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ColorThemeManager colorThemeManager;

    [Header("Constant Infos")]
    [SerializeField] Difficulty easy;
    [SerializeField] Difficulty normal;
    [SerializeField] Difficulty hard;

    void Start()
    {
        SetupDifficulties();

        inputManager.Setup();
        tileBoardManager.Setup();
    }

    public void PlayEasy()
    {
        tileBoardManager.SetDifficulty(easy);
        tileBoardManager.StartGenerate();
    }

    public void PlayNormal()
    {
        tileBoardManager.SetDifficulty(normal);
        tileBoardManager.StartGenerate();
    }

    public void PlayHard()
    {
        tileBoardManager.SetDifficulty(hard);
        tileBoardManager.StartGenerate();
    }

    private void SetupDifficulties()
    {
        easy = new Difficulty
        {
            name = "EASY",
            //easy.theme = 
            //easy.timing = 90;
            biasModifier = 5,
            obstacleSpawnDelay = 200
        };

        normal = new Difficulty
        {
            name = "NORMAL",
            //theme = 
            //timing = 90;
            biasModifier = 20,
            obstacleSpawnDelay = 25
        };

        hard = new Difficulty
        {
            name = "HARD",
            //theme = 
            //timing = 90;
            biasModifier = 60,
            obstacleSpawnDelay = 10
        };
    }
}
