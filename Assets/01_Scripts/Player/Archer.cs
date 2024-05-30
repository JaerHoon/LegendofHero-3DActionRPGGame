using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Character
{
    // Start is called before the first frame update
    void Start()
    {
        playerSkillsSlot[0] = SkillManager.instance.archerSkills[0];
        playerSkillsSlot[1] = SkillManager.instance.archerSkills[4];
    }

    // Update is called once per frame
    void Update()
    {
        OnUseSkill();
    }
}
