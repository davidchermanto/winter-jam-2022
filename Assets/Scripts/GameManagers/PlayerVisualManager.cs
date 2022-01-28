using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisualManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TileBoardManager tileBoardManager;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;

    [SerializeField] private GameObject playerObject;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteMochi;
    [SerializeField] private SpriteRenderer spriteShadow;

    private Vector3 defaultPos;

    public void Setup()
    {
        playerObject.transform.position = defaultPos;
        SetLayer(tileBoardManager.GetPlayerTile().GetLayer());
    }

    public void OnMove(string direction)
    {
        float accuracy = RhythmManager.Instance.GetTimer();

        uiManager.SendHitEnd();

        if (tileBoardManager.GetPlayerTile().GetNextTile().GetCorrectDirection().Equals(direction) && accuracy > Constants.badThreshold)
        {
            if (!RhythmManager.Instance.GetBeatMarked())
            {
                // Successful movement

                // Add score
                string grade = AccuracyToGrade(accuracy);
                int score = CalculateScore(Constants.baseScore, grade, DataManager.Instance.GetCurrentCombo(), gameManager.GetDifficulty());

                gameManager.AddScore(score, grade);

                // Move player
                Vector3 targetCoord = tileBoardManager.CalculateRealPosition(tileBoardManager.GetPlayerTile(), direction);
                targetCoord.y += Constants.playerHeight;

                StartCoroutine(PlayJumpAnimation(targetCoord));

                // Flip sprite if heading left or down
                if (direction.Equals("left") || direction.Equals("down"))
                {
                    spriteMochi.flipX = true;
                }
                else
                {
                    spriteMochi.flipX = false;
                }

                // Order tileboard to generate new tile and remove old one
                tileBoardManager.OnPlayerMove(direction);

                //Debug.Log("Direction: " + direction + " / Accuracy: " + System.Math.Round(accuracy * 100, 2) + "% / Grade: " + grade
                //    + " / Total Score: " + DataManager.Instance.GetScore() + " / BScore: " + score + " / Combo: " + DataManager.Instance.GetCurrentCombo());
            }
        }
        else
        {
            // Lose a life
            gameManager.SubstractLife(false);
        }
    }

    private string AccuracyToGrade(float accuracy)
    {
        if(accuracy > Constants.perfectThreshold)
        {
            return Constants.perfect;
        }
        else if(accuracy > Constants.goodThreshold)
        {
            return Constants.good;
        }
        else if(accuracy > Constants.badThreshold)
        {
            return Constants.bad;
        }
        else
        {
            return Constants.miss;
        }
    }

    private int CalculateScore(int baseScore, string grade, int currentCombo, Difficulty difficulty)
    {
        float multiplier;

        if (grade.Equals(Constants.perfect))
        {
            multiplier = Constants.perfectScoreMultiplier;
        }
        else if (grade.Equals(Constants.good))
        {
            multiplier = Constants.goodScoreMultiplier;
        }
        else
        {
            // Bad
            multiplier = Constants.badScoreMultiplier;
        }

        return Mathf.FloorToInt(baseScore * multiplier * ((100 + currentCombo) * Constants.bonusComboMultiplier) * difficulty.scoreMultiplier);
    }

    public void SetLayer(int newLayer)
    {
        spriteMochi.sortingOrder = newLayer + Constants.playerSpriteOffset;
        spriteShadow.sortingOrder = newLayer + Constants.playerShadowOffset;
    }

    public void ResetPos()
    {
        playerObject.transform.position = defaultPos;
    }

    private IEnumerator PlayJumpAnimation(Vector3 targetCoord)
    {
        Vector3 playerPos = playerObject.transform.position;
        Vector3 mochiPos = new Vector3(0, Constants.playerMochiHeight, 0);

        Vector3 peakPoint = new Vector3(0, mochiPos.y + Constants.playerJumpHeight, 0);

        float timer = 0;

        while(timer < 0.5)
        {
            timer += Time.deltaTime / Constants.playerJumpDuration;

            playerObject.transform.position = Vector3.Lerp(playerPos, targetCoord, timer);
            spriteMochi.transform.localPosition = Vector3.Lerp(mochiPos, peakPoint, Mathf.SmoothStep(0, 1, timer * 2));

            yield return new WaitForEndOfFrame();
        }

        while (timer < 1)
        {
            timer += Time.deltaTime / Constants.playerJumpDuration;

            playerObject.transform.position = Vector3.Lerp(playerPos, targetCoord, timer);
            spriteMochi.transform.localPosition = Vector3.Lerp(peakPoint, mochiPos, (timer - 0.5f) * 2);

            yield return new WaitForEndOfFrame();
        }

        SetLayer(tileBoardManager.GetPlayerTile().GetLayer());
    }
}
