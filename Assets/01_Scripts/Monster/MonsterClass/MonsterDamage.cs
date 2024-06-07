using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    protected Monster monster;
    [SerializeField]
    protected ParticleSystem FX;
    [HideInInspector]
    public float UpDamage = 0;

    protected void Init()
    {
        monster = GetComponent<Monster>();
    }

    public virtual void OnDamage(float pow)
    {
       
        monster.DetectedPlayer();
        monster.ChangeStat(Monster.MonsterStat.Damage);
        monster.CurrenHP -=  pow + (pow*UpDamage/100);
        DamageFx(pow);
    }

    public virtual void OnPoisonDamage(float pow)
    {
        monster.CurrenHP -= pow;
        GreenDamageFx(pow);
    }

    public virtual void DamageFx(float amount)
    {
        FX.Play();
        GameObject dmgText = PoolFactroy.instance.GetPool(Consts.DamageText);
        DamageText damageText = dmgText.GetComponent<DamageText>();
        damageText.OnTexting(amount);
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2.5f, gameObject.transform.position.z);
        dmgText.transform.position = pos;
    }


    public virtual void GreenDamageFx(float amount)
    {
        GameObject dmgText = PoolFactroy.instance.GetPool(Consts.GreenDamageText);
        GreenDamageText damageText = dmgText.GetComponent<GreenDamageText>();
        damageText.OnTexting(amount);
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2.5f, gameObject.transform.position.z);
        dmgText.transform.position = pos;
    }
}
