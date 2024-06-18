using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionDamage : MonoBehaviour
{
    public LayerMask monsterLayer;       // 몬스터 레이어 설정
    void Start()
    {
       
    }

    public void FindAllClosestMonsterWithinRadius(float detectionRadius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        List<GameObject> objectsInLayer = new List<GameObject>();

        foreach (Collider collider in colliders)
        {
            if (((1 << collider.gameObject.layer) & monsterLayer) != 0)
            {
                collider.gameObject.GetComponent<MonsterDamage>().OnDamage(50);
                objectsInLayer.Add(collider.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
