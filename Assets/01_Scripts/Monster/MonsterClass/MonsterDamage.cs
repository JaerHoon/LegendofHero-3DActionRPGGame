using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    protected Monster monster;
    [SerializeField]
    protected ParticleSystem FX;

    protected void Init()
    {
        monster = GetComponent<Monster>();
    }

    public virtual void OnDamage(float pow)
    {   
        monster.DetectedPlayer();
        monster.ChangeStat(Monster.MonsterStat.Damage);
        monster.CurrenHP -= pow;
        DamageFx(pow);
    }

    public virtual void DamageFx(float amount)
    {
        FX.Play();

    }
}
