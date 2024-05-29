using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorSkill : SkillParent
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void UsedSkill(float damage, float cd, float gcd, float scale, float speedRate, float eventRate, int damageCount, int skillCount, int damageRate)
    {
        base.UsedSkill(damage, cd, gcd, scale, speedRate, eventRate, damageCount, skillCount, damageRate);
    }

}
