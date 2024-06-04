using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWrriorAttack : MonsterAttack
{
    [SerializeField]
    float KnockBackSpeed;

    private void Start()
    {
        Init();
        hitBox.gameObject.SetActive(false);
    }

    public override void Attack()
    {
        hitBox.gameObject.SetActive(true);
        Invoke("EndAttack", 0.4f);
        
    }

    public override void EndAttack()
    {
        hitBox.gameObject.SetActive(false);
    }

    public override void Hit(CharacterDamage player)
    {
        //StartCoroutine(KnockBack(player));
        player.OnDamage(monster.monsterData.ATKPow);
        Vector3 dir = player.gameObject.transform.position - transform.position;
        Vector3 dirNom = dir.normalized;
        player.GetComponent<Rigidbody>().AddForce(dirNom * KnockBackSpeed, ForceMode.Impulse);
    }

    IEnumerator KnockBack(CharacterDamage player)
    {
        float time = 0;
        while(time < 1)
        {
            time += Time.deltaTime;
           
            yield return new WaitForFixedUpdate();
        }

    }
}
