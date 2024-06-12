using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void OnHit(float damage)
    {
        animator.SetTrigger("Hit");
        GameObject pool = PoolFactroy.instance.GetPool(Consts.DamageText);
        DamageText damageText = pool.GetComponent<DamageText>();
        damageText.OnTexting(damage);
        pool.transform.position =
            new Vector3(transform.position.x, transform.position.y + 2.5f, transform.position.z);

    }
}
