using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{
    protected Monster monster;
    protected bool IsATK;
    [SerializeField]
    protected BoxCollider hitBox;

    private void OnEnable()
    {
        if (hitBox != null) hitBox.gameObject.SetActive(false);
    }

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
    public virtual void Attack()
    {
        hitBox.gameObject.SetActive(true);
    }
    public virtual void Attack1()
    { 
    }
    

    public virtual void EndAttack()
    {
        hitBox?.gameObject.SetActive(false);
    }

    public virtual void Attack2()
    {

    }

    public virtual void Attack3()
    {

    }
    public virtual void Hit(CharacterDamage player)
    {
        player.OnDamage(monster.monsterData.ATKPow);

    }
}
