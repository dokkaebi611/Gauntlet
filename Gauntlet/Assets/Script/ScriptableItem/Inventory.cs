using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);
        Debug.Log(item.itemName + " added to inventory.");
    }

    public bool HasKey()
    {
        foreach (Item item in items)
        {
            if (item is Key)
            {
                return true;
            }
        }
        return false;
    }

    public void UseKey()
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] is Key)
            {
                items.RemoveAt(i);
                Debug.Log("Key used.");
                break;
            }
        }
    }
}