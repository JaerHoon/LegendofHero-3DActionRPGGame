using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<BaseItem> invenItems = new List<BaseItem>();

    public void AddItem(BaseItem item)
    {
        invenItems.Add(item);
        print($"{item.itemName} ¾ÆÀÌÅÛ È¹µæ!.");
    }

    public BaseItem GetItemName(string itemName)
    {
        return invenItems.Find(item => item.itemName == itemName);
    }

    public void PrintInventory()
    {
        foreach (var item in invenItems)
        {
            Debug.Log($"Item: {item.itemName}, Description: {item.itemDescription}");
        }
    }

   
}
