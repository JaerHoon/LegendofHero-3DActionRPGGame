using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestMonster : MonoBehaviour
{
    public float detectionRadius = 10f;  // Ž�� �ݰ�
    public LayerMask monsterLayer;       // ���� ���̾� ����
    private Transform closestMonster;    // ���� ����� ������ ��ġ

    void Start()
    {
        StartCoroutine(FindClosestMonsterRoutine());
    }

    IEnumerator FindClosestMonsterRoutine()
    {
        while (true)
        {
            FindClosestMonsterWithinRadius();
            yield return new WaitForSeconds(0.2f); // 0.2�ʸ��� ����
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
            // ���� ����� ���� ��ġ ������ ����� �� �ֽ��ϴ�.
        }
        else
        {
            //Debug.Log("No monsters found within radius.");
        }
    }

    void OnDrawGizmosSelected()
    {
        // �÷��̾� �ֺ� Ž�� �ݰ��� �ð������� ǥ��
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
