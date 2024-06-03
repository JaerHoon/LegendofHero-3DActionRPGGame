using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemAttack : MonsterAttack
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public override void Attack1()//serch and attack
    {
        base.Attack1();
        monster.anim.OnAtk1Anim();
    }

    public override void Attack2()
    {
        base.Attack2();
        monster.anim.OnAtk2Anim();
    }

    public override void Attack3()
    {
        base.Attack3();
        monster.anim.OnAtk3Anim();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
