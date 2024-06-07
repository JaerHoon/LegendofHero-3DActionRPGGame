using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemDamage : MonsterDamage
{
    Golem golem;

    // Start is called before the first frame update
    void Start()
    {
        Init();
        golem = GetComponent<Golem>();
    }


    private void OnMouseEnter()
    {
        OnDamage(10f);
    }

    public override void OnDamage(float pow)
    {
        golem.CurrentHP -= pow + (pow * UpDamage / 100);
        DamageFx(pow);
    }

    public override void OnPoisonDamage(float pow)
    {
        golem.CurrentHP -= pow;
        GreenDamageFx(pow);
    }

    public override void DamageFx(float amount)
    {
        FX.Play();
        GameObject redDmgText = PoolFactroy.instance.GetPool(Consts.DamageText);
        DamageText redDamageText = redDmgText.GetComponent<DamageText>();
        redDamageText.OnTexting(amount);
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 7.5f, gameObject.transform.position.z);
        redDmgText.transform.position = pos;
        

    }

    public override void GreenDamageFx(float amount)
    {
        GameObject greenDmgText = PoolFactroy.instance.GetPool(Consts.GreenDamageText);
        GreenDamageText greenDamageText = greenDmgText.GetComponent<GreenDamageText>();
        greenDamageText.OnTexting(amount);
        Vector3 pos = new Vector3(gameObject.transform.position.x + 0.3f, gameObject.transform.position.y + 7.5f, gameObject.transform.position.z);
        greenDmgText.transform.position = pos;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
