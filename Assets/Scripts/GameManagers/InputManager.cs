using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private UIManager uiManager;

    [Header("KeyCodes")]
    [SerializeField] private char upKeycode = 'W';
    [SerializeField] private char downKeycode = 'S';
    [SerializeField] private char leftKeycode = 'A';
    [SerializeField] private char rightKeycode = 'D';

    private string possibleChar = "QAZWSXEDCRFVTGBYHNUJMIKOLP1234567890-=[];',./";
    private List<KeyCode> possibleKeyCodes = new List<KeyCode>();

    /// <summary>
    /// Initialize this class's default values here
    /// </summary>
    public void Setup()
    {
        foreach(char key in possibleChar)
        {
            possibleKeyCodes.Add(GetKeyCode(key));
        }
    }

    public void RandomizeOneKey()
    {
        SetKeyCode(GetRandomKey(), GetRandomDirection());
    }

    public void RandomizeAllKey()
    {
        SetKeycodes(GetRandomKey(), GetRandomKey(), GetRandomKey(), GetRandomKey());
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
            default:
                Debug.LogError("Invalid direction: " + direction);
                break;
        }
    }

    public char GetKey(string direction)
    {
        switch (direction)
        {
            case "up":
                return upKeycode;
            case "left":
                return leftKeycode;
            case "right":
                return rightKeycode;
            case "down":
                return downKeycode;
            default:
                Debug.LogError("Invalid direction: " + direction);
                return '0';
        }
    }

    private void Update()
    {
        if (GameState.Instance.IsGameActive() && !RhythmManager.Instance.GetBeatMarked())
        {
            foreach (KeyCode pressedKey in possibleKeyCodes)
            {
                if (Input.GetKeyDown(pressedKey))
                {
                    if (pressedKey == (GetKeyCode(upKeycode)))
                    {
                        //Debug.Log("You pressed " + upKeycode);
                        OnPressKey("up");
                    }

                    if (pressedKey == (GetKeyCode(downKeycode)))
                    {
                        //Debug.Log("You pressed " + downKeycode);
                        OnPressKey("down");
                    }

                    if (pressedKey == (GetKeyCode(leftKeycode)))
                    {
                        //Debug.Log("You pressed " + leftKeycode);
                        OnPressKey("left");
                    }

                    if (pressedKey == (GetKeyCode(rightKeycode)))
                    {
                        //Debug.Log("You pressed " + rightKeycode);
                        OnPressKey("right");
                    }

                    if (!GetCurrentKeyCodes().Contains(pressedKey))
                    {
                        //Debug.Log("You pressed a wrong key: " + pressedKey.ToString());

                        DataManager.Instance.BreakCombo();
                        gameManager.SubstractLife(false);
                    }
                }
            }
        }
    }

    private void OnPressKey(string direction)
    {
        if (!RhythmManager.Instance.GetBeatMarked())
        {
            playerManager.OnMove(direction);
        }

        RhythmManager.Instance.MarkBeat();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Returns an unused key</returns>
    public char GetRandomKey()
    {
        char randomKey = possibleChar[Random.Range(0, possibleChar.Length)];

        while (KeyExists(randomKey))
        {
            randomKey = possibleChar[Random.Range(0, possibleChar.Length)];
        }

        return randomKey;
    }

    public string GetRandomDirection()
    {
        int random = Random.Range(0, 3);

        switch (random)
        {
            case 0:
                return "up";
            case 1:
                return "down";
            case 2:
                return "left";
            case 3:
                return "right";
            default:
                Debug.LogWarning("Wrong random number: " + random);
                break;
        }

        return null;
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
            case '0':
                return KeyCode.Alpha0;     
            case '1' :
                return KeyCode.Alpha1; 
            case '2' :
                return KeyCode.Alpha2;
            case '3' :
                return KeyCode.Alpha3;
            case '4' :
                return KeyCode.Alpha4;
            case '5' :
                return KeyCode.Alpha5;
            case '6' :
                return KeyCode.Alpha6;
            case '7' :
                return KeyCode.Alpha7;
            case '8' :
                return KeyCode.Alpha8;
            case '9' :
                return KeyCode.Alpha9;
            case '-'  :     
                 return KeyCode.Minus;     
            case '='  :
                 return KeyCode.Equals;
            case '[':
                return KeyCode.LeftBracket;
            case ']':
                return KeyCode.RightBracket;
            case ';':
                 return KeyCode.Semicolon;
            case '\'':
                 return KeyCode.Quote;
            case ',':
                 return KeyCode.Comma;  
            case '.':
                 return KeyCode.Period;
            default:
                break;
        }

        return KeyCode.None;
    }

    public bool KeyExists(char key)
    {
        return key == upKeycode || key == downKeycode || key == leftKeycode || key == rightKeycode;
    }


    public string GetCurrentInputs()
    {
        string newString = "";

        newString += upKeycode + downKeycode + leftKeycode + rightKeycode;

        return newString;
    }

    public List<KeyCode> GetCurrentKeyCodes()
    {
        List<KeyCode> keyCodes = new List<KeyCode>
        {
            GetKeyCode(upKeycode),
            GetKeyCode(downKeycode),
            GetKeyCode(leftKeycode),
            GetKeyCode(rightKeycode)
        };

        return keyCodes;
    }
}
