using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Monster
{

    public enum MONSTERSTATE { Wait, Rise, Idle, Trace, Attack, Die }
    public MONSTERSTATE monsterstate;
    public enum BOSSATTACKTYPE { None,Attack, AttackProjectile, AttackGround}
    public BOSSATTACKTYPE bossAttackType;

    public GolemUI golemUI;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        IsPlayerdetected = false;
        golemUI = GetComponent<GolemUI>();
        ChangeState(MONSTERSTATE.Wait);
        //StartCoroutine(CheckStat());
        StartCoroutine(CalDis());
        golemUI.UI_Update();
    }


    protected override void Init()
    {
        anim = GetComponent<MonsterAnim>();
        monsterAtk = GetComponent<MonsterAttack>();
        monsterdDmg = GetComponent<MonsterDamage>();
        monsterMove = GetComponent<MonsterMove>();
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        MaxHP = monsterData.HP;
        curHP = MaxHP;

    }

    public float CurrentHP
    {
        get
        {
            return curHP;
        }
        set
        {
            if (value <= 0)
            {
                ChangeState(MONSTERSTATE.Die);
                curHP = 0;
            }
            else
            {
                curHP = value;

            }
            golemUI.UI_Update();
        }
    }


    IEnumerator CalDis()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            print(Vector3.Distance(playerTr.position, transform.position));
            print(monsterstate);
            print(bossAttackType);
        }
       
    }
    IEnumerator BossAttackPattern()
    {
        while (monsterstate != MONSTERSTATE.Die)
        {
            int rand = Random.Range(0, 2);
            if (rand == 0)
            {
                BossPattern1();
                yield return new WaitForSeconds(8.0f);
                BossPattern2();
                yield return new WaitForSeconds(1.5f);
                BossPattern3();
                yield return new WaitForSeconds(1.5f);
                BossPattern2();
                yield return new WaitForSeconds(1.5f);
                BossPattern3();
                yield return new WaitForSeconds(1.5f);

            }
            else if(rand == 1)
            {

                for (int i = 0; i < 3; i++)
                {
                    BossPattern2();
                    yield return new WaitForSeconds(1.5f);
                    BossPattern1();
                    yield return new WaitForSeconds(3.0f);
                    BossPattern3();
                    yield return new WaitForSeconds(1.5f);
                    BossPattern1();
                    yield return new WaitForSeconds(3.0f);
                    
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    ChangeAttackState(BOSSATTACKTYPE.AttackProjectile);
                    yield return new WaitForSeconds(2.0f);
                    ChangeAttackState(BOSSATTACKTYPE.AttackGround);
                    yield return new WaitForSeconds(2.0f);
                }
               
            }
        }
    }

    void BossPattern1()//추적 후 평타
    {
        IsPlayerdetected = true;
        ChangeAttackState(BOSSATTACKTYPE.None);
    }
    void BossPattern2()//투사체 발사
    {
        IsPlayerdetected = false;
        ChangeState(MONSTERSTATE.Idle);
        monsterMove.OffMove();
        ChangeAttackState(BOSSATTACKTYPE.AttackProjectile);
    }
    void BossPattern3()//돌 떨구기
    {
        IsPlayerdetected = false;
        ChangeState(MONSTERSTATE.Idle);
        monsterMove.OffMove();
        ChangeAttackState(BOSSATTACKTYPE.AttackGround);
    }

    public override void CheckPlayer()//일반 공격 패턴일 때(추격 -> 공격, 반복)
    {
        if (IsPlayerdetected)//몬스터 상태가 idle 추격 후 공격
        {
            float distance = Vector3.Distance(playerTr.position, transform.position);
            if(distance < AttackDistanc)//공격 거리일때
            {
                Vector3 direction = playerTr.position - transform.position;
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1.5f * Time.deltaTime);
                //ChangeAttackState(BOSSATTACKTYPE.Attack);//일반 공격
                ChangeState(MONSTERSTATE.Attack);
            }
            else
            {
                
                if(bossAttackType == BOSSATTACKTYPE.None)
                    ChangeState(MONSTERSTATE.Trace);//그 후 추격
                //ChangeAttackState(BOSSATTACKTYPE.None);//멀어지면 다시 일반 상태
                
            }
        }
       
       
    }

    public void ChangeState(MONSTERSTATE State)
    {
        if (monsterstate == State) return;
        monsterstate = State;
        switch (State)
        {
            case MONSTERSTATE.Wait: WaitStat(); break;
            case MONSTERSTATE.Rise: RiseStat(); break;
            case MONSTERSTATE.Idle: IdleStat(); break;
            case MONSTERSTATE.Attack: AttackStat(); break;
            case MONSTERSTATE.Trace: TraceStat(); break;
            case MONSTERSTATE.Die: DieStat(); break;
        }
    }

    void ChangeAttackState(BOSSATTACKTYPE AttackState)
    {
        bossAttackType = AttackState;
        switch (AttackState)
        {
            case BOSSATTACKTYPE.None:
                break;
            case BOSSATTACKTYPE.Attack:
                monsterAtk.Attack1();
                break;
            case BOSSATTACKTYPE.AttackProjectile:
                monsterAtk.Attack2();
                break;
            case BOSSATTACKTYPE.AttackGround:
                monsterAtk.Attack3();
                break;
            default:
                break;
        }
    }


    public override void AttackStat()
    {
        ChangeAttackState(BOSSATTACKTYPE.Attack);
        Invoke("DelayAttack", 1.5f);
        monsterMove.OffMove();
    }

    void DelayAttack()
    {
        ChangeAttackState(BOSSATTACKTYPE.None);
        ChangeState(MONSTERSTATE.Idle);
    }

    protected override void TraceStat()
    {
        monsterMove.OnMove();
        anim.OnMovingAnim();
    }

    void WaitStat()
    {

    }

    void RiseStat()
    {
        anim.OnRiseAnim();
    }

    protected override void DieStat()
    {
        monsterMove.OffMove();
        anim.OnDyingAnim();
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) { ChangeState(MONSTERSTATE.Rise); }
        else if (Input.GetKeyDown(KeyCode.W)) { ChangeState(MONSTERSTATE.Idle); }
        else if(Input.GetKeyDown(KeyCode.E)) { ChangeState(MONSTERSTATE.Trace); StartCoroutine(BossAttackPattern()); }
        else if(Input.GetKeyDown(KeyCode.R)) { ChangeState(MONSTERSTATE.Attack); }
        else if(Input.GetKeyDown(KeyCode.T)) { ChangeState(MONSTERSTATE.Die); }

        
        CheckPlayer();
    }
}
