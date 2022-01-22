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
        if (!RhythmManager.Instance.GetBeatMarked())
        {
            playerManager.OnMove(direction);
        }

        RhythmManager.Instance.MarkBeat();
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
            case '[':
                return KeyCode.LeftBracket;
            case ']':
                return KeyCode.RightBracket;  
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
//             case '0':
//                 return KeyCode.Keypad0;     
//             case '1' :
//                 return KeyCode.Keypad1; 
//             case '2' :
//                 return KeyCode.Keypad2;
//             case '3' :
//                 return KeyCode.Keypad3;
//             case '4' :
//                 return KeyCode.Keypad4;
//             case '5' :
//                 return KeyCode.Keypad5;
//             case '6' :
//                 return KeyCode.Keypad6;
//             case '7' :
//                 return KeyCode.Keypad7;
//             case '8' :
//                 return KeyCode.Keypad8;
//             case '9' :
//                 return KeyCode.Keypad9;
            case '-'  :     
                 return KeyCode.Minus; 

             //   case '-'  :      
            //    return KeyCode.KeypadMinus ;       
            case '='  :
                 return KeyCode.Equals;
             //   case '-'  :      
            //    return KeyCode.KeypadEquals ;   
        
                 
            case ';':
                 return KeyCode.Semicolon;
            case '\'':
                 return KeyCode.Quote;
            case ',':
                 return KeyCode.Comma;  
            case '.':
                 return KeyCode.Period;
            case '\u005c' :
                 return KeyCode.Backslash;
            case '"':
                 return KeyCode.DoubleQuote;
            case '/':
                 return KeyCode.Slash;
            //   case '/'  :      
            //    return KeyCode.KeypadDivide ;        
            case '!':
                 return  KeyCode.Exclaim;
            case '@': 
                 return  KeyCode.At;
            case '#':
                 return  KeyCode.Hash;
            case '$':
                 return KeyCode.Dollar;
            case '%':
                 return KeyCode.Percent;
            case '^':
                 return KeyCode.Caret;
            case '&':
                 return KeyCode.Ampersand;
            case '*':
                 return KeyCode.Asterisk;
            //   case '*'  :      
            //    return KeyCode.KeypadMultiply;      
            case '(':
                 return KeyCode.LeftParen;
            case ')':
                 return KeyCode.RightParen; 
            case '_':
                 return KeyCode.Underscore;
            case '+':
                 return KeyCode.Plus;
                //   case '+'  :      
            //    return KeyCode.KeypadPlus;     
            case '`':
                 return KeyCode.BackQuote; 
            case '~':
                 return KeyCode.Tilde;
            case '{':
                 return KeyCode.LeftCurlyBracket;
            case '}':
                  return KeyCode.RightCurlyBracket;   
            case '|':
                  return KeyCode.Pipe;
            case ':':
                  return KeyCode.Colon;    
            case '<':
                  return KeyCode.Less;      
            case '>':
                  return KeyCode.Greater;   
            case '?':
                   return KeyCode.Question;        
            

                                                                                         
               

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
