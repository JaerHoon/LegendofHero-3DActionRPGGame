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

    private void Start()
    {
        gainedSkill_Warrior[0] = warriorSkills[0];
        gainedSkill_Warrior[1] = warriorSkills[4];
        gainedSkill_Warrior[2] = warriorSkills[8];

        gainedSkill_Archer[0] = archerSkills[0];
        gainedSkill_Archer[1] = archerSkills[4];
        gainedSkill_Archer[2] = archerSkills[8];
        changeSkill?.Invoke();
    }

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

    public void GetSkill(CharacterManager.ChoicedCharacter choicedCharacter, SkillInfo skill_Id)
    {
        if((int)choicedCharacter == 0)
        {
            gainedSkill_Warrior[(int)skill_Id.skillType] = skill_Id;
        }
        else
        {
            gainedSkill_Archer[(int)skill_Id.skillType] = skill_Id;
        }

        changeSkill?.Invoke();

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
