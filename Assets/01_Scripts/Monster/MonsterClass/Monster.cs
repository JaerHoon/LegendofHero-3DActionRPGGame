using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData monsterData;
    public MonsterAnim anim;
    public MonsterAttack monsterAtk;
    public MonsterDamage monsterdDmg;
    public MonsterMove monsterMove;

    public enum MonsterStat { Generate, Idle, Trace, Damage ,Attack ,Die }
    public MonsterStat monsterStat;
    protected bool IsDamage;

    public Transform playerTr;
    [SerializeField]
    protected float TraceDistanc;
    [SerializeField]
    protected float AttackDistanc;
    protected WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);

    float curHP;
    public float CurrenHP
    {
        get
        {
            return curHP;
        }
        set
        {
            if(value <= 0)
            {
                ChangeStat(MonsterStat.Die);
                curHP = 0;
            }
            else
            {
                curHP = value;
            }
           
        }
    }

  

    protected void Init()
    {
        anim = GetComponent<MonsterAnim>();
        monsterAtk = GetComponent<MonsterAttack>();
        monsterdDmg = GetComponent<MonsterDamage>();
        monsterMove = GetComponent<MonsterMove>();
        monsterStat = MonsterStat.Generate;
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        ChangeStat(monsterStat);
    }

    public void OnDamage()
    {
        IsDamage = true;
    }
    public void OffDamage()
    {
        IsDamage = false;
    }

    
    protected IEnumerator CheckStat()
    {
        while(monsterStat != MonsterStat.Die)
        {
            yield return waitForSeconds;
            if (IsDamage == false)
            {
                CheckPlayer();
            }
        }
    }
    protected virtual void CheckPlayer()
    {
        float distance = Vector3.Distance(playerTr.position, transform.position);
        if (distance > AttackDistanc && distance < TraceDistanc)
        {

            ChangeStat(MonsterStat.Trace);
            
            
        }
        else if (distance > 0.1f && distance < AttackDistanc)
        {
            ChangeStat(MonsterStat.Attack);
        }
        else
        {
            ChangeStat(MonsterStat.Idle);
        }
    }

    public void ChangeStat(MonsterStat Stat)
    {
        monsterStat = Stat;
        switch (Stat)
        {
           case MonsterStat.Generate: GenerateStat(); break;
           case MonsterStat.Idle: IdleStat(); break;
           case MonsterStat.Attack: AttackStat(); break;
           case MonsterStat.Trace: TraceStat(); break;
           case MonsterStat.Damage: DamageStat(); break;
        }
    }

    protected virtual void GenerateStat()
    {
        //애니메이션 적용
        //생성이 다 끝나면 그때..
        StartCoroutine(CheckStat());
    }

    protected virtual void IdleStat()
    {
        anim.OnIdleAnim();
        monsterMove.OffMove();
    }

    protected virtual void TraceStat()
    {
        monsterMove.OnMove();
        anim.OnMovingAnim();
    }

    protected virtual void AttackStat()
    {
        monsterAtk.OnATK();
        monsterMove.OffMove();
        anim.OnAtkAnim();
        // 어택 애니메이션은 몬스터 어택스크립트에서 호출
    }

    protected virtual void DamageStat()
    {
        OnDamage();
        monsterMove.OffMove();
    }

    protected virtual void DieStat()
    {
        monsterMove.OffMove();
        anim.OnDyingAnim();
        OnDie();
    }

    protected virtual void OnDie()
    {

    }

    private void Update()
    {
        print(monsterStat);
    }
}

