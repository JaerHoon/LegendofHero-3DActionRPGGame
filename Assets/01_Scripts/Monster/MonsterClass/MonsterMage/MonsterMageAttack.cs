using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMageAttack : MonsterAttack
{
    [SerializeField]
    Transform firePos;

    private void Start()
    {
        Init();
    }

    public override void Attack()
    {
        transform.LookAt(monster.playerTr.position);
        GameObject bullet = PoolFactroy.instance.GetPool(Consts.MagicBullet);
        bullet.GetComponent<MagicBullet>().damage = monster.monsterData.ATKPow;
        bullet.transform.position = firePos.position;
    }
}
