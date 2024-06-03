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
            int rand = 1;//Random.Range(0, 3);
            if (rand == 0)
            {
                ChangeAttackState(BOSSATTACKTYPE.AttackProjectile);
                yield return new WaitForSeconds(5.0f);
                ChangeAttackState(BOSSATTACKTYPE.AttackGround);
                yield return new WaitForSeconds(6.0f);
                ChangeAttackState(BOSSATTACKTYPE.Attack);
                yield return new WaitForSeconds(15.0f);
            }
            else if(rand == 1)
            {

                for (int i = 0; i < 3; i++)
                {
                    ChangeAttackState(BOSSATTACKTYPE.AttackProjectile);
                    yield return new WaitForSeconds(2.0f);
                    ChangeAttackState(BOSSATTACKTYPE.Attack);
                    yield return new WaitForSeconds(2.0f);
                    ChangeAttackState(BOSSATTACKTYPE.AttackGround);
                    yield return new WaitForSeconds(2.0f);
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

    public override void CheckPlayer()//일반 공격 패턴일 때(추격 -> 공격, 반복)
    {
        float distance = Vector3.Distance(playerTr.position, transform.position);
        if (distance > AttackDistanc)
        {

            ChangeState(MONSTERSTATE.Trace);


        }
        else
        {
            ChangeState(MONSTERSTATE.Attack);
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
        StartCoroutine(BossAttackPattern());
    }

    void WaitStat()
    {

    }

    void RiseStat()
    {
        anim.OnRiseAnim();
    }

    public void asd()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) { ChangeState(MONSTERSTATE.Rise); }
        else if (Input.GetKeyDown(KeyCode.W)) { ChangeState(MONSTERSTATE.Idle); }
        else if(Input.GetKeyDown(KeyCode.E)) { ChangeState(MONSTERSTATE.Trace); }
        else if(Input.GetKeyDown(KeyCode.R)) { ChangeState(MONSTERSTATE.Attack); }
        else if(Input.GetKeyDown(KeyCode.T)) { ChangeState(MONSTERSTATE.Die); }
    }
}
