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
        //OnDamage(1000f);
    }

    public override void OnDamage(float pow)
    {
        golem.CurrentHP -= pow;
        DamageFx(pow);
    }

    public override void DamageFx(float amount)
    {
        FX.Play();
        GameObject dmgText = PoolFactroy.instance.GetPool(0);
        DamageText damageText = dmgText.GetComponent<DamageText>();
        damageText.OnTexting(amount);
        Vector3 pos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 7.5f, gameObject.transform.position.z);
        dmgText.transform.position = pos;
        

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
