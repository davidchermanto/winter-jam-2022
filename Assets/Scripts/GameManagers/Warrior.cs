using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Player // this class is inheriting the player class 
{
   
  public Warrior (int health , int power , string name ){
          Health = health;
          Power = power;
          Name  = name;
  }  

//   public void ThrowingAxe(){
//       Debug.Log("axe is thrown");
//   }
 public override void Attack(){  // when it want to override it uses the override beside public 
     Debug.Log(" warrior is attacking with axe");
 }
 
} // class
