using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);


        CreateSkill();
    }

    SkillData warriorSkillData;
    SkillData archerSkillData;
    public List<SkillInfo> warriorSkills = new List<SkillInfo>();
    public List<SkillInfo> archerSkills = new List<SkillInfo>();
    public SkillInfo[] gainedSkill_Warrior = new SkillInfo[3];
    public SkillInfo[] gainedSkill_Archer = new SkillInfo[3];

    public delegate void ChangeSkill();
    public ChangeSkill changeSkill;

    private void CreateSkill()
    {
        warriorSkillData = Resources.Load("ScriptableObject/WarriorSkills") as SkillData;
        archerSkillData = Resources.Load("ScriptableObject/ArcherSkills") as SkillData;
        for (int i = 0; i < 9; i++)
        {
            warriorSkills.Add(warriorSkillData.skillInfo[i]);
            archerSkills.Add(archerSkillData.skillInfo[i]);
        }
       
    }

    public void GetSkill(CharacterManager.ChoicedCharacter choicedCharacter, int skill_Id)
    {
        if((int)choicedCharacter == 0)
        {
            for(int i=0; i< warriorSkills.Count; i++)
            {
                if(warriorSkills[i].id == skill_Id)
                {
                    gainedSkill_Warrior[(int)warriorSkills[i].skillType] = warriorSkills[i];
                }
            }
        }
        else
        {
            for(int i=0; i<archerSkills.Count; i++)
            {
                if(archerSkills[i].id == skill_Id)
                {
                    gainedSkill_Archer[(int)archerSkills[i].skillType] = archerSkills[i];
                }
            }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
