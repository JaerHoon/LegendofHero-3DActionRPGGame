using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int playerHp;
    protected float playerAttackCD;
    protected float playerSkillCD;
    protected float playerGCD;
    protected float playerSpeed;
    



    protected Skills playerAttack;//플레이어 평타 정보
    protected Skills playerSkill;//스킬 정보

    protected SkillParent[] skills = new SkillParent[2];



    protected void GlobalCoolDownSKill()
    {
        if (playerGCD > 0)
        {
            StartCoroutine(TimeDownGCD());
        }
    }

    protected void CoolDownAttack()
    {
        if (playerAttackCD > 0)
        {
            StartCoroutine(TimeDownAttack());
        }
    }

    protected void CoolDownSKill()
    {
        
        if (playerSkillCD > 0)
        {
            StartCoroutine(TimeDownSkill());
        }

    }

    IEnumerator TimeDownGCD()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            playerGCD -= 0.1f;
            print($"남은 GCD : {playerGCD}초");
            if (playerGCD < 0)
            {
                playerGCD = 0;
                print("글로벌쿨다운 준비 완료!");
                yield break;
            }
        }

    }

    IEnumerator TimeDownAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            playerAttackCD -= 0.1f;
            if (playerAttackCD < 0)
            {
                playerAttackCD = 0;
                print("평타 준비 완료!");
                yield break;
            }
        }
        
    }

    IEnumerator TimeDownSkill()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            playerSkillCD -= 0.1f;
            if (playerSkillCD < 0)
            {
                playerSkillCD = 0;
                print("스킬 준비 완료!");
                yield break;
            }

        }
    }

    protected void OnUseSkill()
    {
        if (playerGCD > 0) return;
        if (Input.GetMouseButtonDown(0) && playerAttackCD == 0)
        {
            skills[0].UsedSkill(playerAttack.Damage, playerAttack.CD, playerAttack.GCD, playerAttack.Scale, playerAttack.SpeedRate, playerAttack.EventRate, playerAttack.DamageCount, playerAttack.SkillCount, playerAttack.DamageRate);
            playerAttackCD = playerAttack.CD;
            playerGCD = playerAttack.GCD;
            GlobalCoolDownSKill();
            CoolDownAttack();
        }
        if (Input.GetMouseButtonDown(1) && playerSkillCD == 0)
        {
            skills[1].UsedSkill(playerSkill.Damage, playerSkill.CD, playerSkill.GCD, playerSkill.Scale, playerSkill.SpeedRate, playerSkill.EventRate, playerSkill.DamageCount, playerSkill.SkillCount, playerSkill.DamageRate);
            playerSkillCD = playerSkill.CD;
            playerGCD = playerSkill.GCD;
            GlobalCoolDownSKill();
            CoolDownSKill();
        }
    }
}
