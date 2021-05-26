using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryPool : System.IDisposable
{
    //Item
    class Item
    {
        public bool active; // Variable that determines whether an object is being used
        public GameObject gameObject; // Object that wiil be saved
    }
    // table of items
    Item[] table;

    public void Create(Object original, int count)
    {
        Dispose(); // reset Memory pool 
        table = new Item[count]; // create array as many as count

        for (int i = 0; i < count; i++) // repeat as many as count 
        {
            Item item = new Item();
            item.active = false;
            item.gameObject = GameObject.Instantiate(original) as GameObject; // Save original as GameObject type in item.gameObject
            item.gameObject.SetActive(false); // SetActive is an activation function, it will only be loaded into memory, so it is saved as inactive.
            table[i] = item;
        }
    }
    public GameObject NewItem() //Returning pending objects
    {
        if (table == null)
            return null;
        int count = table.Length;
        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            if (item.active == false)
            {
                item.active = true;
                item.gameObject.SetActive(true);
                return item.gameObject;
            }
        }

        return null;
    }
    public void RemoveItem(GameObject gameObject)
    {
        // If table is not instantiated, or there is no gameObject as a parameter,
        if (table == null || gameObject == null)
            return;

        // If table is instantiated or gameObject as a parameter exists, it is executed from here.
        int count = table.Length;

        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            if (item.gameObject == gameObject)
            {
                item.active = false; // active variable to false
                item.gameObject.SetActive(false);
                break;
            }
        }
    }
    public void ClearItem() //make all items to be rested
    {
        if (table == null)
            return;
        int count = table.Length;

        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            if (item != null && item.active)
            {
                //initiate deactivation
                item.active = false;
                item.gameObject.SetActive(false);
            }

        }
    }
    public void Dispose() // delete Memory pool
    {
        if (table == null)
            return;

        int count = table.Length;

        for (int i = 0; i < count; i++)
        {
            Item item = table[i];
            GameObject.Destroy(item.gameObject);
            //Since we are destroying the memory pool, all objects should be destroyed
        }
        table = null;
    }
}
