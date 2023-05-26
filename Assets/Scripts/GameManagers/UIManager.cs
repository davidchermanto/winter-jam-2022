using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.UI;

/// <summary>
/// Controls the UI. This includes even UI outside of the Canvas, such as rhythm bar and background.
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Groups")]
    [SerializeField] private GameObject menuGroup;

    [SerializeField] private GameObject ingameGroup;
    [SerializeField] private GameObject rhythmGroup;

    [SerializeField] private GameObject gameOverGroup;
    [SerializeField] private GameObject controlUI;

    [SerializeField] private TileBoardManager tileBoardManager;

    [Header("Prefabs")]
    [SerializeField] private GameObject rhythmHitPrefab;

    [SerializeField] private GameObject perfectLightPrefab;
    [SerializeField] private GameObject goodLightPrefab;
    [SerializeField] private GameObject badLightPrefab;
    [SerializeField] private GameObject missLightPrefab;

    [SerializeField] private GameObject perfectTextPrefab;
    [SerializeField] private GameObject goodTextPrefab;
    [SerializeField] private GameObject badTextPrefab;
    [SerializeField] private GameObject missTextPrefab;

    [SerializeField] private GameObject backgroundMenu;
    [SerializeField] private GameObject backgroundEasy;
    [SerializeField] private GameObject backgroundNormal;
    [SerializeField] private GameObject backgroundHard;

    [Header("Dynamic Elements")]
    [SerializeField] private GameObject comboObject;
    [SerializeField] private GameObject scoreObject;

    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI combo;
    [SerializeField] private TextMeshProUGUI comboDesc;

    [SerializeField] private TextMeshProUGUI gameEndScore;
    [SerializeField] private TextMeshProUGUI highscoreText;

    [SerializeField] private TextMeshProUGUI highscoreEasy;
    [SerializeField] private TextMeshProUGUI highscoreNormal;
    [SerializeField] private TextMeshProUGUI highscoreHard;

    [Header("Single Objects")]
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private GameObject dynamicBackground;

    [Header("Folders")]
    [SerializeField] private GameObject effectFolder;
    [SerializeField] private GameObject lightFolder;
    [SerializeField] private GameObject cameraFolder;

    [Header("Life")]
    [SerializeField] private GameObject heartObject;

    [SerializeField] private GameObject heartThree;
    [SerializeField] private GameObject heartTwo;
    [SerializeField] private GameObject heartOne;

    [Header("Controls")]
    [SerializeField] private TextMeshProUGUI upKey;
    [SerializeField] private TextMeshProUGUI downKey;
    [SerializeField] private TextMeshProUGUI leftKey;
    [SerializeField] private TextMeshProUGUI rightKey;

    [Header("Input History")]
    [SerializeField] private TextMeshProUGUI inputOne;
    [SerializeField] private TextMeshProUGUI inputTwo;
    [SerializeField] private TextMeshProUGUI inputThree;

    [Header("Temporary")]
    [SerializeField] private GameObject leftHit;
    [SerializeField] private GameObject rightHit;
    [SerializeField] private float hitTime;
    [SerializeField] private Coroutine moveBars;

    public void Setup()
    {
        AdjustColor();
        UpdateHighScore();
    }

    public void EnableRhythmBar()
    {
        rhythmGroup.SetActive(true);
    }

    public void EnableInGameUI()
    {
        menuGroup.SetActive(false);
        gameOverGroup.SetActive(false);

        ingameGroup.SetActive(true);
        rhythmGroup.SetActive(true);

        SetHeart(Constants.maxLives);

        inputOne.text = "";
        inputTwo.text = "";
        inputThree.text = "";

        StartCoroutine(TweenColors(Constants.colorChangeDuration));
    }

    public void TweenColors()
    {
        StartCoroutine(TweenColors(Constants.colorChangeDuration));
    }

    public void EnableMenuUI()
    {
        menuGroup.SetActive(true);

        ingameGroup.SetActive(false);
        rhythmGroup.SetActive(false);
        gameOverGroup.SetActive(false);
    }

    public void ResetVariables()
    {
        score.SetText("0");
        combo.SetText("0");

        UpdateKey("up", 'W');
        UpdateKey("down", 'S');
        UpdateKey("left", 'A');
        UpdateKey("right", 'D');
    }

    public void EnableControlsUI()
    {
        AudioManager.Instance.PlayOneShot("woosh");
        controlUI.SetActive(true);
    }

    public void DisableControlsUI()
    {
        AudioManager.Instance.PlayOneShot("woosh");
        controlUI.SetActive(false);
    }

    public void EnableGameOverUI(bool newHighscore)
    {
        gameOverGroup.SetActive(true);

        CanvasGroup canvasGroup = gameOverGroup.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;

        StartCoroutine(VisualizeCanvasGroup(canvasGroup, 1));
        rhythmGroup.SetActive(false);

        gameEndScore.SetText(DataManager.Instance.GetScore().ToString());

        if (newHighscore)
        {
            highscoreText.SetText("NEW HIGHSCORE!");
        }
        else
        {
            highscoreText.SetText("SCORE");
        }
    }

    public void BackToMainMenu()
    {
        AudioManager.Instance.PlayOneShot("woosh");
        ingameGroup.SetActive(false);
        StartCoroutine(ReopenMainMenu(gameOverGroup.GetComponent<CanvasGroup>(), menuGroup.GetComponent<CanvasGroup>()));
    }

    private IEnumerator VisualizeCanvasGroup(CanvasGroup endScreen, float targetOpacity)
    {
        float timer = 0;
        float targetTime = 1f;

        float currentOpacity = endScreen.alpha;

        while(timer < 1)
        {
            timer += Time.deltaTime / targetTime;

            endScreen.alpha = Mathf.Lerp(currentOpacity, targetOpacity, timer);

            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator ReopenMainMenu(CanvasGroup endScreen, CanvasGroup mainMenu)
    {
        float timer = 0;
        float targetTime = 1f;

        while (timer < 1)
        {
            timer += Time.deltaTime / targetTime;

            endScreen.alpha = Mathf.Lerp(1, 0, timer);

            yield return new WaitForEndOfFrame();
        }

        UpdateHighScore();

        mainMenu.alpha = 0;
        mainMenu.gameObject.SetActive(true);
        timer = 0;

        while (timer < 1)
        {
            timer += Time.deltaTime / targetTime;

            mainMenu.alpha = Mathf.Lerp(0, 1, timer);

            yield return new WaitForEndOfFrame();
        }
    }

    public void UpdateHighScore()
    {
        highscoreEasy.SetText("HIGHSCORE - " + DataManager.Instance.GetHighScoreEasy().ToString());
        highscoreNormal.SetText("HIGHSCORE - " + DataManager.Instance.GetHighScoreNormal().ToString());
        highscoreHard.SetText("HIGHSCORE - " + DataManager.Instance.GetHighScoreHard().ToString());
    }

    public void UpdateKey(string direction, char newKey)
    {
        string newString = "" + newKey;

        switch (direction)
        {
            case "up":
                upKey.SetText(newString);
                break;
            case "down":
                downKey.SetText(newString);
                break;
            case "left":
                leftKey.SetText(newString);
                break;
            case "right":
                rightKey.SetText(newString);
                break;
            default:
                Debug.Log("Weird direction: " + direction);
                break;
        }
    }

    public void SpawnGradeEffect(string grade)
    {
        GameObject gradeLight = null;
        GameObject text = null;

        if (grade.Equals(Constants.perfect))
        {
            gradeLight = Instantiate(perfectLightPrefab);
            text = Instantiate(perfectTextPrefab);
        }
        else if (grade.Equals(Constants.good))
        {
            gradeLight = Instantiate(goodLightPrefab);
            text = Instantiate(goodTextPrefab);
        }
        else if (grade.Equals(Constants.bad))
        {
            gradeLight = Instantiate(badLightPrefab);
            text = Instantiate(badTextPrefab);
        }
        else if (grade.Equals(Constants.miss))
        {
            gradeLight = Instantiate(missLightPrefab);
            text = Instantiate(missTextPrefab);
        }
        else
        {
            Debug.LogError("Invalid grade");
        }

        gradeLight.transform.SetParent(lightFolder.transform);
        gradeLight.transform.localPosition = new Vector3(0, 0, 0);

        text.transform.SetParent(effectFolder.transform);
        text.transform.localScale = new Vector3(1, 1);
        text.transform.position = new Vector3(combo.transform.position.x, combo.transform.position.y + 30f);
    }

    public void OnAddScore()
    {
        score.SetText(DataManager.Instance.GetScore().ToString());

        StartCoroutine(PopOutObject(scoreObject));
    }

    public void OnModifyComboCount()
    {
        combo.SetText(DataManager.Instance.GetCurrentCombo().ToString());

        StartCoroutine(PopOutObject(comboObject));
    }

    public void SetHeart(int heart)
    {
        // Yeah its ugly ik I'm in a hurry haha
        heartThree.SetActive(true);
        heartTwo.SetActive(true);
        heartOne.SetActive(true);

        if(heart <= 2)
        {
            heartThree.SetActive(false);
        }

        if(heart <= 1)
        {
            heartTwo.SetActive(false);
        }

        if(heart <= 0)
        {
            heartOne.SetActive(false);
        }

        StartCoroutine(PopOutObject(heartObject));
    }

    public void CountDown(int countdown)
    {
        if(countdown > 0)
        {
            combo.SetText(countdown.ToString());
            StartCoroutine(PopOutObject(comboObject));
        }
        else
        {
            combo.SetText("GO!");
            StartCoroutine(PopOutObject(comboObject, 1.3f, 1.05f));
        }

        comboDesc.SetText("COUNT");
    }

    public void ChangeBackground(Difficulty difficulty)
    {
        Destroy(dynamicBackground);

        // What is this switch syntax lol Visual studio just suggested it
        // But its cool imma keep it
        dynamicBackground = difficulty.name switch
        {
            "EASY" => Instantiate(backgroundEasy),
            "NORMAL" => Instantiate(backgroundNormal),
            "HARD" => Instantiate(backgroundHard),
            _ => Instantiate(backgroundMenu),
        };

        dynamicBackground.transform.SetParent(cameraFolder.transform);
        dynamicBackground.transform.localPosition = new Vector3(0, 0, 0);
    }

    public void NewInput(KeyCode input)
    {
        inputThree.text = inputTwo.text;
        inputTwo.text = inputOne.text;
        inputOne.text = input.ToString();
    }

    public void SendRhythmHit(float time)
    {
        if(leftHit != null || rightHit != null)
        {
            StartCoroutine(FadeRhythmHit(leftHit, rightHit));
        }

        leftHit = Instantiate(rhythmHitPrefab);
        rightHit = Instantiate(rhythmHitPrefab);

        leftHit.transform.SetParent(rhythmGroup.transform);
        rightHit.transform.SetParent(rhythmGroup.transform);

        leftHit.transform.localPosition = new Vector3(-6f, 0);
        rightHit.transform.localPosition = new Vector3(6f, 0);

        moveBars = StartCoroutine(MoveRhythmHit(leftHit, rightHit, time));
    }

    public void SendHitEnd()
    {
        if (leftHit != null || rightHit != null)
        {
            StartCoroutine(FadeRhythmHit(leftHit, rightHit));
        }
    }

    private IEnumerator MoveRhythmHit(GameObject leftHit, GameObject rightHit, float time)
    {
        float timer = 0;

        Vector3 initialPositionLeft = leftHit.transform.localPosition;
        Vector3 initialPositionRight = rightHit.transform.localPosition;

        SpriteRenderer leftSprite = leftHit.GetComponent<SpriteRenderer>();
        SpriteRenderer rightSprite = leftHit.GetComponent<SpriteRenderer>();

        Vector3 target = new Vector3(0, 0, 0);

        while(timer < 1)
        {
            timer += Time.deltaTime / time;

            if(leftHit != null && rightHit != null)
            {
                leftHit.transform.localPosition = Vector3.Lerp(initialPositionLeft, target, RhythmManager.Instance.GetAccuracy());
                rightHit.transform.localPosition = Vector3.Lerp(initialPositionRight, target, RhythmManager.Instance.GetAccuracy());

                Color col = new Color(1, 1, 1, 1 - timer);
                leftSprite.color = col;
                rightSprite.color = col;
            }
            else { break; }

            yield return new WaitForEndOfFrame();
        }

        leftHit.transform.localPosition = Vector3.Lerp(initialPositionLeft, target, 1);
        rightHit.transform.localPosition = Vector3.Lerp(initialPositionRight, target, 1);
    }

    private IEnumerator FadeRhythmHit(GameObject leftHit, GameObject rightHit)
    {
        float expansionRateX = 2f;
        float expansionRateY = 3f;
        float time = 0.1f;
        float timer = 0;

        SpriteRenderer left = leftHit.GetComponent<SpriteRenderer>();
        SpriteRenderer right = rightHit.GetComponent<SpriteRenderer>();

        Vector3 initialSize = new Vector3(10, 100);
        Vector3 largeSize = new Vector3(initialSize.x * expansionRateX, initialSize.y * expansionRateY, initialSize.z);

        StopCoroutine(moveBars);

        while (timer < 1)
        {
            timer += Time.deltaTime / time;

            if(leftHit != null & rightHit != null)
            {
                left.color = new Color(left.color.r, left.color.g, left.color.b, 1 - timer);
                right.color = new Color(right.color.r, right.color.g, right.color.b, 1 - timer);

                leftHit.transform.localScale = Vector3.Lerp(initialSize, largeSize, timer);
                rightHit.transform.localScale = Vector3.Lerp(initialSize, largeSize, timer);
            }
            else { break; }

            yield return new WaitForEndOfFrame();
        }

        Destroy(leftHit);
        Destroy(rightHit);
    }

    private IEnumerator TweenColors(float duration)
    {
        float timer = 0f;

        while(timer < 1)
        {
            timer += Time.deltaTime;

            AdjustColor();

            yield return new WaitForEndOfFrame();
        }
    }

    private void AdjustColor(bool vanilla = false)
    {
        ColorPack colorPack;

        if (vanilla)
        {
            colorPack = ColorThemeManager.Instance.GetColorPackVanilla();
        }
        else
        {
            colorPack = ColorThemeManager.Instance.GetColorPack();
        }

        List<TileHandler> activeTiles = tileBoardManager.GetActiveTiles();

        foreach (TileHandler tileHandler in activeTiles)
        {
            tileHandler.SetColors(colorPack.brightOne, colorPack.brightTwo, colorPack.darkOne);
            background.color = colorPack.antaOne;
        }
    }

    private IEnumerator PopOutObject(GameObject targetObject, float expansionRateX = 1.1f, float expansionRateY = 1.02f)
    {
        float duration = 0.3f;

        float timer = 0f;

        Vector3 initialSize = new Vector3(1, 1, 1);
        Vector3 largeSize = new Vector3(initialSize.x * expansionRateX, initialSize.y * expansionRateY, initialSize.z);

        targetObject.transform.localScale = largeSize;

        while(timer < 1)
        {
            timer += Time.deltaTime / duration;

            targetObject.transform.localScale = Vector3.Lerp(largeSize, initialSize, timer);

            yield return new WaitForEndOfFrame();
        }
    }
}
