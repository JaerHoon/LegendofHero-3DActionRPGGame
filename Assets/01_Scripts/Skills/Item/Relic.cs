using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Relic : BaseItem
{
    public float power;
    public float scale;
    public float cd;

    public Relic(int itemID, string _name, Sprite icon,Material material, string description,int value, float power, float scale, float cd) : base(itemID, _name,icon, material, description, value)
    {
        this.power = power;
        this.scale = scale;
        this.cd = cd;
    }

}
