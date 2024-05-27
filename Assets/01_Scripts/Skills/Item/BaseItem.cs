using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem 
{
    public string itemName;
    public Sprite itemIcon;
    public string itemDescription;
    

    public BaseItem(string _name, Sprite icon, string description)
    {
        itemName = _name;
        itemIcon = icon;
        itemDescription = description;
    }
}
