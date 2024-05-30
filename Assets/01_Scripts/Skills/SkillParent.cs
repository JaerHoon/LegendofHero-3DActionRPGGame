using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillParent : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    float CalTotalDamage(float Damage, float playerCritDamage)//아이템 효과를 적용, 크리티컬 적용, 최종 데미지 데미지
    {
        int randCirtRate = Random.Range(0, 10);
        if (randCirtRate < 2) playerCritDamage = 1.5f + ItemManager.instance.itemToAddCritDamage;
        else playerCritDamage = 1f;
        return (Damage + ItemManager.instance.itemToAllSkillDamage) * playerCritDamage;//(스킬 데미지 + 추가 위력) *  크리티컬
    }



    public virtual void UsedSkill(SkillInfo skill,float playerCritDamage, float chargeRate)
    {
        float damage = CalTotalDamage(skill.damage, playerCritDamage) * ItemManager.instance.itemToAttackDamageRate * chargeRate;
        print($"스킬발동, 데미지 : {damage}");
    }

}
