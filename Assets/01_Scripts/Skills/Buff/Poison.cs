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
            yield return witForSeconds;
            buffRuntime -= 0.2f;
            if(buffRuntime % dotTime == 0)
            {
                DotDamage();
            }

        }

        OffBuff();
    }

    void DotDamage()
    {
        monsterDamage.OnDamage(buffPow);
    }

    public override void OffBuff()
    {
        IsOnBuff = false;
        monsterBuff.OffPoison();
    }

}
