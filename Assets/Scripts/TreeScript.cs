using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour
{
    // Start is called before the first frame update

    public float health;//health of tree

    public GameObject[] itemsToDrop;

    public static bool chopped;

    void Update()
    {
        if (health <= 0)
            chopped = true;

        if (chopped == true)
        {
            dropItems();
        }
    }

    void dropItems()
    {
        for (int i = 0; i < itemsToDrop.Length; i++)
        {
            if (itemsToDrop[i] == null)
                print("item in the tree named " + this.gameObject.name + " has not been set");

            GameObject spawnedItem = Instantiate(itemsToDrop[i], transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }


}
