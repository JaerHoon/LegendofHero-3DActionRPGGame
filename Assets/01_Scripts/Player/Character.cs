using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{


    protected Skills playerAttack;//�÷��̾� ��Ÿ ����
    protected Skills playerSkill;//��ų ����

    protected SkillParent[] skills = new SkillParent[2];



    protected virtual void UseSkill()
    {
        if (Input.GetMouseButtonDown(0))
        {
            skills[0].UsedSkill(playerAttack.Damage, playerAttack.CD, playerAttack.GCD, playerAttack.Scale, playerAttack.SpeedRate, playerAttack.EventRate, playerAttack.DamageCount, playerAttack.SkillCount, playerAttack.DamageRate);
        }
        if (Input.GetMouseButtonDown(2))
        {
            skills[1].UsedSkill(playerAttack.Damage, playerAttack.CD, playerAttack.GCD, playerAttack.Scale, playerAttack.SpeedRate, playerAttack.EventRate, playerAttack.DamageCount, playerAttack.SkillCount, playerAttack.DamageRate);
        }
    }
}
