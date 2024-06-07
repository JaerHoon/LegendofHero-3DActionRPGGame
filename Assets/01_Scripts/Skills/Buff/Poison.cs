using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Buff
{
    WaitForSeconds witForSeconds;
    public MonsterDamage monsterDamage;

    private void Start()
    {
        witForSeconds = new WaitForSeconds(0.2f);
    }

    public override void OnBuff(float duration, float dotTime, float buffpow)
    {
        base.OnBuff(duration, dotTime, buffpow);

        StartCoroutine(PoisonDamage());
    }

    IEnumerator PoisonDamage()
    {
        
        while (buffRuntime > 0)
        {
            DotDamage();
            yield return new WaitForSeconds(dotTime);
            buffRuntime -= dotTime;

        }

        OffBuff();
    }

    void DotDamage()
    {
        monsterDamage.OnPoisonDamage(buffPow);
    }

    public override void OffBuff()
    {
        IsOnBuff = false;
        monsterBuff.OffPoison();
    }

}
