using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    protected Monster monster;
    protected bool IsATK;

    private void Start()
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
}
