using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
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
    /// Rule 2: Player must be after TileBoard
    /// </summary>
    void Start()
    {
        colorThemeManager.Setup();

        cameraManager.Setup();
        dataManager.Setup();
        inputManager.Setup();

        tileBoardManager.Setup();
        playerManager.Setup();

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

    /// <summary>
    /// Does every effect for losing a life
    /// </summary>
    /// <param name="reasonMiss">Is the reason for losing the life missing a beat? If not then its wrong input</param>
    public void SubstractLife(bool reasonMiss)
    {
        DataManager.Instance.SubstractLife();
        CameraManager.Instance.Shake(Constants.cameraShakeDuration, Constants.cameraShakeIntensity);

        uiManager.OnLifeLost();

    }

    public void Lose()
    {

    }
}