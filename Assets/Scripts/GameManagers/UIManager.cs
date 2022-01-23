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
    [SerializeField] private GameObject gradeTextPrefab;

    [SerializeField] private GameObject perfectHitPrefab;
    [SerializeField] private GameObject goodHitPrefab;
    [SerializeField] private GameObject badHitPrefab;
    [SerializeField] private GameObject missHitPrefab;

    [Header("Dynamic Elements")]
    [SerializeField] private TextMeshProUGUI score;

    [Header("Single Objects")]
    [SerializeField] private SpriteRenderer background;

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
}
