using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWarrior : Monster
{
    private void Awake()
    {
        Init();
    }
    private void OnEnable()
    {
        ReSet();
    }
  

    public override void OutPool()
    {
       
        PoolFactroy.instance.OutPool(this.gameObject, Consts.MonsterWarrior); 
    }
}
