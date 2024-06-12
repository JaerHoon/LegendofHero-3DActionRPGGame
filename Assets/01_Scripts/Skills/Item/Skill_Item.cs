using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Item : BaseItem
{
    public float power;
    public float nonHitTime;
    public float speedRate;
    public float damageRate;
    public float gcd;

    public Skill_Item(int itemID, string _name, Sprite icon,Material material ,string description,int value, float power, float nonHitTime, float speedRate, float damageRate, float gcd) :base(itemID, _name, icon, material, description,value)
    {
        this.power = power;
        this.nonHitTime = nonHitTime;
        this.speedRate = speedRate;
        this.damageRate = damageRate;
        this.gcd = gcd;

    }
}
