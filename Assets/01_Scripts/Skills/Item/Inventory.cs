using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public List<BaseItem> items = new List<BaseItem>();

    public void AddItem(BaseItem item)
    {
        items.Add(item);
        print($"{item.itemName} ¾ÆÀÌÅÛ È¹µæ!.");
    }

    public BaseItem GetItemName(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }

    public void PrintInventory()
    {
        foreach (var item in items)
        {
            Debug.Log($"Item: {item.itemName}, Description: {item.itemDescription}");
        }
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
