using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warrior : CharacterAttackController
{
    // Start is called before the first frame update
    void Start()
    {
        playerSkillsSlot[0] = SkillManager.instance.warriorSkills[0];
        playerSkillsSlot[1] = SkillManager.instance.warriorSkills[4];
        playerSkillsSlot[2] = SkillManager.instance.warriorSkills[8];
        //playerAttack = SkillManager.instance.warriorSkills[0];
        //playerSkill = SkillManager.instance.warriorSkills[7];
        //skills[0] = this.gameObject.GetComponent<WarriorAttack0>();
        //skills[1] = this.gameObject.GetComponent<WarriorSkill>();
        skillSc = this.gameObject.GetComponent<WarriorSkill>();
        WarriorInit();
    }

    void WarriorInit()
    {
      
        playerSpeed = 1;
        playerCritDamage = 1.5f;
        playerAttackChargeRate = 1;
        playerSkillChargeRate = 1;
        maxSkillNum = playerSkillsSlot[1].skillCount;
        curSkillNum = maxSkillNum;
        for (int i = 0; i < 4; i++) isReadySkills[i] = true;

        isNonHit = false;
    }

    public void OnChangeSkills(int SKillNum)
    {
        if(SKillNum >= 0 && SKillNum <= 3)
        {
            playerSkillsSlot[0] = SkillManager.instance.warriorSkills[SKillNum];
        }
        else if(SKillNum >= 4 && SKillNum <= 7)
        {
            playerSkillsSlot[1] = SkillManager.instance.warriorSkills[SKillNum];
            maxSkillNum = playerSkillsSlot[1].skillCount;
            curSkillNum = maxSkillNum;
        }
        else
        {
            print("잘못된 값이 OnChangeSkills에 입력됨");
        }
    }



    // Update is called once per frame
    void Update()
    {
        OnUseSkill();
       
       
    }
}
