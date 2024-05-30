using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    protected Skills playerAttack;//�÷��̾� ��Ÿ ����
    protected Skills playerSkill;//��ų ����
    protected Skills PlayerDefence;//��ų ����
    protected Skills[] skilldata = new Skills[3];
    protected SkillParent[] skills = new SkillParent[3];//��Ÿ �� ��ų ����
    bool IsGlobaReady = true;
    bool IsATKReady = true;
    bool IsSkillready = true;
    bool IsDefenceReady = true;
    

    public virtual void OnAttack()
    {
        if(IsATKReady == true && IsGlobaReady ==true)
        {
            //��ų ����
            skills[0].UsedSkill(playerAttack);
            IsATKReady = false;
            IsGlobaReady = false;
            //��Ÿ�� ����
            StartCoroutine(CoolTime(playerAttack,0));
            
        }

    }

    public virtual void OnSkill()
    {
        if (IsSkillready && IsGlobaReady)
        {
            //��ų ����
            skills[1].UsedSkill(playerSkill);
            IsSkillready = false;
            IsGlobaReady = false;
            StartCoroutine(CoolTime(playerAttack, 1));
        }
        
       
    }

    public virtual void OnDefence()
    {
        if (IsDefenceReady)
        {
            //��ų ����
            skills[3].UsedSkill(PlayerDefence);
            IsDefenceReady = false;
            IsGlobaReady = false;
            StartCoroutine(CoolTime(playerAttack, 2));
        }
    }

    IEnumerator CoolTime(Skills skill,int SkiilsNum)
    {
        float Cooltime = skill.CD;
        float Curtime = 0;
        while (Curtime > Cooltime)
        {
            yield return new WaitForSeconds(0.1f);
            Curtime += 0.1f;
            //UI�� ���⼭??
        }
        switch (SkiilsNum)
        {
            case 0 : IsATKReady = true; break;
            case 1: IsSkillready = true; break;
            case 2: IsDefenceReady = true; break;
        }
        
    }

    IEnumerator GLoBalCoolTime(Skills skill)
    {
        float Cooltime = skill.GCD;
        float Curtime = 0;
        while (Curtime > Cooltime)
        {
            yield return new WaitForSeconds(0.1f);
            Curtime += 0.1f;
            
        }

        IsGlobaReady = true;
    }
}
