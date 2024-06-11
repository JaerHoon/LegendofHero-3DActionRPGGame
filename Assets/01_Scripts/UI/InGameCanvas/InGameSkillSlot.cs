using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSkillSlot : UIModel
{

    SkillManager skillManager;
    public List<SkillInfo> skills = new List<SkillInfo>(new SkillInfo[3]);
    public int? SkillCount = null;


    private void Start()
    {
        skillManager = SkillManager.instance != null ? SkillManager.instance : GameObject.FindFirstObjectByType<SkillManager>();
        skillManager.changeSkill += Setting;
        Setting();
    }

    public void Setting()
    {
        if (CharacterManager.instance.choicedCharacter == CharacterManager.ChoicedCharacter.Warrior)
        {
            for (int i = 0; i < SkillManager.instance.gainedSkill_Warrior.Length; i++)
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

        if (skills[1].skillCount > 1)
        {
            SkillCount = skills[1].skillCount;
        }
        else
        {
            SkillCount = null;
        }

        ChangeUI();
    }

    public void startCooltime(int Slotnum)
    {
        if (Slotnum == 2)
        {
            SlotCoolTimeStart?.Invoke(skills[2].cd, 2);
            SlotCoolTimeStart?.Invoke(skills[2].gcd, 1);
            SlotCoolTimeStart?.Invoke(skills[2].gcd, 0);
        }
        else if (Slotnum == 1)
        {
            if (skills[1].skillCount > 1 && SkillCount > 0 && SkillCount !=null)
            {
                SkillCount--;
                SlotCoolTimeStart?.Invoke(skills[1].gcd, 1);
                SlotCoolTimeStart?.Invoke(skills[1].gcd, 0);
                ChangeUI();
            }
            
            if(skills[1].skillCount > 1 && SkillCount == 0 && SkillCount != null)
            {
                SlotCoolTimeStart?.Invoke(skills[1].cd, 1);
                SlotCoolTimeStart?.Invoke(skills[1].gcd, 0);
                StartCoroutine(CountCoilTime());
            }

        }
        else if(Slotnum == 0)
        {
            SlotCoolTimeStart?.Invoke(skills[0].cd, 0);
            SlotCoolTimeStart?.Invoke(skills[0].gcd, 1);
        }
    }

    IEnumerator CountCoilTime()
    {
        yield return new WaitForSeconds(skills[1].cd);

        SkillCount = skills[1].skillCount;
        ChangeUI();
    }
 
}
