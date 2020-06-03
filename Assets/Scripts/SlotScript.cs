using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class SlotScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{

    private bool hovered;
    public bool empty;
    public GameObject item;
    public Texture itemIcon;

    private GameObject player;

    void Start()
    {
        hovered = false;
        player = GameObject.FindWithTag("Player");


    }

    void Update()
    {
        //if there's an item, not empty
        if (item)
        {
            empty = false;
            //icon
            itemIcon = item.GetComponent<ItemScript>().icon;
            this.GetComponent<RawImage>().texture = itemIcon;
        }
        else
        {
            empty = true;
            itemIcon = null;//after image destroy deleting icon
            this.GetComponent<RawImage>().texture = null;//after image destroy deleting icon
        }
 
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovered = false;
    }

    // Make this item type a Case system
    // On Trigger from inventory
    public void OnPointerClick(PointerEventData eventData)
    {
        if(item)
        {
            ItemScript thisItem = item.GetComponent<ItemScript>();

            //checking for item type
            if (thisItem.type == "Water")
            {
                player.GetComponent<PlayerScript>().Drink(thisItem.decreaseValue);
                Destroy(item);
            }
            if (thisItem.type == "Food")
            {
                player.GetComponent<PlayerScript>().Eat(thisItem.decreaseValue);
                Destroy(item);
            }

            if (thisItem.type == "Weapon" && player.GetComponent<PlayerScript>().weaponEquipped == false)
            {
                thisItem.equipped = true;
                item.SetActive(true);
                player.GetComponent<PlayerScript>().weaponEquipped = true;
            }
        }
    }
        
}
