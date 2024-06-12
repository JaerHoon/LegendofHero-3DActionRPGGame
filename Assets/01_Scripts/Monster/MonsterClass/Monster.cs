using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public MonsterData monsterData;
    [HideInInspector]
    public MonsterAnim anim;
    [HideInInspector]
    public MonsterAttack monsterAtk;
    [HideInInspector]
    public MonsterDamage monsterdDmg;
    [HideInInspector]
    public MonsterMove monsterMove;
    [HideInInspector]
    public MonsterUIModel monsterUI;

    public enum MonsterStat { Generate, Idle, Trace, Damage ,Attack ,Die}
    [HideInInspector]
    public MonsterStat monsterStat;
    protected bool IsDamage;
    protected bool IsPlayerdetected;
    protected bool IsFreeze;

    [HideInInspector]
    public Transform playerTr;
    StageManager stageManager;

    [SerializeField]
    protected float TraceDistanc;
    [SerializeField]
    protected float AttackDistanc;
    protected WaitForSeconds waitForSeconds = new WaitForSeconds(0.1f);
    protected CapsuleCollider myCollider;
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
        stageManager = GameObject.FindFirstObjectByType<StageManager>();
        anim = GetComponent<MonsterAnim>();
        monsterAtk = GetComponent<MonsterAttack>();
        monsterdDmg = GetComponent<MonsterDamage>();
        monsterMove = GetComponent<MonsterMove>();
        monsterUI = GetComponent<MonsterUIModel>();
        monsterStat = MonsterStat.Generate;
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        myCollider = GetComponent<CapsuleCollider>();
        GenerateStat();
        MaxHP = monsterData.HP;
        curHP = MaxHP;
        monsterUI.Init();
    }

    protected virtual void ReSet()
    {
        GenerateStat();
        MaxHP = monsterData.HP;
        curHP = MaxHP;
        monsterUI.Init();
    }

    void PlayerDie()
    {

    }

    private void OnEnable()
    {
        if (myCollider != null) myCollider.enabled = true; 
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
        while (monsterStat != MonsterStat.Die)
        {
            yield return waitForSeconds;
            if (IsDamage == false && IsFreeze == false)
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
        myCollider.enabled = false;
       
    }

    public virtual void OnDie()
    {
        stageManager.MonsterDie(this.gameObject);
    }

    public virtual void OnFreeze()
    {
        IsFreeze = true;
    }

    public virtual void OffFreeze()
    {
        IsFreeze = false;
    }

    private void Update()
    {
      
    }
}

