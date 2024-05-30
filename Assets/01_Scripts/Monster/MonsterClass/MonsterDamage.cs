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
        // ������ �ִϸ��̼� ��ũ��Ʈ���� ������ �˾Ƽ� ���º���
    }
}
