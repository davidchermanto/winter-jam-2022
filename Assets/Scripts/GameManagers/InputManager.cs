using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private PlayerManager playerManager;

    [Header("KeyCodes")]
    [SerializeField] private char upKeycode = 'W';
    [SerializeField] private char downKeycode = 'S';
    [SerializeField] private char leftKeycode = 'A';
    [SerializeField] private char rightKeycode = 'D';

    private string possibleKeyCodes = "QAZWSXEDCRFVTGBYHNUJMIKOLP[]1234567890-=[];',./";

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

    public void SetKeyCode(char keyCode, string direction)
    {
        switch (direction)
        {
            case "up":
                upKeycode = keyCode;
                break;
            case "left":
                leftKeycode = keyCode;
                break;
            case "right":
                rightKeycode = keyCode;
                break;
            case "down":
                downKeycode = keyCode;
                break;
        }
    }

    private void Update()
    {
        if (GameState.Instance.IsGameActive())
        {
            if (Input.GetKeyDown(GetKeyCode(upKeycode)))
            {
                //Debug.Log("You pressed " + upKeycode);
                OnPressKey("up");
            }

            if (Input.GetKeyDown(GetKeyCode(downKeycode)))
            {
                //Debug.Log("You pressed " + downKeycode);
                OnPressKey("down");
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
    }

    private void OnPressKey(string direction)
    {
        playerManager.OnMove(direction);
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
            case 'Q':
                return KeyCode.Q;
            case 'E':
                return KeyCode.E;
            case 'Z':
                return KeyCode.Z;
            case 'C':
                return KeyCode.C;
            case 'R':
                return KeyCode.R;
            case 'F':
                return KeyCode.F;
            case 'V':
                return KeyCode.V;
            case 'B':
                return KeyCode.B;
            case 'T':
                return KeyCode.T;
            case 'G':
                return KeyCode.G;
            case 'Y':
                return KeyCode.Y;
            case 'H':
                return KeyCode.H;
            case 'N':
                return KeyCode.N;
            case 'U':
                return KeyCode.U;
            case 'J':
                return KeyCode.J;
            case 'M':
                return KeyCode.M;
            case 'I':
                return KeyCode.I;
            case 'K':
                return KeyCode.K;
            case 'O':
                return KeyCode.O;
            case 'L':
                return KeyCode.L;
            case 'P':
                return KeyCode.P;
            default:
                break;
        }

        return KeyCode.None;
    }

    public string GetCurrentInputs()
    {
        string newString = "";

        newString += upKeycode + downKeycode + leftKeycode + rightKeycode;

        return newString;
    }
}
