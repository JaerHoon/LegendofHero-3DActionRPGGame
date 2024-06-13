using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosestMonster : MonoBehaviour
{
    public float detectionRadius_Arrow = 14f;  // Ž�� �ݰ�
    public float detectionRadius_Poison = 10f;  // Ž�� �ݰ�
    public float detectionRadius_Curse = 5f;  // Ž�� �ݰ�
    public LayerMask monsterLayer;       // ���� ���̾� ����
    public Transform closestMonster;    // ���� ����� ������ ��ġ


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
            yield return new WaitForSeconds(0.2f); // 0.2�ʸ��� ����
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


    public Color[] colors = { Color.red, Color.green, Color.magenta }; // ���� �迭

    void OnDrawGizmosSelected()
    {
        float[] sizes = { detectionRadius_Arrow, detectionRadius_Poison, detectionRadius_Curse }; // ũ�� �迭
        // ���õ� ���� ������Ʈ�� �����ɴϴ�.

        // ������ �׸��ϴ�.
        for (int i = 0; i < sizes.Length; i++)
        {
            // ���� ���, ��ü�� �׸��ϴ�.
            Gizmos.color = colors[i];
            Gizmos.DrawWireSphere(transform.position, sizes[i]);
        }
    }
}
