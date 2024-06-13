using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRogueAttack : MonsterAttack
{
    [SerializeField]
    Transform firePos;
    [SerializeField]
    Transform lineStartPos;
    [SerializeField]
    float rotationSpeed = 5f; // 회전 속도
    [SerializeField]
    float stopThreshold = 1f; // 멈출 각도 (도)
    [SerializeField]
    float ATKSpeed;

    List<GameObject> pools = new List<GameObject>();
    private void Start()
    {
        Init();
       
    }

    public override void OffATK()
    {
        IsATK = false;
        EndAttack();
    }

    public override void EndAttack()
    {
        StopAllCoroutines();
        if(pools.Count > 0)
        {
            foreach (GameObject pool in pools)
            {
                ArrowLine poolLine = pool.GetComponent<ArrowLine>();
                poolLine.tr.Clear();
                poolLine.tr.enabled = false;
                if(pool.activeSelf == true)
                {
                    PoolFactroy.instance.OutPool(pool, Consts.ArrowLine);
                }
            }
        }

        pools.Clear();

    }
    public override void OnDie()
    {
        
    }



    public override void OnATK()
    {
        
        base.OnATK();
        Attack();
    }

    public override void Attack()
    {
       
        if (IsATK)
        {
            StartCoroutine(Aming());
        }
       
    }


    IEnumerator Aming()
    {
        Vector3 dir = (monster.playerTr.position - transform.position).normalized;
        
        while (true)
        {
          
                
           
            dir.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(dir);

            transform.rotation =
           Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);

            float angle = Quaternion.Angle(transform.rotation, targetRotation);

            // 각도 차이가 임계값 이하이면 코루틴 중지
            if (angle < stopThreshold)
            {
               break;
            }

            yield return new WaitForFixedUpdate();
        }
        

        GameObject pool = PoolFactroy.instance.GetPool(Consts.ArrowLine);
        pools.Add(pool);
        ArrowLine poolLine = pool.GetComponent<ArrowLine>();
        poolLine.Dir = dir;
        pool.transform.position = lineStartPos.position;
        poolLine.tr.enabled = true;
        
        yield return new WaitForSeconds(1f);
        FireArrow();
    }

    void FireArrow()
    {
        GameObject arrow = PoolFactroy.instance.GetPool(Consts.MonsterArrow);
        MonsterArrow arrows = arrow.GetComponent<MonsterArrow>(); 
        arrows.damage = monster.monsterData.ATKPow;
        arrow.transform.rotation = transform.rotation;
        arrow.transform.position = firePos.position;
        arrows.tr.enabled = true;
        

        if (IsATK)
        {
            StopAllCoroutines();
            StartCoroutine(Aming());
            
        }
        else
        {
            EndAttack();
           
        }
    }
}
