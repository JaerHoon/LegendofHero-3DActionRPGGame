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

    float CalTotalDamage(float Damage, float playerCritDamage)//������ ȿ���� ����, ũ��Ƽ�� ����, ���� ������ ������
    {
        int randCirtRate = Random.Range(0, 10);
        if (randCirtRate < 2) playerCritDamage = 1.5f + ItemManager.instance.itemToAddCritDamage;
        else playerCritDamage = 1f;
        return (Damage + ItemManager.instance.itemToAllSkillDamage) * playerCritDamage;//(��ų ������ + �߰� ����) *  ũ��Ƽ��
    }



    public virtual void UsedSkill(SkillInfo skill,float playerCritDamage, float chargeRate)
    {
        float damage = CalTotalDamage(skill.damage, playerCritDamage) * ItemManager.instance.itemToAttackDamageRate * chargeRate;
        print($"��ų�ߵ�, ������ : {damage}");
    }

}
