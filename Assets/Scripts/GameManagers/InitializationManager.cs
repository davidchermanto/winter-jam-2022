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
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private UIManager uiManager;

    [Header("Constant Infos")]
    [SerializeField] Difficulty easy;
    [SerializeField] Difficulty normal;
    [SerializeField] Difficulty hard;

    /// <summary>
    /// Rule 1: Color must be before UI
    /// </summary>
    void Start()
    {
        colorThemeManager.Setup();

        cameraManager.Setup();
        dataManager.Setup();
        inputManager.Setup();
        tileBoardManager.Setup();
        uiManager.Setup();
    }

    public void PlayEasy()
    {
        PlaySetup(easy);
    }

    public void PlayNormal()
    {
        PlaySetup(normal);
    }

    public void PlayHard()
    {
        PlaySetup(hard);
    }

    public void PlaySetup(Difficulty difficulty)
    {
        colorThemeManager.GenerateColorForDifficulty(difficulty);
        tileBoardManager.SetDifficulty(difficulty);
        tileBoardManager.StartGenerate();

        uiManager.EnableInGameUI();

        CameraManager.Instance.SetupGameCamera();
        RhythmManager.Instance.StartCount(difficulty.tempo, 12);
    }
}
