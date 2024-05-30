using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDamage : MonoBehaviour
{
    protected Monster monster;

    protected void Init()
    {
        monster = GetComponent<Monster>();
    }

    public virtual void OnDamage(float pow)
    {
        monster.monsterStat = Monster.MonsterStat.Damage;
        monster.CurrenHP -= pow;
        DamageFx(pow);
    }

    public virtual void DamageFx(float amount)
    {
        // 데미지 애니메이션 스크립트에서 끝나면 알아서 상태변경
    }
}
