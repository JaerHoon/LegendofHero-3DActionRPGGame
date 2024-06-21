using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : CharacterAttackController
{
    

    // Start is called before the first frame update
    void Start()
    {
       
        skillSc = this.gameObject.GetComponent<ArcherSkill>();
        SkillManager.instance.changeSkill += ListenChangeSkill;
        InIt();
      
    }

    protected override void InIt()
    {
        base.InIt();
        playerSkillsSlot[0] = SkillManager.instance.archerSkills[0];
        playerSkillsSlot[1] = SkillManager.instance.archerSkills[4];
        playerSkillsSlot[2] = SkillManager.instance.archerSkills[8];

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
        //if (SKillNum >= 0 && SKillNum <= 3)
        //{
        //    playerSkillsSlot[0] = SkillManager.instance.archerSkills[SKillNum];
        //}
        //else if (SKillNum >= 4 && SKillNum <= 7)
        //{
            
        //    playerSkillsSlot[1] = SkillManager.instance.archerSkills[SKillNum];
        //    maxSkillNum = playerSkillsSlot[1].skillCount;
        //    curSkillNum = maxSkillNum;
        //}
        //else
        //{
        //    print("잘못된 값이 OnChangeSkills에 입력됨");
        //}
    }

    void ListenChangeSkill()
    {
        print("아이텝 체인지 이벤트 발생");
        maxSkillNum = SkillManager.instance.gainedSkill_Archer[1].skillCount;
        curSkillNum = maxSkillNum;
    }

    // Update is called once per frame
    void Update()
    {
        if(IsCanATK == true)
        {
            OnUseSkill(SkillManager.instance.gainedSkill_Archer);
        }
       
    }
}
