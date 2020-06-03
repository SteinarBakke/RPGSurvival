using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerScript : MonoBehaviour
{
    // variables
    public float maxHealth, maxThirst, maxHunger, satisfiedStandard;
    public float thirstIncrement, hungerIncrement, healthImprovement;
    private float health, thirst, hunger;
    public bool alive;


    // hard coding atm (player dmg)
    public float damage;
    public bool weaponEquipped;

    //To see if char is close to AI
    public static bool triggeringWithAI;
    public static GameObject triggeringAI;

    public static bool triggeringWithTree;
    public static GameObject treeObject;

   // functions

    //Start Game
   public void Start()
    {
        health = maxHealth;

    }

    //Update Statuses
    public void Update()
    {

        //If Not dead increase
        if (alive)
        {
            thirst += thirstIncrement * UnityEngine.Time.deltaTime;
            hunger += hungerIncrement * UnityEngine.Time.deltaTime;
            if (thirst > maxThirst) thirst = maxThirst; // not increasing hunger past limit
            if (hunger > maxHunger) hunger = maxHunger; // not increasing thirst past limit
        }
        if (thirst >= maxThirst)
        {
            health -= thirstIncrement * UnityEngine.Time.deltaTime;
        }
        if (hunger >= maxHunger)
        {
            health -= hungerIncrement * UnityEngine.Time.deltaTime;
        }
        //If very satisfied with food and thirst, increase health slowly
        if (thirst < satisfiedStandard && hunger < (satisfiedStandard * 1.5))
        {
            if (health +( healthImprovement * UnityEngine.Time.deltaTime) < maxHealth)
                health += healthImprovement * UnityEngine.Time.deltaTime;
            else
                health = maxHealth;
        }
        if (health < 1)
        {
            Die();
        }

        // detecting and killing animals
        if (triggeringWithAI == true && triggeringAI) //checking if triggeringAI (If the animal actually exist)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Attack(triggeringAI);
            }
        }
        //If animal is dead
        if (!triggeringAI)
            triggeringWithAI = false;


        // tree chopping
        if (triggeringWithTree == true && treeObject)
        {
            if(Input.GetMouseButton(0))
            {
                Attack(treeObject);
            }
        }

        //temp prints
        if (Input.GetKeyDown(KeyCode.T))
        {
            print("Health = "+ health);
            print("Hunger = "+ hunger);
            print("Thirst = "+ thirst);
        }

    }


    //Attack function
    public void Attack(GameObject target)
    {
        if (target.tag == "Animal" && weaponEquipped) //change this
        {
            TigerScript animal = target.GetComponent<TigerScript>(); // just easier to work with this reference so we can use animal instead of all code
            animal.health -= damage;
            print(animal.health);
        }
        if (target.tag == "Tree" && weaponEquipped) //change this
        {
            TreeScript tree = target.GetComponent<TreeScript>();
            tree.health -= damage;
            print(tree.health);
        }
    }


    //Die Function
    public void Die()
    {
        alive = false; ;
        print("You have died");
        //Not the correct way to die, but works for now
        Destroy(gameObject);
    }

    public void Drink(float decreasedRate)
    {
        thirst -= decreasedRate;
        if (thirst < 0) thirst = 0;
    }

    public void Eat(float decreasedRate)
    {
        hunger -= decreasedRate;
        if (hunger < 0) hunger = 0;
    }

    //we doing this to let the tree drop items for a while instead of destroy after done.
    public void OnTriggerStay(Collider other)
    {
        if (other.tag == "Tree")
        {
            print("Colliding on Stay Tree");
            triggeringWithTree = true;
            treeObject = other.gameObject;
        }
    }
    //should also add this for range weapons
    //detecting collision between 2 objects
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Animal")
        {
            print("Colliding with AI");
            triggeringAI = other.gameObject;
            triggeringWithAI = true;
        }

    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Animal")
        {
            triggeringAI = null;
            triggeringWithAI = false;
        }
    }



}
