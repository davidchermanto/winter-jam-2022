using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

/// <summary>
/// Controls the UI. This includes even UI outside of the Canvas, such as rhythm bar and background.
/// </summary>
public class UIManager : MonoBehaviour
{
    [Header("Groups")]
    [SerializeField] private GameObject menuGroup;

    [SerializeField] private GameObject ingameGroup;
    [SerializeField] private GameObject rhythmGroup;

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

    [Header("Dynamic Elements")]
    [SerializeField] private GameObject comboObject;
    [SerializeField] private GameObject scoreObject;

    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private TextMeshProUGUI combo;

    [Header("Single Objects")]
    [SerializeField] private SpriteRenderer background;

    [Header("Folders")]
    [SerializeField] private GameObject effectFolder;
    [SerializeField] private GameObject lightFolder;

    public void Setup()
    {
        AdjustColor();
    }

    public void EnableRhythmBar()
    {
        rhythmGroup.SetActive(true);
    }

    public void EnableInGameUI()
    {
        // TODO: Animate
        menuGroup.SetActive(false);

        ingameGroup.SetActive(true);
        rhythmGroup.SetActive(true);

        StartCoroutine(TweenColors(Constants.colorChangeDuration));
    }

    public void EnableMenuUI()
    {
        menuGroup.SetActive(true);

        ingameGroup.SetActive(false);
        rhythmGroup.SetActive(false);
    }

    public void ResetVariables()
    {
        score.SetText("0");
        combo.SetText("0");
        // TODO: Health
    }

    public void OnGameOver()
    {

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

    private IEnumerator PopOutObject(GameObject targetObject)
    {
        float duration = 0.3f;
        float expansionRateX = 1.1f;
        float expansionRateY = 1.02f;

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
