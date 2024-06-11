using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSkillSlot : UIModel
{

    SkillManager skillManager;
    public List<SkillInfo> skills = new List<SkillInfo>(new SkillInfo[3]);


    private void Start()
    {
        skillManager = SkillManager.instance != null ? SkillManager.instance : GameObject.FindFirstObjectByType<SkillManager>();
        skillManager.changeSkill += Setting;
    }

    public void Setting()
    {
        if(CharacterManager.instance.choicedCharacter == CharacterManager.ChoicedCharacter.Warrior)
        {
            for(int i=0; i< SkillManager.instance.gainedSkill_Warrior.Length; i++)
            {
                skills[i] = SkillManager.instance.gainedSkill_Warrior[i];
            }
            

        }
        else
        {
            for (int i = 0; i < SkillManager.instance.gainedSkill_Archer.Length; i++)
            {
                skills[i] = SkillManager.instance.gainedSkill_Archer[i];
            }
               
        }

        ChangeUI();
    }

    public void startCooltime(int Slotnum)
    {
       for(int i=0; i < skills.Count; i++)
        {
            if(Slotnum == i)
            {
                SlotCoolTimeStart?.Invoke(skills[i].cd, Slotnum);
            }
            else
            {
                SlotCoolTimeStart?.Invoke(skills[i].gcd, Slotnum);
            }
        }
    }
}
