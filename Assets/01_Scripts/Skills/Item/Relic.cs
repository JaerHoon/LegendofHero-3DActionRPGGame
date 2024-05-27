using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Relic : BaseItem
{
    public float power;
    public float scale;
    public float cd;

    public Relic(string _name, Sprite icon, string description, float power, float scale, float cd) : base(_name,icon,description)
    {
        this.power = power;
        this.scale = scale;
        this.cd = cd;
    }

}
