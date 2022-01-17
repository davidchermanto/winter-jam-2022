using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState Instance;

    private bool isPlaying;
    private bool isPaused;

    private void Awake()
    {
        Instance = this;
    }

    public void SetPlaying(bool isPlaying)
    {
        this.isPlaying = isPlaying;
    }

    public void SetPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    public bool CanPlayerMove()
    {
        return isPlaying && !isPaused;
    }
}
