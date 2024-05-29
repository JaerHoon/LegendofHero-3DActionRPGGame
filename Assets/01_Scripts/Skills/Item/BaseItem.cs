using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem 
{
    public int itemID;
    public string itemName;
    public Sprite itemIcon;
    public string itemDescription;
    

    public BaseItem(int ID ,string _name, Sprite icon, string description)
    {
        itemID = ID;
        itemName = _name;
        itemIcon = icon;
        itemDescription = description;
    }
}
