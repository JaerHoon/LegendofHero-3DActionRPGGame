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

    public virtual void UsedSkill(Skills skill)
    {
        float damage = skill.Damage;
        print($"스킬발동, 데미지 : {damage}");
    }

}
