using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private TileBoardManager tileBoardManager;

    [SerializeField] private GameObject playerObject;

    [Header("Visual")]
    [SerializeField] private SpriteRenderer spriteMochi;
    [SerializeField] private SpriteRenderer spriteShadow;

    private void Setup()
    {

    }

    public void OnMove(string direction)
    {
        float accuracy = RhythmManager.Instance.GetTimer();

        if (tileBoardManager.GetPlayerTile().GetNextTile().GetCorrectDirection().Equals(direction) && accuracy > Constants.badThreshold)
        {
            // Successful movement

            // Add score
            string grade = AccuracyToGrade(accuracy);
            int score = CalculateScore(Constants.baseScore, grade, DataManager.Instance.GetCurrentCombo());

            DataManager.Instance.AddScore(score, grade);

            // Move player
            Vector3 targetCoord = CalculateTargetVector(direction);

            StartCoroutine(PlayJumpAnimation(targetCoord));

            // Order tileboard to generate new tile and remove old one
            tileBoardManager.OnPlayerMove(direction);

            Debug.Log("Direction: " + direction + " / Accuracy: " + System.Math.Round(accuracy * 100, 2) + "% / Grade: " + grade
                + " / Total Score: " + DataManager.Instance.GetScore() + " / BScore: " + score + " / Combo: " + DataManager.Instance.GetCurrentCombo());
        }
        else
        {
            // Lose a life
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

    private int CalculateScore(int baseScore, string grade, int currentCombo)
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

        return Mathf.FloorToInt(baseScore * multiplier * ((100 + currentCombo) * Constants.bonusComboMultiplier));
    }

    private Vector3 CalculateTargetVector(string newDirection)
    {
        float distanceX = Constants.tileDistanceX;
        float distanceY = Constants.tileDistanceY;

        switch (newDirection)
        {
            case "up":
                break;
            case "down":
                distanceX *= -1;
                distanceY *= -1;
                break;
            case "left":
                distanceX *= -1;
                break;
            case "right":
                distanceY *= -1;
                break;
            default:
                break;
        }

        Vector3 targetVector = new Vector3(playerObject.transform.position.x + distanceX, playerObject.transform.position.y + distanceY);

        return targetVector;
    }

    private IEnumerator PlayJumpAnimation(Vector3 targetCoord)
    {
        // TODO: Actual animation
        playerObject.transform.position = targetCoord;

        yield return new WaitForEndOfFrame();
    }
}
