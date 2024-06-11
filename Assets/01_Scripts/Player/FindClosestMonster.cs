using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestMonster : MonoBehaviour
{
    public float detectionRadius = 10f;  // 탐지 반경
    public LayerMask monsterLayer;       // 몬스터 레이어 설정
    private Transform closestMonster;    // 가장 가까운 몬스터의 위치

    void Start()
    {
        StartCoroutine(FindClosestMonsterRoutine());
    }

    IEnumerator FindClosestMonsterRoutine()
    {
        while (true)
        {
            FindClosestMonsterWithinRadius();
            yield return new WaitForSeconds(0.2f); // 0.2초마다 실행
        }
    }

    void FindClosestMonsterWithinRadius()
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
            Debug.Log("Closest monster position: " + closestMonster.position);
            Debug.Log(closestMonster.name);
            // 가장 가까운 몬스터 위치 정보를 사용할 수 있습니다.
        }
        else
        {
            //Debug.Log("No monsters found within radius.");
        }
    }

    void OnDrawGizmosSelected()
    {
        // 플레이어 주변 탐지 반경을 시각적으로 표시
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
