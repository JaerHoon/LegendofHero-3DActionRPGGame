using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemDamage : MonsterDamage
{
    Golem golem;
    NavMeshAgent nav;
    Material golemMaterial;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        golem = GetComponent<Golem>();
        nav = GetComponent<NavMeshAgent>();
        golemMaterial = Resources.Load("Assets/Enemy Golem/Materials/Stone_Golem_Material") as Material;
        golemMaterial.color = new Color(1, 1, 1, 1);
    }


   
    public override void OnDamage(float pow)
    {
        int rand = Random.Range(0, 10);
        pow += ItemManager.instance.itemToAllSkillDamage;
        float totalDamage;
        if (rand == 1)
            totalDamage = (pow + (pow * UpDamage / 100)) * (1.5f + ItemManager.instance.itemToAddCritDamage);
        else
            totalDamage = pow + (pow * UpDamage / 100);
        golem.CurrentHP -= Mathf.FloorToInt(totalDamage);
        DamageFx(Mathf.FloorToInt(totalDamage));
        IsRageMonde();
    }

    public override void OnPoisonDamage(float pow)
    {
        golem.CurrentHP -= pow;
        GreenDamageFx(pow);
        IsRageMonde();
    }

    void IsRageMonde()
    {
        if (golem.isRageMode2) return;
        if (golem.CurrentHP / monster.MaxHP < 0.25f)
        {
            golem.isRageMode2 = true;
            golemMaterial.color = new Color(1, 50f / 255f, 50f / 255f);
            GetComponent<GolemAttack>().rockNum += 5;
        }
        if (golem.isRageMode1) return;
        if (golem.CurrentHP / monster.MaxHP < 0.5f)
        {
            golem.isRageMode1 = true;
            golemMaterial.color = new Color(1, 155f / 255f, 155f / 255f);
            GetComponent<GolemAttack>().rockNum += 5;
        }
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
