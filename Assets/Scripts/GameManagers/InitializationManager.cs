using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationManager : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;
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
        dataManager.Setup();
        colorThemeManager.Setup();
        inputManager.Setup();
        tileBoardManager.Setup();
    }

    public void PlayEasy()
    {
        colorThemeManager.GenerateColorForDifficulty(easy);
        tileBoardManager.SetDifficulty(easy);
        tileBoardManager.StartGenerate();

        RhythmManager.Instance.StartCount(easy.tempo, 4);
    }

    public void PlayNormal()
    {
        colorThemeManager.GenerateColorForDifficulty(normal);
        tileBoardManager.SetDifficulty(normal);
        tileBoardManager.StartGenerate();

        RhythmManager.Instance.StartCount(normal.tempo, 4);
    }

    public void PlayHard()
    {
        colorThemeManager.GenerateColorForDifficulty(hard);
        tileBoardManager.SetDifficulty(hard);
        tileBoardManager.StartGenerate();

        RhythmManager.Instance.StartCount(hard.tempo, 4);
    }
}
