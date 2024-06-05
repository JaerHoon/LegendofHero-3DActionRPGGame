using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SkillChoiceController : MonoBehaviour
{
    public enum SkillType { NormalATK, SkillATK, ItemRelic }
    public SkillType skillType;

    public Platform[] platforms = new Platform[3];

    public void Setting()
    {
        switch (skillType)
        {
            case SkillType.NormalATK: NormalATKSetting(); break;
            case SkillType.SkillATK: SkillATKSetting(); break;
            case SkillType.ItemRelic: ItemRelicSetting(); break;
        }
    }

    void NormalATKSetting()
    {
        if (CharacterManager.instance.choicedCharacter == CharacterManager.ChoicedCharacter.Warrior)
        {
            List<SkillInfo> skills = SkillManager.instance.warriorSkills.Where(n => n.skillType == SkillInfo.SkillType.NormalATK).ToList();

            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i].id == SkillManager.instance.gainedSkill_Warrior[0].id)
                {
                    skills.RemoveAt(i);
                }
            }

            int num = Random.Range(0, skills.Count);

            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].Setting(CharacterManager.ChoicedCharacter.Warrior, skills[i].id);
                platforms[i].skillChoiceController = this;
            }
        }
        else
        {

            List<SkillInfo> skills = SkillManager.instance.archerSkills.Where(n => n.skillType == SkillInfo.SkillType.NormalATK).ToList();

            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i].id == SkillManager.instance.gainedSkill_Archer[0].id)
                {
                    skills.RemoveAt(i);
                }
            }

            int num = Random.Range(0, skills.Count);

            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].Setting(CharacterManager.ChoicedCharacter.Archer, skills[i].id);
                platforms[i].skillChoiceController = this;
            }

        }
    }

    void SkillATKSetting()
    {
        if (CharacterManager.instance.choicedCharacter == CharacterManager.ChoicedCharacter.Warrior)
        {
            List<SkillInfo> skills = SkillManager.instance.warriorSkills.Where(n => n.skillType == SkillInfo.SkillType.SkillATK).ToList();

            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i].id == SkillManager.instance.gainedSkill_Warrior[1].id)
                {
                    skills.RemoveAt(i);
                }
            }

            int num = Random.Range(0, skills.Count);

            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].Setting(CharacterManager.ChoicedCharacter.Warrior, skills[i].id);
                platforms[i].skillChoiceController = this;
            }
        }
        else
        {
            List<SkillInfo> skills = SkillManager.instance.archerSkills.Where(n => n.skillType == SkillInfo.SkillType.SkillATK).ToList();

            for (int i = 0; i < skills.Count; i++)
            {
                if (skills[i].id == SkillManager.instance.gainedSkill_Archer[1].id)
                {
                    skills.RemoveAt(i);
                }
            }

            int num = Random.Range(0, skills.Count);

            for (int i = 0; i < platforms.Length; i++)
            {
                platforms[i].Setting(CharacterManager.ChoicedCharacter.Archer, skills[i].id);
                platforms[i].skillChoiceController = this;
            }

        }
    }

    void ItemRelicSetting()
    {

    }


    public void GetSkill(SkillInfo skill)
    {
        if(skill.characterType == SkillInfo.CharacterType.Warrior)
        {
            SkillManager.instance.warriorSkills[(int)skill.skillType] = skill; 
        }
        else
        {
            SkillManager.instance.archerSkills[(int)skill.skillType] = skill;
        }
    }

    public void GetItemRelic()
    {

    }

 }
