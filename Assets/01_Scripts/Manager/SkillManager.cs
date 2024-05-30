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

    // Update is called once per frame
    void Update()
    {
        
    }
}
