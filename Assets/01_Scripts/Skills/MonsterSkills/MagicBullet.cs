using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    Transform playerTr;
    Vector3 pos;

    public float speed;
    public float duration;
    [HideInInspector]
    public GameObject hitFx;
    [HideInInspector]
    public float damage;
    [HideInInspector]
    public MonsterMageAttack monsterMageAttack;
    bool isReleased;

    private void OnEnable()
    {
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        pos = new Vector3(playerTr.position.x, 2, playerTr.position.z);
        isReleased = false;
        Invoke("Offobject", duration);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
           other.GetComponent<CharacterDamage>().OnDamage(damage);
           hitFx =  PoolFactroy.instance.GetPool(Consts.MagicBulletHit);
           hitFx.transform.position = transform.position;
           Invoke("Offobject", 0.15f);

        }
    }


    void Offobject()
    {
        if (isReleased) return;
        
        if(hitFx != null)
        {
            PoolFactroy.instance.OutPool(hitFx, Consts.MagicBulletHit);
            hitFx = null;
        }
        PoolFactroy.instance.OutPool(this.gameObject, Consts.MagicBullet);
        isReleased = true;

    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, pos, speed * Time.deltaTime);
    }

    private void OnDisable()
    {
        CancelInvoke("Offobject");
    }
}
