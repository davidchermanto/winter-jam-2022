using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private DataManager dataManager;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private TileBoardManager tileBoardManager;
    [SerializeField] private PlayerVisualManager playerManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private ColorThemeManager colorThemeManager;
    [SerializeField] private CameraManager cameraManager;
    [SerializeField] private UIManager uiManager;

    [Header("Constant Infos")]
    [SerializeField] Difficulty easy;
    [SerializeField] Difficulty normal;
    [SerializeField] Difficulty hard;

    private Difficulty difficulty;

    // Helper
    private bool hasLifeBeenSubstracted;

    /// <summary>
    /// Rule 1: Color must be before UI
    /// Rule 2: Player must be after TileBoard
    /// </summary>
    void Start()
    {
        colorThemeManager.Setup();
        audioManager.Setup();

        cameraManager.Setup();
        dataManager.Setup();
        inputManager.Setup();

        tileBoardManager.Setup();
        playerManager.Setup();

        uiManager.Setup();

        audioManager.PlayWeather("wind");
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
        this.difficulty = difficulty;

        audioManager.PlayOneShot("tap");

        audioManager.StopSoundtrack();
        audioManager.StopWeather();

        DataManager.Instance.ResetStageVariables();

        colorThemeManager.GenerateColorForDifficulty(difficulty);
        tileBoardManager.SetDifficulty(difficulty);
        tileBoardManager.StartGenerate();

        uiManager.ResetVariables();
        uiManager.EnableInGameUI();
        uiManager.ChangeBackground(difficulty);

        CameraManager.Instance.SetupGameCamera();

        StartCoroutine(DelayedPlaySetup(difficulty));
    }

    private IEnumerator DelayedPlaySetup(Difficulty difficulty)
    {
        yield return new WaitForSeconds(Constants.rhythmWait);

        audioManager.PlaySoundtrack(difficulty.name);
        audioManager.StopWeather();

        RhythmManager.Instance.StartCount(difficulty.tempo, Constants.beatDelay);
    }

    public void AddScore(int score, string grade)
    {
        DataManager.Instance.AddScore(score, grade);

        uiManager.SpawnGradeEffect(grade);
        uiManager.OnAddScore();
        uiManager.OnModifyComboCount();
    }

    public Difficulty GetDifficulty()
    {
        return difficulty;
    }

    /// <summary>
    /// Does every effect for losing a life
    /// </summary>
    /// <param name="reasonMiss">Is the reason for losing the life missing a beat? If not then its wrong input</param>
    public void SubstractLife(bool reasonMiss)
    {
        DataManager.Instance.BreakCombo();
        uiManager.SpawnGradeEffect(Constants.miss);
        uiManager.OnModifyComboCount();

        if (!hasLifeBeenSubstracted)
        {
            audioManager.PlayOneShot("miss");

            DataManager.Instance.SubstractLife();
            uiManager.SetHeart(DataManager.Instance.GetLifes());
            hasLifeBeenSubstracted = true;

            StartCoroutine(LifeLostCooldown());
        }

        if (dataManager.GetLifes() == 0)
        {
            OnLose();
        }
    }

    private void OnLose()
    {
        audioManager.PlayOneShot("thunder");

        cameraManager.SetupMenuCamera();

        bool newHighscore = DataManager.Instance.EvaluateScore(difficulty);
        DataManager.Instance.EvaluateMiscellaneous();

        GameState.Instance.SetPlaying(false);

        uiManager.EnableGameOverUI(newHighscore);
        uiManager.ChangeBackground(new Difficulty());

        tileBoardManager.ResetVariables();

        inputManager.ResetVariables();

        playerManager.SetLayer(Constants.initialTileLayer);
        playerManager.StopAllCoroutines();
        playerManager.ResetPos();

        // RESET COLOR
        Difficulty menu = new Difficulty
        {
            name = "menu"
        };
        colorThemeManager.GenerateColorForDifficulty(menu);
        uiManager.TweenColors();

        audioManager.StopSoundtrack();

        audioManager.PlayWeather("wind");

        DataManager.Instance.ResetStageVariables();
    }

    private IEnumerator LifeLostCooldown()
    {
        yield return new WaitForSeconds(1);

        hasLifeBeenSubstracted = false;
    }
}
