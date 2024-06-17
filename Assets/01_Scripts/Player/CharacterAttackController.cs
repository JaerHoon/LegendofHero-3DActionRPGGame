using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    protected InGameCanvasController inGameCanvasController;
    protected float playerSpeed;
    protected float playerCritDamage;
    protected float playerAttackChargeRate;
    protected float playerSkillChargeRate;

    protected bool isNonHit;
    protected bool[] isReadySkills = new bool[4];// 0 ��Ÿ, 1 ��ų, 2 ���, 3 �۷ι�
    public SkillInfo[] playerSkillsSlot = new SkillInfo[3];// 0 ��Ÿ, 1 ��ų, 2 ���

    protected SkillParent skillSc;//��ų ���� ��ũ��Ʈ

    [SerializeField]
    protected int maxSkillNum;//�ִ� ��ų ��� ���� Ƚ��
    [SerializeField]
    protected int curSkillNum;

    protected virtual void InIt()
    {
        inGameCanvasController = GameObject.FindFirstObjectByType<InGameCanvasController>();
    }

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
              
                yield break;
            }

        }
    }

    IEnumerator SKillCoolDown_CountType(string skillType, int MaxSkillCount, float skillCD)
    {
        
        float CD = skillCD;
        yield return new WaitForFixedUpdate();
        while (MaxSkillCount > curSkillNum)
        {
            yield return new WaitForSeconds(0.1f);
            CD -= 0.1f;
            if (CD < 0)
            {
                curSkillNum ++;
                print(curSkillNum);
                CD = skillCD;
                
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
                print($"�����ð� ����");
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

                //PlayerAttack.instance.block();
                

                if (PlayerAttack.instance != null) PlayerAttack.instance.block();
                if (ArcherAttack.instance != null) ArcherAttack.instance.block();
                inGameCanvasController.StartSkillCoolTime(2);
                print("1.5�ʰ� ����!");
                StartCoroutine(ImmunityTiem(playerSkillsSlot[2].gcd + ItemManager.instance.itemToNonHitTime));
                StartCoroutine(SkillCoolDown("���", 2, playerSkillsSlot[2].cd));
            }

            if (isReadySkills[3] == false) return;

            if (Input.GetMouseButtonDown(0) && isReadySkills[0] == true)
            {

                //PlayerAttack.instance.KnightAttack();
                

                if(PlayerAttack.instance != null) PlayerAttack.instance.KnightAttack();
                if(ArcherAttack.instance != null) ArcherAttack.instance.ArrowAttack();
                inGameCanvasController.StartSkillCoolTime(0);
                skillSc.UsedSkill(playerSkillsSlot[0], playerCritDamage, playerAttackChargeRate);
                StartCoroutine(SkillCoolDown("��Ÿ", 0, playerSkillsSlot[0].cd));
                StartCoroutine(SkillGlobalCoolDown(playerSkillsSlot[0].gcd + ItemManager.instance.itemToSkillGCD));
            }
            /*else if (Input.GetMouseButtonUp(0))
            {
                if (ArcherAttack.instance != null) ArcherAttack.instance.AttackStop();
            }*/

            if (Input.GetMouseButtonDown(1) &&  isReadySkills[1] == true &&  curSkillNum >= 1 )
            {
                
                //PlayerAttack.instance.skillAttack();
                

                if (PlayerAttack.instance != null) PlayerAttack.instance.skillAttack();
                if (ArcherAttack.instance != null) ArcherAttack.instance.skillAttack();
                //inGameCanvasController.StartSkillCoolTime(1);
                
                skillSc.UsedSkill(playerSkillsSlot[1], playerCritDamage, playerAttackChargeRate);
                //StartCoroutine(SkillCoolDown("��ų", 1, playerSkillsSlot[1].cd));

                if(maxSkillNum == curSkillNum)
                    StartCoroutine(SKillCoolDown_CountType("��ų", playerSkillsSlot[1].skillCount, playerSkillsSlot[1].cd));

                curSkillNum--;
                print("fire : " + curSkillNum);
                StartCoroutine(SkillGlobalCoolDown(playerSkillsSlot[1].gcd + ItemManager.instance.itemToSkillGCD));
            }
        }

    }
}
