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

    [Header("Prefabs")]
    [SerializeField] private GameObject rhythmHitPrefab;
    [SerializeField] private GameObject gradeTextPrefab;

    [SerializeField] private GameObject perfectHitPrefab;
    [SerializeField] private GameObject goodHitPrefab;
    [SerializeField] private GameObject badHitPrefab;
    [SerializeField] private GameObject missHitPrefab;

    [Header("Dynamic Elements")]
    [SerializeField] private TextMeshProUGUI score;

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
    }

    public void EnableMenuUI()
    {
        menuGroup.SetActive(true);

        ingameGroup.SetActive(false);
        rhythmGroup.SetActive(false);
    }


}
