using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRogue : Monster
{
    private void Start()
    {
        Init();
    }

    protected override void TraceStat()
    {
        base.TraceStat();
        monsterAtk.OffATK();
        monsterAtk.EndAttack();
    }

    protected override void DieStat()
    {
        base.DieStat();
        monsterAtk.OffATK();
        monsterAtk.EndAttack();
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
}
