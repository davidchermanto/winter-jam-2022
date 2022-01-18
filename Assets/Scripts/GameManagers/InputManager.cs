using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("KeyCodes")]
    [SerializeField] private char upKeycode = 'W';
    [SerializeField] private char downKeycode = 'S';
    [SerializeField] private char leftKeycode = 'A';
    [SerializeField] private char rightKeycode = 'D';

    /// <summary>
    /// Initialize this class's default values here
    /// </summary>
    public void Setup()
    {

    }

    public void SetKeycodes(char up, char down, char left, char right)
    {
        upKeycode = up;
        downKeycode = down;
        leftKeycode = left;
        rightKeycode = right;
    }

    private void Update()
    {
        if (Input.GetKeyDown(GetKeyCode(upKeycode)))
        {
            //Debug.Log("You pressed " + upKeycode);
            OnPressKey("up");
            DataManager.instance.AddScore();
        }

        if (Input.GetKeyDown(GetKeyCode(downKeycode)))
        {
            //Debug.Log("You pressed " + downKeycode);
            OnPressKey("down");

            DataManager.instance.ResetScore();
            
        }

        if (Input.GetKeyDown(GetKeyCode(leftKeycode)))
        {
            //Debug.Log("You pressed " + leftKeycode);
            OnPressKey("left");
        }

        if (Input.GetKeyDown(GetKeyCode(rightKeycode)))
        {
            //Debug.Log("You pressed " + rightKeycode);
            OnPressKey("right");
        }
    }

    private void OnPressKey(string direction)
    {
        // Call the stuff here
    }

    /// <summary>
    /// Takes in a char and returns the corresponding KeyCodes.
    /// </summary>
    /// <param name="letter">The char to be converted into a KeyCode</param>
    /// <returns></returns>
    private KeyCode GetKeyCode(char letter)
    {
        switch (letter)
        {
            case 'W':
                return KeyCode.W;
            case 'A':
                return KeyCode.A;
            case 'S':
                return KeyCode.S;
            case 'D':
                return KeyCode.D;

            // TODO: More Keys
            
            default:
                break;
        }

        return KeyCode.None;
    }
}
