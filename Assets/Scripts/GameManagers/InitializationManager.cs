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

    void Start()
    {
        inputManager.Setup();
        //tileBoardManager.Setup();
    }
}
