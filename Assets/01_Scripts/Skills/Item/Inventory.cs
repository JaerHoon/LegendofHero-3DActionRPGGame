using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Inventory : MonoBehaviour
{

    public List<BaseItem> invenItems = new List<BaseItem>();
    public delegate void ItemSetting();
    public ItemSetting itemSetting;

    public void AddItem(BaseItem item)
    {
        invenItems.Add(item);
        print($"{item.itemName} æ∆¿Ã≈€ »πµÊ!.");
        itemOdrer(invenItems);
        ItemManager.instance.StopActiveItem();
        StartCoroutine("ItemActive");
        
    }
    IEnumerator ItemActive()
    {
        yield return new WaitForFixedUpdate();
        ItemManager.instance.UseItemActiveItem();
        ItemManager.instance.UsePassiveSkillItem();
    }

    public void itemOdrer(List<BaseItem> baseItems)
    {
        invenItems = invenItems.OrderBy(data => data.itemID)
                     .ToList();
        itemSetting?.Invoke();
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
