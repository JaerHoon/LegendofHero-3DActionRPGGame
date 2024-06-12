using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{


    protected float playerSpeed;
    protected float playerCritDamage;
    protected float playerAttackChargeRate;
    protected float playerSkillChargeRate;

    protected bool isNonHit;
    protected bool[] isReadySkills = new bool[4];// 0 평타, 1 스킬, 2 방어, 3 글로벌
    public SkillInfo[] playerSkillsSlot = new SkillInfo[3];// 0 평타, 1 스킬, 2 방어

    protected SkillParent skillSc;//스킬 내부 스크립트

    [SerializeField]
    protected int maxSkillNum;//최대 스킬 사용 가능 횟수
    [SerializeField]
    protected int curSkillNum;

    IEnumerator SkillCoolDown(string skillType, int isSkillReady, float skillCD)
    {
        isReadySkills[isSkillReady] = false;
        float CD = skillCD;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            CD -= 0.1f;
            if (CD < 0)
            {
                isReadySkills[isSkillReady] = true;
                print($"{skillType} 준비 완료!");
                yield break;
            }

        }
    }

    IEnumerator SKillCoolDown_CountType(string skillType, int MaxSkillCount, float skillCD)
    {
        print("SKillCoolDown_CountType 코루틴 동작");
        float CD = skillCD;
        yield return new WaitForFixedUpdate();
        while (MaxSkillCount > curSkillNum)
        {
            yield return new WaitForSeconds(0.1f);
            CD -= 0.1f;
            if (CD < 0)
            {
                curSkillNum ++;
                CD = skillCD;
                print($"{skillType} 사용 횟수 1 증가!");
                print($"현재 사용 가능 횟수 : {curSkillNum}");
                if(MaxSkillCount == curSkillNum)
                    yield break;
            }

        }
    }

    IEnumerator SkillGlobalCoolDown(float skillGCD)
    {
        isReadySkills[3] = false;
        float GCD = skillGCD;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            GCD -= 0.1f;
            if (GCD < 0)
            {
                isReadySkills[3] = true;
                print($"글로벌 쿨다운 완료!");
                yield break;
            }

        }
    }

    IEnumerator ImmunityTiem(float ImmunTime)
    {
        isNonHit = true;
        float Immun = ImmunTime;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            Immun -= 0.1f;
            if (Immun < 0)
            {
                isNonHit = false;
                print($"무적시간 종료");
                yield break;
            }

        }
    }

    protected void OnUseSkill()
    {
        if (!isNonHit)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isReadySkills[2] == true)
            {
                if (PlayerAttack.instance != null) PlayerAttack.instance.block();
                if (ArcherAttack.instance != null) ArcherAttack.instance.block();
                print("1.5초간 무적!");
                StartCoroutine(ImmunityTiem(playerSkillsSlot[2].gcd + ItemManager.instance.itemToNonHitTime));
                StartCoroutine(SkillCoolDown("방어", 2, playerSkillsSlot[2].cd));
            }

            if (isReadySkills[3] == false) return;

            if (Input.GetMouseButtonDown(0) && isReadySkills[0] == true)
            {
                if(PlayerAttack.instance != null) PlayerAttack.instance.KnightAttack();
                if (ArcherAttack.instance != null) ArcherAttack.instance.ArrowAttack();
                skillSc.UsedSkill(playerSkillsSlot[0], playerCritDamage, playerAttackChargeRate);
                StartCoroutine(SkillCoolDown("평타", 0, playerSkillsSlot[0].cd));
                StartCoroutine(SkillGlobalCoolDown(playerSkillsSlot[0].gcd + ItemManager.instance.itemToSkillGCD));
            }

            if (Input.GetMouseButtonDown(1) &&  isReadySkills[1] == true &&  curSkillNum >= 1 )
            {
                if (PlayerAttack.instance != null) PlayerAttack.instance.skillAttack();
                if (ArcherAttack.instance != null) ArcherAttack.instance.skillAttack();
                skillSc.UsedSkill(playerSkillsSlot[1], playerCritDamage, playerAttackChargeRate);
                //StartCoroutine(SkillCoolDown("스킬", 1, playerSkillsSlot[1].cd));

                if(maxSkillNum == curSkillNum)
                    StartCoroutine(SKillCoolDown_CountType("스킬", playerSkillsSlot[1].skillCount, playerSkillsSlot[1].cd));

                curSkillNum--;
                StartCoroutine(SkillGlobalCoolDown(playerSkillsSlot[1].gcd + ItemManager.instance.itemToSkillGCD));
            }
        }

    }
}
