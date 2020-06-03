using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * I don't want F to be unequip
 * Unequip should be pressing inventory. (Maybe equip everything, as you don't start with inventory?)
 * F should be Pick Up. As this would count for everyuthing
 * Automatically equip everything picked up
 * If I have something in hand, it can be interacted with whatever is on ground?
 * such as.. Wood interact with Stone, can be "crafted" into x amount of things.
 * 
 * Can eventually, with Animal Skin and Sewing, make inventory/backpack
 * 
 * 
 * 
 * 
 */
public class ItemScript : MonoBehaviour
{

    private GameObject player;
    public Texture icon;

    public string type;
    public float decreaseValue;

    //For holding weapon properly
    public Vector3 position;
    public Vector3 rotation;
    public Vector3 scale;

    public bool pickedUp;//detect if already picked up
    public bool equipped;

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    public void Update()
    {

        if(equipped)
        {
            if (Input.GetKeyDown(KeyCode.F))
                Unequip();
        }
    }

    public void Unequip()
    {
        player.GetComponent<PlayerScript>().weaponEquipped = false;
        equipped = false;
        this.gameObject.SetActive(false);
    }

}
