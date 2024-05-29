using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected int playerHp;
    protected float playerAttackCD;
    protected float playerSkillCD;
    protected float playerNonHitCD_cur;//���� ��Ÿ��
    protected float playerNonHitCD;//���� ��Ÿ��
    protected float playerGCD;
    protected float playerSpeed;
    protected float playerCritDamage;

    protected float playerAttackChargeRate;
    protected float playerSkillChargeRate;
    protected float playerNonHitTime;

    protected bool isNonHit;



    protected Skills playerAttack;//�÷��̾� ��Ÿ ����
    protected Skills playerSkill;//��ų ����

    protected SkillParent[] skills = new SkillParent[2];//��Ÿ �� ��ų ����


    

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
            //print($"���� GCD : {playerGCD}��");
            if (playerGCD < 0)
            {
                playerGCD = 0;
                print("�۷ι���ٿ� �غ� �Ϸ�!");
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
                print("��Ÿ �غ� �Ϸ�!");
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
                print("��ų �غ� �Ϸ�!");
                yield break;
            }

        }
    }

    float CalTotalDamage(float Damage)//������ ȿ���� ����, ũ��Ƽ�� ����, ���� ������ ������
    {
        int randCirtRate = Random.Range(0, 10);
        if (randCirtRate < 2) playerCritDamage = 1.5f + ItemManager.instance.itemToAddCritDamage;
        else playerCritDamage = 1f;
        return (Damage + ItemManager.instance.itemToAllSkillDamage)  * playerCritDamage;//(��ų ������ + �߰� ����) *  ũ��Ƽ��
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
                print("��ų �غ� �Ϸ�!");
                yield break;
            }

        }
    }

    protected void OnUseSkill()
    {
       
        if (!isNonHit)//���� �ð����� ��ų ��� �Ұ�
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                isNonHit = true; print("1.5�ʰ� ����!");
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
