using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = SkillManager.instance.WarriorSkills[0];
        playerSkill = SkillManager.instance.WarriorSkills[7];
        skills[0] = this.gameObject.GetComponent<WarriorAttack0>();
        skills[1] = this.gameObject.GetComponent<WarriorSkill>();
    }

    void WarriorInit()
    {
        playerHp = 10;
        playerAttackCD = 0;
        playerSkillCD = 0;
        playerGCD = 0;
        playerSpeed = 1;
    }

    void OnChangeSkills(int SKillNum)
    {
        if(SKillNum >= 0 && SKillNum <= 4)
        {
            playerAttack = SkillManager.instance.WarriorSkills[SKillNum];
        }
        else
        {
            playerSkill = SkillManager.instance.WarriorSkills[SKillNum];
        }
    }


    

    // Update is called once per frame
    void Update()
    {
        OnUseSkill();
       
       
    }
}
