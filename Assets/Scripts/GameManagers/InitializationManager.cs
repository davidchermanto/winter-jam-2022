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

    [Header("Constant Infos")]
    [SerializeField] Difficulty easy;
    [SerializeField] Difficulty normal;
    [SerializeField] Difficulty hard;

    void Start()
    {
        cameraManager.Setup();
        dataManager.Setup();
        colorThemeManager.Setup();
        inputManager.Setup();
        tileBoardManager.Setup();
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

        CameraManager.Instance.SetupGameCamera();
        RhythmManager.Instance.StartCount(difficulty.tempo, 12);
    }
}
