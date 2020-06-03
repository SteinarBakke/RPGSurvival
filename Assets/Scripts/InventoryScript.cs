using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour
{

    public GameObject inventory;
    public GameObject slotHolder;
    public GameObject itemManager;
    public bool inventoryEnabled;

    private int slots;
    private Transform[] slot;

    private GameObject itemPickedUp;
    private bool itemAdded;


    public void Start()
    {
        //slots being detected
        slots = slotHolder.transform.childCount;
        slot = new Transform[slots];
        DetectInventorySlots();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //basically just setting true if false, and false if true
            inventoryEnabled = !inventoryEnabled;
        }

        if (inventoryEnabled)
        {
            inventory.GetComponent<Canvas>().enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            inventory.GetComponent<Canvas>().enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            print("Colliding");
            itemPickedUp = other.gameObject;
            AddItem(itemPickedUp);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Item")
        {
            itemAdded = false;
        }
    }

    public void AddItem(GameObject item)
    {
        for (int i = 0; i< slots; i++)
        {
            if(slot[i].GetComponent<SlotScript>().empty && itemAdded == false)
            {
                slot[i].GetComponent<SlotScript>().item = itemPickedUp;
                slot[i].GetComponent<SlotScript>().itemIcon = itemPickedUp.GetComponent<ItemScript>().icon;

                item.transform.parent = itemManager.transform;
                item.transform.position = itemManager.transform.position;

                //position, rotation and scale
                item.transform.localPosition = item.GetComponent<ItemScript>().position;
                item.transform.localEulerAngles = item.GetComponent<ItemScript>().rotation;
                item.transform.localScale = item.GetComponent<ItemScript>().scale;


                item.GetComponent<ItemScript>().pickedUp = true;
                Destroy(item.GetComponent<Rigidbody>());

                itemAdded = true;
                //completely disabling item upon pickup
                item.SetActive(false);

                //destroying components of item
                /*
                if (item.GetComponent<MeshRenderer>())
                    item.GetComponent<MeshRenderer>().enabled = false;
                if (item.GetComponent<BoxCollider>())
                    item.GetComponent<BoxCollider>().enabled = false;
                    */

            }
        }

    }

    public void DetectInventorySlots()
    {
        for (int i = 0; i<slots; i++)
        {
            slot[i] = slotHolder.transform.GetChild(i);
        }
    }
}
