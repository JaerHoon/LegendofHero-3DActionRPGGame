using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRogue : Monster
{
    private void Awake()
    {
        Init();
    }
    private void OnEnable()
    {
        ReSet();
    }
   

    protected override void TraceStat()
    {
        base.TraceStat();
        monsterAtk.OffATK();
        monsterAtk.EndAttack();
    }

    protected override void DieStat()
    {
        print("DieState");
        base.DieStat();
        
    }

    public override void OnFreeze()
    {
        base.OnFreeze();
        monsterAtk.OffATK();
        monsterAtk.EndAttack();
    }

    protected override void IdleStat()
    {
        base.IdleStat();
        monsterAtk.OffATK();
        monsterAtk.EndAttack();
    }

    protected override void DamageStat()
    {
        base.DamageStat();
        monsterAtk.EndAttack();
    }

    public override void OnDie()
    {
        base.OnDie();
        PoolFactroy.instance.OutPool(this.gameObject, Consts.MonsterRogue);
    }
}
