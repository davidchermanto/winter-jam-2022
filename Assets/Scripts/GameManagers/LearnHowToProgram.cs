using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnHowToProgram : MonoBehaviour
{
    // data and variable type
    float speed = 5.0f; 

    double mana  = 15.5;

    int  health = 100;

     string name = "Warrior";

     bool isDead = true; 
     bool isnotDead = false ;

     char onechar = 'a';



      Player warrior; // object class 

       Player archer;

    private Player GetWarrior()
    {
        return warrior;
    }

    private void Start()//the main method of C# in unity 
    { 
        // basic operation 
        // int a = 10;
        // int b = 5;
        // int c = a+b;
        // int d = a/b;
        // two function for printing    
    //     print(" this is from print");       
    //   Debug.Log("A + B  = " + c );
    //   Debug.Log (a + " + " + b + " = " + c);
    //   Debug.Log(a + " / " +  b  + " = " + d);
          CalculateTwoNumbers();   // calling method
          CalculateNumbers(1 ,2);
         Debug.Log(Numbers(30 , 40));

         StartCoroutine(ExecuteSomething());  // calling the method using coroutine 
         StartCoroutine("ExecuteSomething");
         StopCoroutine("ExecuteSomething");


        // Player archer = new Player(100,50, "archer"); 

          warrior = new Player (100 , 50 , "warrior");
          archer = new Player(100,50, "archer"); 
          
        //   warrior.info(); // calling the method from class player
        //   archer.info();

        //  Debug.Log(warrior.GetHealth());
        //   warrior.SetHealth(20);
          Warrior w = new Warrior(100 , 50 , "me");  
          w.info();
          w.Attack();
         // using another to get and set health 
         // another way to set and get 
         //using 

//    private int  _health;

//    public int Health{
//         get{
//              return _health;
//         }
//         set{
//              _health = value;
//         }
//    }
         
         // when calling the method 

        //  warrior.Health = 20; 
        //  warrior.Health;

        }
         void CalculateTwoNumbers(){ // define the method type void
        float one = 10;
        float two = 5;
        float three = (one+two)/one;
        float four = ((one+two)/one) + ((one+two)/one);
        Debug.Log(four);        

    }
      void CalculateNumbers(float a , float b ){ // having a parameter
       Debug.Log (a + b);
     }
        float Numbers(float a , float b ){ // having a parameter and return type float
        return a + b ;
     }
     void conditionalStatement(int health){
         if(health <0){                         // example of conditional statement 
             Debug.Log("player is dead");


         }else if(health == 50){
                Debug.Log("health is half");
         }else if(health <=50 ){
             Debug.Log("health is low");
         }else {
             Debug.Log("player is not dead");
         }

      switch (health){
          case 100:     // this means the health is 100
          break;

          case 50: // this means when health is 50 
          break;
          
          case 0: // this means when health is 0
          break;

          default: // this is the else of swith condition
           break;


      }

     }

     void loop(){
         int iterationtime = 100;
         int number = 0 ;
        for (int i = 0; i < iterationtime ; i++) // for loop 
        {
            Debug.Log("count : " + i);
        } 
        while(number < iterationtime ) {  // while loop
            Debug.Log("count : " + number );
            number = number++; 
        }

     }
     IEnumerator ExecuteSomething(){  // method for coroutine 

         yield return new WaitForSeconds(2f);

         Debug.Log("something exceuted");
     }
        IEnumerator ExecuteParameter(float a ){  // method for coroutine with parameter 

         yield return new WaitForSeconds(a);

         Debug.Log("something exceuted");
     }
}// class
