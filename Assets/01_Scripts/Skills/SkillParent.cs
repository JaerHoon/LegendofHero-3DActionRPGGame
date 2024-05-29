using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillParent : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void UsedSkill(float damage, float cd, float gcd, float scale, float speedRate, float eventRate, int damageCount, int skillCount, int damageRate)
    {
        print($"스킬발동, 데미지 : {damage}");
    }

}
