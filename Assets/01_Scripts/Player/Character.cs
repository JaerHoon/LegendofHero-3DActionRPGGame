using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int playerHp;
    protected float playerAttackCD;
    protected float playerSkillCD;
    protected float playerNonHitCD_cur;//현재 쿨타임
    protected float playerNonHitCD;//현재 쿨타임
    protected float playerGCD;
    protected float playerSpeed;
    protected float playerCritDamage;

    protected float playerAttackChargeRate;
    protected float playerSkillChargeRate;
    protected float playerNonHitTime;

    protected bool isNonHit;



    protected Skills playerAttack;//플레이어 평타 정보
    protected Skills playerSkill;//스킬 정보

    protected SkillParent[] skills = new SkillParent[2];//평타 및 스킬 슬롯


    

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
            //print($"남은 GCD : {playerGCD}초");
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

    float CalTotalDamage(float Damage)//아이템 효과를 적용, 크리티컬 적용, 최종 데미지 데미지
    {
        int randCirtRate = Random.Range(0, 10);
        if (randCirtRate < 2) playerCritDamage = 1.5f + ItemManager.instance.itemToAddCritDamage;
        else playerCritDamage = 1f;
        return (Damage + ItemManager.instance.itemToAllSkillDamage)  * playerCritDamage;//(스킬 데미지 + 추가 위력) *  크리티컬
    }

    IEnumerator CoolDownNonHitTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            playerNonHitCD_cur -= 0.1f;
            if (playerNonHitCD_cur < 0)
            {
                playerNonHitCD_cur = 0;
                isNonHit = false;
                print("방어스킬 준비 완료!");
                yield break;
            }

        }
    }

    protected void OnUseSkill()
    {
       
        if (!isNonHit)//무적 시간에는 스킬 사용 불가
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isNonHit = true; print("1.5초간 무적!");
                playerNonHitCD_cur = playerNonHitCD;
                StartCoroutine(CoolDownNonHitTime());
            }

            if (playerGCD > 0) return;

            if (Input.GetMouseButtonDown(0) && playerAttackCD == 0)
            {
                skills[0].UsedSkill(playerAttack);
                playerAttackCD = playerAttack.CD;
                playerGCD = playerAttack.GCD + ItemManager.instance.itemToSkillGCD;
                GlobalCoolDownSKill();
                CoolDownAttack();
            }
            if (Input.GetMouseButtonDown(1) && playerSkillCD == 0)
            {
                skills[1].UsedSkill(playerSkill);
               playerSkillCD = playerSkill.CD;
                playerGCD = playerSkill.GCD + ItemManager.instance.itemToSkillGCD;
                GlobalCoolDownSKill();
                CoolDownSKill();
            }
        }
    }
}
