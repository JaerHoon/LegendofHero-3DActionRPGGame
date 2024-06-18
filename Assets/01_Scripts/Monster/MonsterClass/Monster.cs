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
    [HideInInspector]
    public MonsterGetCoin monsterGetCoin;

    public enum MonsterStat { Generate, Idle, Trace, Damage ,Attack ,Die}

    public MonsterStat monsterStat;
    protected bool IsDamage;
    protected bool IsPlayerdetected;
    public bool IsFreeze;

    [HideInInspector]
    public Transform playerTr;
    [HideInInspector]
    public Character player;
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
        monsterGetCoin = GetComponent<MonsterGetCoin>();
        monsterStat = MonsterStat.Generate;
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        player = playerTr.GetComponent<Character>();
        myCollider = GetComponent<CapsuleCollider>();
        MaxHP = monsterData.HP;
        curHP = MaxHP;
        monsterUI.Init();
       
    }

    protected virtual void ReSet()
    {
        monsterStat = MonsterStat.Generate;
        GenerateStat();

        player.playerDie += PlayerDie;
        myCollider.enabled = true;
        MaxHP = monsterData.HP;
        curHP = MaxHP;
        monsterUI.Init();

    }

    void PlayerDie()
    {
        StopAllCoroutines();
        anim.OnIdleAnim();
        monsterAtk.OffATK();
        IsPlayerdetected = false;
        IsDamage = false;
        IsFreeze = false;
        myCollider.enabled = false;
        monsterMove.DieMove();
        OutPool();
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
        StopAllCoroutines();
        anim.OnDyingAnim();
        monsterAtk.OffATK();
        IsPlayerdetected = false;
        IsDamage = false;
        IsFreeze = false;
        myCollider.enabled = false;
        monsterMove.DieMove();

       

    }

    public virtual void OnDie()
    {
        stageManager.MonsterDie(this.gameObject);
        OutPool();
    }

    public virtual void OutPool()
    {

    }

    public virtual void OnFreeze()
    {
        IsFreeze = true;
        ChangeStat(MonsterStat.Idle);
    }

    public virtual void OffFreeze()
    {
        IsFreeze = false;
    }

    private void OnDisable()
    {
        player.playerDie -= PlayerDie;
    }

}

