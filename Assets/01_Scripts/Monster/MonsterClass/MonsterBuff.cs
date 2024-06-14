using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBuff : MonoBehaviour
{
    Poison poison;
    Freeze freeze;
    Curse curse;

    MonsterDebuff monsterDebuff;
    Monster monster;

    [SerializeField]
    GameObject poisonFX;
    [SerializeField]
    GameObject freezeFX;
    [SerializeField]
    GameObject curseFx;

    protected virtual void Init()
    {
        poison = gameObject.AddComponent<Poison>();
        poison.monsterBuff = this;
        poison.monsterDamage = GetComponent<MonsterDamage>();
        freeze = gameObject.AddComponent<Freeze>();
        freeze.monsterBuff = this;
        freeze.monster = GetComponent<Monster>();
        curse = gameObject.AddComponent<Curse>();
        curse.monsterBuff = this;
        curse.monsterDamage = GetComponent<MonsterDamage>();
        poisonFX.SetActive(false);
        freezeFX.SetActive(false);
        curseFx.SetActive(false);
        monster = GetComponent<Monster>();

        monsterDebuff = GetComponent<MonsterDebuff>();
    }

    private void OnEnable()
    {
        OffPoison();
        OffFreeze();
        OffPoison();
    }

    public virtual void OnPoison(float duration, float dotTime, float buffpow)
    {
        poison.OnBuff(duration, dotTime, buffpow);
        poisonFX.SetActive(true);
        monsterDebuff.OnPoisonState(duration);
    }

    public virtual void OffPoison()
    {
        poisonFX.SetActive(false);
    }

    public virtual void Onfreeze(float duration, float dotTime, float buffpow)
    {
        if(monster.IsFreeze ==true)
        {
            return;
        }
        monster.OnFreeze();
        freeze.OnBuff(duration, dotTime, buffpow);
        freezeFX.SetActive(true);
        monsterDebuff.OnFreezeState(duration);
        

    }

    public virtual void OffFreeze()
    {
        freezeFX.SetActive(false);
        monster.OffFreeze();
    }

    public virtual void OnCurse(float duration, float dotTime, float buffpow)
    {
        curse.OnBuff(duration, dotTime, buffpow);
        curseFx.SetActive(true);
        monsterDebuff.OnCurseState(duration);
    }

    public virtual void OffCurse()
    {
        curseFx.SetActive(false);
    }
    private void OnDisable()
    {
        OffPoison();
        OffFreeze();
        OffPoison();
    }
}
