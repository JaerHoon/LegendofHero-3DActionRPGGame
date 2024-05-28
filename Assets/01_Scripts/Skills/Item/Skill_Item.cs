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

    public Skill_Item(string _name, Sprite icon, string description, float power, float nonHitTime, float speedRate, float damageRate, float gcd) :base(_name, icon, description)
    {
        this.power = power;
        this.nonHitTime = nonHitTime;
        this.speedRate = speedRate;
        this.damageRate = damageRate;
        this.gcd = gcd;

    }
}
