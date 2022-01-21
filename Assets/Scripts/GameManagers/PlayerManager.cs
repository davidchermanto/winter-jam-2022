using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TileBoardManager tileBoardManager;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteMochi;
    [SerializeField] private SpriteRenderer spriteShadow;

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
