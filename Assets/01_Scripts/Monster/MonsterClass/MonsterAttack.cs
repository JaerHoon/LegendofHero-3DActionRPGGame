using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    protected Monster monster;
    protected bool IsATK;

    protected void Init()
    {
        monster = GetComponent<Monster>();
    }


    public virtual void OnATK()
    {
        IsATK = true;
    }

    public virtual void OffATK()
    {
        IsATK = false;
    }

    public virtual void Attack1()
    {

    }

    public virtual void Attack2()
    {

    }

    public virtual void Attack3()
    {

    }
}
