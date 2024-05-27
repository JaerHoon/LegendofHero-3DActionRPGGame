using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : Character
{
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = SkillManager.instance.WarriorSkills[0];
        playerSkill = SkillManager.instance.WarriorSkills[4];
        skills[0] = this.gameObject.GetComponent<WarriorAttack0>();
    }

    // Update is called once per frame
    void Update()
    {
        UseSkill();
    }
}
