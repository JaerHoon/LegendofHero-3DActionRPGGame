using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : Buff
{
    WaitForSeconds witForSeconds;
    public Monster monster;

    private void Start()
    {
        witForSeconds = new WaitForSeconds(0.2f);
    }

    public override void OnBuff(float duration, float dotTime, float buffpow)
    {
        base.OnBuff(duration, dotTime, buffpow);
        monster.OnFreeze();
        StartCoroutine(Freezing());
    }

    IEnumerator Freezing()
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
        monsterBuff.OffFreeze();
    }
}
