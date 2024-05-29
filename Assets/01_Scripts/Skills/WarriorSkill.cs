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

    public override void UsedSkill(Skills skill, float playerCritDamage, float chargeRate)
    {
        base.UsedSkill(skill, playerCritDamage, chargeRate);
    }

}
