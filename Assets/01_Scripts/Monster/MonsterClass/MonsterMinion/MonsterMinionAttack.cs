using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMinionAttack : MonsterAttack
{
    void Start()
    {
        Init();
    }

    public override void Attack()
    {
        hitBox.gameObject.SetActive(true);
    }

    public override void EndAttack()
    {
        hitBox.gameObject.SetActive(false);
    }

    public override void Hit(CharacterDamage player)
    {
        player.OnDamage(monster.monsterData.ATKPow);
    }
}
