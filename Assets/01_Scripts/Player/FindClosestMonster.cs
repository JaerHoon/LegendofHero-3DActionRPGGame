using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestMonster : MonoBehaviour
{
    public float detectionRadius_Arrow = 14f;  // 탐지 반경
    public float detectionRadius_Poison = 10f;  // 탐지 반경
    public float detectionRadius_Curse = 5f;  // 탐지 반경
    public LayerMask monsterLayer;       // 몬스터 레이어 설정
    public Transform closestMonster;    // 가장 가까운 몬스터의 위치


    void Start()
    {
        StartCoroutine(FindClosestMonsterRoutine());
    }

    IEnumerator FindClosestMonsterRoutine()
    {
        yield return new WaitForFixedUpdate();
        while (true)
        {
            FindClosestMonsterWithinRadius(detectionRadius_Arrow,0);
            FindClosestMonsterWithinRadius(detectionRadius_Poison,1);
            FindClosestMonsterWithinRadius(detectionRadius_Curse,2);
            FindAllClosestMonsterWithinRadius(detectionRadius_Curse);
            yield return new WaitForSeconds(0.2f); // 0.2초마다 실행
        }
    }

    void FindClosestMonsterWithinRadius(float detectionRadius, int typeNum)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius, monsterLayer);
        closestMonster = null;
        float closestDistanceSqr = Mathf.Infinity;

        foreach (Collider collider in hitColliders)
        {
            if (collider.CompareTag("Monster"))
            {
                Vector3 directionToTarget = collider.transform.position - transform.position;
                float dSqrToTarget = directionToTarget.sqrMagnitude;
                if (dSqrToTarget < closestDistanceSqr)
                {
                    closestDistanceSqr = dSqrToTarget;
                    closestMonster = collider.transform;
                }
            }
        }

        if (closestMonster != null)
        {

            ItemManager.instance.SetIsMonsterExist(true,typeNum);
            ItemManager.instance.SetClosestMonster(closestMonster);
        }
        else
        {
            ItemManager.instance.SetIsMonsterExist(false, typeNum);
        }
    }

    void FindAllClosestMonsterWithinRadius(float detectionRadius)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);

        List<GameObject> objectsInLayer = new List<GameObject>();

        foreach (Collider collider in colliders)
        {
            if (((1 << collider.gameObject.layer) & monsterLayer) != 0)
            {
                objectsInLayer.Add(collider.gameObject);
            }
        }

        ItemManager.instance.SetAllClosestMonster(objectsInLayer);

    }


    public Color[] colors = { Color.red, Color.green, Color.magenta }; // 색상 배열

    void OnDrawGizmosSelected()
    {
        float[] sizes = { detectionRadius_Arrow, detectionRadius_Poison, detectionRadius_Curse }; // 크기 배열
        // 선택된 게임 오브젝트를 가져옵니다.

        // 기지모를 그립니다.
        for (int i = 0; i < sizes.Length; i++)
        {
            // 예를 들어, 구체를 그립니다.
            Gizmos.color = colors[i];
            Gizmos.DrawWireSphere(transform.position, sizes[i]);
        }
    }
}
