using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{   
    [SerializeField]
    private float moveForce = 10f;
     [SerializeField]
    private float jumpForce = 11f;
    //public float maxVelocity =22f;
    

    private float movementX;
     private bool isGrounded ;

    [SerializeField]    
    private Rigidbody2D myBody;
     
     private SpriteRenderer sr;
     private Animator anim;
     private string WALK_ANIMATION = "walk"; // this is the parameter from animator 
     private string GROUND_TAG = "Ground";



      private void Awake(){
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        sr= GetComponent<SpriteRenderer>();

      }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMoveKeyboard();
        AnimatePlayer();
        // PlayerJump();
    }
    
    
    private void FixedUpdate(){
            PlayerJump();
    }
    

   void PlayerMoveKeyboard(){
          movementX = Input.GetAxisRaw("Horizontal"); // this is for the a and d key or left and right key
        //   Debug.Log("movement X is " + movementX);
          transform.position += new Vector3(movementX , 0f,0f) *Time.deltaTime*moveForce; // vector 3 is vector of x , y , z
   }    

   void  AnimatePlayer(){

       if(movementX >0){

           sr.flipX = false; // its in the right side 
          anim.SetBool(WALK_ANIMATION , true); // animated to walk 
           

       }else if(movementX <0){

           sr.flipX = true;    // to flip the left side 
            anim.SetBool(WALK_ANIMATION , true);


       }else{
        anim.SetBool(WALK_ANIMATION , false);
       }
          
   } 
  
    void PlayerJump(){
      if(Input.GetButtonDown("Jump") && isGrounded ){
          isGrounded = false;
        //   Debug.Log("jump pressed");
         myBody.AddForce(new Vector2(0f , jumpForce), ForceMode2D.Impulse);  // vector 2 is x and y 
      }

      

        // when the GetButtonDown is where the button is pressed it returns true
        // when the GetButtonUp is where the button is released after pressed it returns true 
        // when GetButton only is where the button is just pressed and release it returns true
    }
    private void OnCollisionEntry2D(Collision2D collision){ // inherit fuction from monobehavior to detect collision
       if(collision.gameObject.CompareTag(GROUND_TAG)){
         isGrounded = true;       
       }
    }

} // class 
