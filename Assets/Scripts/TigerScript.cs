using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


//Could definitely make this an overall "Animal script
//if you set some values such as roaming speed, radius, etc. Make these public and locked to the animal. Each animal can roam with same script.
//ATM PUBLIC VARIABLES ::: Radius = distance of route
//Timer = making a new route - Change to MaxTimer for That Animal -> Random
// Should later only have Walk speed/options on Random
// And have Run on Danger/Fight w/e
public class TigerScript : MonoBehaviour
{

    private GameObject player;

    public float health;

    public int amountOfItems;
    public GameObject[] item;//array of items

    public float radius;
    public int maxRouteTimer;
    public int maxRouteSpeed;
    private float timer;

    private Transform target;
    private NavMeshAgent agent;
    private Animation anim;
    private float currentTimer;


    //for fighting
    private bool dead;


    void OnEnable()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        timer = maxRouteTimer;
        currentTimer = timer;

        player = GameObject.FindWithTag("Player");

        //item = new GameObject[amountOfItems];
    }

    void Update()
    {
        currentTimer += Time.deltaTime;

        if (currentTimer >= timer)
        {
            //setting values for Tiger at current position
            Vector3 newPosition = RandomNavSphere(transform.position, radius, -1);
            agent.SetDestination(newPosition);
            //Setting new random timer
            System.Random random = new System.Random();
            timer = random.Next(maxRouteTimer);
            //Setting random between walk-run
            agent.speed = random.Next(maxRouteSpeed);
            //Setting different animations
            if (agent.speed < 1)
                anim.CrossFade("idle");
            else if (agent.speed < 5)
                anim.CrossFade("walk");
            else
                anim.CrossFade("run");

            //agent.velocity = Vector3.zero; Comment Section
            currentTimer = 0;
        }


        if (health <= 0)
        {
            Die();
        }
    }

    public void DropItems()
    {
        for (int i = 0; i< amountOfItems; i++)
        {
            GameObject droppedItem = Instantiate(item[i], transform.position, Quaternion.identity);
            //break;
        }
    }


    public void Die()
    {
        //DropItems
        DropItems();
        Destroy(this.gameObject);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layerMask)
    {
        //temp values to avoid error
        Vector3 randomDirection = Random.insideUnitSphere * distance;
        randomDirection += origin;

        //see Update
        NavMeshHit navHit;
        NavMesh.SamplePosition(randomDirection, out navHit, distance, layerMask);

        return navHit.position;
    }


}
