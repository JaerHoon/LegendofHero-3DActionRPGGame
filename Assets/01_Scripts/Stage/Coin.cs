using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float explosionForce = 10f; // ���� Ƣ������� ��
    public float upForce = 5f; // ������ ���� Ƣ������� ��
    public float coinLifetime = 3f; // ������ ������� �������� �ð�

    Character player;

    private void Start()
    {
        player = FindFirstObjectByType<Character>();
    }

    private void OnEnable()
    {
       
    }
    public void StartMovementAndReturnToPool(Vector3 pos)
    {
        Vector3 startPosition = pos;
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(1f, 2f), Random.Range(-1f, 1f)).normalized;
        Vector3 targetPosition = startPosition + randomDirection * explosionForce;
        float duration = 0.5f; // ������ Ƣ������� �ð�

        StartCoroutine(MoveAndReturn(startPosition, targetPosition, duration));
    }

    private IEnumerator MoveAndReturn(Vector3 startPosition, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;

        // ������ Ƣ������� ���� ��ġ ������Ʈ
        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // ������ ���� �������� ���� ��ġ ������Ʈ
        Vector3 fallStartPosition = transform.position;
        Vector3 fallTargetPosition = new Vector3(fallStartPosition.x, 0.5f, fallStartPosition.z);
      
        while (transform.position.y > 1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, fallTargetPosition, Time.deltaTime*upForce*8);
            yield return null;
        }

        // ���� �ð� �� ������ Ǯ�� ��ȯ
        yield return new WaitForSeconds(1f); // �̵� �ð��� �ݿ����� ����
        player.GoldValue += 10;
        PoolFactroy.instance.OutPool(gameObject, Consts.Coin);
    }

}
