using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : CharacterAttackController
{
    // Start is called before the first frame update
    void Start()
    {
        ArcherInit();
        playerSkillsSlot[0] = SkillManager.instance.archerSkills[0];
        playerSkillsSlot[1] = SkillManager.instance.archerSkills[4];
        playerSkillsSlot[1] = SkillManager.instance.archerSkills[8];
        skillSc = this.gameObject.GetComponent<ArcherSkill>();
    }

    void ArcherInit()
    {

        playerSpeed = 1;
        playerCritDamage = 1.5f;
        playerAttackChargeRate = 1;
        playerSkillChargeRate = 1;

        for (int i = 0; i < 4; i++) isReadySkills[i] = true;

        isNonHit = false;
    }

    void OnChangeSkills(int SKillNum)
    {
        if (SKillNum >= 0 && SKillNum <= 3)
        {
            playerSkillsSlot[0] = SkillManager.instance.archerSkills[SKillNum];
        }
        else
        {
            playerSkillsSlot[1] = SkillManager.instance.archerSkills[SKillNum];
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnUseSkill();
    }
}
