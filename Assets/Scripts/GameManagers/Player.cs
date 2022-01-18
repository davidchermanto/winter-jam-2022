using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player   // player class  or the blueprint
{  
 // another way to set and get 
   private int  _health;
   private int _power;

   private string _name;
   public int Health{
        get{
             return _health;
        }
        set{
             _health = value;
        }
   }
 public int Power{
        get{
             return _power;
        }
        set{
             _power = value;
        }
   }
 public string Name{
        get{
             return _name;
        }
        set{
             _name = value;
        }
   }



   private  int health  ;
    private int power  ;
    private string name ;
    public Player(){  // this constructor is for pass on to another class 

    }
    public Player( int health , int power , string name ){// constructor

       
        // this.health = health;   // the this.variable is the variable out side the constructor
        // this.power = power;
        // this.name = name ;                  
         Health = health;   // the this.variable is the variable out side the constructor
        Power = power;
        Name = name ;  
    }
     public int GetHealth(){
         return health;
     }
    public int GetPower(){
        return power;
    }
   
    public string GetName(){
        return name;
    }
    public void SetHealth(int health){
        this.health = health;
    }

    public void SetPower(int power){
        this.power= power;
    }
    public void SetName(string name){
        this.name = name;
    }

      public void info(){
        Debug.Log(" Health is " + Health);
        Debug.Log(" Poweris " + Power);
        Debug.Log(" Name is " + Name);
      }

      public virtual void Attack(){  // when overriding it needs to have virtual beside public 
                                     // for the main override 
               Debug.Log("player is attacking");

      }
    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
