using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            print("You pressed W");
        }

        if (Input.GetKey(KeyCode.S))
        {
            print("You pressed S");
        }

        if (Input.GetKey(KeyCode.A))
        {
            print("You pressed A");
        }

        if (Input.GetKey(KeyCode.D))
        {
            print("You pressed D");
        }
    }
}
