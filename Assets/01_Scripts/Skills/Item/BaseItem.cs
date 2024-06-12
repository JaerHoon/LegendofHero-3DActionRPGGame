using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem 
{
    public int itemID;
    public string itemName;
    public Sprite itemIcon;
    public Material itemMaterial;
    public string itemDescription;
    public int value;
    

    public BaseItem(int ID ,string _name, Sprite icon, Material material, string description, int value)
    {
        itemID = ID;
        itemName = _name;
        itemIcon = icon;
        itemMaterial = material;
        itemDescription = description;
        this.value = value;
    }
}
