using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TileBoardManager tileBoardManager;

    private void Setup()
    {

    }

    public void OnMove(string direction)
    {

        tileBoardManager.OnPlayerMove(direction);
    }

    private IEnumerator PlayJumpAnimation()
    {
        yield return new WaitForEndOfFrame();
    }
}
