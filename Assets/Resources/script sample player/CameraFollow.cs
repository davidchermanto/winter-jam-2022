using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{  


    private Transform player;

    private Vector3 temPos;
    

    [SerializeField]
    private float minX , maxX ;   



    // Start is called before the first frame update
    void Start()
    {
         player = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void LateUpdate() // this is to prevent the glitch 
    {  

        temPos = transform.position;    
        temPos.x = player.position.x;
        transform.position = temPos;

        if(temPos.x <minX){// this blocked the vision to set into the minX position for it to be stop at the end of the map  
            temPos.x = minX;
        }
        if(temPos.x>maxX){// this blocked the vision to set into the maxX position for it to be stop at the end of the map  
            temPos.x = maxX;
        }
        transform.position = temPos;
    }
}
