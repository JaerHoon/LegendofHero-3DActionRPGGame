using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackController : MonoBehaviour
{
    protected Skills playerAttack;//플레이어 평타 정보
    protected Skills playerSkill;//스킬 정보
    protected Skills PlayerDefence;//방어스킬 정보
    protected Skills[] skilldata = new Skills[3];
    protected SkillParent[] skills = new SkillParent[3];//평타 및 스킬 슬롯
    bool IsGlobaReady = true;
    bool IsATKReady = true;
    bool IsSkillready = true;
    bool IsDefenceReady = true;
    

    public virtual void OnAttack()
    {
        if(IsATKReady == true && IsGlobaReady ==true)
        {
            //스킬 실행
            skills[0].UsedSkill(playerAttack);
            IsATKReady = false;
            IsGlobaReady = false;
            //쿨타임 시작
            StartCoroutine(CoolTime(playerAttack,0));
            
        }

    }

    public virtual void OnSkill()
    {
        if (IsSkillready && IsGlobaReady)
        {
            //스킬 실행
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
            //스킬 실행
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
            //UI도 여기서??
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
