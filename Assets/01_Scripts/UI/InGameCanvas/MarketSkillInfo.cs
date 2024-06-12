using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketSkillInfo : UIModel
{
    public string noticeText;
    public string skillName;
    public string skillDescription;
    public Sprite skillIcon;
    public int SkillValue;


    public void Setting(SkillInfo skill)
    {
        if (skill.skillType == SkillInfo.SkillType.NormalATK)
        {
            noticeText = "Normal Attack";
        }
        else if (skill.skillType == SkillInfo.SkillType.SkillATK)
        {
            noticeText = "Skill Attack";
        }

        skillIcon = skill.image;
        skillName = skill.skillName;
        skillDescription = skill.skillDescription;
        SkillValue = skill.skillValue;

        ChangeUI();
    }

    public void Setting(BaseItem item)
    {
        noticeText = "Item Infomation";

        skillIcon = item.itemIcon;
        skillName = item.itemName;
        skillDescription = item.itemDescription;
        SkillValue = item.value;

        ChangeUI();
    }
}
