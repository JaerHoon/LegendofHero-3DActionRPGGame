using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMage : Monster
{
    private void Awake()
    {
        Init();
    }
    private void OnEnable()
    {
        ReSet();
    }

    public override void OnDie()
    {
        base.OnDie();
        PoolFactroy.instance.OutPool(this.gameObject, Consts.MonsterMage);
    }
}
