using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curse : Buff
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
        monsterDamage.UpDamage = buffPow;
        StartCoroutine(Cursing());

    }

    IEnumerator Cursing()
    {
        while (buffRuntime > 0)
        {
            yield return witForSeconds;
            buffRuntime -= 0.2f;

        }

        OffBuff();
    }

    public override void OffBuff()
    {
        monsterDamage.UpDamage = 0;
        monsterBuff.OffCurse();
    }
}
