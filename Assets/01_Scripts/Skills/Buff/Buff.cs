using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    public MonsterBuff monsterBuff;

    public float duration; // 버프지속시간
   
    public float dotTime; // 도트 시간
  
    public float buffPow;

    public float buffRuntime;

    public bool IsOnBuff;

    public virtual void OnBuff(float duration, float dotTime, float buffpow)
    {
       
        if (!IsOnBuff)
        {
            IsOnBuff = true;
            this.duration = duration;
            this.dotTime = dotTime;
            this.buffPow = buffpow;
            buffRuntime = duration;
        }
        else
        {
            this.duration = duration+buffRuntime;
            this.dotTime = dotTime;
            this.buffPow = buffpow;
            StopAllCoroutines();
        }

    }

    public virtual void OffBuff()
    {

    }
}
