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
    public MonsterUIModel monsterUI;

    public enum MonsterStat { Generate, Idle, Trace, Damage ,Attack ,Die }
    public MonsterStat monsterStat;
    protected bool IsDamage;
    protected bool IsPlayerdetected;

    public Transform playerTr;
    [SerializeField]
    protected float TraceDistanc;
    [SerializeField]
    protected float AttackDistanc;
    protected WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
    float maxHP;

    public float MaxHP { get; set; }
    

    

    protected float curHP;
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
            monsterUI.UIupdate();
        }
    }

    

    protected virtual void Init()
    {
        anim = GetComponent<MonsterAnim>();
        monsterAtk = GetComponent<MonsterAttack>();
        monsterdDmg = GetComponent<MonsterDamage>();
        monsterMove = GetComponent<MonsterMove>();
        monsterUI = GetComponent<MonsterUIModel>();
        monsterStat = MonsterStat.Generate;
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        GenerateStat();
        MaxHP = monsterData.HP;
        curHP = MaxHP;
        monsterUI.Init();
    }

    public void OnDamage()
    {
        IsDamage = true;
    }
    public void OffDamage()
    {
        IsDamage = false;
    }

    public void DetectedPlayer()
    {
        IsPlayerdetected = true;
    }

    public void CheckStart()
    {
        StartCoroutine(CheckStat());
    }

    public IEnumerator CheckStat()
    {
        while(monsterStat != MonsterStat.Die)
        {
            yield return waitForSeconds;
            if (IsDamage == false)
            {
                if (IsPlayerdetected)
                {
                    TracePlayer();
                }
                else
                {
                    CheckPlayer();
                }
               
            }
        }
    }
    public virtual void CheckPlayer()
    {
       
        float distance = Vector3.Distance(playerTr.position, transform.position);
        if (distance > AttackDistanc && distance < TraceDistanc)
        {

            ChangeStat(MonsterStat.Trace);
            
            
        }
        else if (distance > 0.1f && distance < AttackDistanc)
        {
            IsPlayerdetected = true;
            ChangeStat(MonsterStat.Attack);
        }
        else
        {
            ChangeStat(MonsterStat.Idle);
        }
    }

    protected virtual void TracePlayer()
    {
        float distance = Vector3.Distance(playerTr.position, transform.position);

        if (distance > 0.1f && distance < AttackDistanc)
        {
            IsPlayerdetected = true;
            ChangeStat(MonsterStat.Attack);
        }
        else
        {
            ChangeStat(MonsterStat.Trace);
        }


    }

    public void ChangeStat(MonsterStat Stat)

    {
        if (monsterStat == Stat) return;
        monsterStat = Stat;
        switch (Stat)
        {
           case MonsterStat.Generate: GenerateStat(); break;
           case MonsterStat.Idle: IdleStat(); break;
           case MonsterStat.Attack: AttackStat(); break;
           case MonsterStat.Trace: TraceStat(); break;
           case MonsterStat.Damage: DamageStat(); break;
           case MonsterStat.Die: DieStat(); break;
        }
    }


    protected virtual void GenerateStat()
    {
        anim.OnGenerateAnim();
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

    public virtual void AttackStat()
    {
        monsterAtk.OnATK();
        monsterMove.OffMove();
        anim.OnAtkAnim();
        // 어택 애니메이션은 몬스터 어택스크립트에서 호출
    }

    protected virtual void DamageStat()
    {
        OnDamage();
        anim.OnDamageAnim();
        monsterAtk.OffATK();
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
      
    }
}

