using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour // this is inherit from monobehaviour
{

    private Rigidbody2D mybody;
    private BoxCollider2D myCollider;
    private AudioSource audioSource;
    private Animator animator;

    public float speed = 5;
    // Start is called before the first frame update
    private void Awake(){  // first function is called 

     }
     private void OnEnable(){ // second function is called 

     }

    void Start()  // third function is called 
    {   // this can be used so that we do not inistialised the class again
        mybody = GetComponent<Rigidbody2D>(); 
        audioSource = GetComponent<AudioSource>();
    }
       
     

     


    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");  
        float v = Input.GetAxis("Vertical");

        Vector2 pos = transform.position;


        pos.x += h * speed * Time.deltaTime;
        pos.y += h * speed * Time.deltaTime;

        transform.position = pos ;
    }
}
