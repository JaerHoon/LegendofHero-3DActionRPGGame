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
        int rand = Random.Range(0, 10);
        pow += ItemManager.instance.itemToAllSkillDamage;
        monster.DetectedPlayer();
        monster.ChangeStat(Monster.MonsterStat.Damage);
        float totalDamage; 
        if(rand == 1)
            totalDamage = (pow + (pow * UpDamage / 100))*(1.5f+ItemManager.instance.itemToAddCritDamage);
        else
            totalDamage = pow + (pow * UpDamage / 100);
        monster.CurrenHP -= Mathf.FloorToInt(totalDamage);
        DamageFx(Mathf.FloorToInt(totalDamage));
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
