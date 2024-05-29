using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character
{
    // Start is called before the first frame update
    void Start()
    {
        playerAttack = SkillManager.instance.ArcherSkills[0];
        playerSkill = SkillManager.instance.ArcherSkills[4];
    }

    // Update is called once per frame
    void Update()
    {
        OnUseSkill();
    }
}
